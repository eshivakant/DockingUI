// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Ribbon;
using UIComposition.Shell.ViewModels;

namespace UIComposition.Shell.Views
{
    /// <summary>
    /// Interaction logic for ShellView.xaml
    /// </summary>
    public partial class ShellView : DXRibbonWindow
    {
        public ShellView(ShellViewModel vm)
        {
            InitializeComponent();

            this.DataContext = vm;


            this.Loaded += ShellView_Loaded;

        }

        void ShellView_Loaded(object sender, RoutedEventArgs e)
        {
            var screen = System.Windows.Forms.Screen.FromHandle(new WindowInteropHelper(this).Handle);

            Width = screen.Bounds.Width;
            Height = screen.Bounds.Height;

            Top = 0;
            Left = 0;

        }

        private void Timeline_OnCompleted(object sender, EventArgs e)
        {

            WindowState=WindowState.Maximized;
            
        }
    }
}
