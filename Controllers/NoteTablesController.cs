using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FBWeb.Data;
using FBWeb.Models;

namespace FBWeb.Controllers
{
    public class NoteTablesController : Controller
    {
        private readonly FlutterbookContext _context;

        public NoteTablesController(FlutterbookContext context)
        {
            _context = context;
        }

        // GET: NoteTables
        public async Task<IActionResult> Index()
        {
            var flutterbookContext = _context.NoteTable.Include(n => n.User);
            return View(await flutterbookContext.ToListAsync());
        }

        // GET: NoteTables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var noteTable = await _context.NoteTable
                .Include(n => n.User)
                .FirstOrDefaultAsync(m => m.NoteId == id);
            if (noteTable == null)
            {
                return NotFound();
            }

            return View(noteTable);
        }

        // GET: NoteTables/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id");
            return View();
        }

        // POST: NoteTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NoteId,NoteTitle,NoteText,UserId")] NoteTable noteTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(noteTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", noteTable.UserId);
            return View(noteTable);
        }

        // GET: NoteTables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var noteTable = await _context.NoteTable.FindAsync(id);
            if (noteTable == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", noteTable.UserId);
            return View(noteTable);
        }

        // POST: NoteTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NoteId,NoteTitle,NoteText,UserId")] NoteTable noteTable)
        {
            if (id != noteTable.NoteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(noteTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NoteTableExists(noteTable.NoteId))
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
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", noteTable.UserId);
            return View(noteTable);
        }

        // GET: NoteTables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var noteTable = await _context.NoteTable
                .Include(n => n.User)
                .FirstOrDefaultAsync(m => m.NoteId == id);
            if (noteTable == null)
            {
                return NotFound();
            }

            return View(noteTable);
        }

        // POST: NoteTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var noteTable = await _context.NoteTable.FindAsync(id);
            _context.NoteTable.Remove(noteTable);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NoteTableExists(int id)
        {
            return _context.NoteTable.Any(e => e.NoteId == id);
        }
    }
}
