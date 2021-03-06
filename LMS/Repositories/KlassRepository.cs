﻿using LMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace LMS.Repositories {
	public class KlassRepository {
		private ApplicationDbContext ctx = new ApplicationDbContext();

		public IEnumerable<Klass> GetAll() {
			return ctx.Klasses;
		}

        /* Returnera de klasser nuvarande användare är med i */
        public IEnumerable<Klass> GetMyClasses(string userId)
        {
            ApplicationUser appUser = ctx.Users.SingleOrDefault(u => u.Id == userId);
            return appUser.Klasses;
        }

        public Klass GetSpecific(int Id) {
			return ctx.Klasses.SingleOrDefault(k => k.ID == Id);
		}

		public void Add(Klass klass) {
			ctx.Klasses.Add(klass);
			ctx.SaveChanges();
		}

		//get users that are not members of klass
		public IEnumerable<ApplicationUser> GetNonMembers(int Id){
			var klass = ctx.Klasses.SingleOrDefault(k => k.ID == Id);
			if (klass == null) return null;
			else return klass.Members.Aggregate(
				(IQueryable<ApplicationUser>) ctx.Users,
				(nonMembers, member) => nonMembers.Where(user => user.Id != member.Id));
		}

		public bool AddKlassMember(int Id, string UId) {
			var klass = ctx.Klasses.SingleOrDefault(k => k.ID == Id);
			var member = ctx.Users.SingleOrDefault(u => u.Id == UId);
			if (klass != null && member != null && !klass.Members.Any(u => u.Id == UId)) {
				klass.Members.Add(member);
				ctx.SaveChanges();
				return true;
			}
			return false;
		}

		public bool RemoveKlassMember(int Id, string UId) {
			var klass = ctx.Klasses.SingleOrDefault(k => k.ID == Id);
			if (klass != null) {
				var member = klass.Members.SingleOrDefault(m => m.Id == UId);
				if (member != null) {
					klass.Members.Remove(member);
					ctx.SaveChanges();
					return true;
				}
			}
			return false;
		}

		public bool Delete(int ID) {
			var klass = ctx.Klasses.SingleOrDefault(k => k.ID == ID);
			if (klass != null) {
				var lectures = klass.Schedule.Lectures.ToList();
				foreach (var lecture in lectures)
					ctx.Lectures.Remove(lecture);
				ctx.Schedules.Remove(klass.Schedule);
				ctx.Klasses.Remove(klass);
				ctx.SaveChanges();
				return true;
			}
			return false;
		}
	}
}