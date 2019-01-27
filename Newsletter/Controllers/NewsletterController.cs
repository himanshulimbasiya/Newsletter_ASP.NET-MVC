using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Mail;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using Newsletter.Models;

namespace Newsletter.Controllers
{
    public class NewsletterController : Controller
    {
        // GET: Newsletter
        public ActionResult Index()
        {


            SelectList sourceList = new SelectList( new List<Object>
            {
                new {value = "Advert" , text = "Advert"},
                new {value = "Word of Mouth" , text = "Word of Mouth"},
                new {value = "other" , text = "other"}
            }, "value", "text");


            ViewBag.source = sourceList;
            return View();
        }

        [HttpPost]
        public ActionResult Index(SubscribersViewModel data)
        {
            SelectList sourceList = new SelectList(new List<Object>
            {
                new {value = 0 , text = "Advert"},
                new {value = 1 , text = "Word of Mouth"},
                new {value = 2 , text = "other"}
            }, "value", "text");

            try
            {
                Newsletter_SubscribersEntities db = new Newsletter_SubscribersEntities();


                //Server-side Validation
                if (string.IsNullOrEmpty(data.Email) && string.IsNullOrEmpty(data.Source))
                {
                    return Content("Email and source null");
                }
                else if (!string.IsNullOrEmpty(data.Email) && string.IsNullOrEmpty(data.Source))
                {
                    return Content("Source null");
                }
                else if (string.IsNullOrEmpty(data.Email) && !string.IsNullOrEmpty(data.Source))
                {
                    return Content("Null email");
                }
                else if (!string.IsNullOrEmpty(data.Email) && !string.IsNullOrEmpty(data.Source))
                {
                    if (!isValidEmail(data.Email))
                    {
                        return Content("Invalid email");
                    }
                }
                if (db.Subscribers.Any(x => x.Email == data.Email))
                {
                    return Content("Already Subscribed");
                }
                else
                {
                    Guid id = Guid.NewGuid();

                    Subscriber user = new Subscriber
                    {
                        Id = id,
                        Email = data.Email,
                        Source = data.Source,
                        Reason = data.Reason
                    };
                    db.Subscribers.Add(user);
                    db.SaveChanges();
                    return Content("Both valid");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        bool isValidEmail(string email)
        {
            try
            {
                var check = new MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }

        }

        



        /*[HttpPost]
        public ActionResult Subscribe(SubscribersViewModel data)
        {
            try
            {
                Newsletter_SubscribersEntities db = new Newsletter_SubscribersEntities();

                if (db.Subscribers.Any(x => x.Email == data.Email))
                {
                    return Content("Email already Subscribed");
                }
                else
                {
                    Guid id = Guid.NewGuid();

                    Subscriber user = new Subscriber
                    {
                        Id = id,
                        Email = data.Email,
                        Source = data.Source,
                        Reason = data.Reason
                    };
                    db.Subscribers.Add(user);
                    db.SaveChanges();
                    return Content("201");
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
           
        }*/
    }
}