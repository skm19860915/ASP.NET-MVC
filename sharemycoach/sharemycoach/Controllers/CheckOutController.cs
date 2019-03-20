using sharemycoach.Models;
using sharemycoach.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace sharemycoach.Controllers
{
    public class CheckOutController : BaseController
    {
        public ActionResult Index(string id)
        {
            VehicleDetailViewModel detailInformation = null;
            var temp = TempData["Detail"];
            if (temp == null)
                detailInformation = _wc.GetDetail(id, _token);
            else
                detailInformation = temp as VehicleDetailViewModel;

            if (detailInformation == null)
                return RedirectToAction("Index", "Error");

            ViewBag.Title = "Quote Form";

            GetCheckOutInfo(detailInformation);
            return View();
        }

        private void GetCheckOutInfo(VehicleDetailViewModel info)
        {
            ViewBag.Info = info;
            var serviceList = _wc.GetAllServiceList(info.Location ?? Guid.Empty, "sharemycoach.com", _token);
            var optionalItemList = GetSortOptionalItemList(serviceList);
            var optionalItemListInSelectedLocation = optionalItemList.Where(x => x.Location == info.Location).ToList();
            if (optionalItemListInSelectedLocation == null || optionalItemListInSelectedLocation.Count() < 1)
                ViewBag.OptionalItemList = null;
            else
                ViewBag.OptionalItemList = optionalItemListInSelectedLocation;

            ViewBag.InsuranceCompanies = serviceList.LocationInsuranceCompanyList;
            ViewBag.LeadSources = serviceList.LeadSourceList;
        }

        [HttpPost]
        public async Task<ActionResult> Index()
        {
            var vehicleKeyFromWeb = string.Format("{0}", Request.Form["vehicle_id"]);
            var vehicleKeyFromMobile = string.Format("{0}", Request.Form["small_vehicle_id"]);

            if (!string.IsNullOrEmpty(vehicleKeyFromWeb))
            {
                var vehicleOid = string.Format("{0}", Request.Form["vehicle_oid"]);
                var vehicleName = string.Format("{0}", Request.Form["vehicle_name"]);
                var locationOid = string.Format("{0}", Request.Form["location"]);
                var locationName = string.Format("{0}", Request.Form["location_name"]);
                var organizationOid = string.Format("{0}", Request.Form["organization"]);
                var organizationName = string.Format("{0}", Request.Form["organization_name"]);
                var classOid = string.Format("{0}", Request.Form["class_oid"]);
                var className = string.Format("{0}", Request.Form["vehicle_class"]);
                var outgoingUserName = string.Format("{0}", Request.Form["outgoing_username"]);
                var outgoingServerName = string.Format("{0}", Request.Form["outgoing_servername"]);
                var outgoingServerPort = string.Format("{0}", Request.Form["outgoing_serverport"]);
                var outgoingPassword = string.Format("{0}", Request.Form["outgoing_password"]);
                var emailAddress = string.Format("{0}", Request.Form["email_address"]);
                var webQuoteEmailAddress = string.Format("{0}", Request.Form["web_quote_email_address"]);
                var phoneNumber = string.Format("{0}", Request.Form["phone_number"]);
                var dbaName = string.Format("{0}", Request.Form["dba_name"]);
                var webRegionalName = string.Format("{0}", Request.Form["web_regional_name"]);
                var locationState = string.Format("{0}", Request.Form["location_state"]);
                var locationCity = string.Format("{0}", Request.Form["location_city"]);
                var destination = string.Format("{0}", Request.Form["destination"]);
                var distance = string.Format("{0}", Request.Form["trip"]);
                var departureDate = string.Format("{0}", Request.Form["departure_date"]);
                var returnDate = string.Format("{0}", Request.Form["return_date"]);
                var firstName = string.Format("{0}", Request.Form["first_name"]);
                var lastName = string.Format("{0}", Request.Form["last_name"]);
                var country = string.Format("{0}", Request.Form["country"]);
                var address = string.Format("{0}", Request.Form["real_address"]);
                var city = string.Format("{0}", Request.Form["city"]);
                var state = string.Format("{0}", Request.Form["state"]);
                var zip = string.Format("{0}", Request.Form["zip"]);
                var cellPhone = string.Format("{0}", Request.Form["cell_phone"]);
                var secondaryPhone = string.Format("{0}", Request.Form["secondary_phone"]);
                var email = string.Format("{0}", Request.Form["email"]);
                var leadSource = string.Format("{0}", Request.Form["lead_source"]);
                var adults = string.Format("{0}", Request.Form["adults"]);
                var children = string.Format("{0}", Request.Form["children"]);
                var insuranceCompanies = string.Format("{0}", Request.Form["insurancecompanies"]);
                var equipments = string.Format("{0}", Request.Form["equipments"]);
                var fees = string.Format("{0}", Request.Form["fees"]);
                var instructions = string.Format("{0}", Request.Form["instructions"]);

                var model = new QuoteModel()
                {
                    Oid = Guid.NewGuid(),
                    Destination = destination,
                    Distance = distance,
                    LeaveOn = departureDate,
                    ReturnOn = returnDate,
                    FirstName = firstName,
                    LastName = lastName,
                    Name = firstName + " " + lastName,
                    WebUserSelectedCountry = country,
                    Address = address,
                    City = city,
                    State = state,
                    Zip = zip,
                    MobilePhonePrimary = cellPhone,
                    HomePhoneSecondary = secondaryPhone,
                    EmailAddress = email,
                    LeadSourceOid = GetGuidValueFromData(leadSource),
                    LeadSourceValue = GetSpecialNameFromData(leadSource),
                    Adults = adults,
                    Children = children,
                    LocationInsuranceCompanyOidsAndNames = insuranceCompanies,
                    EquipmentTypeOids = GetGuidsFromData(equipments),
                    FeeOids = GetGuidsFromData(fees),
                    EquipmentTypeNames = GetNamesFromData(equipments),
                    FeeNames = GetNamesFromData(fees),
                    WebUserComments = instructions,
                    // hidden datas
                    VehicleId = vehicleOid,
                    VehicleName = vehicleName,
                    ClassOid = classOid,
                    WebUserSelectedClass = className,
                    Location = locationOid,
                    LocationName = locationName,
                    Organization = organizationOid,
                    OrganizationName = organizationName,
                };

                var client = new HttpClient();
                var rmxEndPoint = System.Configuration.ConfigurationManager.AppSettings["RMXWebService"] + "quote";
                var rmxResponse = await client.PostAsJsonAsync(rmxEndPoint, model);
                if (rmxResponse.IsSuccessStatusCode)
                {
                    var statusOfRackspaceQuote = 1;
                    if (!locationOid.Equals("8947F2BE-17FE-4E61-8655-31DD8AEE0377", StringComparison.InvariantCultureIgnoreCase))
                    {
                        var rackpaceEndPoint = System.Configuration.ConfigurationManager.AppSettings["RackSpaceWebService"] + "webrental";
                        var rackspaceResponse = await client.PostAsJsonAsync(rackpaceEndPoint, model);
                        var resultOfRackspace = await rackspaceResponse.Content.ReadAsStringAsync();
                        try
                        {
                            statusOfRackspaceQuote = int.Parse(resultOfRackspace);
                        }
                        catch
                        {
                            statusOfRackspaceQuote = 1;
                        }
                    }

                    if (statusOfRackspaceQuote == 1)
                    {
                        // send email to renter
                        var success = SendEmailToWebUser(firstName, outgoingUserName, outgoingServerName, outgoingServerPort, outgoingPassword, emailAddress, email, phoneNumber, locationName, organizationName);
                        if (success)
                        {
                            if (!string.IsNullOrEmpty(webQuoteEmailAddress))
                              SendEmailToWebQuoteEmailAddress(model, outgoingUserName, outgoingServerName, outgoingServerPort, outgoingPassword, emailAddress, webQuoteEmailAddress, equipments, fees, leadSource);

                            var infos = new List<string>();
                            infos.Add(webRegionalName);
                            infos.Add(locationCity);
                            infos.Add(locationState);
                            infos.Add(phoneNumber);
                            infos.Add(dbaName);

                            TempData["Infos"] = infos;

                            return RedirectToAction("Success", "QuoteResult");
                        }

                        return RedirectToAction("Fail", "QuoteResult");
                    }
                }
            }
            else if (!string.IsNullOrEmpty(vehicleKeyFromMobile))
            {
                var vehicleOid = string.Format("{0}", Request.Form["small_vehicle_oid"]);
                var vehicleName = string.Format("{0}", Request.Form["small_vehicle_name"]);
                var locationOid = string.Format("{0}", Request.Form["small_location"]);
                var locationName = string.Format("{0}", Request.Form["small_location_name"]);
                var organizationOid = string.Format("{0}", Request.Form["small_organization"]);
                var organizationName = string.Format("{0}", Request.Form["small_organization_name"]);
                var classOid = string.Format("{0}", Request.Form["small_class_oid"]);
                var className = string.Format("{0}", Request.Form["small_vehicle_class"]);
                var outgoingUserName = string.Format("{0}", Request.Form["small_outgoing_username"]);
                var outgoingServerName = string.Format("{0}", Request.Form["small_outgoing_servername"]);
                var outgoingServerPort = string.Format("{0}", Request.Form["small_outgoing_serverport"]);
                var outgoingPassword = string.Format("{0}", Request.Form["small_outgoing_password"]);
                var emailAddress = string.Format("{0}", Request.Form["small_email_address"]);
                var webQuoteEmailAddress = string.Format("{0}", Request.Form["small_web_quote_email_address"]);
                var phoneNumber = string.Format("{0}", Request.Form["small_phone_number"]);

                var destination = string.Format("{0}", Request.Form["small_destination"]);
                var distance = string.Format("{0}", Request.Form["small_trip"]);
                var departureDate = string.Format("{0}", Request.Form["small_departure_date"]);
                var returnDate = string.Format("{0}", Request.Form["small_return_date"]);
                var leadSource = string.Format("{0}", Request.Form["small_lead_source"]);
                var firstName = string.Format("{0}", Request.Form["small_first_name"]);
                var lastName = string.Format("{0}", Request.Form["small_last_name"]);
                var country = string.Format("{0}", Request.Form["small_country"]);
                var address = string.Format("{0}", Request.Form["small_address"]);
                var city = string.Format("{0}", Request.Form["small_city"]);
                var state = string.Format("{0}", Request.Form["small_state"]);
                var zip = string.Format("{0}", Request.Form["small_zip"]);
                var cellPhone = string.Format("{0}", Request.Form["small_cell_phone"]);
                var secondaryPhone = string.Format("{0}", Request.Form["small_secondary_phone"]);
                var email = string.Format("{0}", Request.Form["small_email"]);
                var adults = string.Format("{0}", Request.Form["small_adults"]);
                var children = string.Format("{0}", Request.Form["small_children"]);
                var insuranceCompanies = string.Format("{0}", Request.Form["small_insurancecompanies"]);
                var equipments = string.Format("{0}", Request.Form["small_equipments"]);
                var fees = string.Format("{0}", Request.Form["small_fees"]);

                var model = new QuoteModel()
                {
                    Oid = Guid.NewGuid(),
                    Destination = destination,
                    Distance = distance,
                    LeaveOn = departureDate,
                    ReturnOn = returnDate,
                    FirstName = firstName,
                    LastName = lastName,
                    Name = firstName + " " + lastName,
                    WebUserSelectedCountry = country,
                    Address = address,
                    City = city,
                    State = state,
                    MobilePhonePrimary = cellPhone,
                    Zip = zip,
                    HomePhoneSecondary = secondaryPhone,
                    EmailAddress = email,
                    LeadSourceOid = GetGuidValueFromData(leadSource),
                    LeadSourceValue = GetSpecialNameFromData(leadSource),
                    Adults = adults,
                    Children = children,
                    LocationInsuranceCompanyOidsAndNames = insuranceCompanies,
                    EquipmentTypeOids = GetGuidsFromData(equipments),
                    FeeOids = GetGuidsFromData(fees),
                    EquipmentTypeNames = GetNamesFromData(equipments),
                    FeeNames = GetNamesFromData(fees),
                    WebUserComments = string.Empty,
                    // hidden data
                    VehicleId = vehicleOid,
                    VehicleName = vehicleName,
                    WebUserSelectedClass = className,
                    ClassOid = classOid,
                    Location = locationOid,
                    LocationName = locationName,
                    Organization = organizationOid,
                    OrganizationName = organizationName,
                };

                var client = new HttpClient();
                var rmxEndPoint = System.Configuration.ConfigurationManager.AppSettings["RMXWebService"] + "quote";
                var rmxResponse = await client.PostAsJsonAsync(rmxEndPoint, model);
                if (rmxResponse.IsSuccessStatusCode)
                {
                    var statusOfRackspaceQuote = 1;
                    if (!locationOid.Equals("8947F2BE-17FE-4E61-8655-31DD8AEE0377", StringComparison.InvariantCultureIgnoreCase))
                    {
                        var rackpaceEndPoint = System.Configuration.ConfigurationManager.AppSettings["RackSpaceWebService"] + "webrental";
                        var rackspaceResponse = await client.PostAsJsonAsync(rackpaceEndPoint, model);
                        var resultOfRackspace = await rackspaceResponse.Content.ReadAsStringAsync();
                        statusOfRackspaceQuote = int.Parse(resultOfRackspace);
                    }

                    if (statusOfRackspaceQuote == 1)
                    {
                        // send email to renter
                        var succcess = SendEmailToMobileUser(outgoingUserName, outgoingServerName, outgoingServerPort, outgoingPassword, emailAddress, email, phoneNumber, locationName);
                        if (succcess)
                        {
                            if (!string.IsNullOrEmpty(webQuoteEmailAddress))
                                SendEmailToWebQuoteEmailAddress(model, outgoingUserName, outgoingServerName, outgoingServerPort, outgoingPassword, emailAddress, webQuoteEmailAddress, equipments, fees, leadSource);

                            ViewBag.Success = 2;
                        }
                        else
                            ViewBag.Success = 0;

                        ViewBag.Title = "RV CheckOut Success";
                        return View("success");
                    }
                }
            }

            VehicleDetailViewModel info = null;
            if (!string.IsNullOrEmpty(vehicleKeyFromWeb))
                info = _wc.GetDetail(vehicleKeyFromWeb, _token);
            if (!string.IsNullOrEmpty(vehicleKeyFromMobile))
                info = _wc.GetDetail(vehicleKeyFromMobile, _token);
            GetCheckOutInfo(info);
            return View();
        }

        private void SendEmailToWebQuoteEmailAddress(QuoteModel data, string userName, string serverName, string port, string password, string fromEmail, string toEmail, string equipments, string fees, string leadSource)
        {
            var countryNote = data.WebUserSelectedCountry.Equals("-1") == false ? "<br/>Country : " + data.WebUserSelectedCountry : "";
            var addressNote = string.IsNullOrEmpty(data.Address) == false ? "<br/>Address : " + data.Address : "";
            var cityNote = string.IsNullOrEmpty(data.City) == false ? "<br/>City : " + data.City : "";
            var stateNote = string.IsNullOrEmpty(data.State) == false ? "<br/>State : " + data.State : "";
            var zipNote = string.IsNullOrEmpty(data.Zip) == false ? "<br/><span style='color:red;'>PostalCode</span> : " + data.Zip : "";
            var secondaryPhoneNote = string.IsNullOrEmpty(data.HomePhoneSecondary) == false ? "<br/>SecondaryPhone : " + data.HomePhoneSecondary : "";
            var leadSourceName = GetSpecialNameFromData(leadSource);
            var leadSourceNote = string.IsNullOrEmpty(leadSourceName) == false ? "<br/>LeadSource : " + leadSourceName : "";
            var adultsNote = string.IsNullOrEmpty(data.Adults) == false ? "<br/>Adults : " + data.Adults : "";
            var childrenNote = string.IsNullOrEmpty(data.Children) == false ? "<br/>Children : " + data.Children : "";
            var insuranceCompany = GetSpecialNameFromData(data.LocationInsuranceCompanyOidsAndNames);
            var insuranceCompanyNote = string.IsNullOrEmpty(insuranceCompany) == false ? "<br/>InsuranceCompany : " + insuranceCompany : "";
            var specialInstructionNote = string.IsNullOrEmpty(data.WebUserComments) == false ? "<br/>SpecialInstructions : " + data.WebUserComments : "";
            var equipmentList = GetNameAndPriceFromData(equipments);
            var feeList = GetNameAndPriceFromData(fees);
            var equipmentListNote = string.IsNullOrEmpty(equipmentList) == false ? "<br/>OptionalItemsEquipment : " + equipmentList : "";
            var feeListNote = string.IsNullOrEmpty(feeList) == false ? "<br/>OptionalItemsFees : " + feeList : "";

            var subject = data.FirstName + " " + data.LastName + " requested " + data.VehicleName + " from " + data.LocationName;
            var body = "The informations are the followings : <br/><br/><span style='color:red;'>QFKW</span> : " + data.VehicleName + "<br/>Destination : " + data.Destination + 
                        "<br/>Distance : " + data.Distance + "<br/><span style='color:red;'>LeaveOn</span> : " + data.LeaveOn + 
                        "<br/><span style='color:red;'>ReturnOn</span> : " + data.ReturnOn + "<br/> Renter : " + data.FirstName + " " + data.LastName +
                        countryNote + addressNote + cityNote + stateNote + zipNote + "<br/>PrimaryPhone : " + data.MobilePhonePrimary + secondaryPhoneNote +
                        "<br/>PrimaryEmail : " + data.EmailAddress + leadSourceNote + adultsNote + childrenNote + insuranceCompanyNote + specialInstructionNote + equipmentListNote + feeListNote;

            DoEmail(serverName, userName, password, port, fromEmail, toEmail, subject, body);
        }

        private bool SendEmailToWebUser(string firstName, string userName, string serverName, string port, string password, string fromEmail, string toEmail, string phoneNumber, string locationName, string organizationName)
        {
            var subject = "Thank you for submitting a quote to " + locationName;
            var body = "Dear " + firstName + "<br /><br />This email has been automatically generated and we thank you for the request.<br /><br />" +
                "If you need immediate attention please call your local office: " + phoneNumber + 
                "<br /><br />- All of our coaches are set up with the housewares and needed necessities to operate the RV. " +
                "This is done at no charge to the renter as our way of keeping your total cost as low as possible.<br /><br />" +
                "- We offer the most, free miles and generator use and we never charge for early pick ups or late returns.<br /><br />" +
                "- Insurance covering you and the coach will normally be free or almost free with your auto policy. We can assist you in setting this up.<br /><br />- At " +
                organizationName + " we're as excited as you are about your upcoming trip and look forward to being your RV connection.<br /><br />Staff,<br /><br />" + organizationName;

            var status = DoEmail(serverName, userName, password, port, fromEmail, toEmail, subject, body);
            return status;
        }

        private bool SendEmailToMobileUser(string userName, string serverName, string port, string password, string fromEmail, string toEmail, string phoneNumber, string locationName)
        {
            var subject = "Thank you for submitting a quote to " + locationName;
            var body = "If you have any questions please contact us at " + phoneNumber;

            var status = DoEmail(serverName, userName, password, port, fromEmail, toEmail, subject, body);
            return status;
        }

        private bool DoEmail(string serverName, string userName, string password, string port, string from, string to, string subject, string body)
        {
            if (string.IsNullOrEmpty(userName))
                return false;
                
            if (string.IsNullOrEmpty(serverName))
                return false;
                
            if (string.IsNullOrEmpty(port))
                return false;
                
            if (string.IsNullOrEmpty(password))
                return false;
                
            if (string.IsNullOrEmpty(from))
                return false;
                
            if (string.IsNullOrEmpty(to))
                return false;
                
            try
            {
                var client = new SmtpClient(serverName);
                client.UseDefaultCredentials = false;
                var basicAuthenticationInfo = new NetworkCredential(userName, password);
                client.Credentials = basicAuthenticationInfo;
                client.EnableSsl = true;
                client.Port = 587; // port

                var fromEmail = new MailAddress(from, "Renter");
                var toEmail = new MailAddress(to);
                var mail = new MailMessage(fromEmail, toEmail);

                mail.Subject = subject;
                mail.SubjectEncoding = System.Text.Encoding.UTF8;
                mail.Body = body;
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.IsBodyHtml = true;

                ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                client.Send(mail);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private List<OptionalItemViewModel> GetSortOptionalItemList(ServiceListViewModel list)
        {
            var equipments = list.EquipmentTypeList;
            var fees = list.FeeList;
            var sortList = new List<OptionalItemViewModel>();

            if(equipments != null && equipments.Count() > 0)
            {
                foreach (var e in equipments)
                {
                    var equipmentRecord = new OptionalItemViewModel()
                    {
                        Oid = e.Oid,
                        Name = e.Name,
                        WebPrice = e.WebPrice,
                        Location = e.Location,
                        IsEquipmentType = true
                    };
                    sortList.Add(equipmentRecord);
                }
            }
            if(fees != null && fees.Count() > 0)
            {
                foreach (var f in fees)
                {
                    var feeRecord = new OptionalItemViewModel()
                    {
                        Oid = f.Oid,
                        Name = f.Name,
                        WebPrice = f.WebPrice,
                        Location = f.Location,
                        IsEquipmentType = false
                    };
                    sortList.Add(feeRecord);
                }
            }
            if (sortList.Count() < 1)
                return null;

            return sortList.OrderBy(x => x.Name).ToList();
        }
       
        private string GetGuidValueFromData(string value)
        {
            if (string.IsNullOrEmpty(value))
                return null;

            var tokens = value.Split(':');
            return tokens.FirstOrDefault();
        }

        private string GetSpecialNameFromData(string value)
        {
            if (string.IsNullOrEmpty(value))
                return null;

            var tokens = value.Split(':');
            return tokens.LastOrDefault();
        }

        private long GetLocationSequenceId(string id)
        {
            if (string.IsNullOrEmpty(id))
                return 0;

            var sequenceId = Convert.ToInt64(id);
            return sequenceId;
        }

        private string GetNameAndPriceFromData(string value)
        {
            string desc = null;
            if (string.IsNullOrEmpty(value))
                return null;

            var nodes = value.Split(',').ToList();
            foreach(var node in nodes)
            {
                var tokens = node.Split(':');
                var name = tokens[1];
                var price = tokens[2];
                if (string.IsNullOrEmpty(price))
                    desc += name + ", ";
                else
                    desc += name + " / " + price + ", ";
            }
            desc = desc.TrimEnd(' ');
            desc = desc.TrimEnd(',');
            return desc;
        }

        private string GetGuidsFromData(string value)
        {
            string oids = null;
            if (string.IsNullOrEmpty(value))
                return null;

            var nodes = value.Split(',').ToList();
            foreach (var node in nodes)
            {
                var tokens = node.Split(':');
                var oid = tokens.FirstOrDefault();
                oids += oid + ",";
            }
            oids = oids.TrimEnd(',');
            return oids;
        }

        private string GetNamesFromData(string value)
        {
            string names = null;
            if (string.IsNullOrEmpty(value))
                return null;

            var nodes = value.Split(',').ToList();
            foreach(var node in nodes)
            {
                var tokens = node.Split(':');
                var name = tokens[1];
                names += name + ",";
            }
            names = names.TrimEnd(',');
            return names;
        }
    }
}