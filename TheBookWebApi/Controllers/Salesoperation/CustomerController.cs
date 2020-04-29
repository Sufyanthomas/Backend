using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheBookWebApi.Controllers.ServiceResponse;
using TheBookWebApi.Models.DB;
using TheBookWebApi.Models.ViewModel;

namespace TheBookWebApi.Controllers.Salesoperation
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly DefaultContext ctx;

        public CustomerController(DefaultContext context)
        {
            ctx = context;
        }

        [HttpGet]
        public IActionResult GetAllCustomer()
        {
            try
            {
                var query = (from t in ctx.Customer
                             select new
                             {
                                 t.CustId,
                                 t.FirstName,
                                 t.LastName,
                                 t.Email,
                                 t.Mobile,
                                 t.Address,
                                 t.IsActive
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
                var query = (from t in ctx.Customer
                             where t.CustId == id
                             select new
                             {
                                 t.CustId,
                                 t.FirstName,
                                 t.LastName,
                                 t.Email,
                                 t.Mobile,
                                 t.Address,
                                 t.IsActive
                             }
                             ).FirstOrDefault();
                return Ok(new ApiOkResponse(query));
            }
            catch (Exception ex)
            {
                throw new ApiExceptionResponse(ex);
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] CustomerViewModel formData)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiBadRequestResponse(ModelState));
                }

                var objCust = new Customer();
                objCust.FirstName = formData.FirstName;
                objCust.LastName = formData.LastName;
                objCust.Email = formData.Email;
                objCust.Mobile = formData.Mobile;
                objCust.Address = formData.Address;
                objCust.IsActive = formData.IsActive;
                ctx.Customer.Add(objCust);
                ctx.SaveChanges();


                return Ok(new ApiResponse(200, "Record has been saved"));
            }

            catch (Exception ex)
            {
                throw new ApiExceptionResponse(ex);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] CustomerViewModel formData)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiBadRequestResponse(ModelState));
                }

                var objCust = ctx.Customer.Where(x => x.CustId == id).FirstOrDefault();

                if (objCust == null)
                {
                    return NotFound(new ApiResponse(404, "no record found"));
                }

                objCust.FirstName = formData.FirstName;
                objCust.LastName = formData.LastName;
                objCust.Email = formData.Email;
                objCust.Mobile = formData.Mobile;
                objCust.Address = formData.Address;
                objCust.IsActive = formData.IsActive;

                ctx.Entry(objCust).State = EntityState.Modified;
                ctx.SaveChanges();

                return Ok(new ApiResponse(200, "Record has been updated"));
            }

            catch (Exception ex)
            {
                throw new ApiExceptionResponse(ex);
            }
        }

        [HttpDelete("{id}")]

        public IActionResult Delete(int id)
        {
            try
            {
                var objCust = ctx.Customer.Where(x => x.CustId == id).FirstOrDefault();

                if (objCust == null)
                {
                    return NotFound(new ApiResponse(404, "No Record Found"));
                }

                ctx.Entry(objCust).State = EntityState.Deleted;
                ctx.SaveChanges();

                return Ok(new ApiResponse(200, "Record has been deleted"));
            }
            catch (Exception sd)
            {
                throw new ApiExceptionResponse(sd);
            }
        }
    }
}