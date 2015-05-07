using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace WebPortal.Core.Interfaces {
    public interface IWorkingContext {
        int CurrentUserId{
            get; 
            set; 
        }


        bool IsAdmin{
            get; 
            set; 
        }
    }
}
