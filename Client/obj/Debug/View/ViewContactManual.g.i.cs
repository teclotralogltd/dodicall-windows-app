﻿#pragma checksum "..\..\..\View\ViewContactManual.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "5D9878CFC4A15133B76845BFB573FFD4"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
using dodicall.View;


namespace dodicall.View {
    
    
    /// <summary>
    /// ViewContactManual
    /// </summary>
    public partial class ViewContactManual : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 46 "..\..\..\View\ViewContactManual.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TextBoxFirstName;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\..\View\ViewContactManual.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel StackPanelNumberPhone;
        
        #line default
        #line hidden
        
        
        #line 71 "..\..\..\View\ViewContactManual.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonOk;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/dodicall;component/view/viewcontactmanual.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\View\ViewContactManual.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.TextBoxFirstName = ((System.Windows.Controls.TextBox)(target));
            
            #line 46 "..\..\..\View\ViewContactManual.xaml"
            this.TextBoxFirstName.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.FirstLastName_OnPreviewTextInput);
            
            #line default
            #line hidden
            
            #line 46 "..\..\..\View\ViewContactManual.xaml"
            this.TextBoxFirstName.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.FirstLastName_OnTextChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 48 "..\..\..\View\ViewContactManual.xaml"
            ((System.Windows.Controls.TextBox)(target)).PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.FirstLastName_OnPreviewTextInput);
            
            #line default
            #line hidden
            
            #line 48 "..\..\..\View\ViewContactManual.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.FirstLastName_OnTextChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.StackPanelNumberPhone = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 4:
            this.ButtonOk = ((System.Windows.Controls.Button)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

