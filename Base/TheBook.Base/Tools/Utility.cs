using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Web.UI.HtmlControls;
using Pharmazer.Base.ExtensionMethods;
using System.IO;
using System.Net;
using System.Xml.Serialization;
using System.Security.Cryptography;
using System.Xml;

namespace Pharmazer.Base.Tools
{
  public class Utility
  {
	public static T FromXml<T>(string xml)
	{
	  T result = default(T);
	  XmlSerializer serializer = new XmlSerializer(typeof(T));
	  MemoryStream memoryStream = new MemoryStream();
	  StreamWriter writer = new StreamWriter(memoryStream, Encoding.UTF8);
	  StreamReader reader = null;

	  try
	  {
		writer.Write(xml);
		writer.Flush();
		memoryStream.Position = 0;
		reader = new StreamReader(memoryStream, Encoding.UTF8);
		result = (T)serializer.Deserialize(reader);
	  }
	  catch
	  {

	  }
	  finally
	  {
		writer.Close();
		if (reader != null)
		{
		  reader.Close();
		}
		memoryStream.Close();
	  }

	  return result;
	}
	public static string ToXml<T>(T value)
	{
	  string result = "";

	  if (value != null)
	  {
		XmlSerializer serializer = new XmlSerializer(value.GetType());
		MemoryStream memoryStream = new MemoryStream();
		StreamWriter writer = new StreamWriter(memoryStream, Encoding.UTF8);
		StreamReader reader = null;

		try
		{
		  serializer.Serialize(writer, value);
		  memoryStream.Position = 0;
		  reader = new StreamReader(memoryStream, Encoding.UTF8);
		  result = reader.ReadToEnd();
		}
		catch
		{

		}
		finally
		{
		  writer.Close();
		  if (reader != null)
		  {
			reader.Close();
		  }
		  memoryStream.Close();
		}
	  }
	  return result;
	}
	public static T CreateInstance<T>(T obj) where T : new()
	{
	  if (obj != null)
	  {
		return obj;
	  }
	  else
	  {
		return new T();
	  }
	}
	public static string RemoveHtml(string text)
	{
	  return Regex.Replace(text, @"<(.|\n)*?>", string.Empty);
	}
	public static bool IsEmail(string mail)
	{
	  string emailPattern = @"^(([^<>()[\]\\.,;:\s@\""]+"
	  + @"(\.[^<>()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@"
	  + @"((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}"
	  + @"\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+"
	  + @"[a-zA-Z]{2,}))$";

	  return Regex.IsMatch(mail, emailPattern);
	}
	public static bool IsDate(string text)
	{
	  try
	  {
		DateTime temp;

		if (DateTime.TryParse(text, out temp))
		{
		  return true;
		}
		else
		{
		  return false;
		}
	  }
	  catch
	  {
		return false;
	  }
	}
	public static bool isNumeric(string text)
	{
	  try
	  {
		Convert.ToInt32(text);
		return true;
	  }
	  catch
	  {
		return false;
	  }
	}
	public static string Encrypt(string strValue)
	{
	  try
	  {
		return Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(strValue));
	  }
	  catch (Exception)
	  {
		return string.Empty;
	  }
	}
	public static string Decrypt(string strValue)
	{
	  try
	  {
		return System.Text.Encoding.ASCII.GetString(Convert.FromBase64String(strValue));
	  }
	  catch (Exception)
	  {
		return string.Empty;
	  }
	}

	public static Guid GenerateGUID()
	{
	  return Guid.NewGuid();
	}

	public static int CalculateAge(DateTime birthday)
	{
	  try
	  {
		if (birthday == null || birthday > DateTime.Today) return 0;

		DateTime now = DateTime.Today;
		int age = now.Year - birthday.Year;
		if (now < birthday.AddYears(age)) age--;

		return age;
	  }
	  catch (Exception)
	  {
		return 0;
	  }
	}

	public static string CurrencyFormat(decimal currency)
	{

	  string temp = "";
	  if (currency < 1)
	  {
		temp = "0" + currency.ToString("#,##.00");
	  }
	  else
	  {
		temp = currency.ToString("#,##.00");
	  }
	  if (temp.Substring(temp.Length - 3, 1) != ",")
	  {
		temp = temp.Replace(',', '+');
		temp = temp.Replace('.', ',');
		temp = temp.Replace('+', '.');
	  }
	  return temp;
	}
	public static string GetAppSetting(string key)
	{
	  try
	  {
		return ConfigurationManager.AppSettings[key].ToString();
	  }
	  catch
	  {
		return string.Empty;
	  }
	}
	public static string GetAppSetting(string key, string defaultvalue)
	{
	  try
	  {
		return ConfigurationManager.AppSettings[key].ToString();
	  }
	  catch
	  {
		return defaultvalue;
	  }
	}
	public static string GetConnectionString(string connectionstringAlias)
	{
	  return ConfigurationManager.ConnectionStrings[connectionstringAlias].ConnectionString;
	}
	public static string GetSessionValue(string sessionkey, string defaultvalue)
	{
	  if (HttpContext.Current.Session[sessionkey] != null)
	  {
		return HttpContext.Current.Session[sessionkey].ToString();
	  }
	  return defaultvalue;
	}
	public static int GetSessionValue(string sessionkey, int defaultvalue)
	{
	  if (HttpContext.Current.Session[sessionkey] != null)
	  {
		return Convert.ToInt32(HttpContext.Current.Session[sessionkey].ToString());
	  }
	  return defaultvalue;
	}
	public static string GetQueryStringValue(string querystringkey, string defaultvalue)
	{
	  string strRetVal = string.Empty;
	  if (HttpContext.Current.Request.QueryString[querystringkey] != null)
	  {
		return HttpContext.Current.Request.QueryString[querystringkey].ToString();
	  }
	  return defaultvalue;
	}
	public static int GetQueryStringValue(string querystringkey, int defaultvalue)
	{
	  string strRetVal = string.Empty;
	  if (HttpContext.Current.Request.QueryString[querystringkey] != null)
	  {
		return Convert.ToInt32(HttpContext.Current.Request.QueryString[querystringkey].ToString());
	  }
	  return defaultvalue;
	}
	public static string GetUrlPath(string Url, string defaultvalue)
	{
	  HttpServerUtility hs = HttpContext.Current.Server;
	  try
	  {
		return hs.MapPath(Url);
	  }
	  catch
	  {
		return defaultvalue;
	  }
	}
	public static bool SaveImage(string url, string filename)
	{
	  WebResponse response = null;
	  Stream remoteStream = null;
	  StreamReader readStream = null;
	  try
	  {
		WebRequest request = WebRequest.Create(url);
		if (request != null)
		{
		  response = request.GetResponse();
		  if (response != null)
		  {
			remoteStream = response.GetResponseStream();
			readStream = new StreamReader(remoteStream);
			System.Drawing.Image img = System.Drawing.Image.FromStream(remoteStream);
			if (img == null)
			{
			  return false;
			}
			else
			{
			  img.Save(filename, System.Drawing.Imaging.ImageFormat.Jpeg);
			  img.Dispose();
			  return true;
			}
		  }
		  else
		  {
			return false;
		  }
		}
		else
		{
		  return false;
		}
	  }
	  finally
	  {
		if (response != null) response.Close();
		if (remoteStream != null) remoteStream.Close();
		if (readStream != null) readStream.Close();
	  }
	}
	private static Random rnd = new Random();
	public static string GenerateRandomString(int keyLength)
	{
	  string[] keys = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "Q", "W", "E", "R", "T", "Y", "U", "I", "O", "P", "A", "S", "D", "F", "G", "H", "J", "K", "L", "Z", "X", "C", "V", "B", "N", "M" };
	  StringBuilder stbRandomString = new StringBuilder(keyLength);
	  for (int i = 0; i < keyLength; i++)
	  {
		stbRandomString.Append(keys[rnd.Next(0, 36)]);
	  }
	  return stbRandomString.ToString();
	}
	public static string GenerateRandomNumbers(int keyLength)
	{
	  string[] keys = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
	  StringBuilder stbRandomString = new StringBuilder(keyLength);
	  for (int i = 0; i < keyLength; i++)
	  {
		stbRandomString.Append(keys[rnd.Next(0, 10)]);
	  }
	  return stbRandomString.ToString();
	}


	public static int MonthDifference(DateTime startDate, DateTime endDate)
	{
	  try
	  {
		//int monthsApart = 12 * (startDate.Year - endDate.Year) + startDate.Month - endDate.Month;
		//return Math.Abs(monthsApart);
		//return Convert.ToInt32(startDate.Subtract(endDate).Days / (365.25 / 12));

		int months = ((endDate.Year * 12) + endDate.Month) - ((startDate.Year * 12) + startDate.Month);

		if (endDate.Day >= startDate.Day)
		{
		  months++;
		}

		return months;
	  }
	  catch
	  {
		return 0;
	  }
	}

	public static string GetViewStateValue(StateBag ViewStateObject, string ViewStatekey, string defaultvalue)
	{
	  string strRetVal = string.Empty;
	  if (ViewStateObject[ViewStatekey] != null)
	  {
		return ViewStateObject[ViewStatekey].ToString();
	  }
	  return defaultvalue;
	}
	public static string JsAlert(string Message)
	{
	  return "<script language=\"javascript\">alert('" + Message + "');</script>";
	}
	public static string GetTextMonth(int month)
	{
	  string i = string.Empty;
	  switch (month)
	  {
		case 1:
		  i = "Ocak"; break;
		case 2:
		  i = "Şubat"; break;
		case 3:
		  i = "Mart"; break;
		case 4:
		  i = "Nisan"; break;
		case 5:
		  i = "Mayıs"; break;
		case 6:
		  i = "Haziran"; break;
		case 7:
		  i = "Temmuz"; break;
		case 8:
		  i = "Ağustos"; break;
		case 9:
		  i = "Eylül"; break;
		case 10:
		  i = "Ekim"; break;
		case 11:
		  i = "Aralık"; break;
		case 12:
		  i = "Ocak"; break;
		default:
		  i = "Şubat"; break;
	  }
	  return i;
	}
	public static bool ImageResize(string imagesPath, int requestWidth, int requestHeight, string savePath)
	{
	  try
	  {
		System.Drawing.Image asilResim = System.Drawing.Image.FromFile(imagesPath);
		int yeniYukseklik = requestHeight;

		Bitmap bmp = new Bitmap(requestWidth, yeniYukseklik);
		bmp.SetResolution(72, 72);
		Graphics gr = Graphics.FromImage(bmp);
		gr.SmoothingMode = SmoothingMode.AntiAlias;
		gr.CompositingQuality = CompositingQuality.HighQuality;
		gr.InterpolationMode = InterpolationMode.High;
		gr.PixelOffsetMode = PixelOffsetMode.HighQuality;

		Rectangle rect = new Rectangle(0, 0, requestWidth, yeniYukseklik);
		gr.DrawImage(asilResim, rect, 0, 0, asilResim.Width, asilResim.Height, GraphicsUnit.Pixel);

		System.Drawing.Imaging.ImageCodecInfo codec;
		string extension = System.IO.Path.GetExtension(imagesPath).ToLower();
		if (extension == ".png")
		{
		  codec = System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders()[4];
		}
		else
		{
		  codec = System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders()[1];
		}
		System.Drawing.Imaging.EncoderParameters eParams = new System.Drawing.Imaging.EncoderParameters(1);
		eParams.Param[0] = new System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L);

		bmp.Save(savePath, codec, eParams);
		bmp.Dispose();
		asilResim.Dispose();
		return true;
	  }
	  catch (Exception)
	  {
		return false;
	  }
	}
	public static void CreatePageMetaTag(string title, string keywordsValue, string descriptionValue)
	{

	  HtmlMeta keywords = new HtmlMeta() { Name = "keywords", Content = string.Join(", ", Utility.RemoveHtml(keywordsValue).Split(' ', ',', '.', '-')) };
	  HtmlMeta description = new HtmlMeta() { Name = "description", Content = Utility.RemoveHtml(descriptionValue).CropText(150) };
	  Page page = HttpContext.Current.CurrentHandler as Page;
	  page.Title = title;
	  page.Header.Controls.Add(keywords);
	  page.Header.Controls.Add(description);
	}

	private static readonly Dictionary<string, string> MIMETypesDictionary = new Dictionary<string, string>
																			 {
																				 {"ai", "application/postscript"},
																				 {"aif", "audio/x-aiff"},
																				 {"aifc", "audio/x-aiff"},
																				 {"aiff", "audio/x-aiff"},
																				 {"asc", "text/plain"},
																				 {"atom", "application/atom+xml"},
																				 {"au", "audio/basic"},
																				 {"avi", "video/x-msvideo"},
																				 {"bcpio", "application/x-bcpio"},
																				 {"bin", "application/octet-stream"},
																				 {"bmp", "image/bmp"},
																				 {"cdf", "application/x-netcdf"},
																				 {"cgm", "image/cgm"},
																				 {"class", "application/octet-stream"},
																				 {"cpio", "application/x-cpio"},
																				 {"cpt", "application/mac-compactpro"},
																				 {"csh", "application/x-csh"},
																				 {"css", "text/css"},
																				 {"dcr", "application/x-director"},
																				 {"dif", "video/x-dv"},
																				 {"dir", "application/x-director"},
																				 {"djv", "image/vnd.djvu"},
																				 {"djvu", "image/vnd.djvu"},
																				 {"dll", "application/octet-stream"},
																				 {"dmg", "application/octet-stream"},
																				 {"dms", "application/octet-stream"},
																				 {"doc", "application/msword"},
																				 {"dtd", "application/xml-dtd"},
																				 {"dv", "video/x-dv"},
																				 {"dvi", "application/x-dvi"},
																				 {"dxr", "application/x-director"},
																				 {"eps", "application/postscript"},
																				 {"etx", "text/x-setext"},
																				 {"exe", "application/octet-stream"},
																				 {"ez", "application/andrew-inset"},
																				 {"gif", "image/gif"},
																				 {"gram", "application/srgs"},
																				 {"grxml", "application/srgs+xml"},
																				 {"gtar", "application/x-gtar"},
																				 {"hdf", "application/x-hdf"},
																				 {"hqx", "application/mac-binhex40"},
																				 {"htm", "text/html"},
																				 {"html", "text/html"},
																				 {"ice", "x-conference/x-cooltalk"},
																				 {"ico", "image/x-icon"},
																				 {"ics", "text/calendar"},
																				 {"ief", "image/ief"},
																				 {"ifb", "text/calendar"},
																				 {"iges", "model/iges"},
																				 {"igs", "model/iges"},
																				 {
																					 "jnlp", "application/x-java-jnlp-file"
																					 },
																				 {"jp2", "image/jp2"},
																				 {"jpe", "image/jpeg"},
																				 {"jpeg", "image/jpeg"},
																				 {"jpg", "image/jpeg"},
																				 {"js", "application/x-javascript"},
																				 {"kar", "audio/midi"},
																				 {"latex", "application/x-latex"},
																				 {"lha", "application/octet-stream"},
																				 {"lzh", "application/octet-stream"},
																				 {"m3u", "audio/x-mpegurl"},
																				 {"m4a", "audio/mp4a-latm"},
																				 {"m4b", "audio/mp4a-latm"},
																				 {"m4p", "audio/mp4a-latm"},
																				 {"m4u", "video/vnd.mpegurl"},
																				 {"m4v", "video/x-m4v"},
																				 {"mac", "image/x-macpaint"},
																				 {"man", "application/x-troff-man"},
																				 {"mathml", "application/mathml+xml"},
																				 {"me", "application/x-troff-me"},
																				 {"mesh", "model/mesh"},
																				 {"mid", "audio/midi"},
																				 {"midi", "audio/midi"},
																				 {"mif", "application/vnd.mif"},
																				 {"mov", "video/quicktime"},
																				 {"movie", "video/x-sgi-movie"},
																				 {"mp2", "audio/mpeg"},
																				 {"mp3", "audio/mpeg"},
																				 {"mp4", "video/mp4"},
																				 {"mpe", "video/mpeg"},
																				 {"mpeg", "video/mpeg"},
																				 {"mpg", "video/mpeg"},
																				 {"mpga", "audio/mpeg"},
																				 {"ms", "application/x-troff-ms"},
																				 {"msh", "model/mesh"},
																				 {"mxu", "video/vnd.mpegurl"},
																				 {"nc", "application/x-netcdf"},
																				 {"oda", "application/oda"},
																				 {"ogg", "application/ogg"},
																				 {"pbm", "image/x-portable-bitmap"},
																				 {"pct", "image/pict"},
																				 {"pdb", "chemical/x-pdb"},
																				 {"pdf", "application/pdf"},
																				 {"pgm", "image/x-portable-graymap"},
																				 {"pgn", "application/x-chess-pgn"},
																				 {"pic", "image/pict"},
																				 {"pict", "image/pict"},
																				 {"png", "image/png"},
																				 {"pnm", "image/x-portable-anymap"},
																				 {"pnt", "image/x-macpaint"},
																				 {"pntg", "image/x-macpaint"},
																				 {"ppm", "image/x-portable-pixmap"},
																				 {
																					 "ppt", "application/vnd.ms-powerpoint"
																					 },
																				 {"ps", "application/postscript"},
																				 {"qt", "video/quicktime"},
																				 {"qti", "image/x-quicktime"},
																				 {"qtif", "image/x-quicktime"},
																				 {"ra", "audio/x-pn-realaudio"},
																				 {"ram", "audio/x-pn-realaudio"},
																				 {"ras", "image/x-cmu-raster"},
																				 {"rdf", "application/rdf+xml"},
																				 {"rgb", "image/x-rgb"},
																				 {"rm", "application/vnd.rn-realmedia"},
																				 {"roff", "application/x-troff"},
																				 {"rtf", "text/rtf"},
																				 {"rtx", "text/richtext"},
																				 {"sgm", "text/sgml"},
																				 {"sgml", "text/sgml"},
																				 {"sh", "application/x-sh"},
																				 {"shar", "application/x-shar"},
																				 {"silo", "model/mesh"},
																				 {"sit", "application/x-stuffit"},
																				 {"skd", "application/x-koan"},
																				 {"skm", "application/x-koan"},
																				 {"skp", "application/x-koan"},
																				 {"skt", "application/x-koan"},
																				 {"smi", "application/smil"},
																				 {"smil", "application/smil"},
																				 {"snd", "audio/basic"},
																				 {"so", "application/octet-stream"},
																				 {"spl", "application/x-futuresplash"},
																				 {"src", "application/x-wais-source"},
																				 {"sv4cpio", "application/x-sv4cpio"},
																				 {"sv4crc", "application/x-sv4crc"},
																				 {"svg", "image/svg+xml"},
																				 {
																					 "swf", "application/x-shockwave-flash"
																					 },
																				 {"t", "application/x-troff"},
																				 {"tar", "application/x-tar"},
																				 {"tcl", "application/x-tcl"},
																				 {"tex", "application/x-tex"},
																				 {"texi", "application/x-texinfo"},
																				 {"texinfo", "application/x-texinfo"},
																				 {"tif", "image/tiff"},
																				 {"tiff", "image/tiff"},
																				 {"tr", "application/x-troff"},
																				 {"tsv", "text/tab-separated-values"},
																				 {"txt", "text/plain"},
																				 {"ustar", "application/x-ustar"},
																				 {"vcd", "application/x-cdlink"},
																				 {"vrml", "model/vrml"},
																				 {"vxml", "application/voicexml+xml"},
																				 {"wav", "audio/x-wav"},
																				 {"wbmp", "image/vnd.wap.wbmp"},
																				 {"wbmxl", "application/vnd.wap.wbxml"},
																				 {"wml", "text/vnd.wap.wml"},
																				 {"wmlc", "application/vnd.wap.wmlc"},
																				 {"wmls", "text/vnd.wap.wmlscript"},
																				 {
																					 "wmlsc",
																					 "application/vnd.wap.wmlscriptc"
																					 },
																				 {"wrl", "model/vrml"},
																				 {"xbm", "image/x-xbitmap"},
																				 {"xht", "application/xhtml+xml"},
																				 {"xhtml", "application/xhtml+xml"},
																				 {"xls", "application/vnd.ms-excel"},
																				 {"xml", "application/xml"},
																				 {"xpm", "image/x-xpixmap"},
																				 {"xsl", "application/xml"},
																				 {"xslt", "application/xslt+xml"},
																				 {
																					 "xul",
																					 "application/vnd.mozilla.xul+xml"
																					 },
																				 {"xwd", "image/x-xwindowdump"},
																				 {"xyz", "chemical/x-xyz"},
																				 {"zip", "application/zip"},
																				 {"docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document"},
																				 {"xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
																				 {"pptx", "application/vnd.openxmlformats-officedocument.presentationml.presentation"},
																			 };
	public static string GetMIMEType(string fileName)
	{
	  if (MIMETypesDictionary.ContainsKey(Path.GetExtension(fileName).Remove(0, 1)))
	  {
		return MIMETypesDictionary[Path.GetExtension(fileName).Remove(0, 1)];
	  }
	  return "unknown/unknown";
	}

	public static int GetDifferenceInYears(DateTime startDate, DateTime endDate)
	{
	  //Excel documentation says "COMPLETE calendar years in between dates"
	  int years = endDate.Year - startDate.Year;

	  if (startDate.Month == endDate.Month &&// if the start month and the end month are the same
			  endDate.Day > startDate.Day)// BUT the end day is less than the start day
	  {
		years++;
	  }
	  else if (endDate.Month > startDate.Month)// if the end month is less than the start month
	  {
		years++;
	  }

	  return years;
	}

	public static decimal Weekdays(DateTime firstDay, DateTime lastDay)
	{
	  firstDay = firstDay.Date;
	  lastDay = lastDay.Date;
	  if (firstDay > lastDay)
		throw new ArgumentException("Incorrect last day " + lastDay);

	  TimeSpan span = lastDay - firstDay;
	  int businessDays = span.Days + 1;
	  int fullWeekCount = businessDays / 7;
	  // find out if there are weekends during the time exceeding the full weeks
	  if (businessDays > fullWeekCount * 7)
	  {
		// we are here to find out if there is a 1-day or 2-days weekend
		// in the time interval remaining after subtracting the complete weeks
		int firstDayOfWeek = firstDay.DayOfWeek == DayOfWeek.Sunday ? 7 : (int)firstDay.DayOfWeek;
		int lastDayOfWeek = lastDay.DayOfWeek == DayOfWeek.Sunday ? 7 : (int)lastDay.DayOfWeek;
		if (lastDayOfWeek < firstDayOfWeek)
		  lastDayOfWeek += 7;
		if (firstDayOfWeek <= 6)
		{
		  if (lastDayOfWeek >= 7)// Both Saturday and Sunday are in the remaining time interval
			businessDays -= 1;
		  //else if (lastDayOfWeek >= 6)// Only Saturday is in the remaining time interval
		  //	businessDays -= 1;
		}
		else if (firstDayOfWeek <= 7 && lastDayOfWeek >= 7)// Only Sunday is in the remaining time interval
		  businessDays -= 1;
	  }

	  // subtract the weekends during the full weeks in the interval
	  businessDays -= fullWeekCount + fullWeekCount;

	  return Convert.ToDecimal(businessDays);
	}

	public static string ExtractText(string html)
	{
	  try
	  {
		Regex reg = new Regex("<[^>]+>", RegexOptions.IgnoreCase);
		string s = reg.Replace(html, " ");
		s = HttpUtility.HtmlDecode(s);
		return s;
	  }
	  catch
	  {
		return html;
	  }
	}


	//convert image to bytearray
	public static byte[] imgToByteArray(Image img)
	{
	  using (MemoryStream mStream = new MemoryStream())
	  {
		img.Save(mStream, img.RawFormat);
		return mStream.ToArray();
	  }
	}
	//convert bytearray to image
	public static Image byteArrayToImage(byte[] byteArrayIn)
	{
	  using (MemoryStream mStream = new MemoryStream(byteArrayIn))
	  {
		return Image.FromStream(mStream);
	  }
	}
	//another easy way to convert image to bytearray
	public static byte[] imgToByteConverter(Image inImg)
	{
	  ImageConverter imgCon = new ImageConverter();
	  return (byte[])imgCon.ConvertTo(inImg, typeof(byte[]));
	}

	public static DateTime GetPakistanLocalTime(string destinationTimeZoneId = "Pakistan Standard Time")
	{
	  try
	  {

		DateTime currentUtcTime = DateTime.UtcNow;
		DateTime localTimeInPakistan = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(currentUtcTime, destinationTimeZoneId);

		return localTimeInPakistan;

	  }
	  catch
	  {
		return DateTime.UtcNow;
	  }
	}

	public static DateTime FromUnixTime(double unixTime)
	{
	  try
	  {
		DateTime unixStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
		long unixTimeStampInTicks = (long)(unixTime * TimeSpan.TicksPerSecond);
		return new DateTime(unixStart.Ticks + unixTimeStampInTicks, DateTimeKind.Utc);			
	  }
	  catch
	  {
		return DateTime.UtcNow;
	  }  
	 
	}

	public static long ToUnixTime(DateTime dateTime)
	{
	  try
	  {		
		var dateTimeOffset = new DateTimeOffset(dateTime);
		return dateTimeOffset.ToUnixTimeSeconds();
		
	  }
	  catch
	  {
		dateTime = DateTime.Now;
		var dateTimeOffset = new DateTimeOffset(dateTime);
		return dateTimeOffset.ToUnixTimeSeconds();
	  }

	}

	public static decimal CalculateMonthlyReturn(DateTime startDate, DateTime endDate, decimal businessValue = 0)
		{
			try
			{
				if (businessValue <= 0) return 0;

				int months = MonthDifference(startDate, endDate);

				if (months <= 0) return businessValue;

				return (businessValue / months);

			}
			catch
			{
				return 0;
			}

		}

	}
}