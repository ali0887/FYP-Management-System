﻿#pragma checksum "..\..\..\SupervisorRegistration.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "AB48E2FD4182F8C50B7C4804B51375DC6858509D"
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
        
        
        #line 85 "..\..\..\SupervisorRegistration.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox name;
        
        #line default
        #line hidden
        
        
        #line 95 "..\..\..\SupervisorRegistration.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox roll1;
        
        #line default
        #line hidden
        
        
        #line 105 "..\..\..\SupervisorRegistration.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox roll2;
        
        #line default
        #line hidden
        
        
        #line 115 "..\..\..\SupervisorRegistration.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox roll3;
        
        #line default
        #line hidden
        
        
        #line 125 "..\..\..\SupervisorRegistration.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox mission;
        
        #line default
        #line hidden
        
        
        #line 132 "..\..\..\SupervisorRegistration.xaml"
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
            
            #line 49 "..\..\..\SupervisorRegistration.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.TLibrary);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 50 "..\..\..\SupervisorRegistration.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.TTeams);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 64 "..\..\..\SupervisorRegistration.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_Out);
            
            #line default
            #line hidden
            return;
            case 6:
            this.name = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.roll1 = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.roll2 = ((System.Windows.Controls.TextBox)(target));
            return;
            case 9:
            this.roll3 = ((System.Windows.Controls.TextBox)(target));
            return;
            case 10:
            this.mission = ((System.Windows.Controls.TextBox)(target));
            return;
            case 11:
            
            #line 130 "..\..\..\SupervisorRegistration.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_Add);
            
            #line default
            #line hidden
            return;
            case 12:
            this.popup = ((System.Windows.Controls.Primitives.Popup)(target));
            return;
            case 13:
            
            #line 136 "..\..\..\SupervisorRegistration.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.PopUpClose);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

