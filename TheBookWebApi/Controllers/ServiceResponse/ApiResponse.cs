using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheBookWebApi.Controllers.ServiceResponse
{
  public class ApiResponse
  {
	public int StatusCode { get; }

	[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
	public string Message { get; }
		
	public ApiResponse(int statusCode, string message = null)
	{
	  StatusCode = statusCode;
	  Message = message ?? GetDefaultMessageForStatusCode(statusCode);
	}

	private static string GetDefaultMessageForStatusCode(int statusCode)
	{
	  switch (statusCode)
	  {
		case 200:
		  return "Ok";
		case 204:
		  return "No Content";
		case 400:
		  return "Bad Request: The request cannot be fulfilled due To bad syntax";
		case 401:
		  return "Unauthorized";

		case 403:
		  return "Forbidden";

		case 404:
		  return "Not Found: Requested data  is not found";

		case 405:
		  {
			// e.g.
			// REGISTERUSER: User cannot be registered because Of age less than 13.
			//LOGINUSERBYEMAIL: User found With provided email but it Is Not verified. Verify email And retry Or retry With mobile Or user name.

			return "Not Acceptable: The provided information does Not meet the logical conditions And Or flow Of the system.";
		  }
		case 408:
		  return "Request Timeout";

		case 409:
		  return "Conflict";

		case 422:
		  {
			// this Is raised when an app/server logic cannot accept the data for creating New record Or performing some function. details in response
			// e.g.
			// ERROR : There Is already a user-record available with provided mobile number.
			// SUCCES: has RegisterUserFailed Object
			return "Unprocessable Entity: The request was well-formed but was unable To be followed due To semantic errors";
		  }
		case 500:
		  return "Internal Server Error";

		case 501:
		  return "Not Implemented: the server either does Not recognize the request method, Or it lacks the ability To fulfill the request";

		case 503:
		  return "Service Unavailable";

		default:
		  return null;
	  }
	}

	public override string ToString()
	{
	  return JsonConvert.SerializeObject(this);
	}
  }
}
