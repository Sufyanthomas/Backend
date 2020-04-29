using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TheBookWebApi.Controllers.ServiceResponse;
using TheBookWebApi.Models.DB;

namespace TheBookWebApi.Controllers.Salesoperation
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController : Controller
    {
        private readonly DefaultContext asd;

        public InvoicesController(DefaultContext context)
        {
            asd = context;
        }

        [HttpGet]
        public IActionResult GetAllInvoices()
        {
            try
            {
                var query = (from i in asd.Invoices
                             join c in asd.Customer on i.CustId equals c.CustId into ic
                             from icResult in ic.DefaultIfEmpty()

                             join s in asd.Sales on i.SalesId equals s.SalesId into si
                             from siResult in si.DefaultIfEmpty()
                             select new
                             {
                                 i.InvoId,
                                 i.InvoNo,
                                 i.CustId,
                                 //   FirstName = icResult != null ? icResult.FirstName : string.Empty,
                                 //   LastName = icResult != null ? icResult.LastName : string.Empty,
                                 //   Email = icResult != null ? icResult.Email : string.Empty,
                                 //   Mobile = icResult != null ? icResult.Mobile : int.MaxValue,

                                 i.SalesId,
                                 Name = siResult != null ? siResult.Name : string.Empty,

                                 i.Amount,
                                 i.AmountDue,
                                 i.TransactionDate,
                                 i.IsCommited
                             }
                             ).ToList();
                return Ok(new ApiOkResponse(query));
            }
            catch (Exception cx)
            {
                throw new ApiExceptionResponse(cx);
            }
        }
    }
}