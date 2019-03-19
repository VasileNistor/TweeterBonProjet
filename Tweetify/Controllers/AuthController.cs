using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tweetify.DAL;
using Tweetify.Models;

namespace Tweetify.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Register()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User u)
        {
            using (var context = new TweetifyContext())
            {
                User temp = context.Users.FirstOrDefault(x => x.Username == u.Username);

                if (temp != null)
                {
                    throw new Exception("User all ready exists");
                }
                else
                {
                    u.Username = u.Username.ToLower();
                    context.Users.Add(u);
                    await context.SaveChangesAsync();
                    return RedirectToAction("Login");
                }
            }
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User u)
        {
            using (var context = new TweetifyContext())
            {
                User temp = context.Users.FirstOrDefault(x => x.Username == u.Username);

                if (temp != null)
                {
                    if(temp.Password == u.Password)
                    {
                        HttpContext.Session.SetInt32("UserId", temp.Id);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        throw new Exception("Wrong password");
                    }
                }
                else
                {
                    throw new Exception("Unknown user");
                }
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login","Auth");
        }
    }
}