using EventApps.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EventApps.Helpers
{
    public class EventHelper
    {
        public static DBEventEntities entities = new DBEventEntities();

        public static bool SaveEvent(Event events)
        {
            try
            {
                var items = new Event
                {
                    ID = Guid.NewGuid(),
                    Title = events.Title,
                    Description = events.Description,
                    Location = events.Location,
                    Latitude = events.Latitude,
                    Longitude = events.Longitude,
                    StartDate = events.StartDate,
                    EndDate = events.EndDate,
                    NormalPrice = events.NormalPrice,
                    HighPrice = events.HighPrice,
                    OtherPrice = events.OtherPrice,
                    Quota = events.Quota,
                    IDType = events.IDType,
                    Images = events.Images,
                    Created = DateTime.Now,
                    CreatedBy = "System"
                };

                entities.Events.Add(items);
                entities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static List<Event> GetAllEventCertificate()
        {
            return entities.Events.OrderByDescending(x => x.Created).ToList();
        }

        public static List<Event> GetAllListEvent()
        {
            var today = DateTime.Today;
            var listOfEvent = entities.Events.Where(x => DbFunctions.TruncateTime(x.StartDate) >= today).OrderBy(x => x.StartDate).ToList();
            return listOfEvent;
        }

        public static int GetTotalEvent()
        {
            var listOfEvent = entities.Events.Count();
            return listOfEvent;
        }

        public static int GetTotalComingEvent()
        {
            var today = DateTime.Today;
            var listOfEvent = entities.Events.Where(x => DbFunctions.TruncateTime(x.StartDate) >= today).OrderBy(x => x.StartDate).Count();
            return listOfEvent;
        }

        public static int GetTotalPastEvent()
        {
            var today = DateTime.Today;
            var listOfEvent = entities.Events.Where(x => DbFunctions.TruncateTime(x.StartDate) <= today).OrderBy(x => x.StartDate).Count();
            return listOfEvent;
        }

        public static List<Event> Get5ComingEvent()
        {
            var today = DateTime.Today;
            var listOfEvent = entities.Events.Where(x => DbFunctions.TruncateTime(x.StartDate) >= today).OrderBy(x => x.StartDate).Take(5).ToList();
            return listOfEvent;
        }

        public static List<Event> GetAllListTodayEvent()
        {
            var today = DateTime.Today;
            var listOfEvent = entities.Events.Where(x => DbFunctions.TruncateTime(x.StartDate) == today).OrderBy(x => x.StartDate).ToList();
            return listOfEvent;
        }

        public static bool DeleteEvent(string id)
        {
            var idGuid = Guid.Parse(id);

            var item = entities.Events.Where(x => x.ID == idGuid).SingleOrDefault();
            if (item != null)
            {
                entities.Events.Remove(item);
                entities.SaveChanges();
                return true;
            }

            return false;
        }

        public static Event GetDetailEvents(string id)
        {
            var idGuid = Guid.Parse(id);
            return entities.Events.Where(x => x.ID == idGuid).SingleOrDefault();
        }

        public static bool EditEvent(Event events)
        {
            try
            {
                var item = entities.Events.Where(x => x.ID == events.ID).SingleOrDefault();
                if (item != null)
                {
                    item.ID = events.ID;
                    item.Title = events.Title;
                    item.Description = events.Description;
                    item.Location = events.Location;
                    item.Latitude = events.Latitude;
                    item.Longitude = events.Longitude;
                    item.StartDate = events.StartDate;
                    item.EndDate = events.EndDate;
                    item.NormalPrice = events.NormalPrice;
                    item.HighPrice = events.HighPrice;
                    item.OtherPrice = events.OtherPrice;
                    item.IDType = events.IDType;
                    item.Quota = events.Quota;
                    item.Images = events.Images;
                    item.Modified = DateTime.Now;
                    item.ModifiedBy = "System";

                    entities.SaveChanges();

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        // Mobile
        public List<EventMobile> GetAllListEventMobile()
        {
            var today = DateTime.Today;
            var listOfEventMobile = new List<EventMobile>();
            var listOfEvent = entities.Events.Where(x => DbFunctions.TruncateTime(x.StartDate) >= today).OrderBy(x => x.StartDate).ToList();

            foreach(var items in listOfEvent)
            {
                listOfEventMobile.Add(new EventMobile { ID = items.ID, Title = items.Title, Location = items.Location, Description = items.Description, StartDate = items.StartDate, EndDate = items.EndDate, NormalPrice = items.NormalPrice, HighPrice = items.HighPrice, OtherPrice = items.OtherPrice, Quota = items.Quota, Images = HttpContext.Current.Request.Url.Host + "/Images/" + items.Images, Type = items.EventType.Name });
            }

            return listOfEventMobile;
        }

        public List<EventMobile> GetAllListMyEventMobile(string idParticipant)
        {
            var listOfEventMobile = new List<EventMobile>();
            var listOfEvent = entities.Registrations.Where(x => x.IDParticipant == idParticipant && x.Status == 1).ToList();
            if(listOfEvent != null && listOfEvent.Count > 0)
            {
                for(var i = 0; i < listOfEvent.Count; i++)
                {
                    listOfEventMobile.Add(new EventMobile { ID = listOfEvent[i].Event.ID, Title = listOfEvent[i].Event.Title, Location = listOfEvent[i].Event.Location, Description = listOfEvent[i].Event.Description, StartDate = listOfEvent[i].Event.StartDate, EndDate = listOfEvent[i].Event.EndDate, NormalPrice = listOfEvent[i].Event.NormalPrice, HighPrice = listOfEvent[i].Event.HighPrice, OtherPrice = listOfEvent[i].Event.OtherPrice, Images = listOfEvent[i].Event.Images, Quota = listOfEvent[i].Event.Quota, Type = listOfEvent[i].Event.EventType.Name });
                }
            }

            return listOfEventMobile;
        }

        public int GetQuotaAvailable(string idEvent)
        {
            var id = Guid.Parse(idEvent);
            var eventItem = entities.Events.Where(x => x.ID == id).SingleOrDefault();
            var totalRegister = entities.Registrations.Where(x => x.IDEvent == id && x.Status == 1).Count();
            return eventItem.Quota - totalRegister;
        }
    }
}