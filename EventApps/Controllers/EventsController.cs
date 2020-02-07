using EventApps.Helpers;
using EventApps.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventApps.Controllers
{
    public class EventsController : Controller
    {
        // GET: Events
        public ActionResult Index()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            List<Event> listOfEvent = EventHelper.GetAllListEvent();
            return View(listOfEvent);
        }

        public ActionResult Create()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var listOfTypeEvent = EventTypeHelper.GetAllListTypeEvent();
            ViewBag.TypeEvent = listOfTypeEvent.Select(x => new SelectListItem { Text = x.Name, Value = x.ID.ToString() }).ToList();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Event eventItem)
        {
            try
            {
                var FileName = "";
                Event events = new Event();

                if (eventItem.FileImages != null)
                {
                    FileName = Path.GetFileNameWithoutExtension(eventItem.FileImages.FileName);
                    string FileExtension = Path.GetExtension(eventItem.FileImages.FileName);
                    FileName = FileName.Trim() + FileExtension;
                    events.Images = FileName;
                    string UploadPath = Server.MapPath("~/Images/");
                    eventItem.FileImages.SaveAs(UploadPath + FileName);
                }

                events.Title = eventItem.Title;
                events.Description = eventItem.Description;
                events.Location = eventItem.Location;
                events.Latitude = eventItem.Latitude;
                events.Longitude = eventItem.Longitude;
                events.StartDate = eventItem.StartDate;
                events.EndDate = eventItem.EndDate;
                events.NormalPrice = eventItem.NormalPrice;
                events.HighPrice = eventItem.HighPrice;
                events.OtherPrice = eventItem.HighPrice;
                events.IDType = eventItem.IDType;
                events.Quota = eventItem.Quota;

                bool result = EventHelper.SaveEvent(events);
                if (result)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    var listOfTypeEvent = EventTypeHelper.GetAllListTypeEvent();
                    ViewBag.TypeEvent = listOfTypeEvent.Select(x => new SelectListItem { Text = x.Name, Value = x.ID.ToString() }).ToList();
                    TempData["msg"] = "<script>alert('Failed to insert data');</script>";
                    return View("Create");
                }
            }
            catch(Exception ex)
            {
                var listOfTypeEvent = EventTypeHelper.GetAllListTypeEvent();
                ViewBag.TypeEvent = listOfTypeEvent.Select(x => new SelectListItem { Text = x.Name, Value = x.ID.ToString() }).ToList();
                TempData["msg"] = "<script>alert('" + ex.ToString() + "');</script>";
                return View("Create");
            }
        }

        public ActionResult Delete(string ID)
        {
            bool result = EventHelper.DeleteEvent(ID);
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

            var detailItems = EventHelper.GetDetailEvents(id);
            var listOfTypeEvent = EventTypeHelper.GetAllListTypeEvent();
            ViewBag.TypeEvent = listOfTypeEvent.Select(x => new SelectListItem { Text = x.Name, Value = x.ID.ToString() }).ToList();
            return View(detailItems);
        }

        [HttpPost]
        public ActionResult Edit(Event eventItem)
        {
            try
            {
                var FileName = "";
                if (eventItem.FileImages != null)
                {
                    FileName = Path.GetFileNameWithoutExtension(eventItem.FileImages.FileName);
                    string FileExtension = Path.GetExtension(eventItem.FileImages.FileName);
                    FileName = FileName.Trim() + FileExtension;
                    eventItem.Images = FileName;
                    string UploadPath = Server.MapPath("~/Images/");
                    eventItem.FileImages.SaveAs(UploadPath + FileName);
                }

                bool result = EventHelper.EditEvent(eventItem);
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
            catch(Exception ex)
            {
                return RedirectToAction("Index");
            }
        }
    }
}