﻿using LMS.CustomAttributes;
using LMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

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

        [Authorize]
        public ActionResult ShowMyClasses()
        {
            return View(repo.GetMyClasses(User.Identity.GetUserId()));
        }

        [AttendsKlass(ParamFrom = "2")]
        public ActionResult Details(int Id) {
			return View(new KlassDetailsViewModel(repo.GetSpecific(Id), repo.GetNonMembers(Id)));
		}

		[Authorize(Roles="Teacher")]
		public JsonResult RemoveKlassMember(int Id, string UId) {
			bool success = repo.RemoveKlassMember(Id, UId);
			return Json(new { Removed = success }, JsonRequestBehavior.AllowGet);
		}

        [AttendsKlass(ParamFrom = "2")]
        public JsonResult GetNonMembers(int Id) {
			return Json(repo.GetNonMembers(Id).Select(u => new UserViewModel(u)), JsonRequestBehavior.AllowGet);
		}

        [AttendsKlass(ParamFrom = "2")]
        public JsonResult GetMembers(int Id) {
			var klass = repo.GetSpecific(Id);
			if (klass == null) return null;
			else return Json(klass.Members.Select(u => new UserViewModel(u)), JsonRequestBehavior.AllowGet);
		}

		[Authorize(Roles="Teacher")]
		[HttpGet]
		public ActionResult Create() {
			return View();
		}

		[Authorize(Roles = "Teacher")]
		[HttpPost]
		public ActionResult Create(Klass model) {
			model.Schedule = new KlassSchedule();
			if (ModelState.IsValid) {
				repo.Add(model);
				return RedirectToAction("Index");
			}
			return View(model);
		}

		[Authorize(Roles="Teacher")]
		public JsonResult AddKlassMember(int Id, string UId) {

			bool success = repo.AddKlassMember(Id, UId);

			return Json(new { Added = success}, JsonRequestBehavior.AllowGet);
		}

		[Authorize(Roles="Teacher")]
		public ActionResult Delete(int ID) {
			repo.Delete(ID);
			return RedirectToAction("Index");
		}
	}
}