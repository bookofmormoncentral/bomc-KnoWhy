using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics;
using KnoWhy.Interfaces;
using KnoWhy.Model;
using Android.Support.V7.App;
using V7Toolbar = Android.Support.V7.Widget.Toolbar;

namespace KnoWhy.Droid
{
    [Activity(Label = "SortAndFilter", Theme = "@style/Theme.Dialog")]
    public class SortAndFilterActivity : AppCompatActivity, SortAndFilterInterface
    {
        public static int SELECT_BOOK = 0;
        public static int SELECT_CHAPTER = 1;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.SortAndFilter);

            var toolbar = FindViewById<V7Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(false);
            SupportActionBar.SetDisplayShowTitleEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(false);
            //SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_drawer);

            this.Title = KnoWhy.Current.CONSTANT_SORT_AND_FILTER;

            TextView buttonClose = (TextView)FindViewById(Resource.Id.buttonClose);
            buttonClose.Click += (sender, e) => {
                Finish();
            };



            RelativeLayout sortType1LinearLayout = (RelativeLayout)FindViewById(Resource.Id.type1LinearLayout);
            sortType1LinearLayout.Click += (sender, e) => {
                sortByDate();
            };
            RelativeLayout sortType2LinearLayout = (RelativeLayout)FindViewById(Resource.Id.type2LinearLayout);
            sortType2LinearLayout.Click += (sender, e) => {
                sortByChapter();
            };
            RelativeLayout sort1LinearLayout = (RelativeLayout)FindViewById(Resource.Id.sort1LinearLayout);
            sort1LinearLayout.Click += (sender, e) => {
                sortAsc();
            };
            RelativeLayout sort2LinearLayout = (RelativeLayout)FindViewById(Resource.Id.sort2LinearLayout);
            sort2LinearLayout.Click += (sender, e) => {
                sortDesc();
            };
            RelativeLayout filter1LinearLayout = (RelativeLayout)FindViewById(Resource.Id.filter1LinearLayout);
            filter1LinearLayout.Click += (sender, e) => {
                toggleFavorites();
            };
            RelativeLayout filter2LinearLayout = (RelativeLayout)FindViewById(Resource.Id.filter2LinearLayout);
            filter2LinearLayout.Click += (sender, e) => {
                toggleUnreaded();
            };
            RelativeLayout filter3LinearLayout = (RelativeLayout)FindViewById(Resource.Id.filter3LinearLayout);
            filter3LinearLayout.Click += (sender, e) => {
                toggleBookAndChapter();
            };

            RelativeLayout bookLinearLayout = (RelativeLayout)FindViewById(Resource.Id.bookLinearLayout);
            bookLinearLayout.Click += (sender, e) => {
                selectBook();
            };
            RelativeLayout chapterLinearLayout = (RelativeLayout)FindViewById(Resource.Id.chapterLinearLayout);
            chapterLinearLayout.Click += (sender, e) => {
                selectChapter();
            };
            bookLinearLayout.Visibility = ViewStates.Gone;
            if (KnoWhy.Current.filterBookAndChapter == true)
            {
                bookLinearLayout.Visibility = ViewStates.Visible;
            }
            chapterLinearLayout.Visibility = ViewStates.Gone;
            if (KnoWhy.Current.filterBookId > 0)
            {
                Books books = KnoWhy.Current.booksList.ToArray()[KnoWhy.Current.filterBookId];
                if (books.chapters > 0)
                {
                    chapterLinearLayout.Visibility = ViewStates.Visible;
                }
            }

            string path = "fonts/Knowhy.ttf";
            Typeface typeFace = Typeface.CreateFromAsset(Assets, path);

			string path2 = "fonts/Ionicons.ttf";
			Typeface typeFace2 = Typeface.CreateFromAsset(Assets, path2);

            TextView textType1 = (TextView)FindViewById(Resource.Id.type1Checkmark);
            textType1.Typeface = typeFace;

            TextView textType2 = (TextView)FindViewById(Resource.Id.type2Checkmark);
            textType2.Typeface = typeFace;

            TextView textSort1 = (TextView)FindViewById(Resource.Id.sort1Checkmark);
            textSort1.Typeface = typeFace;

            TextView textSort2 = (TextView)FindViewById(Resource.Id.sort2Checkmark);
            textSort2.Typeface = typeFace;

            TextView textFilter1 = (TextView)FindViewById(Resource.Id.filter1Checkmark);
            textFilter1.Typeface = typeFace;

            TextView textFilter2 = (TextView)FindViewById(Resource.Id.filter2Checkmark);
            textFilter2.Typeface = typeFace;

            TextView textFilter3 = (TextView)FindViewById(Resource.Id.filter3Checkmark);
            textFilter3.Typeface = typeFace;

            TextView textFilterIcon1 = (TextView)FindViewById(Resource.Id.filter1Icon);
			textFilterIcon1.Typeface = typeFace2;

			TextView textFilterIcon2 = (TextView)FindViewById(Resource.Id.filter2Icon);
			textFilterIcon2.Typeface = typeFace;

            TextView textFilterIcon3 = (TextView)FindViewById(Resource.Id.filter3Icon);
            textFilterIcon3.Typeface = typeFace2;

            KnoWhy.Current.SortAndFilterInterface = this;
        }

        protected override void OnDestroy()
        {
            KnoWhy.Current.updateSettings();
            KnoWhy.Current.SortAndFilterInterface = null;
            base.OnDestroy();
        }

		public void sortByDate()
		{
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

        public void toggleBookAndChapter()
        {
            KnoWhy.Current.toggleBookAndChapter();
        }

        public void selectBook()
        {
            var activity = new Intent(this, typeof(SelectItemActivity));
            activity.PutExtra("mode", SELECT_BOOK);
            StartActivityForResult(activity, SELECT_BOOK);
            OverridePendingTransition(Resource.Animation.left_in, Resource.Animation.left_out);
        }

        public void selectChapter()
        {
            var activity = new Intent(this, typeof(SelectItemActivity));
            activity.PutExtra("mode", SELECT_CHAPTER);
            StartActivityForResult(activity, SELECT_CHAPTER);
            OverridePendingTransition(Resource.Animation.left_in, Resource.Animation.left_out);
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == SELECT_BOOK)
            {

            } else if (requestCode == SELECT_CHAPTER)
            {

            }
            reloadData();
        }

        public void reloadData()
		{
			TextView textType1 = (TextView)FindViewById(Resource.Id.type1Checkmark);
			if (KnoWhy.Current.sortType == KnoWhy.SORT_BY_DATE)
			{
				textType1.Visibility = ViewStates.Visible;
			}
			else
			{
				textType1.Visibility = ViewStates.Invisible;
			}

			TextView textType2 = (TextView)FindViewById(Resource.Id.type2Checkmark);
			if (KnoWhy.Current.sortType == KnoWhy.SORT_BY_CHAPTER)
			{
				textType2.Visibility = ViewStates.Visible;
			}
			else
			{
				textType2.Visibility = ViewStates.Invisible;
			}

			TextView textSort1 = (TextView)FindViewById(Resource.Id.sort1Checkmark);
			if (KnoWhy.Current.sortMode == KnoWhy.SORT_ASC)
			{
				textSort1.Visibility = ViewStates.Visible;
			}
			else
			{
				textSort1.Visibility = ViewStates.Invisible;
			}

			TextView textSort2 = (TextView)FindViewById(Resource.Id.sort2Checkmark);
			if (KnoWhy.Current.sortMode == KnoWhy.SORT_DESC)
			{
				textSort2.Visibility = ViewStates.Visible;
			}
			else
			{
				textSort2.Visibility = ViewStates.Invisible;
			}

			TextView textFilter1 = (TextView)FindViewById(Resource.Id.filter1Checkmark);
			if (KnoWhy.Current.filterFavorites == true)
			{
				textFilter1.Visibility = ViewStates.Visible;
			}
			else
			{
				textFilter1.Visibility = ViewStates.Invisible;
			}

			TextView textFilter2 = (TextView)FindViewById(Resource.Id.filter2Checkmark);
			if (KnoWhy.Current.filterUnreaded == true)
			{
				textFilter2.Visibility = ViewStates.Visible;
			}
			else
			{
				textFilter2.Visibility = ViewStates.Invisible;
			}

            RelativeLayout bookLinearLayout = (RelativeLayout)FindViewById(Resource.Id.bookLinearLayout);
            RelativeLayout chapterLinearLayout = (RelativeLayout)FindViewById(Resource.Id.chapterLinearLayout);

            TextView textFilter3 = (TextView)FindViewById(Resource.Id.filter3Checkmark);
            if (KnoWhy.Current.filterBookAndChapter == true)
            {
                textFilter3.Visibility = ViewStates.Visible;

                bookLinearLayout.Visibility = ViewStates.Visible;
                chapterLinearLayout.Visibility = ViewStates.Gone;
                if (KnoWhy.Current.filterBookId > 0)
                {
                    Books item = KnoWhy.Current.booksList.ToArray()[KnoWhy.Current.filterBookId];
                    if (item.chapters > 0)
                    {
                        chapterLinearLayout.Visibility = ViewStates.Visible;
                    }
                }
            }
            else
            {
                textFilter3.Visibility = ViewStates.Invisible;

                bookLinearLayout.Visibility = ViewStates.Gone;
                chapterLinearLayout.Visibility = ViewStates.Gone;
            }

            TextView labelBook = (TextView)FindViewById(Resource.Id.labelBook);
            Books book = KnoWhy.Current.booksList.ToArray()[KnoWhy.Current.filterBookId];
            labelBook.Text = book.name;

            TextView labelChapter = (TextView)FindViewById(Resource.Id.labelChapter);
            if (KnoWhy.Current.filterChapterId > 0)
            {
                labelChapter.Text = KnoWhy.Current.filterChapterId.ToString();
            } else
            {
                labelChapter.Text = KnoWhy.Current.CONSTANT_ALL_CHAPTERS;
            }
        }
    }
}