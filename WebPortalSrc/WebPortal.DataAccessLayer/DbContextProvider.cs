using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebPortal.DataAccessLayer {
    public class DbContextProvider : IDbContextProvider {
        public IDbContext CreateContext(){

            return new WebPortalDbContext();
        }
    }
}
