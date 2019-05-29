﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FunApp.Web.Models;
using FunApp.Data.Common;
using FunApp.Data.Models;
using FunApp.Web.Models.Home;

namespace FunApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository<Joke> jokesRepo;

        public HomeController(IRepository<Joke> jokesRepo)
        {
            this.jokesRepo = jokesRepo;
        }

        public IActionResult Index()
        {
            var jokes = this.jokesRepo.All().OrderBy(x => Guid.NewGuid()).Select(x => new IndexJokeViewModel
            {
                Content = x.Content,
                CategoryName = x.Category.Name
            }).Take(20);

            var viewModel = new IndexViewModel
            {
                Jokes = jokes
            };

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
