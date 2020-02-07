using EventApps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventApps.Helpers
{
    public class AttendanceHelper
    {
        public static DBEventEntities entities = new DBEventEntities();

        public static List<Attendance> GetListAttendance(Guid idEvent)
        {
            return entities.Attendances.Where(x => x.IDEvent == idEvent).ToList();
        }

        public bool CheckInCandidate(Attendance attendance)
        {
            try
            {
                var items = new Attendance
                {
                    ID = Guid.NewGuid(),
                    IDParticipant = attendance.IDParticipant,
                    IDEvent = attendance.IDEvent,
                    CheckInDate = DateTime.Now,
                    Created = DateTime.Now,
                    CreatedBy = "System"
                };

                entities.Attendances.Add(items);
                entities.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public bool CheckOutCandidate(Attendance attendance)
        {
            try
            {
                var items = entities.Attendances.Where(x => x.IDEvent == attendance.IDEvent && x.IDParticipant == attendance.IDParticipant).SingleOrDefault();
                if(items != null)
                {
                    items.CheckOutDate = DateTime.Now;
                    items.Status = "Hadir";
                    items.Modified = DateTime.Now;
                    items.ModifiedBy = "System";

                    entities.SaveChanges();
                    return true;
                }

                return false;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public bool CheckInOutStatus(string idEvent, string idParticipant)
        {
            try
            {
                var eventID = Guid.Parse(idEvent);
                var items = entities.Attendances.Where(x => x.IDEvent == eventID && x.IDParticipant == idParticipant).SingleOrDefault();
                if(items != null)
                {
                    return true;
                }

                return false;
            }catch(Exception ex)
            {
                return false;
            }
        }

        public bool CheckCertificateStatus(string idEvent, string idParticipant)
        {
            try
            {
                var eventID = Guid.Parse(idEvent);
                var items = entities.Attendances.Where(x => x.IDEvent == eventID && x.IDParticipant == idParticipant && x.Status.Equals("Hadir")).SingleOrDefault();
                if (items != null)
                {
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