﻿namespace ServerApp.Models
{
    public class User : IdentityUser<int>
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastActive { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Introduction { get; set; }
        public string Hobbies { get; set; }
        public List<Image> Images { get; set; }

        public ICollection<UserToUser> Followings { get; set; }
        public ICollection<UserToUser> Followers { get; set; }
        public ICollection<Message> MessagesSent { get; set; }
        public ICollection<Message> MessagesReceived { get; set; }
    }
}
