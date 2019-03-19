using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tweetify.Models
{
    public class Follow
    {
        public int Id { get; set; }
        public User Follower { get; set; }
        public User Following { get; set; }
    }
}
