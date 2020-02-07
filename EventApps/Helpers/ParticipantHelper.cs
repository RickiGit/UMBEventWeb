using EventApps.Models;
using EventApps.Models.CustomModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventApps.Helpers
{
    public class ParticipantHelper
    {
        public static DBEventEntities entities = new DBEventEntities();

        public static List<Participant> GetListAllParticipant()
        {
            return entities.Participants.ToList();
        }

        public static Participant GetDetailParticipant(string id)
        {
            return entities.Participants.Where(x => x.ID == id).SingleOrDefault();
        }

        public static bool SaveParticipant(Participant participant)
        {
            try
            {
                var items = new Participant
                {
                    ID = participant.ID,
                    Name = participant.Name,
                    Email = participant.Email,
                    Phone = participant.Phone,
                    Gender = participant.Gender,
                    Image = participant.Image,
                    IDType = participant.IDType,
                    Username = participant.Username,
                    Password = participant.Password,
                    Created = DateTime.Now,
                    CreatedBy = "System",
                    Status = 1
                };

                entities.Participants.Add(items);
                entities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool EditParticipant(Participant participant)
        {
            try
            {
                var item = entities.Participants.Where(x => x.ID == participant.ID).SingleOrDefault();
                if (item != null)
                {
                    item.ID = participant.ID;
                    item.Name = participant.Name;
                    item.Email = participant.Email;
                    item.Phone = participant.Phone;
                    item.Gender = participant.Gender;
                    item.IDType = participant.IDType;
                    item.Username = participant.Username;
                    item.Password = participant.Password;
                    item.Modified = DateTime.Now;
                    item.ModifiedBy = "System";

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

        public static bool DeleteParticipant(string id)
        {
            var item = entities.Participants.Where(x => x.ID == id).SingleOrDefault();
            if (item != null)
            {
                entities.Participants.Remove(item);
                entities.SaveChanges();
                return true;
            }

            return false;
        }

        public static int GetTotalParticipant()
        {
            return entities.Participants.Count();
        }

        // Mobile
        public string SaveParticipantMobile(Participant participant)
        {
            try
            {
                var check = CheckUsername(participant.Username);
                if (check)
                {
                    var items = new Participant
                    {
                        ID = participant.ID,
                        Name = participant.Name,
                        Email = participant.Email,
                        Phone = participant.Phone,
                        Gender = participant.Gender,
                        Image = participant.Image,
                        IDType = participant.IDType,
                        Username = participant.Username,
                        Password = EncryptionHelper.Encrypt(participant.Password),
                        Created = DateTime.Now,
                        CreatedBy = "System",
                        Status = 1
                    };

                    entities.Participants.Add(items);
                    entities.SaveChanges();
                    return "success";
                }
                else
                {
                    return "exist";
                }
            }
            catch (Exception ex)
            {
                return "failed";
            }
        }

        public ParticipantMobile GetParticipant(string username, string password)
        {
            try
            {
                var passwordEncrypt = EncryptionHelper.Encrypt(password);
                var items = entities.Participants.Where(x => x.Username == username && x.Password == passwordEncrypt && x.Status == 1).SingleOrDefault();
                var participant = new ParticipantMobile();
                if(items != null)
                {
                    participant.ID = items.ID;
                    participant.Name = items.Name;
                    participant.Email = items.Email;
                    participant.Phone = items.Phone;
                    participant.Gender = items.Gender;
                    participant.IDType = items.IDType;
                    participant.Username = items.Username;
                    participant.Password = items.Password;

                    return participant;
                }

                return null;
            }catch(Exception ex)
            {
                return null;
            }
        }

        public bool CheckUsername(string username)
        {
            var items = entities.Participants.Where(x => x.Username.Equals(username)).SingleOrDefault();
            if (items != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}