using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using WebPortal.DataAccessLayer;
using WebPortal.Entities;
using WebPortal.Entities.Members;

namespace BaseTests
{
    class DbInitializer : DropCreateDatabaseAlways<WebPortalDbContext>{
        protected override void Seed(WebPortalDbContext context){
               PopulateMembers(context);
               PopulateMessages(context);

            context.SaveChanges();
        }

        private void PopulateMessages(WebPortalDbContext context){
            Message message;

            message = new Message{
                SenderId = 1,
                ReceiverId = 2,
                Body = "Hkfs'dfs",
                Header = "Greetings to you",
                IsRead = false,
                CreatedOn = DateTime.Now
            };

            context.Set<Message>().Add(message);
        }


        private void PopulateMembers(WebPortalDbContext context){
            Member member;
                        
            member = new Member{
                CreatedOn = DateTime.Now,
                FirstName = "Nick",
                LastName = "Reshetinsky",
                Email = "nickresh@gmail.com",
                NickName = "NickResh777",
                Gender = "M"
            };
            context.Set<Member>().Add(member);


            member = new Member{
                CreatedOn = DateTime.Now,
                FirstName = "Jake",
                LastName = "Catler",
                Email = "jcatler@gmail.com",
                NickName = "JCatler",
                Gender = "M"
            };
            context.Set<Member>().Add(member);
        }
    }
}
