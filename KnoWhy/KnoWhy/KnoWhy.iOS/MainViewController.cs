using System;

using UIKit;
using Foundation;
using System.Net;
using System.IO;
using System.Drawing;
using System.Threading.Tasks;

using KnoWhy;
using KnoWhy.Model;
using KnoWhy.Interfaces;
using CoreGraphics;
using Reachability;
using System.Linq;

namespace KnoWhy.iOS
{
    public partial class MainViewController : UIViewController, IUITableViewDelegate, IUITableViewDataSource, ListInterface, IUISplitViewControllerDelegate
    {

        public static MainViewController current = null;

		const string CellIdentifier = "ArticleViewCell";

        UITableView tableView = null;

        float maxBlackViewAlpha = (float)0.5;

        float bookAndChapterMaxHeight = (float)26.0;

        float panelFilterMaxHeight = (float)200.0;

        UIRefreshControl RefreshControl;

        bool shouldCollapseDetailViewController;

		public MainViewController(IntPtr handle) : base(handle)
        {
		}

        public async override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            current = this;

            shouldCollapseDetailViewController = true;
            if (this.SplitViewController != null)
            {
                this.SplitViewController.Delegate = this;
            }

            constraintMenuLeft.Constant = -constraintMenuWidth.Constant;

            //this.NavigationController.NavigationBarHidden = true;

            panelFilter2.Hidden = true;
            panelFilterHeight.Constant = 0;
            panelBooksHeight.Constant = 0;
            panelBooks.Hidden = true;
            viewBlack.Alpha = 0;
            viewBlack.Hidden = true;
            bookTitlePanel.Hidden = true;

            tableView = (UITableView)this.View.ViewWithTag(100);

            UIFont font2 = UIFont.FromName("Knowhy", 24);
            arrowLabel.Text = "\uf106";
            arrowLabel.Font = font2;

            UIFont font = UIFont.FromName("Ionicons", 24);

            buttonBack1.SetTitle("\uf3cf", UIControlState.Normal);
            buttonBack1.Font = font;

            buttonNext1.SetTitle("\uf3d1", UIControlState.Normal);
            buttonNext1.Font = font;

            buttonBack2.SetTitle("\uf3cf", UIControlState.Normal);
            buttonBack2.Font = font;

            buttonNext2.SetTitle("\uf3d1", UIControlState.Normal);
            buttonNext2.Font = font;

            updateBookAndChapterButton();

			var userLang = NSLocale.CurrentLocale.LocaleIdentifier;

			if (userLang.Contains("es"))
			{
				KnoWhy.Current.locale = "es";
			}

            setLabels();

            await KnoWhy.Current.init(this);

            if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad) {
                if (KnoWhy.Current.metaList.Count > 0) {
                    /*if (isLandscape()) {
                        RootSplitViewControllerIPad.current.PreferredDisplayMode = UISplitViewControllerDisplayMode.AllVisible;
                    } else {
                        RootSplitViewControllerIPad.current.PreferredDisplayMode = UISplitViewControllerDisplayMode.PrimaryOverlay;
                    }*/
                    //if (isLandscape())
                    //{
                        //Meta meta = (Meta)KnoWhy.Current.metaList.ToArray()[0];
                        Meta meta = (Meta)KnoWhy.Current.getFirstMeta();
                        await ArticleViewController.current.setMeta(meta);
                    //} else {
                        /*RootSplitViewControllerIPad viewController = RootSplitViewControllerIPad.current;
                        viewController.CollapseSecondaryViewController(viewController.ViewControllers[1], viewController);
                        viewController.CollapseSecondViewController(viewController, viewController.ViewControllers[1], viewController.ViewControllers[0]);*/
                        //RootSplitViewControllerIPad.current.CollapseSecondaryViewController(ArticleViewController.current.NavigationController, RootSplitViewControllerIPad.current);
                        //RootSplitViewControllerIPad.current.DisplayMode = UISplitViewControllerDisplayMode
                    //}
                }
            } else {
                if (KnoWhy.Current.lastNode > 0) {
                    showArticle(KnoWhy.Current.getFirstMetaIndex());
                }
            }


            RefreshControl = new UIRefreshControl();
            RefreshControl.ValueChanged += async (sender, e) =>
            {
                await RefreshAsync();
            };
            tableView.Add(RefreshControl);

            KnoWhy.Current.reorder();
        }

        async Task RefreshAsync()
        {
            // only activate the refresh control if the feature is available  
            RefreshControl.BeginRefreshing();
            await KnoWhy.Current.loadData(true);
            RefreshControl.EndRefreshing();
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);


            //KnoWhy.Current.reorder();
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            this.NavigationController.NavigationBarHidden = true;
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);

            this.NavigationController.NavigationBarHidden = false;
            this.View.LayoutIfNeeded();
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

		public nint NumberOfSections(UITableView tableView)
		{
			return 1;
		}

        [Export("tableView:didSelectRowAtIndexPath:")]
        public void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
			/*var handler = Selected;
            if (handler != null)
                handler(this, new RowArgs { Content = tableItems[indexPath.Row] });*/

			tableView.DeselectRow(indexPath, true); // normal iOS behaviour is to remove the blue highlight


            showArticle(indexPath.Row);
        }

		public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
		{
            if (segue.Identifier == "showArticle")
            {
                Int32 position = ((NSNumber)sender).Int32Value;
                Meta meta = (Meta)KnoWhy.Current.metaList.ToArray()[position];

                if (meta != null)
                {
                    ArticleViewController destination = (ArticleViewController)segue.DestinationViewController;
                    destination.NavigationItem.Title = "#" + meta.knowhyNumber.ToString() + " " + meta.title;
                    destination.meta = meta;
                }
            }
		}

		public nint RowsInSection(UITableView tableView, nint section)
		{
			return (nint)KnoWhy.Current.metaList.Count;
		}

		public UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			// request a recycled cell to save memory
			UITableViewCell cell = tableView.DequeueReusableCell(CellIdentifier);

            if (indexPath.Row < KnoWhy.Current.metaList.Count)
            {

                Meta item = (Meta)KnoWhy.Current.metaList.ToArray()[indexPath.Row];

                if (item != null)
                {
                    UIImageView imageView = (UIImageView)cell.ViewWithTag(4);
                    //addBorders(imageView);
                    imageView.Layer.CornerRadius = 5;
                    imageView.ClipsToBounds = true;
                    imageView.Image = null;

                    UILabel label1 = (UILabel)cell.ViewWithTag(1);
                    label1.Text = item.formattedDate;
                    UILabel label2 = (UILabel)cell.ViewWithTag(2);
                    label2.Text = item.title;
                    UILabel label3 = (UILabel)cell.ViewWithTag(3);
                    label3.Text = item.scriptureReference;

                    UIFont font = UIFont.FromName("Knowhy", 16);
                    UILabel labelMarker = (UILabel)cell.ViewWithTag(5);
                    labelMarker.Font = font;
                    labelMarker.Text = "\uf102";
                    if (item.isRead == true)
                    {
                        labelMarker.Text = "";
                    }
                    try
                    {
                        if (item.mainImageURL != null)
                        {
                            loadImage(item.mainImageURL, imageView, item.nodeId.ToString());
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error loading image " + e.Message);
                    }

                    UIFont font1 = UIFont.FromName("Ionicons", 16);
                    UILabel labelFavorite = (UILabel)cell.ViewWithTag(35);
                    labelFavorite.Text = "\uf443";
                    labelFavorite.Font = font1;
                    if (item.isFavorite == true)
                    {
                        labelFavorite.Hidden = false;
                    }
                    else
                    {
                        labelFavorite.Hidden = true;
                    }
                }
            }

			return cell;
		}

		public string TitleForHeader(UITableView tableView, nint section)
		{
			return null;
		}

		public void addBorders(UIImageView temp)
		{
			RectangleF rect = new RectangleF((float)temp.Frame.X, (float)temp.Frame.Y, (float)temp.Frame.Width, (float)temp.Frame.Height);

			temp.Layer.CornerRadius = temp.Frame.Width / 2;
			temp.ClipsToBounds = true;
			temp.ResizableSnapshotView(rect, true, new UIEdgeInsets(0, 50, 0, 0));

		}

		public void loadImage(String urlString, UIImageView imageView, String nodeId)
		{
			string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			string localFilename = nodeId + "_image.png";
			string localPath = Path.Combine(documentsPath, localFilename);

			Console.WriteLine("localPath:" + localPath);

			if (File.Exists(localPath) == false)
			{
    			var webClient = new WebClient();
    			webClient.DownloadDataCompleted += (s, e) =>
    			{
                    try
                    {
                        var bytes = e.Result;


                        File.WriteAllBytes(localPath, bytes);

                        // IMPORTANT: this is a background thread, so any interaction with
                        // UI controls must be done via the MainThread
                        InvokeOnMainThread(() =>
                        {

                            imageView.Image = UIImage.FromFile(localPath);

                        });
                    } catch (Exception ex) {
                        Console.WriteLine("Error loading image " + ex.Message);
                    }
    			};

    			var url = new Uri(urlString);
    			webClient.DownloadDataAsync(url);
			} else {
                InvokeOnMainThread(() =>
                {

                    imageView.Image = UIImage.FromFile(localPath);

                });
            }
		}

        public void setup() {
			UIFont font = UIFont.FromName("Knowhy", 24);

			UIButton buttonFilter = (UIButton)this.View.ViewWithTag(1);
			buttonFilter.SetTitle("\uf100", UIControlState.Normal);
			buttonFilter.SetTitle("\uf101", UIControlState.Selected);

			buttonFilter.Font = font;
			buttonFilter.TouchUpInside += (sender, ea) => {
                KnoWhy.Current.toggleButtonFilter();
			};
        }
        

        public void refreshList(bool firstTime) {
            tableView.ReloadData();
            if (KnoWhy.Current.filterEnabled == true && KnoWhy.Current.filterBookAndChapter == true) {
                openBookAndChapterRow();
            } else {
                closeBookAndChapterRow();
            }
            updateBookAndChapterButton();

            showEmptyMessage();
        }

        public void showProgress() {
            loadingLabel.Text = KnoWhy.Current.CONSTANT_LOADING;
            loadingView.Hidden = false;
            activityIndicator.Hidden = false;
        }

        public void hideProgress() {
            loadingView.Hidden = true;
            activityIndicator.Hidden = true;


            if (isConnected() == false && KnoWhy.Current.metaList.Count == 0)
            {
                var alertController = UIAlertController.Create(KnoWhy.Current.CONSTANT_ERROR_TITLE, KnoWhy.Current.CONSTANT_CONNECTION_FAILED, UIAlertControllerStyle.Alert);
                alertController.AddAction(UIAlertAction.Create(KnoWhy.Current.CONSTANT_DONE, UIAlertActionStyle.Default, null));


                // Present Alert
                PresentViewController(alertController, true, null);
            }
        }

        private void showEmptyMessage() {
            if (tableView.NumberOfRowsInSection(0) == 0) {
                UILabel emptyLabel = new UILabel(new CGRect(0,0,(int)tableView.Bounds.Width,50));
                emptyLabel.Lines = 2;
                emptyLabel.TextAlignment = UITextAlignment.Center;
                emptyLabel.TextColor = UIColor.Black;
                emptyLabel.Text = KnoWhy.Current.CONSTANT_EMPTY_LIST;
                //emptyLabel.SizeToFit();
                tableView.TableHeaderView = emptyLabel;
            } else {
                tableView.TableHeaderView = null;
            }
        }

		public void updateFilter()
		{
            int i = 0;
            while (i < ChildViewControllers.Length) {
                try
                {
                    UITableViewController viewController = (UITableViewController)ChildViewControllers[i];
                    viewController.TableView.ReloadData();
                } catch (Exception e) {
                    Console.WriteLine("Error: " + e.Message);
                }
                i++;
            }

			UIButton buttonFilter = (UIButton)this.View.ViewWithTag(1);
			buttonFilter.SetTitle("\uf100", UIControlState.Normal);
			buttonFilter.SetTitle("\uf101", UIControlState.Selected);
			if (KnoWhy.Current.filterEnabled == true)
			{
				buttonFilter.Selected = true;
			}
			else
			{
				buttonFilter.Selected = false;
			}

            var firstAttributes = new UIStringAttributes
            {
                ForegroundColor = UIColor.Black
            };

            var secondAttributes = new UIStringAttributes
            {
                ForegroundColor = FromHex(0x2E70B1)
            };

            string sortedText = KnoWhy.Current.CONSTANT_SORTED_BY;
            string filterText = KnoWhy.Current.CONSTANT_FILTER_BY;

            UIButton buttonSort = (UIButton)this.View.ViewWithTag(2);
            string buttonSortText = sortedText + " " + KnoWhy.Current.getSortDesc();
            if (KnoWhy.Current.filterEnabled == true)
            {
                buttonSortText = buttonSortText + "\n" + filterText + " " + KnoWhy.Current.getFilterDesc();
            }
            NSMutableAttributedString attributedString = new NSMutableAttributedString(buttonSortText);
            attributedString.SetAttributes(secondAttributes.Dictionary, new NSRange(0, buttonSortText.Length));
            attributedString.SetAttributes(firstAttributes.Dictionary, new NSRange(0, sortedText.Length));

            if (KnoWhy.Current.filterEnabled == true)
            {
                attributedString.SetAttributes(firstAttributes.Dictionary, new NSRange(buttonSortText.IndexOf(filterText, StringComparison.OrdinalIgnoreCase), filterText.Length));
            }

            buttonSort.SetAttributedTitle(attributedString, UIControlState.Normal);
            buttonSort.TitleLabel.TextAlignment = UITextAlignment.Center;

            updateBookAndChapterButton();

            showEmptyMessage();
		}

        public void showSettings() {
            this.PerformSegue("showSettings", null);
        }
		
        public async void showArticle(int position) {
            shouldCollapseDetailViewController = false;
            if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone)
            {
                this.NavigationController.NavigationBarHidden = false;
                this.View.LayoutIfNeeded();
                this.PerformSegue("showArticle", NSNumber.FromNInt(position));
            } else {
                if (this.SplitViewController != null)
                {
                    if (this.SplitViewController.DisplayMode == UISplitViewControllerDisplayMode.PrimaryOverlay)
                    {
                        ArticleViewController.current.toggleOverlay();
                    }
                }
                Meta meta = (Meta)KnoWhy.Current.metaList.ToArray()[position];
                //await ArticleViewController.current.setMeta(meta);
                try
                {
                    if (ArticleViewController.current.NavigationController != null)
                    {
                        ArticleViewController.current.NavigationController.PopToRootViewController(false);
                    }
                    //rootSplitViewController.ViewControllers*/
                } catch (Exception e) {
                    Console.WriteLine("Exception " + e.Message);
                }
                await ArticleViewController.current.setMeta(meta);
            }
        }

        public UIColor FromHex(int hexValue)
        {
            return UIColor.FromRGB(
                (((float)((hexValue & 0xFF0000) >> 16)) / 255.0f),
                (((float)((hexValue & 0xFF00) >> 8)) / 255.0f),
                (((float)(hexValue & 0xFF)) / 255.0f)
            );
        }

        public void addMenuButton() {
            UIButton menuButton = new UIButton(UIButtonType.Custom);
            menuButton.SetImage(UIImage.FromBundle("ic_menu"), UIControlState.Normal);
            menuButton.Frame = new RectangleF(0, 0, 24, 24);

            UIBarButtonItem menuItem = new UIBarButtonItem(menuButton);

            menuButton.TouchUpInside += (sender, e) => {
                Console.WriteLine("menuButton");
                openMenu();
            };

            this.NavigationItem.LeftBarButtonItem = menuItem;
        }



        public void openMenu() {
            if (RootMenuViewController.current == null)
            {
                constraintMenuLeft.Constant = 0;
                hideIsUpdating();
                // view for dimming effect should also be shown
                viewBlack.Hidden = false;

                UIView.Animate(0.3,
                    () =>
                    {
                        this.View.LayoutIfNeeded();
                        viewBlack.Alpha = maxBlackViewAlpha;
                        UIApplication.SharedApplication.SetStatusBarHidden(true, UIStatusBarAnimation.Slide);
                    },
                    () =>
                    {
                        gestureScreenEdgePanItem.Enabled = false;

                    }
                              );
            } else {
                RootMenuViewController.current.openMenu();
            }
        }

        public void hideMenu() {
            if (RootMenuViewController.current == null)
            {
                constraintMenuLeft.Constant = -constraintMenuWidth.Constant;

                UIView.Animate(0.3,
                    () =>
                    {
                        this.View.LayoutIfNeeded();
                        viewBlack.Alpha = 0;
                        UIApplication.SharedApplication.SetStatusBarHidden(false, UIStatusBarAnimation.Slide);
                    },
                    () =>
                    {
                        gestureScreenEdgePanItem.Enabled = true;
                        viewBlack.Hidden = true;

                    }
                              );
            } else {
                RootMenuViewController.current.hideMenu();
            }
        }

        public void setLabels() {
            if (RootMenuViewController.current == null)
            {
                updateContentLabel.Text = KnoWhy.Current.CONSTANT_UPDATE_CONTENT;
                lastUpdateLabel.Text = KnoWhy.Current.getLastUpdate();
                updatingLabel.Text = KnoWhy.Current.CONSTANT_UPDATING;
                settingsLabel.Text = KnoWhy.Current.CONSTANT_SETTINGS;
                webLinksLabel.Text = KnoWhy.Current.CONSTANT_WEB_LINKS;
                link1Label.Text = KnoWhy.Current.CONSTANT_LINK1_DESC;
                if (KnoWhy.Current.CONSTANT_LINK1 != "")
                {
                    CGRect frame = link1Label.Frame;
                    frame.Height = 21;
                    link1Label.Frame = frame;
                    link1View.Hidden = false;
                }
                else
                {
                    CGRect frame = link1Label.Frame;
                    frame.Height = 0;
                    link1Label.Frame = frame;
                    link1View.Hidden = true;
                }
                link2Label.Text = KnoWhy.Current.CONSTANT_LINK2_DESC;
                if (KnoWhy.Current.CONSTANT_LINK2 != "")
                {
                    CGRect frame = link2Label.Frame;
                    frame.Height = 21;
                    link2Label.Frame = frame;
                    link2View.Hidden = false;
                }
                else
                {
                    CGRect frame = link2Label.Frame;
                    frame.Height = 0;
                    link2Label.Frame = frame;
                    link2View.Hidden = true;
                }
                link3Label.Text = KnoWhy.Current.CONSTANT_LINK3_DESC;
                if (KnoWhy.Current.CONSTANT_LINK3 != "")
                {
                    CGRect frame = link3Label.Frame;
                    frame.Height = 21;
                    link3Label.Frame = frame;
                    link3View.Hidden = false;
                }
                else
                {
                    CGRect frame = link3Label.Frame;
                    frame.Height = 0;
                    link3Label.Frame = frame;
                    link3View.Hidden = true;
                }
                this.View.LayoutIfNeeded();
                hideIsUpdating();
            } else {
                RootMenuViewController.current.setLabels();
            }
        }

        private void showIsUpdating() {
            lastUpdateLabel.Hidden = true;
            updatingLabel.Hidden = false;
            progressIndicator.StartAnimating();
        }

        private void hideIsUpdating() {
            lastUpdateLabel.Text = KnoWhy.Current.getLastUpdate();
            lastUpdateLabel.Hidden = false;
            updatingLabel.Hidden = true;
            progressIndicator.StopAnimating();
        }

        partial void tapMenu(UIBarButtonItem sender)
        {
            openMenu();
        }

        partial void gesturePan(UIPanGestureRecognizer sender)
        {

        }

        partial void gestureScreenEdgePan(UIScreenEdgePanGestureRecognizer sender)
        {
            if (sender.State == UIGestureRecognizerState.Began) {
                viewBlack.Hidden = false;
                viewBlack.Alpha = 0;
            } else if (sender.State == UIGestureRecognizerState.Changed) {
                nfloat translationX = sender.TranslationInView(sender.View).X;
                if ((-constraintMenuWidth.Constant + translationX) > 0) {
                    constraintMenuLeft.Constant = 0;
                    viewBlack.Alpha = maxBlackViewAlpha;
                } else if (translationX < 0) {
                    constraintMenuLeft.Constant = -constraintMenuWidth.Constant;
                    viewBlack.Alpha = 0;
                } else {
                    constraintMenuLeft.Constant = -constraintMenuWidth.Constant + translationX;

                    var ratio = translationX / constraintMenuWidth.Constant;
                    var alphaValue = ratio * maxBlackViewAlpha;
                    viewBlack.Alpha = alphaValue;
                }
            } else {
                if (constraintMenuLeft.Constant < (-constraintMenuWidth.Constant / 2)) {
                    hideMenu();
                } else {
                    openMenu();
                }
            }
        }

        partial void gestureTap(UITapGestureRecognizer sender)
        {
            hideMenu();
        }

        async partial void tapUpdate(UITapGestureRecognizer sender)
        {
            showIsUpdating();
            await KnoWhy.Current.loadData(true);
            hideIsUpdating();
        }

        partial void tapSettings(UITapGestureRecognizer sender)
        {
            showAppSettings();
        }

        public void showAppSettings() {
            this.PerformSegue("showAppSettings", null);
        }

        partial void tapLink1(UITapGestureRecognizer sender)
        {
            if (KnoWhy.Current.CONSTANT_LINK1 != "") {
                try
                {
                    UIApplication.SharedApplication.OpenUrl(new NSUrl(KnoWhy.Current.CONSTANT_LINK1));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }

        partial void tapLink2(UITapGestureRecognizer sender)
        {
            if (KnoWhy.Current.CONSTANT_LINK2 != "")
            {
                try {
                    UIApplication.SharedApplication.OpenUrl(new NSUrl(KnoWhy.Current.CONSTANT_LINK2));
                } catch (Exception ex) {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }

        partial void tapLink3(UITapGestureRecognizer sender)
        {
            if (KnoWhy.Current.CONSTANT_LINK3 != "")
            {
                try
                {
                    UIApplication.SharedApplication.OpenUrl(new NSUrl(KnoWhy.Current.CONSTANT_LINK3));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }

        public void updateBookAndChapterButton() {
            if (KnoWhy.Current.filterBookId == 0 && KnoWhy.Current.filterChapterId == 0) {
                buttonBack1.Hidden = true;
                buttonBack2.Hidden = true;
            } else {
                buttonBack1.Hidden = false;
                buttonBack2.Hidden = false;
            }
            Books last = KnoWhy.Current.booksList.ToArray()[KnoWhy.Current.booksList.Count - 1];
            if (KnoWhy.Current.filterBookId == (KnoWhy.Current.booksList.Count - 1) && KnoWhy.Current.filterChapterId == last.chapters)
            {
                buttonNext1.Hidden = true;
                buttonNext2.Hidden = true;
            }
            else
            {
                buttonNext1.Hidden = false;
                buttonNext2.Hidden = false;
            }
            if (KnoWhy.Current.bookChapterPanelStatus == KnoWhy.BOOK_CHAPTER_PANEL_CLOSED) {
                //buttonBack1.Hidden = false;
                //buttonNext1.Hidden = false;
                if (KnoWhy.Current.filterBookId == 0)
                {
                    buttonBookChapter.SetTitle(KnoWhy.Current.CONSTANT_ALL_BOOKS, UIControlState.Normal);

                    buttonBookChapter2.SetTitle(KnoWhy.Current.CONSTANT_ALL_BOOKS, UIControlState.Normal);
                } else {
                    Books book = KnoWhy.Current.booksList.ToArray()[KnoWhy.Current.filterBookId];
                    string desc = book.name;
                    if (KnoWhy.Current.filterChapterId > 0) {
                        desc = desc + " " + KnoWhy.Current.filterChapterId.ToString();
                    } else if (book.chapters > 0) {
                        desc = desc + "(" + KnoWhy.Current.CONSTANT_ALL_CHAPTERS + ")";
                    }

                    buttonBookChapter.SetTitle(desc, UIControlState.Normal);

                    buttonBookChapter2.SetTitle(book.name, UIControlState.Normal);
                }
            } else {
                buttonBookChapter.SetTitle(KnoWhy.Current.CONSTANT_DONE, UIControlState.Normal);

                buttonBack1.Hidden = true;
                buttonNext1.Hidden = true;
                if (KnoWhy.Current.filterBookId == 16)
                {
                    buttonNext2.Hidden = true;
                } else
                {
                    buttonNext2.Hidden = false;
                }
            }
            if (KnoWhy.Current.filterBookId == 0) {
                buttonBookChapter2.SetTitle(KnoWhy.Current.CONSTANT_ALL_BOOKS, UIControlState.Normal);
            } else {
                Books book = KnoWhy.Current.booksList.ToArray()[KnoWhy.Current.filterBookId];
                buttonBookChapter2.SetTitle(book.name, UIControlState.Normal);
            }
        }

        public void openBookAndChapterRow() {
            UIView.Animate(0.3,
    () =>
    {
        //this.View.LayoutIfNeeded();
        //bookAndChapterHeight.Constant = 0;
    },
    () =>
    {
                bookTitlePanel.Hidden = false;
                bookAndChapterHeight.Constant = bookAndChapterMaxHeight;

    }
              );
        }

        public void closeBookAndChapterRow()
        {
            UIView.Animate(0.3,
    () =>
    {
        //this.View.LayoutIfNeeded();
                //bookAndChapterHeight.Constant = bookAndChapterMaxHeight;
    },
    () =>
    {
        bookAndChapterHeight.Constant = 0;
                bookTitlePanel.Hidden = true;
    }
              );
        }

        partial void tapBookAndChapter(UIButton sender)
        {
            KnoWhy.Current.toggleFilterPanel();
        }

        partial void tapNext(UIButton sender)
        {
            Books current = KnoWhy.Current.booksList.ToArray()[KnoWhy.Current.filterBookId];
            /*if (KnoWhy.Current.filterChapterId < current.chapters) {
                KnoWhy.Current.filterChapterId = KnoWhy.Current.filterChapterId + 1;
            } else {
                KnoWhy.Current.filterChapterId = 0;
                KnoWhy.Current.filterBookId = KnoWhy.Current.filterBookId + 1;
            }*/
            if (KnoWhy.Current.bookChapterPanelStatus == KnoWhy.BOOK_CHAPTER_PANEL_CLOSED)
            {
                KnoWhy.Current.moveToNextChapter();
            } else
            {
                KnoWhy.Current.moveToNextBook();
            }
            KnoWhy.Current.reorder();
        }

        partial void tapPrevious(UIButton sender)
        {
            /*Books current = KnoWhy.Current.booksList.ToArray()[KnoWhy.Current.filterBookId];
            if (KnoWhy.Current.filterChapterId > 0)
            {
                KnoWhy.Current.filterChapterId = KnoWhy.Current.filterChapterId - 1;
            }
            else
            {
                KnoWhy.Current.filterBookId = KnoWhy.Current.filterBookId - 1;
                Books previous = KnoWhy.Current.booksList.ToArray()[KnoWhy.Current.filterBookId];
                KnoWhy.Current.filterChapterId = previous.chapters;

            }*/
            if (KnoWhy.Current.bookChapterPanelStatus == KnoWhy.BOOK_CHAPTER_PANEL_CLOSED)
            {
                KnoWhy.Current.moveToPreviousChapter();
            }
            else
            {
                KnoWhy.Current.moveToPreviousBook();
            }
            KnoWhy.Current.reorder();
        }

        public void openFilterPanel()
        {
            updateBookAndChapterButton();

            UIView.Animate(0.3,
() =>
{
        this.View.LayoutIfNeeded();
        //bookAndChapterHeight.Constant = bookAndChapterMaxHeight;
    },
() =>
{
    panelFilter2.Hidden = false;
                panelFilterHeight.Constant = panelFilterMaxHeight;

}
  );
        }

        public void closeFilterPanel()
        {
            updateBookAndChapterButton();

            UIView.Animate(0.3,
() =>
{
    this.View.LayoutIfNeeded();
    //bookAndChapterHeight.Constant = bookAndChapterMaxHeight;
},
() =>
{
                panelFilterHeight.Constant = 0;
                panelFilter2.Hidden = true;
}
                          );
        }

        partial void tapBookAndChapter2(UIButton sender)
        {
            KnoWhy.Current.toggleFilterMode();
        }

        partial void tapBookAndChapter22(UITapGestureRecognizer sender)
        {
            KnoWhy.Current.toggleFilterMode();
        }

        public void hideBooks()
        {
            UIView.Animate(0.3,
() =>
{
    //this.View.LayoutIfNeeded();
    //bookAndChapterHeight.Constant = bookAndChapterMaxHeight;
},
() =>
{
                panelBooks.Hidden = true;
                panelBooksHeight.Constant = 0;
}
                          );
        }

        public void showBooks()
        {
            UIView.Animate(0.3,
() =>
{
    panelBooks.Hidden = false;
    //this.View.LayoutIfNeeded();
    //bookAndChapterHeight.Constant = bookAndChapterMaxHeight;
},
() =>
{
                panelBooksHeight.Constant = panelChapters.Frame.Height;

}
  );
        }

        public bool isConnected()
        {
            NetworkStatus remoteHostStatus = Reachability.Reachability.RemoteHostStatus();

            if (KnoWhy.Current.onlyWiFi == true)
            {
                if (remoteHostStatus == NetworkStatus.NotReachable || remoteHostStatus != NetworkStatus.ReachableViaWiFiNetwork)
                {
                    return false;
                }
            }
            else
            {
                if (remoteHostStatus == NetworkStatus.NotReachable)
                {
                    return false;
                }
            }
            return true;
        }

        public bool isLandscape() {
            if (UIDevice.CurrentDevice.Orientation == UIDeviceOrientation.LandscapeLeft) {
                return true;
            } else if (UIDevice.CurrentDevice.Orientation == UIDeviceOrientation.LandscapeRight)
            {
                return true;
            }
            return false;
        }

        /*public async void loadIfIsEmpty() {
            if (ArticleViewController.current.meta == null)
            {
                //Meta meta = (Meta)KnoWhy.Current.metaList.ToArray()[0];
                Meta meta = (Meta)KnoWhy.Current.getFirstMeta();
                await ArticleViewController.current.setMeta(meta);
            }
        }*/

        [Export("splitViewController:collapseSecondaryViewController:ontoPrimaryViewController:")]
        public bool CollapseSecondViewController(UISplitViewController splitViewController, UIViewController secondaryViewController, UIViewController primaryViewController)
        {
            return shouldCollapseDetailViewController;
        }



    }
}

