using EventApps.Models;
using EventApps.Models.CustomModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EventApps.Helpers
{
    public class RegistrationHelper
    {
        public static DBEventEntities entities = new DBEventEntities();
        public static List<Registration> GetAllListRegistration(string idEvent)
        {
            var id = Guid.Parse(idEvent);
            var today = DateTime.Today;
            return entities.Registrations.Where(x => x.IDEvent == id).OrderBy(x => x.Created).ThenBy(x => x.Status).ToList();
        }
        public static bool Verification(string idRegistration)
        {
            try
            {
                var id = Guid.Parse(idRegistration);
                var items = entities.Registrations.Where(x => x.ID == id).SingleOrDefault();
                if(items != null)
                {
                    items.Status = 1;
                    items.Modified = DateTime.Now;
                    items.ModifiedBy = "System";

                    entities.SaveChanges();
                    return true;
                }

                return false;
            }catch(Exception ex)
            {
                return false;
            }
        }
        // Mobile
        public bool RegistrationEvent(Registration registration)
        {
            try
            {
                var result = CheckRegistration(registration);
                if (result)
                {
                    var items = new Registration
                    {
                        ID = Guid.NewGuid(),
                        IDParticipant = registration.IDParticipant,
                        IDEvent = registration.IDEvent,
                        RegisterDate = DateTime.Now,
                        Price = registration.Price,
                        Status = 0,
                        Created = DateTime.Now,
                        CreatedBy = "System"
                    };

                    entities.Registrations.Add(items);
                    entities.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool CheckRegistration(Registration registration)
        {
            try
            {
                var result = entities.Registrations.Where(x => x.IDEvent == registration.IDEvent && x.IDParticipant == registration.IDParticipant).SingleOrDefault();
                if(result == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }catch(Exception ex)
            {
                return false;
            }
        }
        public bool CheckStatusRegistration(string idParticipant, string idEvent)
        {
            try
            {
                var eventID = Guid.Parse(idEvent);
                var result = entities.Registrations.Where(x => x.IDParticipant == idParticipant && x.IDEvent == eventID).SingleOrDefault();
                if(result == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }catch(Exception ex)
            {
                return false;
            }
        }
        public RegistrationModel GetDetailRegistration(string idParticipant, string idEvent)
        {
            try
            {
                var eventID = Guid.Parse(idEvent);
                var result = entities.Registrations.Where(x => x.IDParticipant == idParticipant && x.IDEvent == eventID).SingleOrDefault();
                if (result == null)
                {
                    return null;
                }
                else
                {
                    RegistrationModel item = new RegistrationModel();
                    item.ID = result.ID;
                    item.NameEvent = result.Event.Title;
                    item.IDParticipant = result.IDParticipant;
                    item.Name = result.Participant.Name;
                    item.Price = result.Price;
                    item.RegisterDate = result.RegisterDate;
                    item.Status = result.Status;

                    return item;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}