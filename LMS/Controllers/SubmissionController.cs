﻿using LMS.Models;
using LMS.Repositories;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LMS.Controllers
{
	//[Authorize(Roles="Teacher")]
	//todo här ska studeter inte komma åt heka filecontroller.
	[Authorize(Roles = "Teacher")]
    public class SubmissionController : FileController<SubmissionFile>{

		//public ActionResult Create()
		//{
		//	//repo.GetKlassName
		//	return View();
		//}
		//[Authorize(Roles="Student")]
		public ActionResult Submit()
		{
			var klassRepo = new KlassRepository();
			var klasses = klassRepo.GetMyClasses(User.Identity.GetUserId()).Select(k => new SelectListItem { Value = k.ID.ToString(), Text = k.Name });
			var model = new UploadFileViewModel();
            model.KlassList = klasses;

            //var repo = new AccessRepository();
            //model.KlassList = repo.GetKlassesForUser(User.Identity.GetUserId());
			//var klassRepo = new KlassRepository();
			//var klasses = klassRepo.GetMyClasses(User.Identity.GetUserId())/*.Select(k => new SelectListItem { Value = k.ID.ToString(), Text = k.Name });*/	;
			//var model = new UploadFileViewModel();
			//model.KlassList = klasses.Select(k => new SelectListItem { Value = k.ID.ToString(), Text = k.Name });
			//model.Files = klasses.SelectMany(k => k.Shared).ToList();
			//var repo = new AccessRepository();
			//model.KlassList = repo.GetKlassesForUser(User.Identity.GetUserId());
            return View("Submit", model);
		}

		

		//private IEnumerable<SelectListItem> GetKlassesForDropdown<Tkey, Tvalue>(IEnumerable<Tkey, Tvalue> elements)
		//{
		//	//denna ska ta en lista på modeller och skapa en selectlist av dem. 
		//	var selectList = new List<SelectListItem>();
		//	foreach(var element in elements) {
		//		selectList.Add( new SelectListItem { Value = element, Text = element });
		//	}
		//	return selectList;
		//}
		
		//[HttpPost]
		//public ActionResult Submit()
		//{
		//	return View();
		//}

		public ActionResult Incoming() {
			var asdf = new KlassRepository()
				.GetMyClasses(User.Identity.GetUserId())
				.SelectMany(k => k.Submission);
			return View(asdf);
		}
    }
}