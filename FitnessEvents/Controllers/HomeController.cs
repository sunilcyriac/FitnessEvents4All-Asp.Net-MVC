using FitnessEvents.Models;
using FitnessEvents.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FitnessEvents.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            return View(new ContactEmailModel());
        }

        //code sourced from FIT5032 tutorials
        [HttpPost]
        public ActionResult Contact(ContactEmailModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    
                    String subject = model.Subject;
                    String contents = model.Contents;

                    ContactEmail es = new ContactEmail();
                    es.Send(subject, contents);

                    ViewBag.Result = "Email has been send.";

                    ModelState.Clear();

                    return View(new ContactEmailModel());
                }
                catch
                {
                    return View();
                }
            }

            return View();
        }

        public ActionResult NavigationMap()
        {
            return View();
        }

        public ActionResult NavigationCalculator()
        {
            return View();
        }

        [Authorize]
        public ActionResult Send_Email()
        {
            return View(new SendEmailViewModel());
        }

        //code sourced from FIT5032 tutorials
        [HttpPost]
        public ActionResult Send_Email(SendEmailViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    String toEmail = model.ToEmail;
                    String subject = model.Subject;
                    String contents = model.Contents;

                    EmailSender es = new EmailSender();
                    es.Send(toEmail, subject, contents);

                    ViewBag.Result = "Email has been send.";

                    ModelState.Clear();

                    return View(new SendEmailViewModel());
                }
                catch
                {
                    return View();
                }
            }

            return View();
        }

        //code sourced from  www.aspsnippets.com/Articles/Implement-Google-Maps-from-Database-in-ASPNet-MVC-Razor.aspx
        [Authorize]
        public ActionResult EventLocation()
        
        {
            string markers = "[";
            string CS = ConfigurationManager.ConnectionStrings["EventRegistration"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("spGetMap", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    markers += "{";
                    markers += string.Format("'title': '{0}',", sdr["Address"]);
                    markers += string.Format("'lat': '{0}',", sdr["Latitude"]);
                    markers += string.Format("'lng': '{0}',", sdr["Longitude"]);
                    markers += string.Format("'description': '{0}'", sdr["Description"]);
                    markers += "},";
                }
            }
            markers += "];";
            ViewBag.Markers = markers;
            return View();
        }


        //code sourced from  www.aspsnippets.com/Articles/Implement-Google-Maps-from-Database-in-ASPNet-MVC-Razor.aspx
        [HttpPost]
        public ActionResult EventLocation(EventLocation location)
        {
            if (ModelState.IsValid)
            {
                string CS = ConfigurationManager.ConnectionStrings["EventRegistration"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("spAddNewLocation", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.Parameters.AddWithValue("@Address", location.Address);
                    cmd.Parameters.AddWithValue("@Latitude", location.Latitude);
                    cmd.Parameters.AddWithValue("@Longitude", location.Longitude);
                    cmd.Parameters.AddWithValue("@Description", location.Description);
                    cmd.ExecuteNonQuery();
                }
            }
            else
            {

            }
            return RedirectToAction("EventLocation");
        }


        //code sourced from  www.aspsnippets.com/Articles/Implement-Google-Maps-from-Database-in-ASPNet-MVC-Razor.aspx
        public ActionResult ViewEventLocations()
        {
            string markers = "[";
            string CS = ConfigurationManager.ConnectionStrings["EventRegistration"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("spGetMap", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    markers += "{";
                    markers += string.Format("'title': '{0}',", sdr["Address"]);
                    markers += string.Format("'lat': '{0}',", sdr["Latitude"]);
                    markers += string.Format("'lng': '{0}',", sdr["Longitude"]);
                    markers += string.Format("'description': '{0}'", sdr["Description"]);
                    markers += "},";
                }
            }
            markers += "];";
            ViewBag.Markers = markers;
            return View();
        }

        //code sourced from  www.aspsnippets.com/Articles/Implement-Google-Maps-from-Database-in-ASPNet-MVC-Razor.aspx
        [HttpPost]
        public ActionResult ViewEventLocations(EventLocation location)
        {
            if (ModelState.IsValid)
            {
                string CS = ConfigurationManager.ConnectionStrings["EventRegistration"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("spAddNewLocation", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.Parameters.AddWithValue("@Address", location.Address);
                    cmd.Parameters.AddWithValue("@Latitude", location.Latitude);
                    cmd.Parameters.AddWithValue("@Longitude", location.Longitude);
                    cmd.Parameters.AddWithValue("@Description", location.Description);
                    cmd.ExecuteNonQuery();
                }
            }
            else
            {

            }
            return RedirectToAction("EventLocation");
        }
    }

    
}  
