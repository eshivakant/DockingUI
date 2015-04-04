using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;

namespace UIComposition.DealModule
{
    [Module(ModuleName = "DealModule")]
    public class DealModule:IModule
    {
        private readonly IUnityContainer _container;
        private readonly IRegionManager _regionManager;


        public DealModule(IRegionManager regionManager)
        {
            //this._container = container;
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
