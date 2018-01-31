
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Support.V7.Widget;
using Android.Support.V7.App;
using Android.Support.V4.App;
using Android.Support.V4.Widget;
using Android.Graphics;
using KnoWhy.Interfaces;
using Java.Util;
using Android.Net;
using KnoWhy.Model;

namespace KnoWhy.Droid
{
    public class ListFragment : Fragment, ListInterface
    {

        static ListFragment _current = null;
        public static ListFragment Current
        {
            get
            {
                return _current;
            }
        }

        EmptyRecyclerView mRecyclerView;
        RecyclerView.LayoutManager mLayoutManager;
        ArticleAdapter mAdapter;
        RecyclerView mRecyclerViewChapter;
        RecyclerView.LayoutManager mLayoutManagerChapter;
        RecyclerView mRecyclerViewBook;
        RecyclerView.LayoutManager mLayoutManagerBook;
        BookAdapter bookAdapter = null;
        ChapterAdapter chapterAdapter = null;

        SwipeRefreshLayout swipeContainer;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            View view = inflater.Inflate(Resource.Layout.MainList, container, false);

            AppCompatActivity activity = (AppCompatActivity)this.Activity;

            Toolbar toolbar = view.FindViewById<Toolbar>(Resource.Id.toolbar);
            activity.SetSupportActionBar(toolbar);
            activity.SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            activity.SupportActionBar.SetDisplayShowTitleEnabled(true);
            activity.SupportActionBar.SetHomeButtonEnabled(true);
            activity.SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_drawer);
            activity.SupportActionBar.Title = "All KnoWhys";

            bookAdapter = new BookAdapter(activity);
            bookAdapter.ItemClick += OnBookClick;

            // Get our RecyclerView layout:
            mRecyclerViewBook = view.FindViewById<RecyclerView>(Resource.Id.myListBooks);
            mLayoutManagerBook = new LinearLayoutManager(activity);
            mRecyclerViewBook.SetLayoutManager(mLayoutManagerBook);
            // Plug the adapter into the RecyclerView:
            mRecyclerViewBook.SetAdapter(bookAdapter);

            chapterAdapter = new ChapterAdapter(activity);
            chapterAdapter.ItemClick += OnChapterClick;

            mRecyclerViewChapter = view.FindViewById<RecyclerView>(Resource.Id.myListChapters);
            mLayoutManagerChapter = new LinearLayoutManager(activity);
            mRecyclerViewChapter.SetLayoutManager(mLayoutManagerChapter);
            // Plug the adapter into the RecyclerView:
            mRecyclerViewChapter.SetAdapter(chapterAdapter);

            swipeContainer = view.FindViewById<SwipeRefreshLayout>(Resource.Id.swipeContainer);
            swipeContainer.Refresh += async delegate {
                await KnoWhy.Current.loadData(true);
                swipeContainer.Refreshing = false;
            };


            string language = Locale.Default.ISO3Language;

            if (language == "spa")
            {
                KnoWhy.Current.locale = "es";
            }

            Handler handler = new Handler();
            Action myAction = async () =>
            {

                await KnoWhy.Current.init(this);

            };

            handler.PostDelayed(myAction, 0);

            return view;
        }


        void OnBookClick(object sender, int position)
        {
            if (KnoWhy.Current.filterBookId != position)
            {
                KnoWhy.Current.filterBookId = position;
                KnoWhy.Current.filterChapterId = 0;
            }
            //OnBackPressed();
            KnoWhy.Current.reorder();
            toggleFilterPanel();
            bookAdapter.refresh();
        }

        void OnChapterClick(object sender, int position)
        {
            KnoWhy.Current.filterChapterId = position;
            //OnBackPressed();
            KnoWhy.Current.reorder();
            chapterAdapter.refresh();
        }


        public void setup()
        {
            Android.Widget.Button buttonPrevious = (Android.Widget.Button)View.FindViewById(Resource.Id.buttonPrevious);
            buttonPrevious.Click += (sender, e) => {
                KnoWhy.Current.moveToPreviousChapter();
            };
            Android.Widget.Button buttonNext = (Android.Widget.Button)View.FindViewById(Resource.Id.buttonNext);
            buttonNext.Click += (sender, e) => {
                KnoWhy.Current.moveToNextChapter();
            };

            Android.Widget.Button buttonChapter = (Android.Widget.Button)View.FindViewById(Resource.Id.buttonChapter);
            buttonChapter.Click += (sender, e) => {
                openFilterPanel();
            };

            Android.Widget.Button buttonPrevious2 = (Android.Widget.Button)View.FindViewById(Resource.Id.buttonPrevious2);
            buttonPrevious2.Click += (sender, e) => {
                KnoWhy.Current.moveToPreviousBook();
                bookAdapter.refresh();
            };
            Android.Widget.Button buttonNext2 = (Android.Widget.Button)View.FindViewById(Resource.Id.buttonNext2);
            buttonNext2.Click += (sender, e) => {
                KnoWhy.Current.moveToNextBook();
                bookAdapter.refresh();
            };
            Android.Widget.Button buttonBook2 = (Android.Widget.Button)View.FindViewById(Resource.Id.buttonBook2);
            buttonBook2.Click += (sender, e) => {
                toggleFilterPanel();
            };


            string path2 = "fonts/Ionicons.ttf";
            Typeface typeFace2 = Typeface.CreateFromAsset(Activity.Assets, path2);
            buttonPrevious.Typeface = typeFace2;
            buttonNext.Typeface = typeFace2;
            buttonPrevious2.Typeface = typeFace2;
            buttonNext2.Typeface = typeFace2;

            Android.Widget.Button buttonFilter = (Android.Widget.Button)View.FindViewById(Resource.Id.buttonFilter);
            string path = "fonts/Knowhy.ttf";
            Typeface typeFace = Typeface.CreateFromAsset(Activity.Assets, path);
            buttonFilter.Typeface = typeFace;
            updateFilter();
            buttonFilter.Click += (sender, e) => {
                KnoWhy.Current.toggleButtonFilter();
            };

            Android.Widget.LinearLayout itemsLinearLayout = (Android.Widget.LinearLayout)View.FindViewById(Resource.Id.itemsLinearLayout);
            itemsLinearLayout.Click += (sender, e) => {
                MainActivity.Current.showSettings();
            };

            // Instantiate the adapter and pass in its data source:
            mAdapter = new ArticleAdapter(MainActivity.Current);
            mAdapter.ItemClick += OnItemClick;


            // Get our RecyclerView layout:
            mRecyclerView = View.FindViewById<EmptyRecyclerView>(Resource.Id.myList);
            mRecyclerView.setEmptyView(View.FindViewById(Resource.Id.empty_view));
            mLayoutManager = new LinearLayoutManager(Activity);
            mRecyclerView.SetLayoutManager(mLayoutManager);
            // Plug the adapter into the RecyclerView:
            mRecyclerView.SetAdapter(mAdapter);

            MainActivity.Current.initMenu();

        }

        void OnItemClick(object sender, int position)
        {
            MainActivity.Current.showArticle(position, true);
        }

        public void updateFilter()
        {
            Activity.RunOnUiThread(() => {
                Android.Widget.LinearLayout sortLinearLayout = (Android.Widget.LinearLayout)View.FindViewById(Resource.Id.sortLinearLayout);
                Android.Widget.LinearLayout filterLinearLayout = (Android.Widget.LinearLayout)View.FindViewById(Resource.Id.filterLinearLayout);

                Android.Widget.TextView descSort = (Android.Widget.TextView)View.FindViewById(Resource.Id.descSort);
                descSort.Text = KnoWhy.Current.getSortDesc();

                Android.Widget.TextView descFilter = (Android.Widget.TextView)View.FindViewById(Resource.Id.descFilter);
                descFilter.Text = KnoWhy.Current.getFilterDesc();

                Android.Widget.Button buttonChapter = (Android.Widget.Button)View.FindViewById(Resource.Id.buttonChapter);
                buttonChapter.Text = KnoWhy.Current.getCurrentChapterDesc();

                Android.Widget.Button buttonBook2 = (Android.Widget.Button)View.FindViewById(Resource.Id.buttonBook2);
                Books books = KnoWhy.Current.booksList.ToArray()[KnoWhy.Current.filterBookId];
                buttonBook2.Text = books.name;

                Android.Widget.Button buttonFilter = (Android.Widget.Button)View.FindViewById(Resource.Id.buttonFilter);
                if (KnoWhy.Current.filterEnabled == false)
                {
                    buttonFilter.Text = "\uf100";
                    filterLinearLayout.Visibility = Android.Views.ViewStates.Gone;
                }
                else
                {
                    buttonFilter.Text = "\uf101";
                    filterLinearLayout.Visibility = Android.Views.ViewStates.Visible;
                }
                Android.Widget.LinearLayout panelFilters = (Android.Widget.LinearLayout)View.FindViewById(Resource.Id.panelFilters);
                if (panelFilters.Visibility == ViewStates.Visible)
                {
                    showButtonNavButtons(true);
                }
                else
                {
                    showButtonNavButtons(false);
                }

                Android.Widget.LinearLayout rowBookAndChapter = (Android.Widget.LinearLayout)View.FindViewById(Resource.Id.rowBookAndChapter);
                if (KnoWhy.Current.filterEnabled == true)
                {
                    rowBookAndChapter.Visibility = ViewStates.Visible;
                }
                else
                {
                    rowBookAndChapter.Visibility = ViewStates.Gone;
                }
            });


        }

        public void showButtonNavButtons(bool show)
        {
            Android.Widget.Button buttonPrevious = (Android.Widget.Button)View.FindViewById(Resource.Id.buttonPrevious);
            Android.Widget.Button buttonNext = (Android.Widget.Button)View.FindViewById(Resource.Id.buttonNext);
            Android.Widget.Button buttonPrevious2 = (Android.Widget.Button)View.FindViewById(Resource.Id.buttonPrevious2);
            Android.Widget.Button buttonNext2 = (Android.Widget.Button)View.FindViewById(Resource.Id.buttonNext2);
            Android.Widget.Button buttonChapter = (Android.Widget.Button)View.FindViewById(Resource.Id.buttonChapter);

            buttonChapter.Text = KnoWhy.Current.getCurrentChapterDesc();
            if (KnoWhy.Current.filterBookId == 0)
            {
                buttonPrevious.Visibility = ViewStates.Gone;
                buttonPrevious2.Visibility = ViewStates.Gone;
            }
            else
            {
                buttonPrevious.Visibility = ViewStates.Visible;
                buttonPrevious2.Visibility = ViewStates.Visible;
            }

            if (KnoWhy.Current.filterBookId == 16 && KnoWhy.Current.filterChapterId == 10) //The last book id is 16 and the last chapter of that book is 10
            {
                buttonNext.Visibility = ViewStates.Gone;
            }
            else
            {
                buttonNext.Visibility = ViewStates.Visible;
            }
            if (KnoWhy.Current.filterBookId == 16)
            {
                buttonNext2.Visibility = ViewStates.Gone;
            }
            else
            {
                buttonNext2.Visibility = ViewStates.Visible;
            }

            if (show == true)
            {
                buttonChapter.Text = KnoWhy.Current.CONSTANT_DONE;
                buttonPrevious.Visibility = ViewStates.Gone;
                buttonNext.Visibility = ViewStates.Gone;
            }
        }


        public void openFilterPanel()
        {
            Android.Widget.LinearLayout panelFilters = (Android.Widget.LinearLayout)View.FindViewById(Resource.Id.panelFilters);
            if (panelFilters.Visibility == ViewStates.Gone)
            {
                Android.Widget.LinearLayout panelBooks = (Android.Widget.LinearLayout)View.FindViewById(Resource.Id.panelBooks);
                Android.Widget.LinearLayout panelChapters = (Android.Widget.LinearLayout)View.FindViewById(Resource.Id.panelChapters);
                panelBooks.Visibility = ViewStates.Gone;
                panelChapters.Visibility = ViewStates.Visible;
                showButtonNavButtons(true);
                Android.Widget.Button buttonBook2 = (Android.Widget.Button)View.FindViewById(Resource.Id.buttonBook2);
                Books books = KnoWhy.Current.booksList.ToArray()[KnoWhy.Current.filterBookId];
                buttonBook2.Text = books.name;
                panelFilters.Visibility = ViewStates.Visible;
            }
            else
            {
                showButtonNavButtons(false);
                panelFilters.Visibility = ViewStates.Gone;
            }
        }

        public void toggleFilterPanel()
        {
            Android.Widget.LinearLayout panelBooks = (Android.Widget.LinearLayout)View.FindViewById(Resource.Id.panelBooks);
            Android.Widget.LinearLayout panelChapters = (Android.Widget.LinearLayout)View.FindViewById(Resource.Id.panelChapters);
            if (panelBooks.Visibility == ViewStates.Visible)
            {
                panelBooks.Visibility = ViewStates.Gone;
                panelChapters.Visibility = ViewStates.Visible;
            }
            else
            {
                panelBooks.Visibility = ViewStates.Visible;
                panelChapters.Visibility = ViewStates.Gone;
            }

        }

        public void closeFilterPanel()
        {
            //throw new NotImplementedException();
        }

        public void hideBooks()
        {
            //throw new NotImplementedException();
        }

        public void showBooks()
        {
            //throw new NotImplementedException();
        }

        public bool isConnected()
        {
            return MainActivity.Current.isConnected();
        }

        public void refreshList(bool firstTime)
        {
            Activity.RunOnUiThread(() => {
                if (mAdapter != null)
                {
                    mAdapter.NotifyDataSetChanged();
                }
                MainActivity.Current.updating(false);

                if (KnoWhy.Current.metaList.Count > 0 && MainActivity.Current.getCurrentPosition() < 0 && firstTime)
                {
                    //if (MainActivity.Current.isTablet == true && (MainActivity.Current.isLandscape == true || KnoWhy.Current.lastNode > 0))
                    if ((MainActivity.Current.isTablet == true && MainActivity.Current.isLandscape == true) || KnoWhy.Current.lastNode > 0)
                    {
                        MainActivity.Current.showArticle(KnoWhy.Current.getFirstMetaIndex(), true);
                    }

                }
            });

        }

        public void showProgress()
        {
            Android.Widget.LinearLayout viewProgress = (Android.Widget.LinearLayout)View.FindViewById(Resource.Id.viewProgress);
            viewProgress.Visibility = ViewStates.Visible;
        }

        public void hideProgress()
        {
            Android.Widget.LinearLayout viewProgress = (Android.Widget.LinearLayout)View.FindViewById(Resource.Id.viewProgress);
            viewProgress.Visibility = ViewStates.Gone;

            if (isConnected() == false && KnoWhy.Current.metaList.Count == 0)
            {
                MainActivity.Current.showConnectionErrorDialog();
            }
        }

        public void showSettings()
        {
            MainActivity.Current.showSettings();
        }

        public void showArticle(int position)
        {

            MainActivity.Current.showArticle(position, true);
        }
    }
}
