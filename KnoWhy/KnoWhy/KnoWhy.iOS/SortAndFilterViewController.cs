using System;

using UIKit;
using Foundation;
using KnoWhy.Interfaces;
using KnoWhy.Model;

namespace KnoWhy.iOS
{
    public partial class SortAndFilterViewController : UIViewController, IUITableViewDelegate, IUITableViewDataSource, SortAndFilterInterface
    {
        UITableView tableView = null;

        public SortAndFilterViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            tableView = (UITableView)this.View.ViewWithTag(100);

            KnoWhy.Current.SortAndFilterInterface = this;

            UILabel labelTitle = (UILabel)this.View.ViewWithTag(55);
            labelTitle.Text = KnoWhy.Current.CONSTANT_SORT_AND_FILTER;
            labelTitle.Hidden = true;

            this.Title = KnoWhy.Current.CONSTANT_SORT_AND_FILTER;
            this.NavigationItem.Title = KnoWhy.Current.CONSTANT_SORT_AND_FILTER;
            this.NavigationController.NavigationItem.Title = KnoWhy.Current.CONSTANT_SORT_AND_FILTER;

			UIButton buttonClose = (UIButton)this.View.ViewWithTag(1);
            buttonClose.SetTitle(KnoWhy.Current.CONSTANT_DONE, UIControlState.Normal);
			buttonClose.TouchUpInside += (sender, ea) => {
                KnoWhy.Current.updateSettings();
                this.DismissViewController(true, () => {
                    KnoWhy.Current.reorder();
                });
			};
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        protected override void Dispose(bool disposing)
        {
            KnoWhy.Current.SortAndFilterInterface = null;
            base.Dispose(disposing);
        }

        [Export("numberOfSectionsInTableView:")]
        public nint NumberOfSections(UITableView tableView)
        {
            if (KnoWhy.Current.filterBookAndChapter == true) {
                return 4;
            }
            return 3;
        }

		public nint RowsInSection(UITableView tableView, nint section)
		{
            if (section == 0)
            {
                return 2;
            }
            else if (section == 1)
            {
                return 1;
            }
            else if (section == 2)
            {
                return 3;
            }
            else if (section == 3)
            {
                if (KnoWhy.Current.filterBookId > 0)
                {
                    Books book = KnoWhy.Current.booksList.ToArray()[KnoWhy.Current.filterBookId];
                    if (book.chapters > 0)
                    {
                        return 2;
                    } else {
                        return 1;
                    }
                } else {
                    return 1;
                }
            }
            else
            {
                return 0;
            }
		}

        [Export("tableView:heightForHeaderInSection:")]
        public nfloat GetHeightForHeader(UITableView tableView, nint section)
        {
            if (section == 1) {
                return 20;
            }
            return 40;
        }

        [Export("tableView:heightForFooterInSection:")]
        public nfloat GetHeightForFooter(UITableView tableView, nint section)
        {
            return 5;
        }

        [Export("tableView:titleForHeaderInSection:")]
        public string TitleForHeader(UITableView tableView, nint section)
        {
			if (section == 0)
			{
                return KnoWhy.Current.CONSTANT_SORT;
			}
			else if (section == 1)
			{
				return "";
			}
			else if (section == 2)
			{
                return KnoWhy.Current.CONSTANT_FILTER;
			}
            else if (section == 3)
            {
                return KnoWhy.Current.CONSTANT_BOOK_CHAPTER.ToUpper() + ":";
            }
			else
			{
				return null;
			}
		}

        public void sortByDate() {
            KnoWhy.Current.sortByDate();
        }

		public void sortByChapter()
		{
			KnoWhy.Current.sortByChapter();
		}

		public void sortAsc()
		{
			KnoWhy.Current.sortAsc();
		}

		public void sortDesc()
		{
			KnoWhy.Current.sortDesc();
		}

		public void toggleFavorites()
		{
			KnoWhy.Current.toggleFavorites();
		}

		public void toggleUnreaded()
		{
			KnoWhy.Current.toggleUnreaded();
		}

        public void toggleBookAndChapter() {
            KnoWhy.Current.toggleBookAndChapter();
        }

        public void selectBook() {
            PerformSegue("showBooks", null);
        }
        public void selectChapter() {
            PerformSegue("showChapters", null);
        }

        public void reloadData() {
            tableView.ReloadData();
        }

		public UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			// request a recycled cell to save memory
            UIFont font = UIFont.FromName("Knowhy", 16);
            UIFont font2 = UIFont.FromName("Ionicons", 16);

            if (indexPath.Section == 0)
			{
                if (indexPath.Row == 0) {
					UITableViewCell cell1 = tableView.DequeueReusableCell("cell1");
                    UILabel labelCheckmark = (UILabel)cell1.ViewWithTag(1);
                    labelCheckmark.Font = font;
                    labelCheckmark.Text = "";
                    if (KnoWhy.Current.sortType == KnoWhy.SORT_BY_DATE) {
                        labelCheckmark.Text = "\uf103";
                    }
                    UITapGestureRecognizer tapGesture1 = new UITapGestureRecognizer(sortByDate);
                    cell1.ContentView.AddGestureRecognizer(tapGesture1);
                    UILabel labelDesc = (UILabel)cell1.ViewWithTag(2);
                    labelDesc.Text = KnoWhy.Current.CONSTANT_PUBLISH_DATE;
					return cell1;
				}
				else if (indexPath.Row == 1)
				{
					UITableViewCell cell2 = tableView.DequeueReusableCell("cell2");
					UILabel labelCheckmark = (UILabel)cell2.ViewWithTag(1);
					labelCheckmark.Font = font;
					labelCheckmark.Text = "";
					if (KnoWhy.Current.sortType == KnoWhy.SORT_BY_CHAPTER)
					{
						labelCheckmark.Text = "\uf103";
					}
                    UITapGestureRecognizer tapGesture2 = new UITapGestureRecognizer(sortByChapter);
					cell2.ContentView.AddGestureRecognizer(tapGesture2);
                    UILabel labelDesc = (UILabel)cell2.ViewWithTag(2);
                    labelDesc.Text = KnoWhy.Current.CONSTANT_CHAPTER_VERSE;
					return cell2;
				}
			}
			else if (indexPath.Section == 1)
			{
                if (indexPath.Row == 0)
                {
                    UITableViewCell cell3 = tableView.DequeueReusableCell("cell3");
                    UIView view1 = (UIView)cell3.ViewWithTag(1);
                    UILabel labelCheckmark1 = (UILabel)view1.ViewWithTag(4);
                    labelCheckmark1.Font = font;
                    labelCheckmark1.Text = "";
                    if (KnoWhy.Current.sortMode == KnoWhy.SORT_ASC)
                    {
                        labelCheckmark1.Text = "\uf103";
                    }
                    UITapGestureRecognizer tapGesture3 = new UITapGestureRecognizer(sortAsc);
                    view1.AddGestureRecognizer(tapGesture3);
                    UILabel labelDesc1 = (UILabel)cell3.ViewWithTag(3);
                    labelDesc1.Text = KnoWhy.Current.CONSTANT_ASCEND_C;
                    UIView view2 = (UIView)cell3.ViewWithTag(2);
                    UILabel labelCheckmark2 = (UILabel)view2.ViewWithTag(6);
                    labelCheckmark2.Font = font;
                    labelCheckmark2.Text = "";
                    if (KnoWhy.Current.sortMode == KnoWhy.SORT_DESC)
                    {
                        labelCheckmark2.Text = "\uf103";
                    }
                    UITapGestureRecognizer tapGesture4 = new UITapGestureRecognizer(sortDesc);
                    view2.AddGestureRecognizer(tapGesture4);
                    UILabel labelDesc2 = (UILabel)cell3.ViewWithTag(5);
                    labelDesc2.Text = KnoWhy.Current.CONSTANT_DESCEND_C;
                    return cell3;
                }
			}
			else if (indexPath.Section == 2)
			{
				if (indexPath.Row == 0)
				{
					UITableViewCell cell4 = tableView.DequeueReusableCell("cell4");
					UILabel labelCheckmark = (UILabel)cell4.ViewWithTag(1);
					labelCheckmark.Font = font;
					labelCheckmark.Text = "";
                    if (KnoWhy.Current.filterFavorites == true)
					{
						labelCheckmark.Text = "\uf103";
					}
                    UILabel labelIcon = (UILabel)cell4.ViewWithTag(3);
                    labelIcon.Font = font2;
                    labelIcon.Text = "\uf443";
                    UITapGestureRecognizer tapGesture5 = new UITapGestureRecognizer(toggleFavorites);
                    cell4.ContentView.AddGestureRecognizer(tapGesture5);
                    UILabel labelDesc = (UILabel)cell4.ViewWithTag(2);
                    labelDesc.Text = KnoWhy.Current.CONSTANT_FAVORITES;
					return cell4;
				}
				else if (indexPath.Row == 1)
				{
					UITableViewCell cell5 = tableView.DequeueReusableCell("cell5");
					UILabel labelCheckmark = (UILabel)cell5.ViewWithTag(1);
					labelCheckmark.Font = font;
					labelCheckmark.Text = "";
					if (KnoWhy.Current.filterUnreaded == true)
					{
						labelCheckmark.Text = "\uf103";
					}
					UILabel labelIcon = (UILabel)cell5.ViewWithTag(3);
					labelIcon.Font = font;
					labelIcon.Text = "\uf102";
                    UITapGestureRecognizer tapGesture6 = new UITapGestureRecognizer(toggleUnreaded);
					cell5.ContentView.AddGestureRecognizer(tapGesture6);
                    UILabel labelDesc = (UILabel)cell5.ViewWithTag(2);
                    labelDesc.Text = KnoWhy.Current.CONSTANT_UNREAD;
					return cell5;
                }
                else if (indexPath.Row == 2)
                {
                    UITableViewCell cell6 = tableView.DequeueReusableCell("cell6");
                    UILabel labelCheckmark = (UILabel)cell6.ViewWithTag(1);
                    labelCheckmark.Font = font;
                    labelCheckmark.Text = "";
                    if (KnoWhy.Current.filterBookAndChapter == true)
                    {
                        labelCheckmark.Text = "\uf103";
                    }
                    UILabel labelIcon = (UILabel)cell6.ViewWithTag(3);
                    labelIcon.Font = font2;
                    labelIcon.Text = "\uf3e8";
                    UITapGestureRecognizer tapGesture6 = new UITapGestureRecognizer(toggleBookAndChapter);
                    cell6.ContentView.AddGestureRecognizer(tapGesture6);
                    UILabel labelDesc = (UILabel)cell6.ViewWithTag(2);
                    labelDesc.Text = KnoWhy.Current.CONSTANT_BOOK_CHAPTER;
                    return cell6;
                }
            } else if (indexPath.Section == 3) {
                if (indexPath.Row == 0)
                {
                    UITableViewCell cell4 = tableView.DequeueReusableCell("cell7");

                    UITapGestureRecognizer tapGesture5 = new UITapGestureRecognizer(selectBook);
                    cell4.ContentView.AddGestureRecognizer(tapGesture5);
                    UILabel labelDesc = (UILabel)cell4.ViewWithTag(2);
                    labelDesc.Text = KnoWhy.Current.CONSTANT_BOOK;
                    UILabel labelBook = (UILabel)cell4.ViewWithTag(3);
                    labelBook.Text = KnoWhy.Current.CONSTANT_ALL_BOOKS;
                    if (KnoWhy.Current.filterBookId > 0) {
                        Books book = KnoWhy.Current.booksList.ToArray()[KnoWhy.Current.filterBookId];
                        labelBook.Text = book.name;
                    }
                    return cell4;
                }
                else if (indexPath.Row == 1)
                {
                    UITableViewCell cell4 = tableView.DequeueReusableCell("cell8");

                    UITapGestureRecognizer tapGesture5 = new UITapGestureRecognizer(selectChapter);
                    cell4.ContentView.AddGestureRecognizer(tapGesture5);
                    UILabel labelDesc = (UILabel)cell4.ViewWithTag(2);
                    labelDesc.Text = KnoWhy.Current.CONSTANT_CHAPTER;
                    UILabel labelChapter = (UILabel)cell4.ViewWithTag(3);
                    labelChapter.Text = KnoWhy.Current.CONSTANT_ALL_CHAPTERS;
                    if (KnoWhy.Current.filterChapterId > 0) {
                        labelChapter.Text = KnoWhy.Current.filterChapterId.ToString();
                    }
                    return cell4;
                }
            }
            return null;
		}

    }
}

