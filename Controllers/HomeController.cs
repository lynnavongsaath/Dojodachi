using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dojodachi.Models;

namespace Dojodachi.Controllers
{
    public class HomeController : Controller
    {
        public void setDachiInSession(Dachi dachi = null) 
        //is this the default for dachi to be null
        {
            if(dachi == null)
            {
                dachi = new Dachi(); //this sets the object dachi with the default properties
                HttpContext.Session.SetString("newGame", "true");
            }
            HttpContext.Session.SetInt32("happiness", dachi.Happiness);
            HttpContext.Session.SetInt32("fullness", dachi.Fullness);
            HttpContext.Session.SetInt32("energy", dachi.Energy);
            HttpContext.Session.SetInt32("meals", dachi.Meals);
        }
        
        private Dachi getDachiFromSession() 
        //this property to set the dachi into sessions
        {
            //setting the dachi constructor with the session values
            return new Dachi 
            (
                HttpContext.Session.GetInt32("happiness"),
                HttpContext.Session.GetInt32("fullness"),
                HttpContext.Session.GetInt32("energy"),
                HttpContext.Session.GetInt32("meals")
            );
        }
        [HttpGet("")]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("newGame") == null)
                setDachiInSession();

            Dachi model = getDachiFromSession();
            return View(model); //passing Dachi model object to view
        }
        [HttpGet("game/{gameType}")]
        public IActionResult Play(string gameType)
        {
            Dachi currDachi = getDachiFromSession(); //you're getting the dachi from session into object currDachi
            TempData["message"] = currDachi.Play(gameType); //This invokes the function Play from the currDachi, the dachi model, & returns a string
            setDachiInSession(currDachi); //this set the new numbers into session
            return RedirectToAction("Index"); 
        }
        [HttpGet("restart")]
        public IActionResult Restart()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}