﻿#pragma checksum "..\..\..\View\ViewChatRedirect.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "D9241E4CEB113EE445A2C04CB8FA54BE"
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
    /// ViewChatRedirect
    /// </summary>
    public partial class ViewChatRedirect : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 23 "..\..\..\View\ViewChatRedirect.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image ImageContact;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\View\ViewChatRedirect.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle RectangleContacts;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\..\View\ViewChatRedirect.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image ImageChat;
        
        #line default
        #line hidden
        
        
        #line 74 "..\..\..\View\ViewChatRedirect.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle RectangleChat;
        
        #line default
        #line hidden
        
        
        #line 90 "..\..\..\View\ViewChatRedirect.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TextBoxSearch;
        
        #line default
        #line hidden
        
        
        #line 91 "..\..\..\View\ViewChatRedirect.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock TextBlockSearch;
        
        #line default
        #line hidden
        
        
        #line 94 "..\..\..\View\ViewChatRedirect.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox ListBoxContact;
        
        #line default
        #line hidden
        
        
        #line 152 "..\..\..\View\ViewChatRedirect.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox ListBoxChat;
        
        #line default
        #line hidden
        
        
        #line 210 "..\..\..\View\ViewChatRedirect.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonCancel;
        
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
            System.Uri resourceLocater = new System.Uri("/dodicall;component/view/viewchatredirect.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\View\ViewChatRedirect.xaml"
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
            this.ImageContact = ((System.Windows.Controls.Image)(target));
            
            #line 23 "..\..\..\View\ViewChatRedirect.xaml"
            this.ImageContact.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.ImageContact_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.RectangleContacts = ((System.Windows.Shapes.Rectangle)(target));
            return;
            case 3:
            this.ImageChat = ((System.Windows.Controls.Image)(target));
            
            #line 56 "..\..\..\View\ViewChatRedirect.xaml"
            this.ImageChat.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.ImageChat_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 4:
            this.RectangleChat = ((System.Windows.Shapes.Rectangle)(target));
            return;
            case 5:
            this.TextBoxSearch = ((System.Windows.Controls.TextBox)(target));
            
            #line 90 "..\..\..\View\ViewChatRedirect.xaml"
            this.TextBoxSearch.GotFocus += new System.Windows.RoutedEventHandler(this.TextBoxSearch_GotFocus);
            
            #line default
            #line hidden
            
            #line 90 "..\..\..\View\ViewChatRedirect.xaml"
            this.TextBoxSearch.LostFocus += new System.Windows.RoutedEventHandler(this.TextBoxSearch_LostFocus);
            
            #line default
            #line hidden
            return;
            case 6:
            this.TextBlockSearch = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 7:
            this.ListBoxContact = ((System.Windows.Controls.ListBox)(target));
            return;
            case 9:
            this.ListBoxChat = ((System.Windows.Controls.ListBox)(target));
            return;
            case 11:
            this.ButtonCancel = ((System.Windows.Controls.Button)(target));
            
            #line 210 "..\..\..\View\ViewChatRedirect.xaml"
            this.ButtonCancel.Click += new System.Windows.RoutedEventHandler(this.ButtonCancel_Click);
            
            #line default
            #line hidden
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
            case 8:
            
            #line 97 "..\..\..\View\ViewChatRedirect.xaml"
            ((System.Windows.Controls.DockPanel)(target)).MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.ItemContact_MouseLeftButtonUp);
            
            #line default
            #line hidden
            break;
            case 10:
            
            #line 155 "..\..\..\View\ViewChatRedirect.xaml"
            ((System.Windows.Controls.Grid)(target)).MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.ItemChat_MouseLeftButtonUp);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}
