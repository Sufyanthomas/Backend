using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheBookWebApi.Controllers.ServiceResponse;
using TheBookWebApi.Models.DB;
using TheBookWebApi.Models.ViewModel.Purchase;

namespace TheBookWebApi.Controllers.PurchaseOperation
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : Controller
    {
        private readonly DefaultContext ctx;

        public PurchaseController(DefaultContext context)
        {
            ctx = context;
        }
        public IActionResult GetAllPurchase()
        {
            try
            {
                var query = (from s in ctx.Purchase
                             select new
                             {
                                 s.PurchaseId,
                                 s.Name,
                                 s.Description,
                                 s.CostPrice,
                                 s.TaxId,
                                 s.Price,
                                 s.IsCommited
                             }
                             ).ToList();
                return Ok(new ApiOkResponse(query));
            }
            catch(Exception yu)
            {
                throw new ApiExceptionResponse(yu);
            }
        }

        [HttpGet("{id}")]

        public IActionResult GetById(int id)
        {
            try
            {
                var query = (from t in ctx.Purchase
                             where t.PurchaseId == id
                             select new
                             {
                                 t.PurchaseId,
                                 t.Name,
                                 t.Description,
                                 t.CostPrice,
                                 t.TaxId,
                                 t.Price,
                                 t.IsCommited
                             }
                             ).FirstOrDefault();
                return Ok(new ApiOkResponse(query));
            }
            catch (Exception rt)
            {
                throw new ApiExceptionResponse(rt);
            }
        }
        [HttpPost]

        public IActionResult Create([FromBody] PurchaseViewModel formdata)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiBadRequestResponse(ModelState));
                }

                var objPurchase = new Purchase();
                objPurchase.Name = formdata.Name;
                objPurchase.Description = formdata.Description;
                objPurchase.CostPrice = formdata.CostPrice;
                objPurchase.TaxId = formdata.TaxId;
                objPurchase.Price = formdata.Price;
                objPurchase.IsCommited = formdata.IsCommited;

                ctx.Purchase.Add(objPurchase);
                ctx.SaveChanges();

                return Ok(new ApiResponse(200, "Record has been saved"));
            }
            catch (Exception zx)
            {
                throw new ApiExceptionResponse(zx);
            }
        }
        [HttpPut("{id}")]

        public IActionResult Update(int id,[FromBody] PurchaseViewModel formdata)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiBadRequestResponse(ModelState));
                }

                var objpurchase = ctx.Purchase.Where(x => x.PurchaseId == id).FirstOrDefault();

                if(objpurchase == null)
                {
                    return NotFound(new ApiResponse(404, "No record found"));
                }

                objpurchase.Name = formdata.Name;
                objpurchase.Description = formdata.Description;
                objpurchase.CostPrice = formdata.CostPrice;
                objpurchase.TaxId = formdata.TaxId;
                objpurchase.Price = formdata.Price;
                objpurchase.IsCommited = formdata.IsCommited;

                ctx.Entry(objpurchase).State = EntityState.Modified;
                ctx.SaveChanges();

                return Ok(new ApiResponse(200, "Record has updated"));
            }
            catch (Exception fg)
            {
                throw new ApiExceptionResponse(fg);
            }
        }
    }
}