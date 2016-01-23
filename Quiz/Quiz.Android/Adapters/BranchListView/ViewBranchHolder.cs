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

namespace Quiz.Android.Adapters
{
    class ViewBranchHolder : Java.Lang.Object
    {
        public TextView BranchName { get; set; }
        public TextView BranchCount { get; set; }
    }
}