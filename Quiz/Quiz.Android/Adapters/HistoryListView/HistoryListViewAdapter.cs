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
using Quiz.Android.Database;

namespace Quiz.Android.Adapters.HistoryListView
{
    class HistoryListViewAdapter : BaseAdapter<History>
    {
        private List<History> history;
        private Activity context;

        public HistoryListViewAdapter(Activity context, List<History> history)
        {
            this.history = history;
            this.context = context;
        }

        public override History this[int position]
        {
            get
            {
                return history[position];
            }
        }

        public override int Count
        {
            get
            {
                return history.Count();
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;
            ViewHistoryHolder viewHolder;

            if (view == null)
            {
                view = context.LayoutInflater.Inflate(Resource.Layout.HistoryListItem, null);

                viewHolder = new ViewHistoryHolder();

                viewHolder.Id = view.FindViewById<TextView>(Resource.Id.textViewId);
                viewHolder.PlayerName = view.FindViewById<TextView>(Resource.Id.textViewName);
                viewHolder.Score = view.FindViewById<TextView>(Resource.Id.textViewPlayerScore);

                view.Tag = viewHolder;
            }
            else
            {
                viewHolder = (ViewHistoryHolder)view.Tag;
            }

            var record = history[position];

            viewHolder.Id.Text = record.Id.ToString();
            viewHolder.PlayerName.Text = record.PlayerName;
            viewHolder.Score.Text = String.Format("{0:N2}", record.Scores.ToString()) + "%";

            return view;
        }


    }
}