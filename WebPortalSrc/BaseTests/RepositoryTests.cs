using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using DatingHeaven.Core;
using NUnit.Framework;
using WebPortal.DataAccessLayer;
using WebPortal.DataAccessLayer.Infrastructure.EntityOperations;
using WebPortal.DataAccessLayer.IoCInjection;
using WebPortal.Entities;
using System.Data.Entity;

namespace BaseTests
{
    [TestFixture]
    public class RepositoryTests{
        private IRepository<Message> _repoMessages; 
        private GlobalInjectionContainer _container;
        private IEntityInfoResolver _resolver;

        [SetUp]
        public void Init(){
            Database.SetInitializer(new DbInitializer());
            _container = GlobalInjectionContainer.Instance;
            _container.LoadModules(new DalNInjectModule());
            _repoMessages = _container.Get<IRepository<Message>>();
        }

        [Test]
        public void repository_GetById(){
            Message result = _repoMessages.GetById(1);
            Assert.IsTrue(result != null);
            Assert.IsNull(result.Receiver, "Property [Receiver] should equal NULL");
            Assert.IsNull(result.Author, "Property [Author] should equal NULL");
        }

        [Test]
        public void repository_GetById_with_included_property(){
            Message message;

            message = _repoMessages.GetById(1, m => m.Author);
            Assert.IsNotNull(message);
            Assert.IsNotNull(message.Author);
            Assert.IsNull(message.Receiver);

            _repoMessages = _container.Get<IRepository<Message>>();
            message = _repoMessages.GetById(1, m => m.Receiver);
            Assert.IsNotNull(message);
            Assert.IsNotNull(message.Receiver);
            Assert.IsNull(message.Author);
        }

        [Test]
        public void repository_GetWhere(){
            var result = _repoMessages.GetWhere(m => (m.ReceiverId < 5));
            Assert.IsTrue(result.Count > 0);
            Assert.IsTrue(result.All(m => m.ReceiverId < 5));
            Assert.IsTrue(result.All(m => (m.Author == null)));
            Assert.IsTrue(result.All(m => (m.Receiver == null)));
        }

        [Test]
        public void repository_GetWhere_with_included_properties(){
            var result = _repoMessages.GetWhere(m => (m.ReceiverId < 5), m => m.Author, m => m.Receiver);
            Assert.IsTrue(result.Count > 0);
            Assert.IsTrue(result.All(m => m.ReceiverId < 5));
            Assert.IsTrue(result.All(m => (m.Author != null)));
            Assert.IsTrue(result.All(m => (m.Receiver != null)));
        }


    }
}
