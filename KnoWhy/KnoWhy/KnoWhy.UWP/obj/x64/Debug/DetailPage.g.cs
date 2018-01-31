﻿#pragma checksum "C:\Users\j_rob\Desktop\KnoWhy\KnoWhy\KnoWhy.UWP\DetailPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "EA8664472936FC98A73B0A5A2D694B8C"
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
    partial class DetailPage : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        internal class XamlBindingSetters
        {
            public static void Set_Windows_UI_Xaml_Controls_TextBlock_Text(global::Windows.UI.Xaml.Controls.TextBlock obj, global::System.String value, string targetNullValue)
            {
                if (value == null && targetNullValue != null)
                {
                    value = targetNullValue;
                }
                obj.Text = value ?? global::System.String.Empty;
            }
        };

        private class DetailPage_obj1_Bindings :
            global::Windows.UI.Xaml.Markup.IComponentConnector,
            IDetailPage_Bindings
        {
            private global::KnoWhy.UWP.DetailPage dataRoot;
            private bool initialized = false;
            private const int NOT_PHASED = (1 << 31);
            private const int DATA_CHANGED = (1 << 30);

            // Fields for each control that has bindings.
            private global::Windows.UI.Xaml.Controls.TextBlock obj5;

            private DetailPage_obj1_BindingsTracking bindingsTracking;

            public DetailPage_obj1_Bindings()
            {
                this.bindingsTracking = new DetailPage_obj1_BindingsTracking(this);
            }

            // IComponentConnector

            public void Connect(int connectionId, global::System.Object target)
            {
                switch(connectionId)
                {
                    case 5:
                        this.obj5 = (global::Windows.UI.Xaml.Controls.TextBlock)target;
                        break;
                    default:
                        break;
                }
            }

            // IDetailPage_Bindings

            public void Initialize()
            {
                if (!this.initialized)
                {
                    this.Update();
                }
            }
            
            public void Update()
            {
                this.Update_(this.dataRoot, NOT_PHASED);
                this.initialized = true;
            }

            public void StopTracking()
            {
                this.bindingsTracking.ReleaseAllListeners();
                this.initialized = false;
            }

            // DetailPage_obj1_Bindings

            public void SetDataRoot(global::KnoWhy.UWP.DetailPage newDataRoot)
            {
                this.bindingsTracking.ReleaseAllListeners();
                this.dataRoot = newDataRoot;
            }

            public void Loading(global::Windows.UI.Xaml.FrameworkElement src, object data)
            {
                this.Initialize();
            }

            // Update methods for each path node used in binding steps.
            private void Update_(global::KnoWhy.UWP.DetailPage obj, int phase)
            {
                this.bindingsTracking.UpdateChildListeners_(obj);
                if (obj != null)
                {
                    if ((phase & (NOT_PHASED | DATA_CHANGED | (1 << 0))) != 0)
                    {
                        this.Update_loadingLabel(obj.loadingLabel, phase);
                    }
                }
            }
            private void Update_loadingLabel(global::System.String obj, int phase)
            {
                if((phase & ((1 << 0) | NOT_PHASED | DATA_CHANGED)) != 0)
                {
                    XamlBindingSetters.Set_Windows_UI_Xaml_Controls_TextBlock_Text(this.obj5, obj, null);
                }
            }

            private class DetailPage_obj1_BindingsTracking
            {
                global::System.WeakReference<DetailPage_obj1_Bindings> WeakRefToBindingObj; 

                public DetailPage_obj1_BindingsTracking(DetailPage_obj1_Bindings obj)
                {
                    WeakRefToBindingObj = new global::System.WeakReference<DetailPage_obj1_Bindings>(obj);
                }

                public void ReleaseAllListeners()
                {
                    UpdateChildListeners_(null);
                }

                public void PropertyChanged_(object sender, global::System.ComponentModel.PropertyChangedEventArgs e)
                {
                    DetailPage_obj1_Bindings bindings;
                    if(WeakRefToBindingObj.TryGetTarget(out bindings))
                    {
                        string propName = e.PropertyName;
                        global::KnoWhy.UWP.DetailPage obj = sender as global::KnoWhy.UWP.DetailPage;
                        if (global::System.String.IsNullOrEmpty(propName))
                        {
                            if (obj != null)
                            {
                                    bindings.Update_loadingLabel(obj.loadingLabel, DATA_CHANGED);
                            }
                        }
                        else
                        {
                            switch (propName)
                            {
                                case "loadingLabel":
                                {
                                    if (obj != null)
                                    {
                                        bindings.Update_loadingLabel(obj.loadingLabel, DATA_CHANGED);
                                    }
                                    break;
                                }
                                default:
                                    break;
                            }
                        }
                    }
                }
                public void UpdateChildListeners_(global::KnoWhy.UWP.DetailPage obj)
                {
                    DetailPage_obj1_Bindings bindings;
                    if(WeakRefToBindingObj.TryGetTarget(out bindings))
                    {
                        if (bindings.dataRoot != null)
                        {
                            ((global::System.ComponentModel.INotifyPropertyChanged)bindings.dataRoot).PropertyChanged -= PropertyChanged_;
                        }
                        if (obj != null)
                        {
                            bindings.dataRoot = obj;
                            ((global::System.ComponentModel.INotifyPropertyChanged)obj).PropertyChanged += PropertyChanged_;
                        }
                    }
                }
            }
        }
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
                    global::Windows.UI.Xaml.Controls.Page element1 = (global::Windows.UI.Xaml.Controls.Page)(target);
                    #line 8 "..\..\..\DetailPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Page)element1).SizeChanged += this.Page_SizeChanged;
                    #line 9 "..\..\..\DetailPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Page)element1).Loaded += this.Page_Loaded;
                    #line 10 "..\..\..\DetailPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Page)element1).Unloaded += this.Page_Unloaded;
                    #line default
                }
                break;
            case 2:
                {
                    this.toolbar = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 3:
                {
                    this.webView = (global::Windows.UI.Xaml.Controls.WebView)(target);
                    #line 33 "..\..\..\DetailPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.WebView)this.webView).ScriptNotify += this.webView_ScriptNotify;
                    #line 33 "..\..\..\DetailPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.WebView)this.webView).ContainsFullScreenElementChanged += this.webView_ContainsFullScreenElementChanged;
                    #line 33 "..\..\..\DetailPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.WebView)this.webView).NewWindowRequested += this.webView_NewWindowRequested;
                    #line 33 "..\..\..\DetailPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.WebView)this.webView).NavigationStarting += this.webView_NavigationStarting;
                    #line 33 "..\..\..\DetailPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.WebView)this.webView).NavigationCompleted += this.webView_NavigationCompleted;
                    #line default
                }
                break;
            case 4:
                {
                    this.progressGrid = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 6:
                {
                    this.backButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 26 "..\..\..\DetailPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.backButton).Click += this.backButton_Click;
                    #line default
                }
                break;
            case 7:
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
            switch(connectionId)
            {
            case 1:
                {
                    global::Windows.UI.Xaml.Controls.Page element1 = (global::Windows.UI.Xaml.Controls.Page)target;
                    DetailPage_obj1_Bindings bindings = new DetailPage_obj1_Bindings();
                    returnValue = bindings;
                    bindings.SetDataRoot(this);
                    this.Bindings = bindings;
                    element1.Loading += bindings.Loading;
                }
                break;
            }
            return returnValue;
        }
    }
}
