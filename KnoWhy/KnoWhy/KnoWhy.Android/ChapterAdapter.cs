using System;
using Android.Views;
using Android.Widget;
using Android.Graphics;
using Android.OS;
using Android.Support.V7.Widget;
using KnoWhy;
using KnoWhy.Model;
using Java.Util;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;
using System.Net;
using Android.Graphics.Drawables;
using Android.App;

namespace KnoWhy.Droid
{
    public class ChapterAdapter : RecyclerView.Adapter
    {
        Activity activity = null;

        public ChapterAdapter(Activity _activity)
        {
            activity = _activity;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).
                                          Inflate(Resource.Layout.item, parent, false);
            ChapterViewHolder vh = new ChapterViewHolder(itemView, OnClick);
            return vh;
        }

        public override void
            OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            ChapterViewHolder vh = holder as ChapterViewHolder;

            if (position > 0)
            {
                vh.Text1.Text = position.ToString();
            } else
            {
                vh.Text1.Text = KnoWhy.Current.CONSTANT_ALL_CHAPTERS;
            }

            string path = "fonts/Knowhy.ttf";
            Typeface typeFace = Typeface.CreateFromAsset(activity.Assets, path);
            vh.TextMarker.Typeface = typeFace;

            if (KnoWhy.Current.filterChapterId != position)
            {
                vh.TextMarker.Visibility = ViewStates.Invisible;
            }
            else
            {
                vh.TextMarker.Visibility = ViewStates.Visible;
            }
        }

        public override int ItemCount
        {
            get {
                Books book = KnoWhy.Current.booksList.ToArray()[KnoWhy.Current.filterBookId];
                if (book == null)
                {
                    return 1;
                }
                else
                {
                    return book.chapters + 1;
                }
            }
        }

        void OnClick(int position)
        {
            if (ItemClick != null)
                ItemClick(this, position);
        }

        public void refresh()
        {
            this.NotifyDataSetChanged();
        }

        public event EventHandler<int> ItemClick;

    }


    public class ChapterViewHolder : RecyclerView.ViewHolder
    {
        public TextView Text1 { get; private set; }
        public TextView TextMarker { get; private set; }

        public ChapterViewHolder(View itemView, Action<int> listener) : base(itemView)
        {
            // Locate and cache view references:
            Text1 = itemView.FindViewById<TextView>(Resource.Id.label);
            TextMarker = itemView.FindViewById<TextView>(Resource.Id.checkmark);

            itemView.Click += (sender, e) => listener(base.AdapterPosition);
        }
    }


}
