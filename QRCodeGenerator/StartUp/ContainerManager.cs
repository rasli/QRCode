using QRGenerator.ViewModels;
using Service.Interfaces;
using System;
using Unity;

namespace QRGenerator.StartUp
{

    public static class ContainerManager
    {
        private static Lazy<IUnityContainer> container =
            new Lazy<IUnityContainer>(() =>
            {
                var container = new UnityContainer();
                RegisterType(container);
                return container;
            });
        public static T Resolve<T>(string name = null)
        {
            return container.Value.Resolve<T>(name);
        }
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        public static void RegisterType(IUnityContainer container)
        {
            container
                
                .RegisterType<IQRCodeGenerator,Service.Implementation.QRCodeGenerator>()
                
                .RegisterType<MainViewModel>()
               .RegisterType<LoginViewModel>()
               .RegisterType<QRGeneratorViewModel>()
               .RegisterType<ViewManager>()
                ;
        }
        

    }
}
