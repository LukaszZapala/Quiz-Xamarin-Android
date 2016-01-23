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

namespace Quiz.Android.Database.Quiz
{
    public class Item
    {
        public string Question { get; set; }
        public string CorrectAnswers { get; set; }
        public int CheckedIndex { get; set; }
        public string this [int index]
        {
            get
            {
                return option[index];
            }
            set
            {
                option[index] = value;
            }
        }

        private string[] option = new string[5];

        public Item() { }
    }
}