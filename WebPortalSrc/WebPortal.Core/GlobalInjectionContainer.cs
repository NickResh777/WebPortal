using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;
using Ninject.Modules;

namespace DatingHeaven.Core
{
    public class GlobalInjectionContainer
    {
        private GlobalInjectionContainer(){
            // hidden constructor to avoid object creation of the current class
        }

      

        private static readonly GlobalInjectionContainer _Instance = new GlobalInjectionContainer();

        private StandardKernel _nInjectKernel;

        public static GlobalInjectionContainer Instance{
            get{
                return _Instance;
            }
        }

        public void Load(params INinjectModule[] modules){
            // create the kernel
            _nInjectKernel = new StandardKernel(modules);
        }

        public T Get<T>() where T : class{
            if (_nInjectKernel == null){
                   // cannot get any registered type cause Kernel is not defined
                   throw new NullReferenceException("Injection Kernel is not defined!");
            }

            return _nInjectKernel.Get<T>();
        }
    }
}
