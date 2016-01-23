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

namespace Quiz.Android.Adapters.HistoryListView
{
    public class ViewHistoryHolder : Java.Lang.Object
    {
        public TextView Id { get; set; }
        public TextView PlayerName { get; set; }
        public TextView Score { get; set; }
    }
}