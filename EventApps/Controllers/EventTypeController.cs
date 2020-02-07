using EventApps.Helpers;
using EventApps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventApps.Controllers
{
    public class EventTypeController : Controller
    {
        // GET: EventType
        public ActionResult Index()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            List<EventType> listOfEvents = EventTypeHelper.GetAllListTypeEvent();
            return View(listOfEvents);
        }

        public ActionResult Create()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Create(string Name, string Description)
        {
            if(Name.Equals("") || Description.Equals(""))
            {
                TempData["msg"] = "<script>alert('Name and Description can't be empty');</script>";
                return View("Create");
            }
            else
            {
                EventType eventType = new EventType();
                eventType.Name = Name;
                eventType.Description = Description;

                bool result = EventTypeHelper.SaveEventType(eventType);
                if (result)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["msg"] = "<script>alert('Failed to insert data');</script>";
                    return View("Create");
                }
            }
        }

        public ActionResult Delete(string ID)
        {
            bool result = EventTypeHelper.DeleteEventType(ID);
            if (result)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        public ActionResult Edit(string id)
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var detailItems = EventTypeHelper.GetDetailEventType(id);
            return View(detailItems);
        }

        [HttpPost]
        public ActionResult Edit(EventType eventItem)
        {
            bool result = EventTypeHelper.EditEventType(eventItem);
            if (result)
            {
                return RedirectToAction("Index");
            }
            else
            {
                TempData["msg"] = "<script>alert('Data failed to update');</script>";
                return View("Edit");
            }
        }
    }
}