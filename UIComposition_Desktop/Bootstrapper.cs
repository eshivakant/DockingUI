// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Windows;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.UnityExtensions;
using UIComposition.Shell.Views;

namespace UIComposition.Shell
{
    public class UiCompositionBootstrapper : UnityBootstrapper
    {

        protected override IModuleCatalog CreateModuleCatalog()
        {
            var catalog = new DirectoryModuleCatalog { ModulePath = @".\Modules" };
            return catalog;
        }


        protected override DependencyObject CreateShell()
        {
            

            // Use the container to create an instance of the shell.
            ShellView view = this.Container.TryResolve<ShellView>();
            return view;
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();
            App.Current.MainWindow = (Window)this.Shell;
            App.Current.MainWindow.Show();
        }
    }


    public class CustomDirectoryModuleCatalog : DirectoryModuleCatalog
    {
        protected override void InnerLoad()
        {
            base.InnerLoad();

            string[] folders = System.IO.Directory.GetDirectories(ModulePath, "*", System.IO.SearchOption.AllDirectories);

            foreach (string folder in folders)
            {
                ModulePath = folder;
                base.InnerLoad();
            }
        }
    }
}