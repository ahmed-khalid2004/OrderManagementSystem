using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Data;
using OrderManagementSystem.Models;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class InvoiceController : ControllerBase
    {
        private readonly OrderManagementDbContext _context;

        public InvoiceController(OrderManagementDbContext context)
        {
            _context = context;
        }

        [HttpGet("{invoiceId}")]
        public async Task<ActionResult<Invoice>> GetInvoice(int invoiceId)
        {
            var invoice = await _context.Invoices
                .Include(i => i.Order)
                .ThenInclude(o => o.OrderItems)
                .FirstOrDefaultAsync(i => i.InvoiceId == invoiceId);

            if (invoice == null)
                return NotFound();

            return Ok(invoice);
        }

        [HttpGet]
        public async Task<ActionResult> GetAllInvoices()
        {
            var invoices = await _context.Invoices
                .Include(i => i.Order)
                .ThenInclude(o => o.OrderItems)
                .ToListAsync();

            return Ok(invoices);
        }
    }
}