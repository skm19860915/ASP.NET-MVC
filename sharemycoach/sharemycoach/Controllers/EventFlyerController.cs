using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web.Mvc;

namespace sharemycoach.Controllers
{
    public class EventFlyerController : BaseController
    {
        public ActionResult Index(string id)
        {
            GetEventFlyerInformation(id, null);
            return View();
        }

        [HttpPost]
        public ActionResult Index()
        {
            var message = "Failed sent email!";

            var targetParam = string.Format("{0}", Request.Form["target_param"]);
            var outgoingUserName = string.Format("{0}", Request.Form["outgoing_username"]);
            var outgoingServerName = string.Format("{0}", Request.Form["outgoing_servername"]);
            var outgoingPassword = string.Format("{0}", Request.Form["outgoing_password"]);
            var outgoingServerPort = string.Format("{0}", Request.Form["outgoing_serverport"]);
            var primaryEmail = string.Format("{0}", Request.Form["primary_email"]);
            var secondaryEmail = string.Format("{0}", Request.Form["secondary_email"]);
            var dbaName = string.Format("{0}", Request.Form["dba_name"]);
            var mobilePhone = string.Format("{0}", Request.Form["mobile_phone"]);
            var number = string.Format("{0}", Request.Form["number"]);
            var userName = string.Format("{0}", Request.Form["user_name"]);
            var userPrimaryPhone = string.Format("{0}", Request.Form["user_primary_phone"]);
            var userMobilePhone = string.Format("{0}", Request.Form["user_mobile_phone"]);
            var userEmail = string.Format("{0}", Request.Form["user_email"]);
            var description = string.Format("{0}", Request.Form["description"]);
            var targetDir = string.Format("{0}", Request.Form["target_dir"]);

            var success1 = SendLocationEmail(outgoingUserName, outgoingServerName, outgoingServerPort, outgoingPassword, secondaryEmail, number, userName, userPrimaryPhone, userMobilePhone, userEmail, description, dbaName, targetDir);
            if (success1)
            {
                var success2 = SendCustomerEmail(userName, outgoingUserName, outgoingServerName, outgoingServerPort, outgoingPassword, primaryEmail, userEmail, mobilePhone, dbaName);
                if (success2)
                    message = "Success sent email!";
            }
            GetEventFlyerInformation(targetParam, message);

            return View("Index");
        }

        private bool SendLocationEmail(string userName, string serverName, string serverPort, string password, string email, string number, string name, string primaryPhone, string userPhone, string userEmail, string description, string dbaName, string targetDir)
        {
            try
            {
                var client = new SmtpClient(serverName);
                client.UseDefaultCredentials = false;
                var basicAuthenticationInfo = new NetworkCredential(userName, password);
                client.Credentials = basicAuthenticationInfo;
                client.EnableSsl = true;
                client.Port = 587;

                var from = new MailAddress(userEmail);
                var to = new MailAddress(email);
                var mail = new MailMessage(from, to);
                mail.Subject = targetDir + " Event Flyer Submission for " + dbaName;
                mail.SubjectEncoding = System.Text.Encoding.UTF8;
                mail.Body = "Event : " + targetDir + "<br />" + "Requested Coach Number : " + number + "<br />" + "Web User Name : " + userName + "<br />" 
                            + "Web User Primary Phone : " + primaryPhone + "<br />" + "Web User Mobile Phone : " + userPhone + "<br />" 
                            + "Email Address : " + userEmail + "<br />" + "Description : " + description + "<br />";
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.IsBodyHtml = true;

                ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                { return true; };

                client.Send(mail);
                return true;
            }
            catch (SmtpException ex)
            {
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private bool SendCustomerEmail(string name, string userName, string serverName, string serverPort, string password, string primaryEmail, string userEmail, string mobilePhone, string dbaName)
        {
            if (string.IsNullOrEmpty(userName))
                return false;
                
            if (string.IsNullOrEmpty(serverName))
                return false;
                
            if (string.IsNullOrEmpty(serverPort))
                return false;
                
            if (string.IsNullOrEmpty(password))
                return false;
                
            if (string.IsNullOrEmpty(primaryEmail))
                return false;
                
            if (string.IsNullOrEmpty(userEmail))
                return false;
                
            try
            {
                var client = new SmtpClient(serverName);
                client.UseDefaultCredentials = false;
                var basicAuthenticationInfo = new NetworkCredential(userName, password);
                client.Credentials = basicAuthenticationInfo;
                client.EnableSsl = true;
                client.Port = 587;

                var from = new MailAddress(primaryEmail);
                var to = new MailAddress(userEmail);
                var mail = new MailMessage(from, to);
                mail.Subject = "Thank you for your submission";
                mail.SubjectEncoding = System.Text.Encoding.UTF8;
                mail.Body = "Dear " + name + "<br /><br />Thank you for your submission.<br />Please call if you have any questions.<br /><br />" + dbaName + "<br />" + mobilePhone;
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.IsBodyHtml = true;

                ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                { return true; };

                client.Send(mail);
                return true;
            }
            catch (SmtpException ex)
            {
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private void GetEventFlyerInformation(string id, string msg)
        {
            ViewBag.Title = "Event Flyer | " + Properties.Resources.SHARE_MY_COACH;
            ViewBag.Msg = msg;

            var dir = GetDirectoryName(id);
            ViewBag.TargetDir = dir;
            ViewBag.TargetParam = id;

            var eventFlyer = _wc.GetEventFlyers(id, _token);
            if (eventFlyer == null || eventFlyer.Count() < 1)
            {
                ViewBag.EventFlyer = null;
                ViewBag.MainEventInfo = null;
                ViewBag.ClassName = null;
                ViewBag.WebRegionalName = null;
                ViewBag.Delivered = null;
                ViewBag.Delivered2 = null;
                ViewBag.Delivered3 = null;
                ViewBag.PickedUp = null;
                return;
            }

            ViewBag.EventFlyer = eventFlyer;
            ViewBag.MainEventInfo = eventFlyer.FirstOrDefault();
            ViewBag.ClassName = eventFlyer.FirstOrDefault().ClassName;
            ViewBag.WebRegionalName = eventFlyer.FirstOrDefault().WebRegionalName;

            var webEventPriceForDelivered = eventFlyer.FirstOrDefault().WebEventPriceForDelivered;
            if (webEventPriceForDelivered == null || webEventPriceForDelivered == 0)
                ViewBag.Delivered = null;
            else
                ViewBag.Delivered = string.Format("{0:C0}", webEventPriceForDelivered);

            var webEventPriceForDelivered2 = eventFlyer.FirstOrDefault().WebEventPriceForDelivered2;
            if (webEventPriceForDelivered2 == null || webEventPriceForDelivered2 == 0)
                ViewBag.Delivered2 = null;
            else
                ViewBag.Delivered2 = string.Format("{0:C0}", webEventPriceForDelivered2);

            var webEventPriceForDelivered3 = eventFlyer.FirstOrDefault().WebEventPriceForDelivered3;
            if (webEventPriceForDelivered3 == null || webEventPriceForDelivered3 == 0)
                ViewBag.Delivered3 = null;
            else
                ViewBag.Delivered3 = string.Format("{0:C0}", webEventPriceForDelivered3);

            var webEventPriceForPickedUp = eventFlyer.FirstOrDefault().WebEventPriceForPickedUp;
            if (webEventPriceForPickedUp == null || webEventPriceForPickedUp == 0)
                ViewBag.PickedUp = null;
            else
                ViewBag.Pickedup = string.Format("{0:C0}", webEventPriceForPickedUp);
        }

        private string GetDirectoryName(string id)
        {
            var dirName = id.Substring(0, 4);
            return dirName;
        }
    }
}