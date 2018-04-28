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
    public class TimesController : Controller
    {
        //todo: refatory pattern repository
        private readonly CopaContext _context;

        public TimesController(CopaContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var times = _context.Times;

            var vm = times.Select(x =>
                new TimeViewModel()
                {
                    Id = x.Id,
                    Bandeira = x.Bandeira,
                    Nome = x.Nome,
                    Site = x.Site
                }).ToList();

            return View(vm);
        }

        public IActionResult CreateOrEdit(int? id)
        {
            TimeViewModel vm = new TimeViewModel();

            if (id.HasValue)
            {
                var time = _context.Times.FirstOrDefault(m => m.Id == id);
                if (time == null)
                {
                    return NotFound();
                }

                vm = new TimeViewModel()
                {
                    Id = time.Id,
                    Nome = time.Nome,
                    Bandeira = time.Bandeira,
                    Site = time.Site
                };

                return View(vm);
            }

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateOrEdit(TimeViewModel vm)
        {
            if (ModelState.IsValid)
            {
                if(vm.Id == 0)
                {
                    var time = new Time()
                    {
                        Id = vm.Id,
                        Bandeira = vm.Bandeira,
                        Nome = vm.Nome,
                        Site = vm.Site
                    };

                    _context.Add(time);
                    _context.SaveChanges();
                }
                else
                {
                    var time = _context.Times.FirstOrDefault(m => m.Id == vm.Id);
                    if (time == null)
                        return NotFound();

                    time.Atualizar(vm.Bandeira, vm.Nome, vm.Site);
                    _context.Times.Attach(time).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

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

            var time = await _context.Times
                .SingleOrDefaultAsync(m => m.Id == id);
            if (time == null)
            {
                return NotFound();
            }

            return View(new TimeViewModel()
            {
                Id = time.Id,
                Nome = time.Nome,
                Site = time.Site
            });
        }

        // POST: Times/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var time = await _context.Times.SingleOrDefaultAsync(m => m.Id == id);
            _context.Times.Remove(time);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
