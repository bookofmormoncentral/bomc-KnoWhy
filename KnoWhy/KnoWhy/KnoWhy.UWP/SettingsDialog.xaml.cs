using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using KnoWhy;
using KnoWhy.Interfaces;
using Windows.UI.Popups;

// La plantilla de elemento del cuadro de diálogo de contenido está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace KnoWhy.UWP
{
    public sealed partial class SettingsDialog : ContentDialog, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private void onPropertyChanged(object sender, string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(sender, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            // Raise the PropertyChanged event, passing the name of the property whose value has changed.
            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

        }

        private string _title;
        public string title
        {
            get { return _title; }
            set
            {
                _title = value;
                this.OnPropertyChanged();

            }
        }

        private string _doneLabel;
        public string doneLabel
        {
            get { return _doneLabel; }
            set
            {
                _doneLabel = value;
                this.OnPropertyChanged();

            }
        }

        private string _onlyWiFiLabel;
        public string onlyWiFiLabel
        {
            get { return _onlyWiFiLabel; }
            set
            {
                _onlyWiFiLabel = value;
                this.OnPropertyChanged();

            }
        }

        private string _versionLabel;
        public string versionLabel
        {
            get { return _versionLabel; }
            set
            {
                _versionLabel = value;
                this.OnPropertyChanged();

            }
        }

        private string _version;
        public string version
        {
            get { return _version; }
            set
            {
                _version = value;
                this.OnPropertyChanged();

            }
        }

        private string _build;
        public string build
        {
            get { return _build; }
            set
            {
                _build = value;
                this.OnPropertyChanged();

            }
        }

        private string _buildLabel;
        public string buildLabel
        {
            get { return _buildLabel; }
            set
            {
                _buildLabel = value;
                this.OnPropertyChanged();

            }
        }

        private string _resetLabel;
        public string resetLabel
        {
            get { return _resetLabel; }
            set
            {
                _resetLabel = value;
                this.OnPropertyChanged();

            }
        }

        private string _reset;
        public string reset
        {
            get { return _reset; }
            set
            {
                _reset = value;
                this.OnPropertyChanged();

            }
        }

        private string _fullResetLabel;
        public string fullResetLabel
        {
            get { return _fullResetLabel; }
            set
            {
                _fullResetLabel = value;
                this.OnPropertyChanged();

            }
        }

        private string _fullReset;
        public string fullReset
        {
            get { return _fullReset; }
            set
            {
                _fullReset = value;
                this.OnPropertyChanged();

            }
        }

        private Boolean? _onlyWiFi;
        public Boolean? onlyWiFi
        {
            get { return _onlyWiFi; }
            set
            {
                _onlyWiFi = value;
                this.OnPropertyChanged();

            }
        }

        public SettingsDialog()
        {
            this.InitializeComponent();

            title = KnoWhy.Current.CONSTANT_SETTINGS_TITLE;
            doneLabel = KnoWhy.Current.CONSTANT_DONE;
            onlyWiFi = KnoWhy.Current.onlyWiFi;
            onlyWiFiLabel = KnoWhy.Current.CONSTANT_AUTOMATIC_UPDATE;
            versionLabel = KnoWhy.Current.CONSTANT_VERSION;
            version = KnoWhy.VERSION;
            buildLabel = KnoWhy.Current.CONSTANT_BUILD;
            build = KnoWhy.BUILD_WINDOWS;
            resetLabel = KnoWhy.Current.CONSTANT_RESET_CONTENT_LABEL;
            reset = KnoWhy.Current.CONSTANT_RESET_CONTENT;
            fullResetLabel = KnoWhy.Current.CONSTANT_RESET_DATABASE_LABEL;
            fullReset = KnoWhy.Current.CONSTANT_RESET_DATABASE;
        }
        
        public void toggleOnlyWiFi()
        {
            //KnoWhy.Current.toggleOnlyWiFi();
        }
        

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            KnoWhy.Current.setOnlyWiFi((bool)checkbox.IsChecked);
            KnoWhy.Current.updateSettings();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            //toggleOnlyWiFi();
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            //toggleOnlyWiFi();
        }

        private async void resetContent_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();

            var messageDialog = new MessageDialog(KnoWhy.Current.CONSTANT_ALERT_RESET1_2, KnoWhy.Current.CONSTANT_ALERT_RESET1_1);

            messageDialog.Commands.Add(new UICommand(KnoWhy.Current.CONSTANT_ALERT_RESET1_4, new UICommandInvokedHandler(this.Reset1CommandInvokedHandler)));
            messageDialog.Commands.Add(new UICommand(KnoWhy.Current.CONSTANT_ALERT_RESET1_3, new UICommandInvokedHandler(this.CancelCommandInvokedHandler)));
            // Show the message dialog
            await messageDialog.ShowAsync();
        }

        private async void resetDatabase_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();

            var messageDialog = new MessageDialog(KnoWhy.Current.CONSTANT_ALERT_RESET2_2, KnoWhy.Current.CONSTANT_ALERT_RESET2_1);

            messageDialog.Commands.Add(new UICommand(KnoWhy.Current.CONSTANT_ALERT_RESET2_4, new UICommandInvokedHandler(this.Reset2CommandInvokedHandler)));
            messageDialog.Commands.Add(new UICommand(KnoWhy.Current.CONSTANT_ALERT_RESET2_3, new UICommandInvokedHandler(this.CancelCommandInvokedHandler)));
            // Show the message dialog
            await messageDialog.ShowAsync();
        }

        private void CancelCommandInvokedHandler(IUICommand command)
        {

        }

        private async void Reset1CommandInvokedHandler(IUICommand command)
        {
            await KnoWhy.Current.reset(false);
            MainPage.Current.toggleMenu();
        }

        private async void Reset2CommandInvokedHandler(IUICommand command)
        {
            await KnoWhy.Current.reset(true);
            MainPage.Current.toggleMenu();
        }
    }
}
