using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheBookWebApi.Controllers.ServiceResponse;

namespace TheBookWebApi.Controllers.ActionFilters
{
  public class ApiExceptionFilter : ExceptionFilterAttribute
  {
	public override void OnException(ExceptionContext context)
	{
	  ApiResponse apiResponse = null;
	  if (context.Exception is ApiExceptionResponse)
	  {
		// handle explicit 'known' API errors
		var ex = context.Exception as ApiExceptionResponse;
		context.Exception = null;
		apiResponse = new ApiResponse(ex.StatusCode, ex.Message);		
		context.HttpContext.Response.StatusCode = ex.StatusCode;
	  }
	  else if (context.Exception is UnauthorizedAccessException)
	  {
		apiResponse = new ApiResponse(401, "Unauthorized Access");
		context.HttpContext.Response.StatusCode = 401;

		// handle logging here
	  }
	  else
	  {
		// Unhandled errors

		var msg = context.Exception.GetBaseException().Message;
		string stack = context.Exception.StackTrace;

		apiResponse = new ApiResponse(500, msg);
		
		context.HttpContext.Response.StatusCode = 500;

		// handle logging here
	  }

	  // always return a JSON result
	  context.Result = new JsonResult(apiResponse);

	  base.OnException(context);
	}
  }
}
