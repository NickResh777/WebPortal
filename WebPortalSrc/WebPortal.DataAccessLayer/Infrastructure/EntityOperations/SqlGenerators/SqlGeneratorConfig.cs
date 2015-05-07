using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebPortal.DataAccessLayer.Infrastructure.EntityOperations.SqlGenerators {
    public class SqlGeneratorConfig {

        public string ParameterSymbol{
            get{
                return "@p";
            }
        }
    }
}
