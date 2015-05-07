using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using EntityFrameworkDll;
using EntityFrameworkDll.DataModels;

namespace EF_ConsoleTest {
    class Program {
        static void Main(string[] args) {
            Console.ForegroundColor = ConsoleColor.Green;

            Database.SetInitializer( new DropCreateDatabaseAlways<MemberPhotosContext>());
           
            using (var ctx = new MemberPhotosContext()){

             
                Log("ready");
                Console.ReadLine();
            }
        }

        private static void Log(object msg){
            Console.WriteLine(msg);
        }

        private static void CreateDb(MemberPhotosContext ctx){
            var newMember = new Member{
                FirstName = "Nick",
                LastName = "Reshetinsky",
                Login = "NickResh777"
            };

            var photo = new Photo{
                Url = "http://google.com/drive/photo_id_332.jpg",
                Height = 400,
                Width = 400
            };

            newMember.Photos.Add(photo);


            photo = new Photo{
                Url = "http://facebook.com/make/photos/4433.jpg",
                Width = 400,
                Height = 600
            };

            newMember.Photos.Add(photo);

            ctx.Members.Add(newMember);
            ctx.SaveChanges();
        }
    }
}
