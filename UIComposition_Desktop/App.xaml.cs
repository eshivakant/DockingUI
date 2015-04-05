// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System.Linq;
using System.Windows;
using DevExpress.Xpf.Editors.Helpers;

namespace UIComposition.Shell
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            if(e.Args.Any())
                e.Args.ForEach(a=>GlobalVars.LoadedModules.Add(a));

            var bootstrapper = new UiCompositionBootstrapper();
            bootstrapper.Run();
        }
    }
}
