using System.Diagnostics;
using WebPortal.DataAccessLayer.Infrastructure.EntityOperations.SqlGenerators;

namespace WebPortal.DataAccessLayer.Infrastructure{
    [DebuggerDisplay("{Column} {Operator} {Value}")]
    public class WhereCondition : IWhereConditionRoot{
        public string Column{
            get; 
            set;
        }

        public Comparison Operator{
            get; 
            set;
        }

        public object Value{
            get; 
            set; 
        }

        public int ParameterIndex{
            get; 
            set; 
        }
    }
}
    