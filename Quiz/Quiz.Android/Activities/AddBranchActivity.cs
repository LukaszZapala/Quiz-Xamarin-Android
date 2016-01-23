using System;

using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Widget;
using Quiz.Android.Database;

namespace Quiz.Android.Activities
{
    [Activity(Label = "Quiz", ScreenOrientation = ScreenOrientation.Portrait)]
    public class AddBranchActivity : Activity
    {
        TextView tvBranchName;
        Button btnAdd;
        QuizDatabase dbSupport;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.AddBranch);

            tvBranchName = FindViewById<TextView>(Resource.Id.editTextName);

            btnAdd = FindViewById<Button>(Resource.Id.buttonCreate);
            btnAdd.Click += BtnAdd_Click;
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            dbSupport = new QuizDatabase(Resources.GetString(Resource.String.quizDatabaseName));
            var n = dbSupport.BranchQuery.Where(x => x.Name.ToLower() == tvBranchName.Text.ToLower()).Count();

            if (n == 0)
            {
                var branch = new Branch(tvBranchName.Text);
                dbSupport.InsertAndUpdate(branch);
            }
            else
            {
                Toast.MakeText(this, Resources.GetString(Resource.String.branchExsists), ToastLength.Long).Show();
            }
            Finish();
        }
    }
}