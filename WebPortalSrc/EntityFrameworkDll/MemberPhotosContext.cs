using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using EntityFrameworkDll.DataModels;

namespace EntityFrameworkDll {
    public class MemberPhotosContext : DbContext{
        private DbSet<Member> _members;
        private DbSet<Photo> _photos;

        public MemberPhotosContext(): base("CoolDB"){
            
        }

        public DbSet<Member> Members{
            get{
                if (_members == null){
                    _members = base.Set<Member>();
                }
                return _members;
            }
        }

        public DbSet<Photo> Photos{
            get{
                if (_photos == null){
                    _photos = base.Set<Photo>();
                }

                return _photos;
            }
        } 

    }
}
