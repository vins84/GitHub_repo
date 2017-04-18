using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FullStackPluralsight.Models
{
    public class UserNotification
    {
        [Key]
        [Column(Order = 1)]
        public string UserId { get; private set; }

        [Key]
        [Column(Order = 2)]
        public int NotificationId { get; private set; }

        public ApplicationUser User { get; private set; }               //Navigation property to navigate lol

        public Notification Notification { get; private set; }          //Navigation property to navigate lol

        public bool IsRead { get; private set; }

        protected UserNotification()        //Default constructor for entity framework as it will scream when using the below custom constructor
        {
        }
        
        public UserNotification(ApplicationUser user, Notification notification)
        {
            if (user == null)
            {
                throw new ArgumentNullException("User null you fool");
            }
            
            if (notification == null)
            {
                throw new ArgumentNullException("Notifications are null");
            }
            User = user;
            Notification = notification;
        }

        //public void Read()
        //{
        //    IsRead = true;
        //}
    }
}