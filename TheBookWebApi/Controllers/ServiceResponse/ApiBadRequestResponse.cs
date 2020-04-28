using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheBookWebApi.Controllers.ServiceResponse
{
  public class ApiBadRequestResponse : ApiResponse
  {
	public IEnumerable<string> Errors { get; }
	public ApiBadRequestResponse(ModelStateDictionary modelState) : base(400)
	{
	  if (modelState.IsValid)
	  {
		throw new ArgumentException("Validation failed", nameof(modelState));
	  }

	  Errors = modelState.SelectMany(x => x.Value.Errors)
		  .Select(x => x.ErrorMessage).ToArray();
	}

	public ApiBadRequestResponse(ModelStateDictionary modelState,int statusCode, string message) : base(statusCode, message)
	{
	  if (modelState.IsValid)
	  {
		throw new ArgumentException("Validation failed", nameof(modelState));
	  }

	  Errors = modelState.SelectMany(x => x.Value.Errors)
		 .Select(x => x.ErrorMessage).ToArray();
	 
	}


  }
}
