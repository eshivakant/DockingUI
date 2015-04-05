// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using DevExpress.Xpf.Docking;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Unity;
using UIComposition.Shell.Merges;

namespace UIComposition.Shell.ViewModels
{
    public class ShellViewModel : INotifyPropertyChanged
    {
        private readonly IUnityContainer _unityContainer;

        public ShellViewModel(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
            // Initialize this ViewModel's commands.
            this.ExitCommand = new DelegateCommand<object>(this.AppExit, this.CanAppExit);

            GlobalVars.LoadedModules.ForEach(AddDocument);
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

        ICommand _addPanelCommand;
        public ICommand AddPanelCommand
        {
            get
            {
                if (_addPanelCommand == null)
                {
                    _addPanelCommand = new DelegateCommand(AddPanel);
                }
                return _addPanelCommand;
            }
        }

        void AddPanel()
        {
            var panelViewModel1 = new PanelViewModel
            {
                Content = "Panel View Model", DisplayName = "Panel View Model"
            };

            this.Workspaces.Add(panelViewModel1);
        }


        ICommand _addDocumentCommand;
        public ICommand AddDocumentCommand
        {
            get
            {
                if (_addDocumentCommand == null)
                {
                    _addDocumentCommand = new DelegateCommand<string>(AddDocument);
                }
                return _addDocumentCommand;
            }
        }

        void AddDocument()
        {
           
        }

        void AddDocument(string name)
        {
            if(Workspaces.Any(w=>w.DisplayName==name)) return;
            

            var factory = _unityContainer.Resolve<ViewMergeFactory>();
            this.Workspaces.Add(factory.GetDocumentViewModel(name));
        }
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