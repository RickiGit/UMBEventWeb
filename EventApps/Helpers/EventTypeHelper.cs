using EventApps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventApps.Helpers
{
    public class EventTypeHelper
    {
        public static DBEventEntities entities = new DBEventEntities();

        public static bool SaveEventType(EventType eventType)
        {
            try
            {
                var items = new EventType
                {
                    ID = Guid.NewGuid(),
                    Name = eventType.Name,
                    Description = eventType.Description,
                    Created = DateTime.Now,
                    CreatedBy = "System"
                };

                entities.EventTypes.Add(items);
                entities.SaveChanges();
                return true;
            }catch(Exception ex)
            {
                return false;
            }
        }

        public static List<EventType> GetAllListTypeEvent()
        {
            List<EventType> listOfType = entities.EventTypes.ToList();
            return listOfType;
        }

        public static bool DeleteEventType(string id)
        {
            var idGuid = Guid.Parse(id);

            var item = entities.EventTypes.Where(x => x.ID == idGuid).SingleOrDefault();
            if (item != null)
            {
                entities.EventTypes.Remove(item);
                entities.SaveChanges();
                return true;
            }

            return false;
        }

        public static EventType GetDetailEventType(string id)
        {
            var idGuid = Guid.Parse(id);
            return entities.EventTypes.Where(x => x.ID == idGuid).SingleOrDefault();
        }

        public static bool EditEventType(EventType events)
        {
            try
            {
                var item = entities.EventTypes.Where(x => x.ID == events.ID).SingleOrDefault();
                if (item != null)
                {
                    item.ID = events.ID;
                    item.Name = events.Name;
                    item.Description = events.Description;
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

    }
}