using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Workshop.API.DTOs;
using Workshop.API.Entities;
using Workshop.API.Interfaces;
using Workshop.API.Models;

namespace Workshop.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public InvoiceController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<ActionResult> AddInvoice([FromBody]InvoiceAddDto invoiceDto)
        {
            var invoice = _mapper.Map<Invoice>(invoiceDto);

            invoice.Number = await _unitOfWork.InvoiceRepository.GetInvoiceNextNumber(invoiceDto.KanbanTaskId);
            invoice.InvoiceCode = $"FV/{invoice.KanbanTaskId,6:D6}/{invoice.Number}";

            var kanbanTask = await _unitOfWork.KanbanRepository.GetKanbanTask(invoice.KanbanTaskId);

            var kt = _mapper.Map<KanbanTaskHistoryDto>(kanbanTask);
            kt.TotalWorkHoursCosts *= 100; 

            invoice.WorkHoursPriceNetto = kt.TotalWorkHoursCosts;
            invoice.VAT = 23;
            invoice.WorkHoursPriceBrutto = kt.TotalWorkHoursCosts + (kt.TotalWorkHoursCosts * invoice.VAT/100);
            invoice.PriceNetto = kt.TotalBasketPrice + kt.TotalWorkHoursCosts;
            invoice.PriceBrutto = invoice.PriceNetto + (invoice.PriceNetto * invoice.VAT/100);

            _unitOfWork.InvoiceRepository.AddInvoice(invoice);

            var result = await _unitOfWork.SaveAsync();

            if(result)
                return Ok(invoice.Id);

            return StatusCode(500);
        }

        [HttpGet]
        [Route("invoices")]
        public async Task<ActionResult> GetInvoices([FromQuery]InvoiceQuery query)
        {
            if(!query.Validate())
                return BadRequest();

            var invoices = await _unitOfWork.InvoiceRepository.GetInvoices(query);
            var invoicesToReturn = _mapper.Map<InvoiceDto[]>(invoices);

            return Ok(invoicesToReturn);
        }

        [HttpGet]
        public async Task<ActionResult> GetInvoice([FromQuery]int invoiceId)
        {
            if(invoiceId <= 0)
                return BadRequest();

            var invoice = await _unitOfWork.InvoiceRepository.GetInvoice(invoiceId);
            var invoiceToReturn = _mapper.Map<InvoiceDetailsDto>(invoice);

            return Ok(invoiceToReturn);
        }

        [HttpDelete]
        public async Task<ActionResult> RemoveInvoice([FromQuery]int invoiceId)
        {
            if(invoiceId <= 0)
                return BadRequest();

            var invoice = await _unitOfWork.InvoiceRepository.GetInvoice(invoiceId);

            if(invoice == null)
                return NotFound();

            _unitOfWork.InvoiceRepository.RemoveInvoice(invoice);

            var result = await _unitOfWork.SaveAsync();

            if(result)
                return Ok();

            return StatusCode(500);
        }
    }
}