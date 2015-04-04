// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.Practices.Prism.Commands;
using UIComposition.Shell.Merges;

namespace UIComposition.Shell.ViewModels
{
    public class ShellViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<string> _modules;// = new ObservableCollection<string>();

        public ShellViewModel()
        {
            // Initialize this ViewModel's commands.
            this.ExitCommand = new DelegateCommand<object>(this.AppExit, this.CanAppExit);

            _modules = new ObservableCollection<string>();
            _modules.Add("DealMerge");
            _modules.Add("ShipmentMerge");
        }


        #region ExitCommand

        public DelegateCommand<object> ExitCommand { get; private set; }

        public ObservableCollection<string> Modules
        {
            get { return _modules; }
            set { _modules = value; NotifyPropertyChanged("Modules"); }
        }

        private void AppExit(object commandArg)
        {
        }

        private bool CanAppExit(object commandArg)
        {
            return true;
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}