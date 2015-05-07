using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebPortal.DataAccessLayer;
using WebPortal.DataAccessLayer.Infrastructure.EntityOperations;

namespace WebPortal.BusinessLogic {
    public abstract class BaseService{
        private readonly IEntityContextProvider _entityContextProvider;

        protected BaseService(){
            // empty constructor
        }

        protected BaseService(IEntityContextProvider entityContextProvider){
            _entityContextProvider = entityContextProvider;
            
        }

        protected IEntityContextProvider EntityOperations{
            get{
                return _entityContextProvider;
            }
        }
    }
}
