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
    public class BookAdapter : RecyclerView.Adapter
    {
        Activity activity = null;

        public BookAdapter(Activity _activity)
        {
            activity = _activity;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).
                                          Inflate(Resource.Layout.item, parent, false);
            BookViewHolder vh = new BookViewHolder(itemView, OnClick);
            return vh;
        }

        public override void
            OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            BookViewHolder vh = holder as BookViewHolder;

            Books book = KnoWhy.Current.booksList.ToArray()[position];
            vh.Text1.Text = book.name;

            string path = "fonts/Knowhy.ttf";
            Typeface typeFace = Typeface.CreateFromAsset(activity.Assets, path);
            vh.TextMarker.Typeface = typeFace;

            if (KnoWhy.Current.filterBookId != position)
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
            get { return KnoWhy.Current.booksList.Count; }
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
    

    public class BookViewHolder : RecyclerView.ViewHolder
    {
        public TextView Text1 { get; private set; }
        public TextView TextMarker { get; private set; }

        public BookViewHolder(View itemView, Action<int> listener) : base(itemView)
        {
            // Locate and cache view references:
            Text1 = itemView.FindViewById<TextView>(Resource.Id.label);
            TextMarker = itemView.FindViewById<TextView>(Resource.Id.checkmark);

            itemView.Click += (sender, e) => listener(base.AdapterPosition);
        }
    }


}
