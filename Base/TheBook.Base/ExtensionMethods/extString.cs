using System;

namespace Pharmazer.Base.ExtensionMethods
{
  public static class extString
  {
	public static bool isDate(this string Date)
	{
	  try
	  {
		Convert.ToDateTime(Date);
		return true;
	  }
	  catch
	  {
		return false;
	  }
	}
	public static bool isNumeric(this string Number)
	{
	  try
	  {
		Convert.ToInt32(Number);
		return true;
	  }
	  catch
	  {
		return false;
	  }
	}
	public static bool isDecimal(this string Decimal)
	{
	  try
	  {
		Convert.ToDecimal(Decimal);
		return true;
	  }
	  catch
	  {
		return false;
	  }
	}

	public static bool isBoolean(this string boolean)
	{
	  try
	  {
		Convert.ToBoolean(boolean);
		return true;
	  }
	  catch
	  {
		return false;
	  }
	}
	
	public static int ToInt32(this string str)
	{
	  return Convert.ToInt32(str == string.Empty ? null : str);
	}
	public static decimal ToDecimal(this string str)
	{
	  return Convert.ToDecimal(str == string.Empty ? null : str);
	}
	public static DateTime ToDateTime(this string str)
	{
	  return Convert.ToDateTime(str == string.Empty ? null : str);
	}
	public static string CropText(this string str, int length)
	{
	  return str.Length >= length ? str.Substring(0, length) : str;
	}
  }
}