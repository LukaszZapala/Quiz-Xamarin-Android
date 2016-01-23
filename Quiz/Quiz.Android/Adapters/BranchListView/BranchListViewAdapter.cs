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

namespace Quiz.Android.Adapters
{
    
    class BranchListViewAdapter : BaseAdapter<BranchItem>
    {
        private List<BranchItem> branches;
        private Activity context;

        public BranchListViewAdapter(Activity context, List<BranchItem> branches)
        {
            this.branches = branches;
            this.context = context;
        }

        public override BranchItem this[int position]
        {
            get
            {
                return branches[position];
            }
        }

        public override int Count
        {
            get
            {
                return branches.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;
            ViewBranchHolder viewHolder;

            if (view == null)
            {
                view = context.LayoutInflater.Inflate(Resource.Layout.BranchListItem, null);

                viewHolder = new ViewBranchHolder();
                viewHolder.BranchName = view.FindViewById<TextView>(Resource.Id.textViewBranchName);
                viewHolder.BranchCount = view.FindViewById<TextView>(Resource.Id.textViewQuestionCount);

                view.Tag = viewHolder;
            }
            else
            {
                viewHolder = (ViewBranchHolder)view.Tag;
            }

            var branch = branches[position];

            viewHolder.BranchName.Text = branch.BranchName;
            viewHolder.BranchCount.Text = branch.QuestionCount.ToString();

            return view;
        }
    }
}