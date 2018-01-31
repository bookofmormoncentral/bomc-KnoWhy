using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
using Windows.Networking.Connectivity;
using Windows.UI.Core;
using Windows.UI.Notifications;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;

using KnoWhy;
using KnoWhy.Model;
using KnoWhy.Interfaces;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace KnoWhy.UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page, ListInterface, INotifyPropertyChanged
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

        static MainPage _current = null;
        public static MainPage Current
        {
            get
            {
                return _current;
            }
        }

        private bool _filterEnabled;
        public bool filterEnabled
        {
            get { return _filterEnabled; }
            set
            {
                _filterEnabled = value;
                this.OnPropertyChanged();

            }
        }

        private string _sortLabel;
        public string sortLabel
        {
            get { return _sortLabel; }
            set
            {
                _sortLabel = value;
                this.OnPropertyChanged();

            }
        }

        private string _sortDesc;
        public string sortDesc
        {
            get { return _sortDesc; }
            set
            {
                _sortDesc = value;
                this.OnPropertyChanged();

            }
        }

        private string _filterLabel;
        public string filterLabel
        {
            get { return _filterLabel; }
            set
            {
                _filterLabel = value;
                this.OnPropertyChanged();

            }
        }

        private string _filterDesc;
        public string filterDesc
        {
            get { return _filterDesc; }
            set
            {
                _filterDesc = value;
                this.OnPropertyChanged();

            }
        }

        private string _updateLabel;
        public string updateLabel
        {
            get { return _updateLabel; }
            set
            {
                _updateLabel = value;
                this.OnPropertyChanged();

            }
        }

        private string _lastUpdate;
        public string lastUpdate
        {
            get { return _lastUpdate; }
            set
            {
                _lastUpdate = value;
                this.OnPropertyChanged();

            }
        }

        private string _isUpdatingLabel;
        public string isUpdatingLabel
        {
            get { return _isUpdatingLabel; }
            set
            {
                _isUpdatingLabel = value;
                this.OnPropertyChanged();

            }
        }

        private string _settingsLabel;
        public string settingsLabel
        {
            get { return _settingsLabel; }
            set
            {
                _settingsLabel = value;
                this.OnPropertyChanged();

            }
        }

        private string _webLinksLabel;
        public string webLinksLabel
        {
            get { return _webLinksLabel; }
            set
            {
                _webLinksLabel = value;
                this.OnPropertyChanged();

            }
        }

        private string _link1Url;
        public string link1Url
        {
            get { return _link1Url; }
            set
            {
                _link1Url = value;
                this.OnPropertyChanged();

            }
        }

        private string _link1Label;
        public string link1Label
        {
            get { return _link1Label; }
            set
            {
                _link1Label = value;
                this.OnPropertyChanged();

            }
        }

        private string _link2Url;
        public string link2Url
        {
            get { return _link2Url; }
            set
            {
                _link2Url = value;
                this.OnPropertyChanged();

            }
        }

        private string _link2Label;
        public string link2Label
        {
            get { return _link2Label; }
            set
            {
                _link2Label = value;
                this.OnPropertyChanged();

            }
        }

        private string _link3Url;
        public string link3Url
        {
            get { return _link3Url; }
            set
            {
                _link3Url = value;
                this.OnPropertyChanged();

            }
        }

        private string _link3Label;
        public string link3Label
        {
            get { return _link3Label; }
            set
            {
                _link3Label = value;
                this.OnPropertyChanged();

            }
        }

        private bool _isUpdating;
        public bool isUpdating
        {
            get { return _isUpdating; }
            set
            {
                _isUpdating = value;
                this.OnPropertyChanged();

            }
        }

        private int _updateFilterButtons; //only helps to force update binding of next and previous buttons
        public int updateFilterButtons
        {
            get { return _updateFilterButtons; }
            set
            {
                _updateFilterButtons = value;
                this.OnPropertyChanged();

            }
        }

        private string _textFilter;
        public string textFilter
        {
            get { return _textFilter; }
            set
            {
                _textFilter = value;
                this.OnPropertyChanged();

            }
        }

        private string _bookLabel;
        public string bookLabel
        {
            get { return _bookLabel; }
            set
            {
                _bookLabel = value;
                this.OnPropertyChanged();

            }
        }

        private string _chapterLabel;
        public string chapterLabel
        {
            get { return _chapterLabel; }
            set
            {
                _chapterLabel = value;
                this.OnPropertyChanged();

            }
        }

        private string _loadingLabel;
        public string loadingLabel
        {
            get { return _loadingLabel; }
            set
            {
                _loadingLabel = value;
                this.OnPropertyChanged();

            }
        }

        private string _emptyListLabel;
        public string emptyListLabel
        {
            get { return _emptyListLabel; }
            set
            {
                _emptyListLabel = value;
                this.OnPropertyChanged();

            }
        }

        private bool _allowExpand;
        public bool allowExpand
        {
            get { return _allowExpand; }
            set
            {
                _allowExpand = value;
                this.OnPropertyChanged();

            }
        }

        ObservableCollection<Books> booksList = new ObservableCollection<Books>();
        ObservableCollection<Chapter> chaptersList = new ObservableCollection<Chapter>();

        //private ObservableCollection<Meta> items = new ObservableCollection<Meta>();
        private KnoWhy knoWhy = KnoWhy.Current;

        bool isFullScreen = false;

        public MainPage()
        {
            this.InitializeComponent();

            _current = this;

            SystemNavigationManager.GetForCurrentView().BackRequested += SystemNavigationManager_BackRequested;

            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {
                var statusBar = Windows.UI.ViewManagement.StatusBar.GetForCurrentView();
                statusBar.BackgroundColor = Windows.UI.Colors.DarkBlue;
                statusBar.BackgroundOpacity = 1;
            }

        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {

            System.Globalization.CultureInfo culture = GetCurrentCultureInfo();

            if (culture.TwoLetterISOLanguageName.ToLower() == "es")
            {
                KnoWhy.Current.locale = culture.TwoLetterISOLanguageName.ToLower();
            }
            else
            {
                KnoWhy.Current.locale = "en";
            }
            loadingLabel = KnoWhy.Current.CONSTANT_LOADING;

            await KnoWhy.Current.init(this);

            updateLabel = KnoWhy.Current.CONSTANT_UPDATE_CONTENT;
            lastUpdate = KnoWhy.Current.getLastUpdate();
            isUpdatingLabel = KnoWhy.Current.CONSTANT_UPDATING;
            settingsLabel = KnoWhy.Current.CONSTANT_SETTINGS;
            webLinksLabel = KnoWhy.Current.CONSTANT_WEB_LINKS;
            link1Url = KnoWhy.Current.CONSTANT_LINK1;
            link1Label = KnoWhy.Current.CONSTANT_LINK1_DESC;
            link2Url = KnoWhy.Current.CONSTANT_LINK2;
            link2Label = KnoWhy.Current.CONSTANT_LINK2_DESC;
            link3Url = KnoWhy.Current.CONSTANT_LINK3;
            link3Label = KnoWhy.Current.CONSTANT_LINK3_DESC;

            bookLabel = KnoWhy.Current.CONSTANT_BOOK;
            chapterLabel = KnoWhy.Current.CONSTANT_CHAPTER;

            emptyListLabel = KnoWhy.Current.CONSTANT_EMPTY_LIST;

        }

        #region BackRequested Handlers

        private void SystemNavigationManager_BackRequested(object sender, BackRequestedEventArgs e)
        {
            bool handled = e.Handled;
            this.BackRequested(ref handled);
            e.Handled = handled;
        }

        private void BackRequested(ref bool handled)
        {
            // Get a hold of the current frame so that we can inspect the app back stack.

            if (this.Frame == null)
                return;

            // Check to see if this is the top-most page on the app back stack.
            if (this.Frame.CanGoBack && !handled)
            {
                // If not, set the event to handled and go back to the previous page in the app.
                handled = true;
                this.Frame.GoBack();

            }
            else
            {
                if (listGrid.Visibility == Visibility.Collapsed)
                {
                    showLeftPanel();
                    handled = true;
                    return;
                }
                else
                {
                    App.Current.Exit();
                }
            }
        }

        #endregion

        public void updateBooksList()
        {
            booksList.Clear();
            foreach (Books item in KnoWhy.Current.booksList)
            {
                booksList.Add(item);
            }
            comboBooks.SelectedIndex = KnoWhy.Current.filterBookId;
        }

        public void updateChaptersList()
        {
            chaptersList.Clear();
            List<Chapter> list = Chapter.getChaptersList(KnoWhy.Current.filterBookId);
            foreach (Chapter item in list)
            {
                chaptersList.Add(item);
            }
            comboChapters.SelectedIndex = KnoWhy.Current.filterChapterId;
        }

        public System.Globalization.CultureInfo GetCurrentCultureInfo()
        {
            return new System.Globalization.CultureInfo(
                Windows.System.UserProfile.GlobalizationPreferences.Languages[0].ToString());
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView lv = (ListView)sender;
            if (lv.SelectedItem != null)
            {
                Meta item = (Meta)lv.SelectedItem;
                showMeta(item, false);
            }
        }

        public void showMeta(Meta meta, bool isAuto)
        {
            if (isAuto == false || KnoWhy.Current.lastNode > 0)
            {
                showRightPanel();
            }
            //articleControl.loadMeta(meta);
            frameArticle.CacheSize = 5;
            frameArticle.Navigate(typeof(DetailPage), meta);
            frameArticle.BackStack.Clear();
            
        }

        public void showRightPanel()
        {
            if (ContentSplitView.DisplayMode == SplitViewDisplayMode.Overlay)
            {
                ContentSplitView.IsPaneOpen = false;
            }
        }

        public void showLeftPanel()
        {
            if (ContentSplitView.DisplayMode == SplitViewDisplayMode.Overlay)
            {
                ContentSplitView.IsPaneOpen = true;
                //frameArticle.Content = null;
            }
        }

        public bool isSmallDeviceMode()
        {
            if (ContentSplitView.DisplayMode == SplitViewDisplayMode.Overlay)
            {
                return true;
            }
            return false;
        }

        public bool isPaneOpen()
        {
            return ContentSplitView.IsPaneOpen;
        }

        public void setup()
        {
            filterEnabled = KnoWhy.Current.filterEnabled;
            sortLabel = KnoWhy.Current.CONSTANT_SORTED_BY;
            sortDesc = KnoWhy.Current.getSortDesc();
            filterLabel = KnoWhy.Current.CONSTANT_FILTER_BY;
            filterDesc = KnoWhy.Current.getFilterDesc();
        }
        

        public void refreshList(bool firstTime)
        {
            filterEnabled = KnoWhy.Current.filterEnabled;
            updateFilter();
            if (knoWhy.metaList.Count > 0 && frameArticle.Content == null && ((ContentSplitView.IsPaneOpen == true && ContentSplitView.DisplayMode != SplitViewDisplayMode.Overlay) || KnoWhy.Current.lastNode > 0) && firstTime)
            {
                //if (ContentSplitView.DisplayMode != SplitViewDisplayMode.Overlay)
                //{
                    //showMeta(knoWhy.metaList.ToArray()[0], true);
                    showMeta((Meta)KnoWhy.Current.getFirstMeta(), true);
                //}
                
            }/* else if (knoWhy.metaList.Count > 0)
            {
                if (frameArticle.Content is DetailPage)
                {
                    DetailPage detail = (DetailPage)frameArticle.Content;
                    await detail.loadData();
                }
            }*/
        }

        public void showProgress()
        {
            progressGrid.Visibility = Visibility.Visible;
        }

        public async void hideProgress()
        {
            progressGrid.Visibility = Visibility.Collapsed;

            if (isConnected() == false && knoWhy.metaList.Count == 0)
            {
                var messageDialog = new MessageDialog(KnoWhy.Current.CONSTANT_CONNECTION_FAILED, KnoWhy.Current.CONSTANT_ERROR_TITLE);
                messageDialog.Commands.Add(new UICommand(KnoWhy.Current.CONSTANT_DONE, new UICommandInvokedHandler(this.CancelCommandInvokedHandler)));
                // Show the message dialog
                await messageDialog.ShowAsync();
            }
        }

        private void CancelCommandInvokedHandler(IUICommand command)
        {

        }

        public void updateFilter()
        {
            sortDesc = KnoWhy.Current.getSortDesc();
            filterDesc = KnoWhy.Current.getFilterDesc();

            textFilter = KnoWhy.Current.getCurrentChapterDesc();
            updateFilterButtons = updateFilterButtons + 1;

            updateBooksList();
            updateChaptersList();
        }

        public async void showSettings()
        {
            SortAndFilterDialog dialog = new SortAndFilterDialog();
            if (ApplicationView.GetForCurrentView().VisibleBounds.Width < 400 || ApplicationView.GetForCurrentView().VisibleBounds.Height < 400)
            {
                dialog.MinWidth = ApplicationView.GetForCurrentView().VisibleBounds.Width;
                dialog.MaxWidth = ApplicationView.GetForCurrentView().VisibleBounds.Width;
                dialog.MinHeight = ApplicationView.GetForCurrentView().VisibleBounds.Height;
                dialog.MaxHeight = ApplicationView.GetForCurrentView().VisibleBounds.Height;
            }
            else
            {
                dialog.MinWidth = 400;
                dialog.MaxWidth = 400;
                if (ApplicationView.GetForCurrentView().VisibleBounds.Height < 600)
                {
                    dialog.MinHeight = ApplicationView.GetForCurrentView().VisibleBounds.Height;
                    dialog.MaxHeight = ApplicationView.GetForCurrentView().VisibleBounds.Height;
                }
                else
                {
                    dialog.MinHeight = 600;
                    dialog.MaxHeight = 600;
                }
            }
            await dialog.ShowAsync();
            KnoWhy.Current.updateSettings();
            KnoWhy.Current.reorder();
        }

        public void showArticle(int position)
        {
           
        }
        
        private void buttonFilter_Click(object sender, RoutedEventArgs e)
        {
            KnoWhy.Current.toggleButtonFilter();
            filterEnabled = KnoWhy.Current.filterEnabled;
            KnoWhy.Current.updateSettings();
        }

        private void buttonSort_Click(object sender, RoutedEventArgs e)
        {
            showSettings();
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            lastUpdate = KnoWhy.Current.getLastUpdate();
            RootSplitView.IsPaneOpen = !RootSplitView.IsPaneOpen;
        }

        private async void updateButton_Click(object sender, RoutedEventArgs e)
        {
            isUpdating = true;
            await KnoWhy.Current.loadData(true);
            lastUpdate = KnoWhy.Current.getLastUpdate();
            isUpdating = false;
        }

        private async void settingsButton_Click(object sender, RoutedEventArgs e)
        {
            SettingsDialog dialog = new SettingsDialog();
            if (ApplicationView.GetForCurrentView().VisibleBounds.Width < 400 || ApplicationView.GetForCurrentView().VisibleBounds.Height < 400)
            {
                dialog.MinWidth = ApplicationView.GetForCurrentView().VisibleBounds.Width;
                dialog.MaxWidth = ApplicationView.GetForCurrentView().VisibleBounds.Width;
                dialog.MinHeight = ApplicationView.GetForCurrentView().VisibleBounds.Height;
                dialog.MaxHeight = ApplicationView.GetForCurrentView().VisibleBounds.Height;
            }
            else
            {
                dialog.MinWidth = 400;
                dialog.MaxWidth = 400;
                if (ApplicationView.GetForCurrentView().VisibleBounds.Height < 600)
                {
                    dialog.MinHeight = ApplicationView.GetForCurrentView().VisibleBounds.Height;
                    dialog.MaxHeight = ApplicationView.GetForCurrentView().VisibleBounds.Height;
                }
                else
                {
                    dialog.MinHeight = 600;
                    dialog.MaxHeight = 600;
                }
            }
            await dialog.ShowAsync();
            KnoWhy.Current.updateSettings();
        }

        private async void link1Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var uri = new Uri(link1Url);

                var success = await Windows.System.Launcher.LaunchUriAsync(uri);

                if (success)
                {
                    // URI launched
                }
                else
                {
                    // URI launch failed
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        private async void link2Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var uri = new Uri(link2Url);

                var success = await Windows.System.Launcher.LaunchUriAsync(uri);

                if (success)
                {
                    // URI launched
                }
                else
                {
                    // URI launch failed
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        private async void link3Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var uri = new Uri(link3Url);

                var success = await Windows.System.Launcher.LaunchUriAsync(uri);

                if (success)
                {
                    // URI launched
                }
                else
                {
                    // URI launch failed
                }
            } catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        public void openFilterPanel()
        {
            throw new NotImplementedException();
        }

        public void closeFilterPanel()
        {
            throw new NotImplementedException();
        }

        public void hideBooks()
        {
            throw new NotImplementedException();
        }

        public void showBooks()
        {
            throw new NotImplementedException();
        }

        private void previousChapterButton_Click(object sender, RoutedEventArgs e)
        {
            KnoWhy.Current.moveToPreviousChapter();
        }

        private void nextChapterButton_Click(object sender, RoutedEventArgs e)
        {
            KnoWhy.Current.moveToNextChapter();
        }

        private void comboBooks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int val = ((ComboBox)sender).SelectedIndex;
            if (val != KnoWhy.Current.filterBookId && val > -1)
            {
                KnoWhy.Current.filterBookId = val;
                KnoWhy.Current.filterChapterId = 0;
                updateChaptersList();
                //reloadData();
                KnoWhy.Current.reorder();
            }
        }

        private void comboChapters_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int val = ((ComboBox)sender).SelectedIndex;
            if (val != KnoWhy.Current.filterChapterId && val > -1)
            {
                KnoWhy.Current.filterChapterId = val;
                //reloadData();
                KnoWhy.Current.reorder();
            }
        }

        public void toggleMenu()
        {
            RootSplitView.IsPaneOpen = !RootSplitView.IsPaneOpen;
        }

        public void startFullScreenDetail()
        {
            ContentSplitView.DisplayMode = SplitViewDisplayMode.Overlay;
            ContentSplitView.IsPaneOpen = false;
            isFullScreen = true;
        }

        public void stopFullScreenDetail()
        {
            //if (allowExpand)
            //{
                ContentSplitView.DisplayMode = SplitViewDisplayMode.Inline;
                ContentSplitView.IsPaneOpen = true;
                isFullScreen = false;
            //}
        }

        private async void listView_RefreshRequested(object sender, RefreshableListView.RefreshRequestedEventArgs e)
        {
            using (Deferral deferral = e.GetDeferral())
            {
                await KnoWhy.Current.loadData(true);

                if (SpinnerStoryboard.GetCurrentState() != Windows.UI.Xaml.Media.Animation.ClockState.Stopped)
                {
                    SpinnerStoryboard.Stop();
                }
            }
            
        }

        private void listView_PullProgressChanged(object sender, RefreshableListView.RefreshProgressEventArgs e)
        {
            if (e.IsRefreshable)
            {
                if (e.PullProgress == 1)
                {
                    // Progress = 1.0 means that the refresh has been triggered.
                    if (SpinnerStoryboard.GetCurrentState() == Windows.UI.Xaml.Media.Animation.ClockState.Stopped)
                    {
                        SpinnerStoryboard.Begin();
                    }
                }
                else if (SpinnerStoryboard.GetCurrentState() != Windows.UI.Xaml.Media.Animation.ClockState.Stopped)
                {
                    SpinnerStoryboard.Stop();
                }
                else
                {
                    // Turn the indicator by an amount proportional to the pull progress.
                    SpinnerTransform.Angle = e.PullProgress * 360;
                }
            }
        }

        public bool isConnected()
        {
            try
            {
                bool connected = false;

                var temp = Windows.Networking.Connectivity.NetworkInformation.GetInternetConnectionProfile();
                if (temp != null)
                {
                    if (temp.IsWlanConnectionProfile)
                    {
                        // its wireless
                        connected = true;
                    }
                    else if (temp.IsWwanConnectionProfile)
                    {
                        // its mobile
                        if (KnoWhy.Current.onlyWiFi == false)
                        {
                            connected = true;
                        }
                    } /*else
                    {
                        connected = true;
                    }*/
                }
                return connected;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
            return false;
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (frameArticle.Visibility == Visibility.Collapsed && frameArticle.Content != null)
            {
                showRightPanel();
            }

            if (ContentSplitView.DisplayMode == SplitViewDisplayMode.Overlay)
            {
                if (this.ActualWidth <= 600)
                {
                    ContentSplitView.OpenPaneLength = this.ActualWidth;
                } else
                {
                    ContentSplitView.OpenPaneLength = 400;
                }
                allowExpand = false;
            }
            else
            {
                ContentSplitView.OpenPaneLength = 400;

                allowExpand = true;

                if (isFullScreen)
                {
                    startFullScreenDetail();
                }
            }

        }
        
    }
}