﻿using LMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMS.Controllers
{
	[Authorize]
    public class KlassController : Controller {
		private Repositories.KlassRepository repo = new Repositories.KlassRepository();
        // GET: Klass
		[Authorize(Roles="Teacher")]
        public ActionResult Index(){
            return View(repo.GetAll());
        }

		public ActionResult Details(int Id) {
			return View(repo.GetSpecific(Id));
		}

		[Authorize(Roles="Teacher")]
		public JsonResult RemoveKlassMember(int Id, string UId) {
			bool success = repo.RemoveKlassMember(Id, UId);
			return Json(new { Removed = success }, JsonRequestBehavior.AllowGet);
		}

		[Authorize(Roles="Teacher")]
		[HttpGet]
		public ActionResult Create() {
			return View();
		}

		[Authorize(Roles = "Teacher")]
		[HttpPost]
		public ActionResult Create(Klass model) {
			if (ModelState.IsValid) {
				repo.Add(model);
				return RedirectToAction("Index");
			}
			return View(model);
		}
	}
}