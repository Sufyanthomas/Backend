using System;
using System.Net;
using System.Net.Mail;
using System.Net.Security;

namespace Pharmazer.Base.Tools
{
	public class SMTPConf
	{
		public string Email { get; set; }
		public string Password { get; set; }
		public string Host { get; set; }
		public int Port { get; set; }
		public bool SSL { get; set; }
	}

	public class EMail
	{
		public static string SendMail(string subject, string messageBody, string receiverMail, string receivers = "")
		{
					bool isEnableNotifications = Convert.ToBoolean(Tools.Utility.GetAppSetting("IsEnableNotification"));
					if (!isEnableNotifications) return "Notifications Not Enabled";

					try
					{

						MailMessage message = new MailMessage();
						
						message.To.Add(receiverMail);
					
						message.From = new MailAddress(System.Configuration.ConfigurationManager.AppSettings["SmtpUserName"], System.Configuration.ConfigurationManager.AppSettings["FromName"]);


						message.IsBodyHtml = true;
						message.Headers.Add("charset", "iso-8859-9");
						message.Subject = subject;
						message.Body = messageBody;

						//System.Net.NetworkCredential basicAuthenticationInfo = new System.Net.NetworkCredential(System.Configuration.ConfigurationManager.AppSettings["SmtpUserName"], System.Configuration.ConfigurationManager.AppSettings["SmtpPassword"]);
						//System.Net.Mail.SmtpClient MailClient = new System.Net.Mail.SmtpClient();
						//MailClient.Host = System.Configuration.ConfigurationManager.AppSettings["SmtpHost"];
						//MailClient.UseDefaultCredentials = false;
						//MailClient.Credentials = basicAuthenticationInfo;
						//MailClient.Port = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SmtpPort"]);
						//MailClient.EnableSsl = bool.Parse(System.Configuration.ConfigurationManager.AppSettings["SmtpSSL"]);
						//MailClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
						//MailClient.Timeout = 10000;
						//MailClient.Send(message);

						var smtp = new SmtpClient
						{
							Host = "smtp.gmail.com",
							Port = 587,
							EnableSsl = true,
							DeliveryMethod = SmtpDeliveryMethod.Network,
							UseDefaultCredentials = false,
							Credentials = new NetworkCredential(System.Configuration.ConfigurationManager.AppSettings["SmtpUserName"], System.Configuration.ConfigurationManager.AppSettings["SmtpPassword"]),
							Timeout = 100000
						   
						};

						smtp.Send(message);


                        return "Sent";
					}
					catch (Exception ex)
					{
                        string errorMessage = string.Empty;
                        if (!string.IsNullOrWhiteSpace(ex.Message))
                        {
                            errorMessage = ex.Message;
                        }

                        if (ex.InnerException != null && !string.IsNullOrWhiteSpace(ex.InnerException.Message))
                        {
                            errorMessage += ex.InnerException.Message;
                        }

                        return errorMessage;
					}
				}
				 
			
		public static bool SendMail(string subject, string messageBody, string senderAddress, string senderDisplayName, string receivers, SMTPConf smtpConf)
		{
					bool isEnableNotifications = Convert.ToBoolean(Tools.Utility.GetAppSetting("IsEnableNotification"));
					if (!isEnableNotifications) return false;


			try
			{
				System.Net.Mail.MailAddress sender = new System.Net.Mail.MailAddress(senderAddress, senderDisplayName);
				System.Net.Mail.MailAddress receiver = new System.Net.Mail.MailAddress(receivers, "");
				System.Net.Mail.MailMessage email = new System.Net.Mail.MailMessage(sender, receiver);

				email.IsBodyHtml = true;
				email.Headers.Add("charset", "iso-8859-9");
				email.Subject = subject;
				email.Body = messageBody;


								//System.Net.NetworkCredential basicAuthenticationInfo = new System.Net.NetworkCredential(smtpConf.Email, smtpConf.Password);
								//System.Net.Mail.SmtpClient smtpClient = new System.Net.Mail.SmtpClient();
								//smtpClient.Host = smtpConf.Host;
								//smtpClient.UseDefaultCredentials = false;
								//smtpClient.Credentials = basicAuthenticationInfo;
								//smtpClient.Port = smtpConf.Port;
								//smtpClient.EnableSsl = smtpConf.SSL;
								//smtpClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
								//smtpClient.Timeout = 10000;
								//smtpClient.Send(email);

								var smtp = new SmtpClient
								{
									Host = "smtp.gmail.com",
									Port = 587,
									EnableSsl = true,
									DeliveryMethod = SmtpDeliveryMethod.Network,
									UseDefaultCredentials = false,
									Credentials = new NetworkCredential(System.Configuration.ConfigurationManager.AppSettings["SmtpUserName"], System.Configuration.ConfigurationManager.AppSettings["SmtpPassword"]),
									Timeout = 100000
								};

								smtp.Send(email);


				return true;
			}
			catch
			{
				return false;
			}
		}

				public static int SendMail(string subject, string messageBody, string senderMail, string senderName, string receiverMail, bool IsMultipleReceivers, string CcReceipients = "")
				{
					bool isEnableNotifications = Convert.ToBoolean(Tools.Utility.GetAppSetting("IsEnableNotification"));
					if (!isEnableNotifications) return 0;

					try
					{

						MailMessage message = new MailMessage();
						if (IsMultipleReceivers == true)
						{
							foreach (var emailId in receiverMail.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
							{
								message.To.Add(emailId);
							}
						}
						else
						{
							message.To.Add(receiverMail);
						}

						if (!string.IsNullOrEmpty(CcReceipients))
						{
							foreach (var ccEmail in CcReceipients.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
							{
								message.CC.Add(ccEmail);
							}
						}

						message.From = new MailAddress(System.Configuration.ConfigurationManager.AppSettings["SmtpUserName"], System.Configuration.ConfigurationManager.AppSettings["FromName"]);

						message.IsBodyHtml = true;
						message.Headers.Add("charset", "iso-8859-9");
						message.Subject = subject;
						message.Body = messageBody;
					
						//System.Net.NetworkCredential basicAuthenticationInfo = new System.Net.NetworkCredential(System.Configuration.ConfigurationManager.AppSettings["SmtpUserName"], System.Configuration.ConfigurationManager.AppSettings["SmtpPassword"]);
						//System.Net.Mail.SmtpClient MailClient = new System.Net.Mail.SmtpClient();
						//MailClient.Host = System.Configuration.ConfigurationManager.AppSettings["SmtpHost"];
						//MailClient.UseDefaultCredentials = false;
						//MailClient.Credentials = basicAuthenticationInfo;
						//MailClient.Port = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SmtpPort"]);
						//MailClient.EnableSsl = bool.Parse(System.Configuration.ConfigurationManager.AppSettings["SmtpSSL"]);
						//MailClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
						//MailClient.Timeout = 10000;
						//MailClient.Send(message);

						var smtp = new SmtpClient
						{
							Host = "smtp.gmail.com",
							Port = 587,
							EnableSsl = true,
							DeliveryMethod = SmtpDeliveryMethod.Network,
							UseDefaultCredentials = false,
							Credentials = new NetworkCredential(System.Configuration.ConfigurationManager.AppSettings["SmtpUserName"], System.Configuration.ConfigurationManager.AppSettings["SmtpPassword"]),
							Timeout = 100000
						};

						smtp.Send(message);


						return 1;
					}
					catch
					{
						return -1;
					}
				}
			
	}
}