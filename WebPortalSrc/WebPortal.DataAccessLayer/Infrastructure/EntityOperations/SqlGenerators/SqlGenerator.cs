using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebPortal.DataAccessLayer.Infrastructure.EntityOperations.SqlGenerators {
    public abstract class SqlGenerator{
        /// <summary>
        /// Generate the SQL text
        /// </summary>
        public abstract string GenerateSql();
    }
}
