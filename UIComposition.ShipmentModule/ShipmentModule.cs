using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;

namespace UIComposition.ShipmentModule
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
