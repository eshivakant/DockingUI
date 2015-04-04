using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;

namespace DealModule
{
    [Module(ModuleName = "DealModule")]
    public class DealModule:IModule
    {
        private readonly IUnityContainer _container;
        private readonly IRegionManager _regionManager;


        public DealModule(IRegionManager regionManager, IUnityContainer container)
        {
            this._container = container;
            this._regionManager = regionManager;
            

        }

        public void Initialize()
        {
      
            this._regionManager.RegisterViewWithRegion(RegionNames.DealRegion,
                                                       () => this._container.Resolve<DealMainView>());
        }
    }

    public static class RegionNames
    {
        public const string DealRegion = "DealRegion";
    }
}
