
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace KnoWhy.Droid
{
    public class EmptyRecyclerView : RecyclerView
    {
        private View mEmptyView;

        private ListObserver observer;

        public EmptyRecyclerView(Context context) :
            base(context)
        {
            Initialize();
        }

        public EmptyRecyclerView(Context context, IAttributeSet attrs) :
            base(context, attrs)
        {
            Initialize();
        }

        public EmptyRecyclerView(Context context, IAttributeSet attrs, int defStyle) :
            base(context, attrs, defStyle)
        {
            Initialize();
        }

        void Initialize()
        {
            observer = new ListObserver(this);
        }

        private void checkIfEmpty()
        {
            if (mEmptyView != null)
            {
                if (GetAdapter() == null || GetAdapter().ItemCount == 0) {
                    mEmptyView.Visibility = ViewStates.Visible;
                } else {
                    mEmptyView.Visibility = ViewStates.Gone;
                }
            }
        }

        public void setEmptyView(View view) {
            mEmptyView = view;
            checkIfEmpty();
        }

        public override void SetAdapter(RecyclerView.Adapter adapter)
        {
            Adapter oldAdapter = GetAdapter();

            base.SetAdapter(adapter);

            if (oldAdapter != null) {
                oldAdapter.UnregisterAdapterDataObserver(observer);
            }

            if (adapter != null) {
                adapter.RegisterAdapterDataObserver(observer);
            }
        }

        internal class ListObserver : AdapterDataObserver
        {
            EmptyRecyclerView recyclerView;

            internal ListObserver(EmptyRecyclerView _recyclerView) {
                recyclerView = _recyclerView;
            }

            public override void OnChanged()
            {
                base.OnChanged();

                recyclerView.checkIfEmpty();
            }

            public override void OnItemRangeInserted(int positionStart, int itemCount)
            {
                base.OnItemRangeInserted(positionStart, itemCount);

                recyclerView.checkIfEmpty();
            }

            public override void OnItemRangeRemoved(int positionStart, int itemCount)
            {
                base.OnItemRangeRemoved(positionStart, itemCount);

                recyclerView.checkIfEmpty();
            }
        }
    }
}
