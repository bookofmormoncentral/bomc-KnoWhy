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
using Android.Support.V7.App;
using V7Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.V7.Widget;
using KnoWhy.Model;

namespace KnoWhy.Droid
{
    [Activity(Label = "SelectItemActivity", Theme = "@style/Theme.Dialog", ParentActivity = typeof(SortAndFilterActivity))]
    public class SelectItemActivity : AppCompatActivity
    {
        RecyclerView mRecyclerView;
        RecyclerView.LayoutManager mLayoutManager;
        BookAdapter bookAdapter = null;
        ChapterAdapter chapterAdapter = null;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.SelectItem);

            var toolbar = FindViewById<V7Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowTitleEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);
            //SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_drawer);

            int mode = this.Intent.GetIntExtra("mode", 0);

            if (mode == 0)
            {
                this.Title = KnoWhy.Current.CONSTANT_SELECT_BOOK;

                // Instantiate the adapter and pass in its data source:
                bookAdapter = new BookAdapter(this);
                bookAdapter.ItemClick += OnBookClick;


                // Get our RecyclerView layout:
                mRecyclerView = FindViewById<RecyclerView>(Resource.Id.myList);
                mLayoutManager = new LinearLayoutManager(this);
                mRecyclerView.SetLayoutManager(mLayoutManager);
                // Plug the adapter into the RecyclerView:
                mRecyclerView.SetAdapter(bookAdapter);
            } else
            {
                this.Title = KnoWhy.Current.CONSTANT_SELECT_CHAPTER;
                

                // Instantiate the adapter and pass in its data source:
                chapterAdapter = new ChapterAdapter(this);
                chapterAdapter.ItemClick += OnChapterClick;


                // Get our RecyclerView layout:
                mRecyclerView = FindViewById<RecyclerView>(Resource.Id.myList);
                mLayoutManager = new LinearLayoutManager(this);
                mRecyclerView.SetLayoutManager(mLayoutManager);
                // Plug the adapter into the RecyclerView:
                mRecyclerView.SetAdapter(chapterAdapter);
            }
        }

        void OnBookClick(object sender, int position)
        {
            if (KnoWhy.Current.filterBookId != position)
            {
                KnoWhy.Current.filterBookId = position;
                KnoWhy.Current.filterChapterId = 0;
            }
            OnBackPressed();
        }

        void OnChapterClick(object sender, int position)
        {
            KnoWhy.Current.filterChapterId = position;
            OnBackPressed();
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Android.Resource.Id.Home)
            {
                OnBackPressed();
                return false;
            }
            else
            {
                return base.OnOptionsItemSelected(item);
            }
        }

        public override void OnBackPressed()
        {
            base.OnBackPressed();
            OverridePendingTransition(Resource.Animation.right_in, Resource.Animation.right_out);
        }
    }
}