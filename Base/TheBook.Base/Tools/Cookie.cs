using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Web;

namespace Pharmazer.Base.Tools
{
	public class Cookie
	{
		public static void WriteCookie(string key, string value)
		{
			try
			{
				//Create a Cookie with a suitable Key.
				HttpCookie nameCookie = new HttpCookie(key);

				//Set the Cookie value.
				nameCookie.Values[key] = value;

				//Set the Expiry date.
				nameCookie.Expires = DateTime.Now.AddDays(30);

				//Add the Cookie to Browser.
				HttpContext.Current.Response.Cookies.Add(nameCookie);
			}
			catch
			{
				return;
			}
		}

		public static string ReadCookie(string key)
		{
			try
			{
				//Fetch the Cookie using its Key.
				HttpCookie nameCookie = HttpContext.Current.Request.Cookies[key];
				//If Cookie exists fetch its value.
				string name = nameCookie != null ? HttpContext.Current.Server.HtmlEncode(nameCookie.Value.Split('=')[1]) : string.Empty;
				return name;

			}
			catch
			{
				return string.Empty;
			}
		}

		public static void RemoveCookie(string key)
		{
			try
			{
				//Fetch the Cookie using its Key.
				HttpCookie nameCookie = HttpContext.Current.Request.Cookies[key];

				//Set the Expiry date to past date.
				nameCookie.Expires = DateTime.Now.AddDays(-1);

				//Update the Cookie in Browser.
				HttpContext.Current.Response.Cookies.Add(nameCookie);

			}
			catch
			{
				return;
			}
		}

	}
}