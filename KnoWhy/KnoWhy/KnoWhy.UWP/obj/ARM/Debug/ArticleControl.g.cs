﻿#pragma checksum "C:\Users\j_rob\Desktop\KnoWhy\KnoWhy\KnoWhy.UWP\ArticleControl.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "86B39D159A5E0E043E3CA425AA1B3427"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KnoWhy.UWP
{
    partial class ArticleControl : 
        global::Windows.UI.Xaml.Controls.UserControl, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                {
                    this.webView = (global::Windows.UI.Xaml.Controls.WebView)(target);
                    #line 32 "..\..\..\ArticleControl.xaml"
                    ((global::Windows.UI.Xaml.Controls.WebView)this.webView).ScriptNotify += this.webView_ScriptNotify;
                    #line 32 "..\..\..\ArticleControl.xaml"
                    ((global::Windows.UI.Xaml.Controls.WebView)this.webView).ContainsFullScreenElementChanged += this.webView_ContainsFullScreenElementChanged;
                    #line 32 "..\..\..\ArticleControl.xaml"
                    ((global::Windows.UI.Xaml.Controls.WebView)this.webView).NewWindowRequested += this.webView_NewWindowRequested;
                    #line default
                }
                break;
            case 2:
                {
                    this.backButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 25 "..\..\..\ArticleControl.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.backButton).Click += this.backButton_Click;
                    #line default
                }
                break;
            case 3:
                {
                    this.titleLabel = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

