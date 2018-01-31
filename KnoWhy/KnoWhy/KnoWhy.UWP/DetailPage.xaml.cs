using KnoWhy.Model;
using KnoWhy.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Storage.Streams;
using Windows.Security.Cryptography;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Popups;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace KnoWhy.UWP
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class DetailPage : Page, DetailInterface, INotifyPropertyChanged
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

        public Meta meta = null;

        public Article article = null;

        private StreamUriWinRTResolver myResolver;

        bool hasContent = false;

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

        private string _errorTextMessage;
        public string errorTextMessage
        {
            get { return _errorTextMessage; }
            set
            {
                _errorTextMessage = value;
                this.OnPropertyChanged();

            }
        }

        public DetailPage()
        {
            this.InitializeComponent();

            loadingLabel = KnoWhy.Current.CONSTANT_LOADING;
            errorTextMessage = KnoWhy.Current.CONSTANT_CONNECTION_FAILED;

            this.NavigationCacheMode = NavigationCacheMode.Required;

            //SystemNavigationManager.GetForCurrentView().BackRequested += SystemNavigationManager_BackRequested;
            //SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            displayBackButton();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.NavigationMode != NavigationMode.Back)
            {
                meta = (Meta)e.Parameter;
                await loadData();
            }
        }

        public async Task loadData()
        {

            if (meta != null)
            {
                showProgress();

                hasContent = false;

                //var appView = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView();
                //appView.Title = "#" + meta.knowhyNumber.ToString() + " " + meta.title;
                titleLabel.Text = "#" + meta.knowhyNumber.ToString() + " " + meta.title;



                article = await KnoWhy.Current.loadNode(meta, (int)this.webView.Width, (int)this.webView.Height);

                myResolver = new StreamUriWinRTResolver(article);
                
                if (article != null)
                {
                    if (article.parsedHTML != null)
                    {
                        if (article.parsedHTML != "")
                        {
                            hasContent = true;
                        }
                    }
                }

                webView.ContainsFullScreenElementChanged += webView_ContainsFullScreenElementChanged;
                //webView.NavigateToString(meta.article.parsedHTML);

                if (hasContent == true)
                {
                    webView.Visibility = Visibility.Visible;
                    errorMessage.Visibility = Visibility.Collapsed;

                    Uri url = webView.BuildLocalStreamUri("Article", "/default.html");

                    // The resolver object needs to be passed in to the navigate call.
                    webView.NavigateToLocalStreamUri(url, myResolver);
                }
                else
                {
                    webView.NavigateToString("");
                    webView.Visibility = Visibility.Collapsed;
                    errorMessage.Visibility = Visibility.Visible;
                }
            }
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            SystemNavigationManager.GetForCurrentView().BackRequested -= SystemNavigationManager_BackRequested;
        }

        public void showProgress()
        {
            progressGrid.Visibility = Visibility.Visible;
        }

        public async void hideProgress()
        {
            progressGrid.Visibility = Visibility.Collapsed;

            if (hasContent == false)
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

        #region BackRequested Handlers

        private void SystemNavigationManager_BackRequested(object sender, BackRequestedEventArgs e)
        {
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;

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
            if (this.Frame.CanGoBack)
            {
                // If not, set the event to handled and go back to the previous page in the app.
                handled = true;
                this.Frame.GoBack();


            }
        }

        #endregion

        private void webView_ContainsFullScreenElementChanged(WebView sender, object args)
        {
            var applicationView = ApplicationView.GetForCurrentView();

            if (sender.ContainsFullScreenElement)
            {
                MainPage.Current.startFullScreenDetail();
                toolbar.Visibility = Visibility.Collapsed;
                applicationView.TryEnterFullScreenMode();
            }
            else
            {
                MainPage.Current.stopFullScreenDetail();
                toolbar.Visibility = Visibility.Visible;
                // It is harmless to exit full screen mode when not full screen.
                applicationView.ExitFullScreenMode();
            }
        }

        private void webView_ScriptNotify(object sender, NotifyEventArgs e)
        {
            try
            {
                string value = e.Value;
                if (value == "0")
                {
                    KnoWhy.Current.removeMetaAsFavorite(meta);
                }
                else if (value == "1")
                {
                    KnoWhy.Current.markMetaAsFavorite(meta);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        /// <summary>
        /// Sample URI resolver object for use with NavigateToLocalStreamUri.
        /// The object must implement the IUriToStreamResolver interface
        /// 
        /// Note: If your content is stored in the package or local state,
        /// then you can use the ms-appx-web: or ms-appdata: protocol demonstrated in the
        /// "Navigate to package and local state" scenario. Those protocols will
        /// will create a resolver for you.
        /// </summary>
        public sealed class StreamUriWinRTResolver : Windows.Web.IUriToStreamResolver
        {
            Article article = null;

            public StreamUriWinRTResolver(Article _article)
            {
                article = _article;
            }

            /// <summary>
            /// The entry point for resolving a URI to a stream.
            /// </summary>
            /// <param name="uri"></param>
            /// <returns></returns>
            public IAsyncOperation<IInputStream> UriToStreamAsync(Uri uri)
            {
                // Because of the signature of the this method, it can't use await, so we 
                // call into a separate helper method that can use the C# await pattern.
                return GetContentAsync(uri).AsAsyncOperation();
            }


            /// <summary>
            /// Helper that produces the contents corresponding to a Uri.
            /// Uses the C# await pattern to coordinate async operations.
            /// </summary>
            /// <param name="uri"></param>
            /// <returns></returns>
            private async Task<IInputStream> GetContentAsync(Uri uri)
            {
                string path = uri.AbsolutePath;
                string contents;

                switch (path)
                {
                    case "/default.html":
                        contents = article.parsedHTML;
                        // Convert the string to a stream.
                        IBuffer buffer = CryptographicBuffer.ConvertStringToBinary(contents, BinaryStringEncoding.Utf8);
                        var stream = new InMemoryRandomAccessStream();
                        await stream.WriteAsync(buffer);
                        return stream.GetInputStreamAt(0);
                        //break;

                    case "/styles.css":
                        StorageFile f = await StorageFile.GetFileFromApplicationUriAsync(new Uri($"ms-appx:///Assets/styles.css"));
                        var randomAccessStream = await f.OpenReadAsync();
                        return randomAccessStream.AsStreamForRead().AsInputStream();
                        //break;
                    case "/Ionicons.ttf":
                        StorageFile f2 = await StorageFile.GetFileFromApplicationUriAsync(new Uri($"ms-appx:///Assets/fonts/Ionicons.ttf"));
                        var randomAccessStream2 = await f2.OpenReadAsync();
                        return randomAccessStream2.AsStreamForRead().AsInputStream();
                        //break;

                    default:
                        throw new Exception($"Could not resolve URI \"{uri}\"");
                }

                throw new Exception($"Could not resolve URI \"{uri}\"");
            }
        }
        

        private async void webView_NewWindowRequested(WebView sender, WebViewNewWindowRequestedEventArgs args)
        {
            try
            {
                string url = args.Uri.ToString();
                string[] ldsUrls = KnoWhy.Current.getLDSUrls(url);
                if (ldsUrls.Length == 2)
                {
                    try
                    {
                        args.Handled = true;
                        LaunchQuerySupportStatus supported = await Launcher.QueryUriSupportAsync(new Uri(ldsUrls[0]), LaunchQuerySupportType.Uri);
                        if (supported == LaunchQuerySupportStatus.Available)
                        {
                            var success = await Launcher.LaunchUriAsync(new Uri(ldsUrls[0]));
                            if (success == false)
                            {
                                await Launcher.LaunchUriAsync(new Uri(ldsUrls[1]));
                            }
                        } else
                        {
                            await Launcher.LaunchUriAsync(new Uri(ldsUrls[1]));
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error: " + e.Message);
                    }
                }
                else
                {
                    int node = KnoWhy.Current.getKnowhyNode(url);
                    if (node >= 0)
                    {
                        Meta item = KnoWhy.Current.getMeta(node);
                        if (item != null)
                        {
                            args.Handled = true;
                            Frame.Navigate(typeof(ChildDetailPage), item);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            if (MainPage.Current.isSmallDeviceMode() == true)
            {
                MainPage.Current.showLeftPanel();
            }
            if (Frame.CanGoBack == true)
            {
                Frame.GoBack();
            }
        }

        private void webView_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            hideProgress();
        }

        private async void webView_NavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args)
        {
            if (args.Uri != null)
            {
                if (args.Uri.OriginalString.StartsWith("http"))
                {
                    args.Cancel = true;
                    await Launcher.LaunchUriAsync(args.Uri);
                }
            }
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            displayBackButton();
            displayExpandButton();
            displayCollapseButton();
        }

        private void displayBackButton()
        {
            if (Frame.CanGoBack == true || MainPage.Current.isSmallDeviceMode() == true)
            {
                backButton.Visibility = Visibility.Visible;
            }
            else
            {
                backButton.Visibility = Visibility.Collapsed;
            }
        }

        private void displayExpandButton()
        {
            if (MainPage.Current.allowExpand == true && MainPage.Current.isPaneOpen() == true)
            {
                expandButton.Visibility = Visibility.Visible;
            }
            else
            {
                expandButton.Visibility = Visibility.Collapsed;
            }
        }

        private void displayCollapseButton()
        {
            if (MainPage.Current.allowExpand == true && MainPage.Current.isPaneOpen() == false)
            {
                collapseButton.Visibility = Visibility.Visible;
            }
            else
            {
                collapseButton.Visibility = Visibility.Collapsed;
            }
        }

        private void expandButton_Click(object sender, RoutedEventArgs e)
        {
            MainPage.Current.startFullScreenDetail();
        }

        private void collapseButton_Click(object sender, RoutedEventArgs e)
        {
            MainPage.Current.stopFullScreenDetail();
        }
    }
}
