using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmazer.Base.ExtensionMethods
{
  public static class extBool
  {
	public static bool? ToBoolean(string value)
	{
	  try
	  {
		bool flag;
		Boolean.TryParse(value, out flag);

		if (Boolean.TryParse(value, out flag))
		  return flag;
		else
		{
		  if (value == "0")
			flag = false;
		  else if (value == "1")
			flag = true;
		  else
			return null;
		}

		return flag;

	  }
	  catch
	  {
		return null;
	  }

	}

  }
}
