using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebPortal.BusinessLogic.DTOs;

namespace WebPortal.BusinessLogic {

    public class ServiceResponse{
        public bool IsSuccess{
            get; 
            set; 
        }

        public string Error{
            get; 
            set; 
        }
    }



    public class ServiceResponse<TDto> : ServiceResponse where TDto : class {

        public TDto Dto{
            get; 
            set; 
        }
    }
}
