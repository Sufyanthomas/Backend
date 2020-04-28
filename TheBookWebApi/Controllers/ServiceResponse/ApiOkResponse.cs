using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheBookWebApi.Controllers.ServiceResponse
{
  public class ApiOkResponse : ApiResponse
  {
	public object Data { get; }
	public ApiOkResponse(object result) : base(200)
	{
	  Data = result;
	}

  }
}
