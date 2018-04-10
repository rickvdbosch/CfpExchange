﻿using System;
using System.Linq;
using CfpExchange.Data;
using CfpExchange.Helpers;
using CfpExchange.Models;
using Microsoft.AspNetCore.Mvc;

namespace CfpExchange.Controllers
{
	public class CfpController : Controller
	{
		private readonly CfpContext _cfpContext;

		public CfpController(CfpContext cfpContext)
		{
			_cfpContext = cfpContext;
		}

		[HttpPost]
		public IActionResult GetMetadata(string url)
		{
			if (string.IsNullOrWhiteSpace(url))
				return Ok();

			// TODO check url for scheme
			var metadata = MetaScraper.GetMetaDataFromUrl(url);

			return Json(metadata);
		}

		public IActionResult Browse()
		{
			return View();
		}

		public IActionResult Submit()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Submit(Cfp submittedCfp)
		{
			// Check validity

			// Save CFP

			// Post to Twitter

			return View();
		}

		public IActionResult Details(Guid id)
		{
			if (id == Guid.Empty)
				RedirectToAction("index", "home");
			
			var selectedCfp = _cfpContext.Cfps.SingleOrDefault(cfp => cfp.Id == id);

			if (selectedCfp == null)
				// TODO to error page?
				RedirectToAction("index", "home");

			selectedCfp.Views++;
			_cfpContext.SaveChanges();
			 
			return View(selectedCfp);
		}
	}
}