using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EFImages.Web.Models;
using Microsoft.Extensions.Configuration;
using EFImages.Data;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace EFImages.Web.Controllers
{
    public class HomeController : Controller
    {
        private string _connenctionString;
        public HomeController(IConfiguration config)
        {
            _connenctionString = config.GetConnectionString("ConStr");
        }
        public IActionResult Index()
        {
            var repos = new ImageRepository(_connenctionString);
            return View(repos.GetAllImages());
        }
        public IActionResult DisplayImage(int id)
        {
            var repos = new ImageRepository(_connenctionString);
            var vm = new DisplayImageViewModel();
            var likes = HttpContext.Session.Get<List<int>>("Likes");
            if (likes != null)
            {
                vm.Liked = likes.Contains(id);
            }
            else
            {
                vm.Liked = false;
            }
            vm.Image = repos.GetImageById(id);
            return View(vm);
        }
        public IActionResult AddImage()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(Image image)
        {
            var repos = new ImageRepository(_connenctionString);
            var result = new Image
            {
                Title = image.Title,
                URL = image.URL,
                DatePosted = DateTime.Now
            };
            repos.AddImage(result);
            return Redirect("/");
        }
        [HttpPost]
        public IActionResult Like(int id)
        {
            var repos = new ImageRepository(_connenctionString);
            var likes = HttpContext.Session.Get<List<int>>("Likes");
            if (likes == null)
            {
                likes = new List<int>();
            }
            else if (likes.Contains(id))
            {
                return null;
            }
            likes.Add(id);
            HttpContext.Session.Set("Likes", likes);
            var image = repos.GetImageById(id);
            image.NumberOfLikes++;
            repos.LikeImage(image);

            return Json(image);
        }
        public IActionResult NumberOfLikes(int id)
        {
            var repos = new ImageRepository(_connenctionString);
            var image = repos.GetImageById(id);
            var amount = 0;
            if (image.NumberOfLikes != 0)
            {
                amount = image.NumberOfLikes;
            }
            return Json(amount);
        }
    }
    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T Get<T>(this ISession session, string key)
        {
            string value = session.GetString(key);

            return value == null ? default(T) :
                JsonConvert.DeserializeObject<T>(value);
        }
    }
}
