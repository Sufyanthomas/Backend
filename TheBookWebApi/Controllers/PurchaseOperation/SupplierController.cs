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
    public class SupplierController : Controller
    {
        private readonly DefaultContext dg;

        public SupplierController(DefaultContext context)
        {
            dg = context;
        }
        public IActionResult GetAllSupplier()
        {
            try
            {
                var query = (from i in dg.Supplier
                             select new
                             {
                                 i.SupplierId,
                                 i.FirstName,
                                 i.LastName,
                                 i.Email,
                                 i.Mobile,
                                 i.Address,
                                 i.IsActive
                             }
                             ).DefaultIfEmpty();
                return Ok(new ApiOkResponse(query));
            }
            catch(Exception df)
            {
                throw new ApiExceptionResponse(df);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var query = (from i in dg.Supplier
                             where i.SupplierId == id
                             select new
                             {
                                 i.SupplierId,
                                 i.FirstName,
                                 i.LastName,
                                 i.Email,
                                 i.Mobile,
                                 i.Address,
                                 i.IsActive
                             }
                             ).FirstOrDefault();
                return Ok(new ApiOkResponse(query));
            }
            catch(Exception dg)
            {
                throw new ApiExceptionResponse(dg);
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] SupplierViewModel formData)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiBadRequestResponse(ModelState));
                }

                var objSupp = new Supplier();
                objSupp.FirstName = formData.FirstName;
                objSupp.LastName = formData.LastName;
                objSupp.Email = formData.Email;
                objSupp.Mobile = formData.Mobile;
                objSupp.Address = formData.Address;
                objSupp.IsActive = formData.IsActive;

                    dg.Supplier.Add(objSupp);
                dg.SaveChanges();

                return Ok(new ApiResponse(200, "Record has been saved"));
            }
            catch (Exception sf)
            {
                throw new ApiExceptionResponse(sf);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] SupplierViewModel formData)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiBadRequestResponse(ModelState));
                }

                var objSupp = dg.Supplier.Where(x => x.SupplierId == id).FirstOrDefault();

                if(objSupp == null)
                {
                    return NotFound(new ApiResponse(404, "No record found"));
                }

                objSupp.FirstName = formData.FirstName;
                objSupp.LastName = formData.LastName;
                objSupp.Email = formData.Email;
                objSupp.Mobile = formData.Mobile;
                objSupp.Address = formData.Address;
                objSupp.IsActive = formData.IsActive;

                dg.Entry(objSupp).State = EntityState.Modified;
                dg.SaveChanges();

                return Ok(new ApiResponse(200, "Record has been updated"));
            }
            catch (Exception sf)
            {
                throw new ApiExceptionResponse(sf);
            }
        }

        [HttpDelete("{id}")]

        public IActionResult Delete(int id)
        {
            try
            {
                var objSupp = dg.Supplier.Where(x => x.SupplierId == id).FirstOrDefault();

                if(objSupp == null)
                {
                    return NotFound(new ApiResponse(404, "No record found"));
                }

                dg.Entry(objSupp).State = EntityState.Deleted;
                dg.SaveChanges();

                return Ok(new ApiResponse(200, "Record has been deleted"));
            }
            catch(Exception sd)
            {
                throw new ApiExceptionResponse(sd);
            }
        }
    }
}