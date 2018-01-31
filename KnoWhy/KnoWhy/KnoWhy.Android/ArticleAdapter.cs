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
using System.Linq;

namespace KnoWhy.Droid
{
    public class ArticleAdapter : RecyclerView.Adapter
    {
        MainActivity activity = null;
        
        public ArticleAdapter(MainActivity _activity)
        {
            activity = _activity;
        }

		public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
		{
			View itemView = LayoutInflater.From(parent.Context).
                                          Inflate(Resource.Layout.article, parent, false);
			ArticleViewHolder vh = new ArticleViewHolder(itemView, OnClick);
			return vh;
		}

        public override void
			OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
		{
			ArticleViewHolder vh = holder as ArticleViewHolder;

            Meta item = (Meta)KnoWhy.Current.metaList.ToArray()[position];
            try
            {
                //vh.Image.SetImageBitmap(await GetImageBitmapFromUrlAsync(item.mainImageURL));
                loadImage(item.mainImageURL, vh.Image, item.nodeId.ToString());
            } catch (Exception e) {
                Console.WriteLine(e.Message);
            }

            vh.Text1.Text = item.formattedDate;
            vh.Text2.Text = item.title;
            vh.Text3.Text = item.scriptureReference;

			string path = "fonts/Knowhy.ttf";
            Typeface typeFace = Typeface.CreateFromAsset(activity.Assets, path);
            vh.TextMarker.Typeface = typeFace;

            if (item.isRead == true) {
                vh.TextMarker.Visibility = ViewStates.Invisible;
            } else {
                vh.TextMarker.Visibility = ViewStates.Visible;
            }

            string path2 = "fonts/Ionicons.ttf";
            Typeface typeFace2 = Typeface.CreateFromAsset(activity.Assets, path2);
            vh.TextFavorite.Typeface = typeFace2;

            if (item.isFavorite == true)
            {
                vh.TextFavorite.Visibility = ViewStates.Visible;
            }
            else
            {
                vh.TextFavorite.Visibility = ViewStates.Gone;
            }
		}

		public override int ItemCount
		{
            get { return KnoWhy.Current.metaList.Count; }
		}

        void OnClick(int position)
        {
            if (ItemClick != null)
                ItemClick(this, position);
        }

        private async Task<Bitmap> GetImageBitmapFromUrlAsync(string url)
		{
			Bitmap imageBitmap = null;

			using (var httpClient = new HttpClient())
			{
				var imageBytes = await httpClient.GetByteArrayAsync(url);
				if (imageBytes != null && imageBytes.Length > 0)
				{
					imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
				}
			}

			return imageBitmap;
		}

		public void loadImage(String urlString, ImageView imageView, String nodeId)
		{
            try
            {
                string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                string localFilename = nodeId + "_image.png";
                string localPath = System.IO.Path.Combine(documentsPath, localFilename);

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
                        activity.RunOnUiThread(() =>
                                {
                                /*int CORNER_RADIUS = 24;
                                int MARGIN = 12;
                                float density = activity.Resources.DisplayMetrics.Density;
                                int mCornerRadius = (int)(CORNER_RADIUS * density + 0.5f);
                                int mMargin = (int)(MARGIN * density + 0.5f);
                                imageView.SetImageDrawable(new RoundedDrawable(BitmapFactory.DecodeFile(localPath), mCornerRadius, mMargin));*/
                                    imageView.SetImageBitmap(BitmapFactory.DecodeFile(localPath));
                                });
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Ex: " + ex.Message);
                        }
                    };

                    var url = new Uri(urlString);
                    webClient.DownloadDataAsync(url);
                }
                else
                {
                    try
                    {

                        // IMPORTANT: this is a background thread, so any interaction with
                        // UI controls must be done via the MainThread
                        activity.RunOnUiThread(() =>
                        {
                            /*int CORNER_RADIUS = 24;
                            int MARGIN = 12;
                            float density = activity.Resources.DisplayMetrics.Density;
                            int mCornerRadius = (int)(CORNER_RADIUS * density + 0.5f);
                            int mMargin = (int)(MARGIN * density + 0.5f);
                            imageView.SetImageDrawable(new RoundedDrawable(BitmapFactory.DecodeFile(localPath), mCornerRadius, mMargin));*/
                            imageView.SetImageBitmap(BitmapFactory.DecodeFile(localPath));
                        });
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Ex: " + ex.Message);
                    }
                }
            } catch (Exception e) {
                Console.WriteLine("Error loading image: " + e.Message);
            }
		}

        public void refresh() {
            this.NotifyDataSetChanged();
        }

        public event EventHandler<int> ItemClick;

    }

    public class RoundedDrawable : Drawable
    {
		private float mCornerRadius;
		private RectF mRect = new RectF();
		private BitmapShader mBitmapShader;
        private Paint mPaint;
        private int mMargin;

        public RoundedDrawable (Bitmap bitmap, float cornerRadius, int margin) {
			mCornerRadius = cornerRadius;

			mBitmapShader = new BitmapShader(bitmap,
                                             Shader.TileMode.Clamp, Shader.TileMode.Clamp);

			mPaint = new Paint();
            mPaint.AntiAlias = true;
            mPaint.SetShader(mBitmapShader);

			mMargin = margin;
        }

        public override int Opacity
        {
            get
            {
                return -1;
            }
        }

        public override void Draw(Canvas canvas)
        {
            canvas.DrawRoundRect(mRect, mCornerRadius, mCornerRadius, mPaint);
        }

        public override void SetAlpha(int alpha)
        {
            mPaint.Alpha = alpha;
        }

        public override void SetColorFilter(ColorFilter colorFilter)
        {
            mPaint.SetColorFilter(colorFilter);
        }
    }

    public class ArticleViewHolder : RecyclerView.ViewHolder
	{
		public ImageView Image { get; private set; }
		public TextView Text1 { get; private set; }
        public TextView Text2 { get; private set; }
        public TextView Text3 { get; private set; }
        public TextView TextMarker { get; private set; }
        public TextView TextFavorite { get; private set; }

		public ArticleViewHolder(View itemView, Action<int> listener) : base(itemView)
		{
			// Locate and cache view references:
			Image = itemView.FindViewById<ImageView>(Resource.Id.imageView);
			Text1 = itemView.FindViewById<TextView>(Resource.Id.textView1);
            Text2 = itemView.FindViewById<TextView>(Resource.Id.textView2);
            Text3 = itemView.FindViewById<TextView>(Resource.Id.textView3);
            TextMarker = itemView.FindViewById<TextView>(Resource.Id.textViewMarker);
            TextFavorite = itemView.FindViewById<TextView>(Resource.Id.textViewFavorite);

            itemView.Click += (sender, e) => listener(base.AdapterPosition);
        }
	}


}
