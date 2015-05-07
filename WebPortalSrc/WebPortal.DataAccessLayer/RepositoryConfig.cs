using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebPortal.DataAccessLayer {
    public class RepositoryConfig {
        public bool RetryIfFailed{
            get; 
            set; 
        }


        public int MaxRetryCount{
            get; 
            set; 
        }

        public int RetryTimeout{
            get; 
            set; 
        }
    }
}
