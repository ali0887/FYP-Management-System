﻿#pragma checksum "..\..\..\SupervisorRegistration.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8D9A476C5849DC1D3EA652DEAB1ADF4ABDE7C3F2"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MahApps.Metro;
using MahApps.Metro.Accessibility;
using MahApps.Metro.Actions;
using MahApps.Metro.Automation.Peers;
using MahApps.Metro.Behaviors;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Converters;
using MahApps.Metro.IconPacks;
using MahApps.Metro.IconPacks.Converter;
using MahApps.Metro.Markup;
using MahApps.Metro.Theming;
using MahApps.Metro.ValueBoxes;
using Project_1;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace Project_1 {
    
    
    /// <summary>
    /// SupervisorRegistration
    /// </summary>
    public partial class SupervisorRegistration : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 83 "..\..\..\SupervisorRegistration.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox name;
        
        #line default
        #line hidden
        
        
        #line 93 "..\..\..\SupervisorRegistration.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox roll1;
        
        #line default
        #line hidden
        
        
        #line 103 "..\..\..\SupervisorRegistration.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox roll2;
        
        #line default
        #line hidden
        
        
        #line 113 "..\..\..\SupervisorRegistration.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox roll3;
        
        #line default
        #line hidden
        
        
        #line 123 "..\..\..\SupervisorRegistration.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox mission;
        
        #line default
        #line hidden
        
        
        #line 130 "..\..\..\SupervisorRegistration.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.Popup popup;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.3.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Project 1;component/supervisorregistration.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\SupervisorRegistration.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.3.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 47 "..\..\..\SupervisorRegistration.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.TDashboard);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 48 "..\..\..\SupervisorRegistration.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.TRegistration);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 62 "..\..\..\SupervisorRegistration.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_Out);
            
            #line default
            #line hidden
            return;
            case 4:
            this.name = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.roll1 = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.roll2 = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.roll3 = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.mission = ((System.Windows.Controls.TextBox)(target));
            return;
            case 9:
            
            #line 128 "..\..\..\SupervisorRegistration.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_Add);
            
            #line default
            #line hidden
            return;
            case 10:
            this.popup = ((System.Windows.Controls.Primitives.Popup)(target));
            return;
            case 11:
            
            #line 134 "..\..\..\SupervisorRegistration.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.PopUpClose);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

