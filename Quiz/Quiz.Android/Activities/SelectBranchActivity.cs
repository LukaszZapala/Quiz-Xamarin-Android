using System;
using System.Collections.Generic;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Quiz.Android.Adapters;
using Android.Content.PM;

namespace Quiz.Android.Activities
{
    [Activity(Label = "Quiz", Icon = "@mipmap/icon", ScreenOrientation = ScreenOrientation.Portrait)]
    public class SelectBranchActivity : Activity
    {
        Button btnAdd;
        ListView lvBranch;
        List<BranchItem> branchItemList;
        bool isEditMode;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.SelectBranch);

            isEditMode = Intent.GetBooleanExtra("Visible", false);

            if (isEditMode)
            {
                btnAdd = FindViewById<Button>(Resource.Id.buttonAdd);
                btnAdd.Visibility = ViewStates.Visible;
                btnAdd.Click += AddNewBranch;
            }

            FillBranchItemList();
            lvBranch = FindViewById<ListView>(Resource.Id.listViewBranch);
            lvBranch.Adapter = new BranchListViewAdapter(this, branchItemList);
            lvBranch.ItemClick += SelectBranch;
        }

        private void AddNewBranch(object sender, EventArgs e)
        {
            StartActivity(typeof(AddBranchActivity));
        }

        private void SelectBranch(object sender, AdapterView.ItemClickEventArgs e)
        {
            Intent intent;

            if(isEditMode)
            {
                intent = new Intent(this, typeof(AddQuestionActivity));
            }
            else
            {
                intent = new Intent(this, typeof(QuizSettingsActivity));
            }

            intent.PutExtra("BranchId", e.Position + 1);
            StartActivity(intent);
            Finish();
        }

        private void FillBranchItemList()
        {
            var database = new QuizDatabase(Resources.GetString(Resource.String.quizDatabaseName));
            branchItemList = new List<BranchItem>();

            foreach (var branch in database.BranchQuery)
            {
                branchItemList.Add(new BranchItem
                {
                    BranchName = branch.Name,
                    QuestionCount = database.QuestionQuery.Where(x => x.BranchId == branch.Id).Count(),
                });
            }
        }
    }
}