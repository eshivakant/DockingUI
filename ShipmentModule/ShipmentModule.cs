using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;

namespace ShipmentModule
{
    [Module(ModuleName = "ShipmentModule")]
    public class ShipmentModule:IModule
    {
        private readonly IUnityContainer _container;
        private readonly IRegionManager _regionManager;


        public ShipmentModule(IRegionManager regionManager, IUnityContainer container)
        {
            this._container = container;
            this._regionManager = regionManager;

        }

        public void Initialize()
        {

            this._regionManager.RegisterViewWithRegion(RegionNames.ShipmentRegion,
                                                       () => this._container.Resolve<ShipmentMainView>());
        }
    }

    public static class RegionNames
    {
        public const string ShipmentRegion = "ShipmentRegion";
    }
}
