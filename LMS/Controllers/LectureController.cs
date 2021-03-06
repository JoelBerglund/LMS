﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LMS.Models;
using LMS.Repositories;

namespace LMS.Controllers
{
    public class LectureController : Controller {
		private LectureRepository repo = new LectureRepository();
		private string defaultReturnUrl {
			get {
				return this.Url.Action("MySchedule", "Schedule", new { }, this.Request.Url.Scheme);
			}
		}


		//TODO by klass authentication
        public ActionResult Lecture(int ID, string rUrl){
			ViewBag.rUrl = rUrl ?? defaultReturnUrl;
            return View(repo.get(ID));
        }

		[HttpGet, Authorize(Roles="Teacher")]
		public ActionResult Edit(int ID, string rUrl) {
			ViewBag.rUrl = rUrl ?? defaultReturnUrl;
			return View(repo.get(ID));
		}

		[HttpPost, ValidateAntiForgeryToken]
		public ActionResult Edit(Lecture lecture, string rUrl) {
			ViewBag.rUrl = rUrl ?? defaultReturnUrl;
			if (ModelState.IsValid && repo.Update(lecture)) return Redirect(ViewBag.rUrl);
			return Redirect(ViewBag.rUrl);
		}

		[HttpGet, Authorize(Roles = "Teacher")]
		public ActionResult Create(int sID, string rUrl) {
			ViewBag.rUrl = rUrl ?? defaultReturnUrl;
            var lect = new Lecture { ScheduleID = sID };
            lect.Start = DateTime.Now;
            lect.End = DateTime.Now.AddHours(1);
            return View(lect);
		}

		[HttpPost, ValidateAntiForgeryToken]
		public ActionResult Create(Lecture lecture, string rUrl) {
			ViewBag.rUrl = rUrl ?? defaultReturnUrl;
			if (ModelState.IsValid && repo.Add(lecture)) return Redirect(ViewBag.rUrl);
			return View(lecture);
		}

		[HttpGet, Authorize(Roles = "Teacher")]
		public ActionResult Remove(int ID, string rUrl) {
			ViewBag.rUrl = rUrl ?? defaultReturnUrl;
			return View(repo.get(ID));
		}

		[HttpPost, ValidateAntiForgeryToken, ActionName("Remove")]
		public ActionResult RemoveConfirmed(int ID, string rUrl) {
			ViewBag.rUrl = rUrl ?? defaultReturnUrl;
			repo.Delete(ID);
			return Redirect(ViewBag.rUrl);
		}
    }
}