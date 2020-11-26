using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus.DataSets;
using Project.DAL.ContextClasses;
using Project.ENTITIES.Models;

namespace Project.DAL.StratgeyPattern
{
    class MyInit: CreateDatabaseIfNotExists<MyContext>
    {
        protected override void Seed(MyContext context)
        {
            



            for (int i = 0; i < 10; i++)
            {
                AppUser ap = new AppUser();
                ap.UserName = new Internet("tr").UserName();
                ap.Password = new Internet("tr").Password();
                ap.Email = new Internet("tr").Email();

                context.AppUsers.Add(ap);

            }
            context.SaveChanges();

            for (int i = 2; i < 12; i++)
            {
                UserProfile up = new UserProfile();
                up.ID = i;
                up.FirstName = new Name("tr").FirstName();
                up.LastName = new Name("tr").LastName();
                
                context.UserProfiles.Add(up);

            }

            context.SaveChanges();


            for (int i = 0; i < 10; i++)
            {
                Category c = new Category();
                c.CategoryName = new Commerce("tr").Categories(1)[0];
                c.Description = new Lorem("tr").Sentence(10);
                c.ImagePath = new Images().Nightlife();
                for (int j = 0; j < 15; j++)
                {
                    Article a = new Article();
                    a.ArticleHeader = new Lorem("tr").Sentence(2);
                    a.ArticleContent = new Lorem("tr").Sentence(10);


                    c.Articles.Add(a);
                }

                context.Categories.Add(c);
                context.SaveChanges();

                

            }
        }

    }
}
