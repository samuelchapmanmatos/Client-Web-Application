using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebAppClient.Models;

namespace WebAppClient.Controllers
{
    public delegate Decimal InvoicesDetailsDelegate(decimal A, decimal B);

    public class InvoiceDetailsController : Controller
    {
        private readonly Test_InvoiceContext _context;


        public InvoiceDetailsController(Test_InvoiceContext context)
        {
            _context = context;
        }

        // GET: InvoiceDetails
        public async Task<IActionResult> Index()
        {
            var test_InvoiceContext = _context.InvoiceDetail.Include(i => i.Customer);
            return View(await test_InvoiceContext.ToListAsync());
        }

        // GET: InvoiceDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceDetail = await _context.InvoiceDetail
                .Include(i => i.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (invoiceDetail == null)
            {
                return NotFound();
            }

            return View(invoiceDetail);
        }

        // GET: InvoiceDetails/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Invoice, "Id", "Id");
            return View();
        }

        //POST: InvoiceDetails/CompleteInvoiceDetails
        [HttpPost]
        public async Task<String> CompleteInvoiceDetails(int Id, int CustomerId,
            int Qty, decimal Price, decimal TotalItbis, decimal SubTotal, decimal Total)
        {

            InvoiceDetail inD = new InvoiceDetail();

            //Using Delegates and lambda expressions
            Func<decimal, decimal, decimal> Mult = (v1, v2) => (v1 * v2);
            Func<decimal, decimal, decimal> Subt = (v1, v2) => (v1 - v2);

            decimal dTot = 0;
            decimal dIte = 0;
            decimal dSubt = 0;

            if ((Qty > 0) & (Price > 0))
            {
                dTot = Mult(Qty, Price);
                dIte = Mult(dTot, 0.18m);
                dSubt = Subt(dTot, dIte);
            }

            inD = new InvoiceDetail()
            {
                CustomerId = CustomerId,
                Qty = Qty,
                Price = Price,
                TotalItbis = dIte,
                SubTotal = dSubt,
                Total = dTot
            };

            return JsonConvert.SerializeObject(inD);
        }

        // POST: InvoiceDetails/Create        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CustomerId,Qty,Price,TotalItbis,SubTotal,Total")] InvoiceDetail invoiceDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(invoiceDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Invoice, "Id", "Id", invoiceDetail.CustomerId);
            return View(invoiceDetail);
        }

        // GET: InvoiceDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceDetail = await _context.InvoiceDetail.FindAsync(id);
            if (invoiceDetail == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Invoice, "Id", "Id", invoiceDetail.CustomerId);
            return View(invoiceDetail);
        }

        // POST: InvoiceDetails/Edit/5        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CustomerId,Qty,Price,TotalItbis,SubTotal,Total")] InvoiceDetail invoiceDetail)
        {
            if (id != invoiceDetail.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(invoiceDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvoiceDetailExists(invoiceDetail.Id))
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
            ViewData["CustomerId"] = new SelectList(_context.Invoice, "Id", "Id", invoiceDetail.CustomerId);
            return View(invoiceDetail);
        }

        // GET: InvoiceDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceDetail = await _context.InvoiceDetail
                .Include(i => i.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (invoiceDetail == null)
            {
                return NotFound();
            }

            return View(invoiceDetail);
        }

        // POST: InvoiceDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var invoiceDetail = await _context.InvoiceDetail.FindAsync(id);
            _context.InvoiceDetail.Remove(invoiceDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InvoiceDetailExists(int id)
        {
            return _context.InvoiceDetail.Any(e => e.Id == id);
        }
    }
}
