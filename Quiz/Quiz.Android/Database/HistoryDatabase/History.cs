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
using SQLite;

namespace Quiz.Android.Database
{
    [Table("History")]
    public class History
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string PlayerName { get; set; }
        public double Scores { get; set; }

        public int BranchId { get; set; }

        public History() { }
    }
}