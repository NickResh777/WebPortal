using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using DatingHeaven.Core;
using NUnit.Framework;
using WebPortal.DataAccessLayer;
using WebPortal.DataAccessLayer.IoCInjection;
using WebPortal.Entities;
using System.Data.Entity;

namespace BaseTests
{
    [TestFixture]
    public class RepositoryTests{
        private IRepository<Message> _repoMessages; 
        private GlobalInjectionContainer _container;

        [SetUp]
        public void Init(){
            Database.SetInitializer(new DropCreateDatabaseAlways<WebPortalDbContext>());
            _container = GlobalInjectionContainer.Instance;
            _container.LoadModules(new DalNInjectModule());
            _repoMessages = _container.Get<IRepository<Message>>();
        }

        [Test]
        public void test_method_GetById(){
            Message message = _repoMessages.GetById(1);
        }


    }
}
