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
    public class TaskTablesController : Controller
    {
        private readonly FlutterbookContext _context;

        public TaskTablesController(FlutterbookContext context)
        {
            _context = context;
        }

        // GET: TaskTables
        public async Task<IActionResult> Index()
        {
            var flutterbookContext = _context.TaskTable.Include(t => t.User);
            return View(await flutterbookContext.ToListAsync());
        }

        // GET: TaskTables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskTable = await _context.TaskTable
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.TaskId == id);
            if (taskTable == null)
            {
                return NotFound();
            }

            return View(taskTable);
        }

        // GET: TaskTables/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id");
            return View();
        }

        // POST: TaskTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TaskId,TextTask,DateTask,CompleteTask,UserId")] TaskTable taskTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(taskTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", taskTable.UserId);
            return View(taskTable);
        }

        // GET: TaskTables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskTable = await _context.TaskTable.FindAsync(id);
            if (taskTable == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", taskTable.UserId);
            return View(taskTable);
        }

        // POST: TaskTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TaskId,TextTask,DateTask,CompleteTask,UserId")] TaskTable taskTable)
        {
            if (id != taskTable.TaskId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(taskTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskTableExists(taskTable.TaskId))
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
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", taskTable.UserId);
            return View(taskTable);
        }

        // GET: TaskTables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskTable = await _context.TaskTable
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.TaskId == id);
            if (taskTable == null)
            {
                return NotFound();
            }

            return View(taskTable);
        }

        // POST: TaskTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var taskTable = await _context.TaskTable.FindAsync(id);
            _context.TaskTable.Remove(taskTable);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaskTableExists(int id)
        {
            return _context.TaskTable.Any(e => e.TaskId == id);
        }
    }
}
