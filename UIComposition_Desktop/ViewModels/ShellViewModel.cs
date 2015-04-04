// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using DevExpress.Xpf.Docking;
using Microsoft.Practices.Prism.Commands;
using UIComposition.Shell.Merges;

namespace UIComposition.Shell.ViewModels
{
    public class ShellViewModel : INotifyPropertyChanged
    {

        public ShellViewModel()
        {
            // Initialize this ViewModel's commands.
            this.ExitCommand = new DelegateCommand<object>(this.AppExit, this.CanAppExit);

            AddDocument("Deal");
            AddDocument("Shipment");
        }


        #region ExitCommand

        public DelegateCommand<object> ExitCommand { get; private set; }


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





        ObservableCollection<PanelViewModel> _workspaces;
        public ObservableCollection<PanelViewModel> Workspaces
        {
            get
            {
                if (_workspaces == null)
                {
                    _workspaces = new ObservableCollection<PanelViewModel>();
                }
                return _workspaces;
            }
        }

        ICommand addPanelCommand;
        public ICommand AddPanelCommand
        {
            get
            {
                if (addPanelCommand == null)
                {
                    addPanelCommand = new DelegateCommand(AddPanel);
                }
                return addPanelCommand;
            }
        }

        void AddPanel()
        {
            PanelViewModel panelViewModel1 = new PanelViewModel();
            panelViewModel1.Content = "Panel View Model";
            panelViewModel1.DisplayName = "Panel View Model";
            this.Workspaces.Add(panelViewModel1);
        }


        ICommand addDocumentCommand;
        public ICommand AddDocumentCommand
        {
            get
            {
                if (addDocumentCommand == null)
                {
                    addDocumentCommand = new DelegateCommand(AddDocument);
                }
                return addDocumentCommand;
            }
        }

        void AddDocument()
        {
            var documentViewModel1 = new DocumentViewModel();
            documentViewModel1.Content = new DealMergeViewModel();
            documentViewModel1.DisplayName = "Deals";
            this.Workspaces.Add(documentViewModel1);
        }

        void AddDocument(string name)
        {
            var documentViewModel1 = new DocumentViewModel();

            switch (name)
            {
                case "Deal":
                    documentViewModel1.Content = new DealMergeViewModel();
                    documentViewModel1.DisplayName = name;
                    break;
                case "Shipment":
                    documentViewModel1.Content = new ShipmentMergeViewModel();
                    documentViewModel1.DisplayName = name;
                    break;
            }


            this.Workspaces.Add(documentViewModel1);
        }
        
        
    }

    public class DealMergeViewModel
    {

    }
    public class ShipmentMergeViewModel
    {

    }


    public class PanelViewModel : DependencyObject
    {
        public static readonly DependencyProperty DisplayNameProperty =
            DependencyProperty.Register("DisplayName", typeof(string), typeof(PanelViewModel), null);
        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(object), typeof(PanelViewModel), null);
        public PanelViewModel()
        {
            MVVMHelper.SetTargetName(this, "PanelHost");
        }
        public string DisplayName
        {
            get { return (string)GetValue(DisplayNameProperty); }
            set { SetValue(DisplayNameProperty, value); }
        }
        public object Content
        {
            get { return (object)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }
    }
    public class DocumentViewModel : PanelViewModel
    {
        public DocumentViewModel()
        {
            MVVMHelper.SetTargetName(this, "DocumentHost");
        }
    }
}