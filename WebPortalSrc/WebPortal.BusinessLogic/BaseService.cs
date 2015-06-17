using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebPortal.DataAccessLayer;
using WebPortal.DataAccessLayer.Infrastructure.EntityOperations;

namespace WebPortal.BusinessLogic {
    public abstract class BaseService{
        private readonly IEntityOperationsProvider _entityOperationsProvider;

        protected BaseService(){
            // empty constructor
        }

        protected BaseService(IEntityOperationsProvider entityContextProvider){
            _entityOperationsProvider = entityContextProvider;         
        }

        protected IEntityOperationsProvider EntityOperations{
            get{
                return _entityOperationsProvider;
            }
        }
    }
}
