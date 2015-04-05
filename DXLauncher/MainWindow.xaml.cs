using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DevExpress.Mvvm;
using DevExpress.Xpf.Core;


namespace DXLauncher
{
    public partial class MainWindow : DXWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainVM();
        }

    }



    public class MainVM:INotifyPropertyChanged
    {
        private const string Path = @"C:\Prism\Quickstarts\UIComposition_Desktop\UIComposition_Desktop\bin\Debug\UIComposition.Shell.exe";

        private ICommand _launchDeals;

        public ICommand LaunchDeals
        {
            get { return _launchDeals; }
            set { _launchDeals = value; OnPropertyChanged("LaunchDeals"); }
        }

        public ICommand LaunchShipments
        {
            get { return _launchShipments; }
            set { _launchShipments = value; OnPropertyChanged("LaunchShipments");}
        }

        private static void LaunchDeals_Execute()
        {
            var param = "Deal";
            System.Diagnostics.Process.Start(Path, param);

        }

        private ICommand _launchShipments;

        private static void LaunchShipments_Execute()
        {
            var param = "Shipment";
            System.Diagnostics.Process.Start(Path, param);
        }

        public MainVM()
        {
            _launchDeals = new DelegateCommand(LaunchDeals_Execute);
            _launchShipments = new DelegateCommand(LaunchShipments_Execute);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

    }



}
