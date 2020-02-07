using EventApps.Helpers;
using EventApps.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventApps.Controllers
{
    public class ParticipantController : Controller
    {
        // GET: Participant
        public ActionResult Index()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var listOfParticipant = ParticipantHelper.GetListAllParticipant();
            return View(listOfParticipant);
        }

        public ActionResult Create()
        {

            if (Session["Username"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var listOfType = ParticipantTypeHelper.GetAllListParticipantType();
            ViewBag.ParticipantType = listOfType.Select(x => new SelectListItem { Text = x.Name, Value = x.ID.ToString() }).ToList();
            
            var listOfGender = new ObservableCollection<String>();
            listOfGender.Add("Laki-Laki");
            listOfGender.Add("Perempuan");
            ViewBag.Gender = listOfGender.Select(x => new SelectListItem { Text = x, Value = x }).ToList();

            return View();
        }

        [HttpPost]
        public ActionResult Create(Participant participant)
        {
            if (!participant.Password.Equals(participant.ConfirmPassword))
            {
                TempData["msg"] = "<script>alert('Confirm password not match');</script>";
                return RedirectToAction("Create");
            }
            else
            {
                var result = ParticipantHelper.SaveParticipant(participant);
                if (result)
                {
                    TempData["msg"] = "<script>alert('Saved successfuly');</script>";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["msg"] = "<script>alert('Failed to Insert Data');</script>";
                    return RedirectToAction("Create");
                }
                
            }
        }

        public ActionResult Edit(string id)
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var listOfType = ParticipantTypeHelper.GetAllListParticipantType();
            ViewBag.ParticipantType = listOfType.Select(x => new SelectListItem { Text = x.Name, Value = x.ID.ToString() }).ToList();
            var item = ParticipantHelper.GetDetailParticipant(id);
            if (item.Gender.Equals("Laki-Laki"))
            {
                var listOfGender = new List<SelectListItem>
                {
                    new SelectListItem { Text = "Laki-Laki", Value = "Laki-Laki", Selected = true },
                    new SelectListItem { Text = "Perempuan", Value = "Perempuan" }
                };
                ViewBag.Gender = listOfGender;
            }
            else
            {
                var listOfGender = new List<SelectListItem>
                {
                    new SelectListItem { Text = "Laki-Laki", Value = "Laki-Laki"},
                    new SelectListItem { Text = "Perempuan", Value = "Perempuan", Selected = true }
                };
                ViewBag.Gender = listOfGender;
            }

            
            return View(item);
        }

        [HttpPost]
        public ActionResult Edit(Participant participant)
        {
            if (!participant.Password.Equals(participant.ConfirmPassword))
            {
                TempData["msg"] = "<script>alert('Confirm password not match');</script>";
                return RedirectToAction("Edit");
            }
            else
            {
                var result = ParticipantHelper.EditParticipant(participant);
                if (result)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["msg"] = "<script>alert('Failed to Insert Data');</script>";
                    return RedirectToAction("Edit");
                }

            }
        }

        public ActionResult Delete(string ID)
        {
            bool result = ParticipantHelper.DeleteParticipant(ID);
            if (result)
            {
                return RedirectToAction("Index");
            }

            return View();
        }
    }
}