﻿#pragma checksum "..\..\..\MainWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1EFEE4D4522B2AC1CAB6B21E08E2E2D2362EA3FF"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using GoldRush;
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


namespace GoldRush {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 9 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal GoldRush.MainWindow mainWindow;
        
        #line default
        #line hidden
        
        
        #line 95 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border inputGrid;
        
        #line default
        #line hidden
        
        
        #line 129 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label selectedFileLabel;
        
        #line default
        #line hidden
        
        
        #line 133 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton rbBFS;
        
        #line default
        #line hidden
        
        
        #line 134 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton rbDFS;
        
        #line default
        #line hidden
        
        
        #line 142 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider animationSlider;
        
        #line default
        #line hidden
        
        
        #line 168 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border visualizeGrid;
        
        #line default
        #line hidden
        
        
        #line 205 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid mazeGrid;
        
        #line default
        #line hidden
        
        
        #line 208 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.ToggleButton showSolutionToggle;
        
        #line default
        #line hidden
        
        
        #line 235 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button stepButton;
        
        #line default
        #line hidden
        
        
        #line 254 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid solutionPanel;
        
        #line default
        #line hidden
        
        
        #line 258 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label routeLabel;
        
        #line default
        #line hidden
        
        
        #line 262 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label numOfNodesLabel;
        
        #line default
        #line hidden
        
        
        #line 268 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label stepsLabel;
        
        #line default
        #line hidden
        
        
        #line 272 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label executionTimeLabel;
        
        #line default
        #line hidden
        
        
        #line 279 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.Popup errorPopup;
        
        #line default
        #line hidden
        
        
        #line 281 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock popUpText;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.4.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/GoldRush;component/mainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\MainWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.4.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.mainWindow = ((GoldRush.MainWindow)(target));
            return;
            case 2:
            this.inputGrid = ((System.Windows.Controls.Border)(target));
            return;
            case 3:
            
            #line 111 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.selectedFileLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.rbBFS = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 6:
            this.rbDFS = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 7:
            this.animationSlider = ((System.Windows.Controls.Slider)(target));
            return;
            case 8:
            
            #line 145 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Visualize);
            
            #line default
            #line hidden
            return;
            case 9:
            this.visualizeGrid = ((System.Windows.Controls.Border)(target));
            return;
            case 10:
            
            #line 184 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.showInput);
            
            #line default
            #line hidden
            return;
            case 11:
            this.mazeGrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 12:
            this.showSolutionToggle = ((System.Windows.Controls.Primitives.ToggleButton)(target));
            
            #line 209 "..\..\..\MainWindow.xaml"
            this.showSolutionToggle.Checked += new System.Windows.RoutedEventHandler(this.showSolution);
            
            #line default
            #line hidden
            
            #line 209 "..\..\..\MainWindow.xaml"
            this.showSolutionToggle.Unchecked += new System.Windows.RoutedEventHandler(this.showMaze);
            
            #line default
            #line hidden
            return;
            case 13:
            this.stepButton = ((System.Windows.Controls.Button)(target));
            
            #line 235 "..\..\..\MainWindow.xaml"
            this.stepButton.Click += new System.Windows.RoutedEventHandler(this.showSteps);
            
            #line default
            #line hidden
            return;
            case 14:
            this.solutionPanel = ((System.Windows.Controls.Grid)(target));
            return;
            case 15:
            this.routeLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 16:
            this.numOfNodesLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 17:
            this.stepsLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 18:
            this.executionTimeLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 19:
            this.errorPopup = ((System.Windows.Controls.Primitives.Popup)(target));
            return;
            case 20:
            this.popUpText = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

