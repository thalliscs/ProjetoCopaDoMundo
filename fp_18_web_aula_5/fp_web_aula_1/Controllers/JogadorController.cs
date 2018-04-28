using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using fp_web_aula_1_core.Models;
using Microsoft.AspNetCore.Authorization;
using fp_web_aula_1_core.Data;
using fp_web_aula_1.ViewModel;

namespace fp_web_aula_1.Controllers
{
    [Authorize]
    public class JogadorController : Controller
    {
        //todo: refatory pattern repository
        private readonly CopaContext _context;

        public JogadorController(CopaContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var times = _context.Jogadores;

            var vm = times.Select(x =>
                new JogadorViewModel()
                {
                    Id = x.Id,
                    Camisa = x.Camisa,
                    Nascimento = x.Nascimento,
                    Nome = x.Nome,
                    Posicao = x.Posicao,
                    TimeId = x.TimeId,
                    NomeTime = x.Time.Nome
                }).ToList();

            return View(vm);
        }

        public IActionResult CreateOrEdit(int? id)
        {
            JogadorViewModel vm = new JogadorViewModel();

            var times = _context.Times;

            if (times.Any())
                vm.Times = times.Select(x => new TimeViewModel() { Id = x.Id, Nome = x.Nome }).ToList();

            if (id.HasValue)
            {
                var jogador = _context.Jogadores.Include(x => x.Time).FirstOrDefault(m => m.Id == id);
                if (jogador == null)
                {
                    return NotFound();
                }

                vm.Id = jogador.Id;
                vm.Camisa = jogador.Camisa;
                vm.Nascimento = jogador.Nascimento;
                vm.Nome = jogador.Nome;
                vm.Posicao = jogador.Posicao;
                vm.TimeId = jogador.TimeId;

                return View(vm);
            }

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateOrEdit(JogadorViewModel vm)
        {
            if (ModelState.IsValid)
            {
                if (vm.Id == 0)
                {
                    var jogador = new Jogador()
                    {
                        Id = vm.Id,
                        Camisa = vm.Camisa.Value,
                        Nascimento = vm.Nascimento.Value,
                        Nome = vm.Nome,
                        Posicao = vm.Posicao,
                        TimeId = vm.TimeId
                    };

                    _context.Add(jogador);
                    _context.SaveChanges();
                }
                else
                {
                    var jogador = _context.Jogadores.Include(x => x.Time).FirstOrDefault(m => m.Id == vm.Id);
                    if (jogador == null)
                        return NotFound();

                    jogador.Atualizar(vm.Nascimento.Value, vm.Nome, vm.Posicao, vm.TimeId, vm.Camisa.Value);
                    _context.Jogadores.Attach(jogador).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                    _context.SaveChanges();
                }

                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: Times/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jogador = await _context.Jogadores.Include(x=>x.Time).SingleOrDefaultAsync(m => m.Id == id);
            if (jogador == null)
            {
                return NotFound();
            }

            return View(new JogadorViewModel()
            {
                Id = jogador.Id,
                Camisa = jogador.Camisa,
                Nascimento = jogador.Nascimento,
                Nome = jogador.Nome,
                NomeTime = jogador.Time.Nome,
                Posicao = jogador.Posicao,
                TimeId = jogador.TimeId
            });
        }

        // POST: Times/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jogador = await _context.Jogadores.SingleOrDefaultAsync(m => m.Id == id);
            _context.Jogadores.Remove(jogador);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}