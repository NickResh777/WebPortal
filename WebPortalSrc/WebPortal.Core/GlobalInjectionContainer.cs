using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;
using Ninject.Modules;
using Ninject.Parameters;

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

        public void LoadModules(params INinjectModule[] modules){
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

        public object Get(Type serviceType){
            if (_nInjectKernel == null) {
                    // cannot get any registered type cause Kernel is not defined
                    throw new NullReferenceException("Injection Kernel is not defined!");
            }

            return _nInjectKernel.Get(serviceType);
        }
    }
}
