using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject.Activation;

namespace WebPortal.DataAccessLayer.IoCInjection
{
    class RepositoryProvider : IProvider{
        public object Create(IContext context){
            if (context.HasInferredGenericArguments){
                
            }

            return null;
        }

        public Type Type
        {
            get{
                return typeof (IRepository<>);
            }
        }
    }
}
