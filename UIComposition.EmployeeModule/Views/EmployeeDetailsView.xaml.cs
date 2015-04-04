// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System.Windows.Controls;
using Microsoft.Practices.Prism.Regions;
using UIComposition.EmployeeModule.Models;
using UIComposition.EmployeeModule.ViewModels;

namespace UIComposition.EmployeeModule.Views
{
    public partial class EmployeeDetailsView : UserControl
    {
        public EmployeeDetailsView(EmployeeDetailsViewModel employeeDetailsViewModel)
        {
            this.InitializeComponent();

          
        }
    }
}