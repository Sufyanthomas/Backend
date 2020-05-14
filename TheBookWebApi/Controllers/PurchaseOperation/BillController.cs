using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TheBookWebApi.Controllers.ServiceResponse;
using TheBookWebApi.Models.DB;

namespace TheBookWebApi.Controllers.PurchaseOperation
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillController : Controller
    {
        private readonly DefaultContext ex;

        public BillController(DefaultContext context)
        {
            ex = context;
        }
        public IActionResult GetAllBill()
        {
            try
            {
                var query = (from t in ex.Bill
                             join f in ex.Supplier on t.SupplierId equals f.SupplierId into ft
                             from ftResult in ft.DefaultIfEmpty()

                             //join p in ex.Supplier on t.SupplierId equals p.SupplierId into pt
                             //from ptResult in pt.DefaultIfEmpty()
                             select new
                             {
                                 t.BillId,
                                 t.BillNo,
                                 t.PurchaseId,


                                 t.SupplierId,
                                 FirstName = ftResult != null ? ftResult.FirstName : string.Empty,

                                 t.TransactionDate,
                                 t.AmountDue,
                                 t.Amount,
                                 t.Status,
                                 t.IsCommited
                             }
                             ).ToList();
                return Ok(new ApiOkResponse(query));
            }
            catch (Exception sd)
            {
                throw new ApiExceptionResponse(sd);
            }
        }
    }
}