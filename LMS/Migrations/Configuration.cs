namespace LMS.Migrations
{
	using LMS.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
	using System.Web;

    internal sealed class Configuration : DbMigrationsConfiguration<LMS.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(LMS.Models.ApplicationDbContext context){

            var roleManager = new RoleManager<Microsoft.AspNet.Identity.EntityFramework.IdentityRole>(new RoleStore<IdentityRole>(context));
			if (!roleManager.RoleExists("Teacher")) {
				var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
				role.Name = "Teacher";
				roleManager.Create(role);
			}
			if (!roleManager.RoleExists("Student")) {
				var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
				role.Name = "Student";
				roleManager.Create(role);
			}
            if (!roleManager.RoleExists("Admin"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);
            }

            UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(
				new UserStore<ApplicationUser>(context));

            var admins = new List<ApplicationUser>(){
                new ApplicationUser { UserName = "admin@admin.com", Email = "admin@admin.com", LastName = "Administratör", FirstName = "(ej namngiven)", PhoneNumber ="070-3416061" }
            };

            var studs = new List<ApplicationUser>(){
				new ApplicationUser { UserName = "test2@test.com", Email = "test2@test.com", LastName = "Andersson", FirstName = "Bengt", PhoneNumber ="070-1234567"},
				new ApplicationUser { UserName = "stud2@test.com", Email = "stud2@test.com", LastName = "Bengtsson", FirstName = "Sandra", PhoneNumber ="070-1234568"},
				new ApplicationUser { UserName = "stud3@test.com", Email = "stud3@test.com", LastName = "Dahl", FirstName = "Lena", PhoneNumber ="070-1234569"},
				new ApplicationUser { UserName = "stud4@test.com", Email = "stud4@test.com", LastName = "Nilsson", FirstName = "Magnus", PhoneNumber ="070-1234570"},
				new ApplicationUser { UserName = "stud5@test.com", Email = "stud5@test.com", LastName = "Steen", FirstName = "Fredrik", PhoneNumber ="070-1234571"},
				new ApplicationUser { UserName = "stud6@test.com", Email = "stud6@test.com", LastName = "Åkerberg", FirstName = "Annika", PhoneNumber ="070-1234572"},
				new ApplicationUser { UserName = "stud7@test.com", Email = "stud7@test.com", LastName = "Hansson", FirstName = "Elin", PhoneNumber ="070-1234573"},
				new ApplicationUser { UserName = "stud8@test.com", Email = "stud8@test.com", LastName = "Miller", FirstName = "Petter", PhoneNumber ="070-1234574"}
			};

			var techs = new List<ApplicationUser>(){
				new ApplicationUser { UserName = "test1@test.com", Email = "test1@test.com", LastName = "Bergdahl", FirstName = "Calle", PhoneNumber ="070-1234575"},
				new ApplicationUser { UserName = "teach2@test.com", Email = "teach2@test.com", LastName = "Mickelsson", FirstName = "Margareta", PhoneNumber ="070-1234576"},
				new ApplicationUser { UserName = "teach3@test.com", Email = "teach3@test.com", LastName = "Österberg", FirstName = "Monica", PhoneNumber ="070-1234578"}
			};

            foreach (var a in admins)
            {
                var result = userManager.Create(a, "Admin@123");
                if (result.Succeeded)
                {
                    var user = userManager.FindByName(a.UserName);
                    userManager.AddToRole(user.Id, "Admin");
                }
            }

            foreach (var t in techs){
				var result = userManager.Create(t, "Password@123");
				if (result.Succeeded) {
					var user = userManager.FindByName(t.UserName);
					userManager.AddToRole(user.Id, "Teacher");
				}
			}

			foreach(var s in studs){
				var result = userManager.Create(s, "Password@123");
				if (result.Succeeded) {
					var user = userManager.FindByName(s.UserName);
					userManager.AddToRole(user.Id, "Student");
				}
			}

			List<SharedFile> SharedFilesSeed = new List<SharedFile>(){
				new SharedFile{ ID = 1, FileName = "crap.txt", ContentType = MimeMapping.GetMimeMapping("crap.txt"), Content = System.Text.Encoding.Unicode.GetBytes("the quick brown fuck you"), Uploader = userManager.FindByName(studs[0].UserName)},
				new SharedFile{ ID = 2, FileName = "crap2_crapHarder.txt", ContentType = MimeMapping.GetMimeMapping("crap2_crapHarder.txt"), Content = System.Text.Encoding.Unicode.GetBytes("the quick brown fucked the lazy crap"), Uploader = userManager.FindByName(studs[2].UserName)},
				new SharedFile{ ID = 3, FileName = "crap3.txt", ContentType = MimeMapping.GetMimeMapping("crap3.txt"), Content = System.Text.Encoding.Unicode.GetBytes("the third brown fuck you"), Uploader = userManager.FindByName(studs[1].UserName)},
				new SharedFile{ ID = 4, FileName = "crap4.txt", ContentType = MimeMapping.GetMimeMapping("crap4.txt"), Content = System.Text.Encoding.Unicode.GetBytes("the fourth brown fuck you"), Uploader = userManager.FindByName(studs[1].UserName)},
				new SharedFile{ ID = 5, FileName = "crap5.txt", ContentType = MimeMapping.GetMimeMapping("crap5.txt"), Content = System.Text.Encoding.Unicode.GetBytes("the something brown fuck you"), Uploader = userManager.FindByName(studs[0].UserName)},
				new SharedFile{ ID = 6, FileName = "crap6.txt", ContentType = MimeMapping.GetMimeMapping("crap6.txt"), Content = System.Text.Encoding.Unicode.GetBytes("the sixth brown fuck you"), Uploader = userManager.FindByName(techs[0].UserName)}
			};

			List<SubmissionFile> SubmissionFilesSeed = new List<SubmissionFile>(){
				new SubmissionFile{ ID = 1, FileName = "shit.txt", ContentType = MimeMapping.GetMimeMapping("shit.txt"), Content = System.Text.Encoding.Unicode.GetBytes("the quick brown fuck you"), Uploader = userManager.FindByName(studs[2].UserName)},
				new SubmissionFile{ ID = 2, FileName = "shit2_shitHarder.txt", ContentType = MimeMapping.GetMimeMapping("shit2_shitHarder.txt"), Content = System.Text.Encoding.Unicode.GetBytes("the quick brown fucked the lazy shit"), Uploader = userManager.FindByName(studs[3].UserName)},
				new SubmissionFile{ ID = 3, FileName = "shit3.txt", ContentType = MimeMapping.GetMimeMapping("shit3.txt"), Content = System.Text.Encoding.Unicode.GetBytes("the third brown fuck you"), Uploader = userManager.FindByName(studs[0].UserName)},
				new SubmissionFile{ ID = 4, FileName = "shit4.txt", ContentType = MimeMapping.GetMimeMapping("shit4.txt"), Content = System.Text.Encoding.Unicode.GetBytes("the fourth brown fuck you"), Uploader = userManager.FindByName(studs[0].UserName)},
				new SubmissionFile{ ID = 5, FileName = "shit5.txt", ContentType = MimeMapping.GetMimeMapping("shit5.txt"), Content = System.Text.Encoding.Unicode.GetBytes("the something brown fuck you"), Uploader = userManager.FindByName(studs[1].UserName)},
				new SubmissionFile{ ID = 6, FileName = "shit6.txt", ContentType = MimeMapping.GetMimeMapping("shit6.txt"), Content = System.Text.Encoding.Unicode.GetBytes("the sixth brown fuck you"), Uploader = userManager.FindByName(studs[1].UserName)}
			};

			var klasses = new List<Klass>() {
				new Klass{ID = 1, Name = "k1",
					Members = new List<ApplicationUser>(){
						userManager.FindByName(techs[0].UserName),
						userManager.FindByName(studs[2].UserName),
						userManager.FindByName(studs[0].UserName),
						userManager.FindByName(studs[5].UserName),
						userManager.FindByName(techs[1].UserName),
						userManager.FindByName(studs[7].UserName)
					},
					Shared = new List<SharedFile>(){SharedFilesSeed[1], SharedFilesSeed[4]},
					Submission = new List<SubmissionFile>(){SubmissionFilesSeed[4], SubmissionFilesSeed[3]}
				},
				new Klass{ID = 2, Name = "k2",
					Members = new List<ApplicationUser>(){
						userManager.FindByName(techs[1].UserName),
						userManager.FindByName(studs[2].UserName),
						userManager.FindByName(studs[7].UserName)
					},
					Shared = new List<SharedFile>(){SharedFilesSeed[2], SharedFilesSeed[0]},
					Submission = new List<SubmissionFile>(){SubmissionFilesSeed[1], SubmissionFilesSeed[5]}
				},
				new Klass{ID = 3, Name = "k3",
					Members = new List<ApplicationUser>(){
						userManager.FindByName(studs[4].UserName),
						userManager.FindByName(studs[0].UserName),
						userManager.FindByName(studs[3].UserName),
						userManager.FindByName(studs[7].UserName)
					},
					Shared = new List<SharedFile>(){SharedFilesSeed[3], SharedFilesSeed[5]},
					Submission = new List<SubmissionFile>(){SubmissionFilesSeed[0], SubmissionFilesSeed[2]}
				}
			};

			foreach (var k in klasses) context.Klasses.AddOrUpdate(k);

			context.SaveChanges();
        }
    }
}
