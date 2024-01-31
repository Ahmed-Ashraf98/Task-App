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
    public class ContactTablesController : Controller
    {
        private readonly FlutterbookContext _context;

        public ContactTablesController(FlutterbookContext context)
        {
            _context = context;
        }

        // GET: ContactTables
        public async Task<IActionResult> Index()
        {
            var flutterbookContext = _context.ContactTable.Include(c => c.User);
            return View(await flutterbookContext.ToListAsync());
        }

        // GET: ContactTables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactTable = await _context.ContactTable
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.ContactId == id);
            if (contactTable == null)
            {
                return NotFound();
            }

            return View(contactTable);
        }

        // GET: ContactTables/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id");
            return View();
        }

        // POST: ContactTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContactId,Name,ImgUrl,Phone,UserId")] ContactTable contactTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contactTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", contactTable.UserId);
            return View(contactTable);
        }

        // GET: ContactTables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactTable = await _context.ContactTable.FindAsync(id);
            if (contactTable == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", contactTable.UserId);
            return View(contactTable);
        }

        // POST: ContactTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ContactId,Name,ImgUrl,Phone,UserId")] ContactTable contactTable)
        {
            if (id != contactTable.ContactId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contactTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactTableExists(contactTable.ContactId))
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
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", contactTable.UserId);
            return View(contactTable);
        }

        // GET: ContactTables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactTable = await _context.ContactTable
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.ContactId == id);
            if (contactTable == null)
            {
                return NotFound();
            }

            return View(contactTable);
        }

        // POST: ContactTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contactTable = await _context.ContactTable.FindAsync(id);
            _context.ContactTable.Remove(contactTable);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactTableExists(int id)
        {
            return _context.ContactTable.Any(e => e.ContactId == id);
        }
    }
}
