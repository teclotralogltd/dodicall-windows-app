﻿#pragma checksum "..\..\..\View\ViewContactDirectory.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "86E8EE406B21AF25B242FD0610D69E74"
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
using dodicall.Converter;
using dodicall.View;


namespace dodicall.View {
    
    
    /// <summary>
    /// ViewContactDirectory
    /// </summary>
    public partial class ViewContactDirectory : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 29 "..\..\..\View\ViewContactDirectory.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid GridContactDirectoryDitail;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\View\ViewContactDirectory.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid GridButtonBackToSearchResult;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\View\ViewContactDirectory.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid GridSearch;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\..\View\ViewContactDirectory.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TextBoxSearch;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\..\View\ViewContactDirectory.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock TextBlockSearch;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\..\View\ViewContactDirectory.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image ImageSearchInDodicall;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\..\View\ViewContactDirectory.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox ListBoxContact;
        
        #line default
        #line hidden
        
        
        #line 82 "..\..\..\View\ViewContactDirectory.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal dodicall.View.ViewProcess ViewProcessMain;
        
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
            System.Uri resourceLocater = new System.Uri("/dodicall;component/view/viewcontactdirectory.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\View\ViewContactDirectory.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
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
            this.GridContactDirectoryDitail = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.GridButtonBackToSearchResult = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            
            #line 35 "..\..\..\View\ViewContactDirectory.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ButtonBackToSearchResult_OnClick);
            
            #line default
            #line hidden
            return;
            case 4:
            this.GridSearch = ((System.Windows.Controls.Grid)(target));
            return;
            case 5:
            this.TextBoxSearch = ((System.Windows.Controls.TextBox)(target));
            
            #line 50 "..\..\..\View\ViewContactDirectory.xaml"
            this.TextBoxSearch.GotFocus += new System.Windows.RoutedEventHandler(this.TextBoxSearch_GotFocus);
            
            #line default
            #line hidden
            
            #line 50 "..\..\..\View\ViewContactDirectory.xaml"
            this.TextBoxSearch.LostFocus += new System.Windows.RoutedEventHandler(this.TextBoxSearch_LostFocus);
            
            #line default
            #line hidden
            return;
            case 6:
            this.TextBlockSearch = ((System.Windows.Controls.TextBlock)(target));
            
            #line 51 "..\..\..\View\ViewContactDirectory.xaml"
            this.TextBlockSearch.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.TextBlockSearch_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 7:
            this.ImageSearchInDodicall = ((System.Windows.Controls.Image)(target));
            return;
            case 8:
            this.ListBoxContact = ((System.Windows.Controls.ListBox)(target));
            
            #line 55 "..\..\..\View\ViewContactDirectory.xaml"
            this.ListBoxContact.SizeChanged += new System.Windows.SizeChangedEventHandler(this.ListBoxContact_SizeChanged);
            
            #line default
            #line hidden
            
            #line 56 "..\..\..\View\ViewContactDirectory.xaml"
            this.ListBoxContact.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ListBoxContact_OnSelectionChanged);
            
            #line default
            #line hidden
            return;
            case 10:
            this.ViewProcessMain = ((dodicall.View.ViewProcess)(target));
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 9:
            
            #line 70 "..\..\..\View\ViewContactDirectory.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ButtonSendRequest_OnClick);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

