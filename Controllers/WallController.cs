using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using TheWall.Models;

namespace TheWall.Controllers
{
    public class WallController : Controller
    {
        private readonly DbConnector _dbConnector;

        public WallController(DbConnector connect)
        {
            _dbConnector = connect;
        }
        // GET: /Home/
        [HttpGet]
        [Route("wall")]
        public IActionResult Wall()
        {
             // ViewBag.errors = new List<string>();
            string user_name = HttpContext.Session.GetString("user_name");
            ViewBag.user_name = user_name;
            // List<Dictionary<string,object>> AllMessages = _dbConnector.Query("Select messages.id As message_id, message, Messages.created_at, Messages.updated_at, first_name from Messages JOIN users ON users.id = messages.user_id ORDER BY Messages.created_at DESC;");
            List<Dictionary<string,object>> AllMessages = _dbConnector.Query("Select messages.id, message, messages.created_at, messages.updated_at, first_name from messages JOIN users ON users.id = messages.user_id ORDER BY messages.created_at DESC;");
            System.Console.WriteLine("All Messages", AllMessages);
            ViewBag.AllMessages = AllMessages;
            List<Dictionary<string, object>> QueryComments = _dbConnector.Query("Select comments.id, comments.message_id, users.first_name, comments.created_at, comments.comment from comments JOIN messages ON comments.message_id = messages.id JOIN users ON comments.user_id = users.id Order By comments.created_at DESC;");
            ViewBag.Comments = QueryComments;
            return View();
        }

        [HttpPost]
        [Route("message")]
        public IActionResult message(string message)
        {
            int? user_id = HttpContext.Session.GetInt32("user_id");
            Console.WriteLine("user_id" + user_id);
            Console.WriteLine(user_id);
            _dbConnector.Execute($"INSERT INTO messages(message, updated_at, created_at, user_id) VALUES ('{message}', NOW(), NOW(), {user_id})");
            return RedirectToAction("wall");
        }

        [HttpPost]
        [Route("comment")]

        public IActionResult comment(string comment, string message_id)
        {
            int? user_id = HttpContext.Session.GetInt32("user_id");
            _dbConnector.Execute($"INSERT INTO comments(comment, updated_at, created_at, message_id, user_id) VALUES ('{comment}', NOW(), NOW(), {message_id}, {user_id})");
            return RedirectToAction("wall");
        }

        [HttpGet]
        [Route("logout")]

        public IActionResult logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("index", "Login");
        }
    }
}