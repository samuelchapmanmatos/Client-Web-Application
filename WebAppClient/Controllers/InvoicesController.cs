using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebAppClient.Models;

namespace WebAppClient.Controllers
{
    public class InvoicesController : Controller
    {
        private readonly Test_InvoiceContext _context;

        public InvoicesController(Test_InvoiceContext context)
        {
            _context = context;
        }

        // GET: Invoices
        public async Task<IActionResult> Invoice()
        {
            var test_InvoiceContext = _context.Invoice.Include(i => i.Customer);
            return View(await test_InvoiceContext.ToListAsync());
        }

        // GET: Invoices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoice
                .Include(i => i.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // GET: Invoices/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "CustName");
            return View();
        }

        //POST: Invoices/CompleteInvoiceI
        [HttpPost]
        public async Task<String> CompleteInvoiceI(decimal totalI)
        {

            Invoice inv = new Invoice();
            decimal Itbis = 0.18m;

            //Using Delegates and lambda expressions
            Func<decimal, decimal, decimal> Mult = (v1, v2) => (v1 * v2);
            Func<decimal, decimal, decimal> Subt = (v1, v2) => (v1 - v2);

            decimal Itebis= 0;
            decimal SubTot = 0;

            if (totalI > 0)
            {
                Itebis = Mult(totalI, Itbis);
                SubTot = Subt(totalI, Itebis);
            }

            inv = new Invoice()
            {                
                TotalItbis = Itebis,
                SubTotal = SubTot,
                Total = totalI
            };

            return JsonConvert.SerializeObject(inv);
        }

        // POST: Invoices/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CustomerId,TotalItbis,SubTotal,Total")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                _context.Add(invoice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Invoice));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "CustName", invoice.CustomerId);
            return View(invoice);
        }

        // GET: Invoices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoice.FindAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "CustName", invoice.CustomerId);
            return View(invoice);
        }

        // POST: Invoices/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CustomerId,TotalItbis,SubTotal,Total")] Invoice invoice)
        {
            if (id != invoice.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(invoice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvoiceExists(invoice.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Invoice));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "CustName", invoice.CustomerId);
            return View(invoice);
        }

        // GET: Invoices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoice
                .Include(i => i.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var invoice = await _context.Invoice.FindAsync(id);
            _context.Invoice.Remove(invoice);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InvoiceExists(int id)
        {
            return _context.Invoice.Any(e => e.Id == id);
        }


    }
}
