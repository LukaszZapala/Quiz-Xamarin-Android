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
    class BranchItem
    {
        public string BranchName { get; set; }
        public int QuestionCount { get; set; }
    }
}