using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tweetify.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tweetify.DAL;
using Microsoft.EntityFrameworkCore;

namespace Tweetify.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (!HttpContext.Session.GetInt32("UserId").HasValue)
            {
                return RedirectToAction("Login", "Auth");
            }
            else
            {
                using (var context = new TweetifyContext())
                {
                    var t = context.Tweets.Include("Author").ToList();

                    ViewData["tweetbox"] = t;
                }
            }
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(Tweet t)
        {
            try
            {
                using (var context = new TweetifyContext())
                {
                    t.Author = context.Users.FirstOrDefault(x => x.Id == HttpContext.Session.GetInt32("UserId"));
                    await context.Tweets.AddAsync(t);
                    await context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));

            }
            catch
            {
                return View();
            }
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
