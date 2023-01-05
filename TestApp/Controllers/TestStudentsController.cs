using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestApp.Models;

namespace TestApp.Controllers
{
    public class TestStudentsController : Controller
    {
        private readonly DbTestContext _context;

        public TestStudentsController(DbTestContext context)
        {
            _context = context;
        }

        // GET: TestStudents
        public async Task<IActionResult> Index()
        {
              return View(await _context.TestStudents.ToListAsync());
        }

        // GET: TestStudents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TestStudents == null)
            {
                return NotFound();
            }

            var testStudent = await _context.TestStudents
                .FirstOrDefaultAsync(m => m.Id == id);
            if (testStudent == null)
            {
                return NotFound();
            }

            return View(testStudent);
        }

        // GET: TestStudents/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TestStudents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Gmail,Mobile")] TestStudent testStudent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(testStudent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(testStudent);
        }

        // GET: TestStudents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TestStudents == null)
            {
                return NotFound();
            }

            var testStudent = await _context.TestStudents.FindAsync(id);
            if (testStudent == null)
            {
                return NotFound();
            }
            return View(testStudent);
        }

        // POST: TestStudents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Gmail,Mobile")] TestStudent testStudent)
        {
            if (id != testStudent.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(testStudent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TestStudentExists(testStudent.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(testStudent);
        }

        // GET: TestStudents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TestStudents == null)
            {
                return NotFound();
            }

            var testStudent = await _context.TestStudents
                .FirstOrDefaultAsync(m => m.Id == id);
            if (testStudent == null)
            {
                return NotFound();
            }

            return View(testStudent);
        }

        // POST: TestStudents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TestStudents == null)
            {
                return Problem("Entity set 'DbTestContext.TestStudents'  is null.");
            }
            var testStudent = await _context.TestStudents.FindAsync(id);
            if (testStudent != null)
            {
                _context.TestStudents.Remove(testStudent);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TestStudentExists(int id)
        {
          return _context.TestStudents.Any(e => e.Id == id);
        }
    }
}
