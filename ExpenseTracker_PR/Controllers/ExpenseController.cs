using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExpenseTracker_PR.Models;

namespace ExpenseTracker_PR.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly ETPRContext _context;

        public ExpenseController(ETPRContext context)
        {
            _context = context;
        }

        // GET: Expense
        public async Task<IActionResult> Index()
        {
            var eTPRContext = _context.Expenses.Include(e => e.Category);
            return View(await eTPRContext.ToListAsync());
        }

        // GET: Expense/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Expenses == null)
            {
                return NotFound();
            }

            var expenseModel = await _context.Expenses
                .Include(e => e.Category)
                .FirstOrDefaultAsync(m => m.ExpensesID == id);
            if (expenseModel == null)
            {
                return NotFound();
            }

            return View(expenseModel);
        }

        // GET: Expense/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CatName");
            return View();
        }

        // POST: Expense/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ExpensesID,CategoryId,Amount,Description,Date")] ExpenseModel expenseModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(expenseModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CatName", expenseModel.CategoryId);
            return View(expenseModel);
        }

        // GET: Expense/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Expenses == null)
            {
                return NotFound();
            }

            var expenseModel = await _context.Expenses.FindAsync(id);
            if (expenseModel == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CatName", expenseModel.CategoryId);
            return View(expenseModel);
        }

        // POST: Expense/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ExpensesID,CategoryId,Amount,Description,Date")] ExpenseModel expenseModel)
        {
            if (id != expenseModel.ExpensesID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(expenseModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpenseModelExists(expenseModel.ExpensesID))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CatName", expenseModel.CategoryId);
            return View(expenseModel);
        }

        // GET: Expense/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Expenses == null)
            {
                return NotFound();
            }

            var expenseModel = await _context.Expenses
                .Include(e => e.Category)
                .FirstOrDefaultAsync(m => m.ExpensesID == id);
            if (expenseModel == null)
            {
                return NotFound();
            }

            return View(expenseModel);
        }

        // POST: Expense/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Expenses == null)
            {
                return Problem("Entity set 'ETPRContext.Expenses'  is null.");
            }
            var expenseModel = await _context.Expenses.FindAsync(id);
            if (expenseModel != null)
            {
                _context.Expenses.Remove(expenseModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExpenseModelExists(int id)
        {
          return (_context.Expenses?.Any(e => e.ExpensesID == id)).GetValueOrDefault();
        }
    }
}
