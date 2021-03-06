﻿using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using System;

namespace LMS.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
		public virtual ICollection<Klass> Klasses { get; set; }
		public virtual ICollection<SubmissionFile> SubmittedFiles { get; set; }	//only student should have this
		public virtual ICollection<SharedFile> SharedFiles { get; set; }
		public virtual ICollection<Comment> SubmittedComments { get; set; }	//only teacher should have this

		public IEnumerable<Comment> Comments {
			get {
				return SubmittedFiles
					.Where(f => f.Comments.Count != 0)
					.SelectMany(x => x.Comments);
			}
		}
		public IEnumerable<Comment> UnreadComments { get { return Comments.Where(c => !c.Read); } }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public string GetUserRoles()
        {
            return this.Roles.ToString(); //GetRolesForUser(appUser.UserName);
        }

		public string GetFullName()
		{
			return string.Format("{0} {1}",FirstName,LastName);
		}
	}

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser> {
        public ApplicationDbContext() : base("DefaultConnection", throwIfV1Schema: false) { }

		public DbSet<Klass> Klasses { get; set; }
		public DbSet<SharedFile> SharedFiles { get; set; }
		public DbSet<SubmissionFile> SubmissionFiles { get; set; }
		public DbSet<Comment> Comments { get; set; }
		public DbSet<KlassSchedule> Schedules { get; set; }
		public DbSet<Lecture> Lectures { get; set; }

        public static ApplicationDbContext Create() {
            return new ApplicationDbContext();
        }
    }
}