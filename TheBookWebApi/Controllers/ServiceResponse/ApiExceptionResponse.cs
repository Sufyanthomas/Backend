using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheBookWebApi.Controllers.ServiceResponse
{
  public class ApiExceptionResponse : Exception
  {
	public int StatusCode { get; set; }

	public ApiExceptionResponse(string message, int statusCode = 500) : base(message)
	{
	  StatusCode = statusCode;
	}
	public ApiExceptionResponse(Exception ex) : base(ex.InnerException.Message)
	{
	  StatusCode = 500;	  
	}
  }

}
