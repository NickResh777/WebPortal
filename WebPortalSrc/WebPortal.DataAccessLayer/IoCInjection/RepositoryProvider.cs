using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;
using Ninject.Activation;
using WebPortal.DataAccessLayer.Infrastructure.EntityOperations;
using WebPortal.DataAccessLayer.Repositories;
using WebPortal.Entities;

namespace WebPortal.DataAccessLayer.IoCInjection
{
    class RepositoryProvider : IProvider{
        public object Create(IContext context){
            Type entityType = context.GenericArguments[0];

            if (typeof (BaseBusinessEntityWithId).IsAssignableFrom(entityType)){
                // for entities having the property [Id] as key (BaseBusinessEntityWithId)
                Type efSingleIdRepoType = typeof (EfSingleIdRepository<>);
                Type genericRepoType = efSingleIdRepoType.MakeGenericType(entityType);
                var result = Activator.CreateInstance(genericRepoType, context.Kernel.Get<IDbContext>());
                return result;
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
