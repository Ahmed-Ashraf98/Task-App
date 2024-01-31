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
    public class EventTablesController : Controller
    {
        private readonly FlutterbookContext _context;

        public EventTablesController(FlutterbookContext context)
        {
            _context = context;
        }

        // GET: EventTables
        public async Task<IActionResult> Index()
        {
            var flutterbookContext = _context.EventTable.Include(e => e.User);
            return View(await flutterbookContext.ToListAsync());
        }

        // GET: EventTables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventTable = await _context.EventTable
                .Include(e => e.User)
                .FirstOrDefaultAsync(m => m.EventId == id);
            if (eventTable == null)
            {
                return NotFound();
            }

            return View(eventTable);
        }

        // GET: EventTables/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id");
            return View();
        }

        // POST: EventTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventId,Text,Year,Month,Day,UserId")] EventTable eventTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eventTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", eventTable.UserId);
            return View(eventTable);
        }

        // GET: EventTables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventTable = await _context.EventTable.FindAsync(id);
            if (eventTable == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", eventTable.UserId);
            return View(eventTable);
        }

        // POST: EventTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EventId,Text,Year,Month,Day,UserId")] EventTable eventTable)
        {
            if (id != eventTable.EventId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eventTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventTableExists(eventTable.EventId))
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
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", eventTable.UserId);
            return View(eventTable);
        }

        // GET: EventTables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventTable = await _context.EventTable
                .Include(e => e.User)
                .FirstOrDefaultAsync(m => m.EventId == id);
            if (eventTable == null)
            {
                return NotFound();
            }

            return View(eventTable);
        }

        // POST: EventTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eventTable = await _context.EventTable.FindAsync(id);
            _context.EventTable.Remove(eventTable);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventTableExists(int id)
        {
            return _context.EventTable.Any(e => e.EventId == id);
        }
    }
}
