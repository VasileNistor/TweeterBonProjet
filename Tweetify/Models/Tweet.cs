using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tweetify.Models
{
    public class Tweet
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public User Author { get; set; }
        public string ImageURL { get; set; }
        public bool IsRetweet { get; set; }
        public Tweet OriginalTweet { get; set; }
        public Like[] Likes { get; set; }
        public Tweet[] Answers { get; set; }

    }
}
