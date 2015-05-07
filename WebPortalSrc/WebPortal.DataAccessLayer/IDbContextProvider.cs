using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebPortal.DataAccessLayer {
    public interface IDbContextProvider{
        /// <summary>
        /// Create a DB context
        /// </summary>
        IDbContext CreateContext();
    }
}
