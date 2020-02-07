using EventApps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventApps.Helpers
{
    public class ParticipantTypeHelper
    {
        public static DBEventEntities entities = new DBEventEntities();

        public static List<ParticipantType> GetAllListParticipantType()
        {
            return entities.ParticipantTypes.ToList();
        }

        public static bool SaveParticipantType(ParticipantType participantType)
        {
            try
            {
                var items = new ParticipantType
                {
                    ID = Guid.NewGuid(),
                    Name = participantType.Name,
                    Description = participantType.Description,
                    Created = DateTime.Now,
                    CreatedBy = "System"
                };

                entities.ParticipantTypes.Add(items);
                entities.SaveChanges();

                return true;
            }catch(Exception ex)
            {
                return false;
            }
        }

        // Mobile
        public List<ParticipantTypeMobile> GetAllListParticipantTypeMobile()
        {
            var listOfType = entities.ParticipantTypes.ToList();
            var listOfTypeParticipant = new List<ParticipantTypeMobile>();
            foreach(var items in listOfType)
            {
                listOfTypeParticipant.Add(new ParticipantTypeMobile { ID = items.ID, Name = items.Name, Description = items.Description });
            }

            return listOfTypeParticipant;
        }


    }
}