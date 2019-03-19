using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tweetify.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Description { get; set; }
        public int NbFollowers { get; set; }
        public ICollection<Tweet> Tweets { get; set; }
        public ICollection<Tweet> Hightlights { get; set; }
        public Follow[] Followers { get; set; }
        public Follow[] Followings { get; set; }



    }
}
