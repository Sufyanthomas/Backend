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
    public class SalesController : Controller
    {
        private readonly DefaultContext ex;

        public SalesController(DefaultContext context)
        {
            ex = context;
        }

        [HttpGet]
        public IActionResult GetAllSales()
        {
            try
            {
                var query = (from s in ex.Sales
                             join t in ex.Tax on s.TaxId equals t.TaxId into st
                             from stResult in st.DefaultIfEmpty()
                             select new
                             {
                                 s.SalesId,
                                 s.Name,
                                 s.Description,
                                 s.CostPrice,

                                 s.TaxId,
                                 Abbrevation = stResult != null ? stResult.Abbrevation : string.Empty,
                                 TaxRate = stResult != null ? stResult.TaxRate : int.MinValue,

                                 s.Price,
                                 s.DateCreated,
                                 s.DateModified,
                                 s.IsCommited
                             }
                             ).ToList();
                return Ok(new ApiOkResponse(query));
            }
            catch (Exception ex)
            {
                throw new ApiExceptionResponse(ex);
            }
        }

        [HttpGet("{id}")]

        public IActionResult GetById(int id)
        {
            try
            {
                var query = (from t in ex.Sales
                             where t.SalesId == id
                             select new
                             {
                                 t.SalesId,
                                 t.Name,
                                 t.Description,
                                 t.CostPrice,
                                 t.TaxId,
                                 t.Price,
                                 t.DateCreated,
                                 t.DateModified,
                                 t.IsCommited
                             }
                            ).FirstOrDefault();
                return Ok(new ApiOkResponse(query));
            }
            catch (Exception cf)
            {
                throw new ApiExceptionResponse(cf);
            }
        }

        [HttpPost]

        public IActionResult Create([FromBody] SalesViewModel formData)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiBadRequestResponse(ModelState));
                }

                var objSales = new Sals();
            }
        }
    }
}