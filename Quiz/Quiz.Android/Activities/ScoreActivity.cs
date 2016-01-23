using System;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Quiz.Android.Database;
using Android.Content.PM;

namespace Quiz.Android.Activities
{
    [Activity(Label = "Quiz", Icon = "@mipmap/icon", ScreenOrientation = ScreenOrientation.Portrait)]
    public class ScoreActivity : Activity
    {
        TextView tvScore;
        Button btnReturn;
        double percentageScore;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Score);

            GetValueFromIntent();

            tvScore = FindViewById<TextView>(Resource.Id.textViewScore);
            tvScore.Text = String.Format("{0:N2}", percentageScore) + "%";

            btnReturn = FindViewById<Button>(Resource.Id.buttonReturn);
            btnReturn.Click += ReturnToMenu;
        }

        private void ReturnToMenu(object sender, EventArgs e)
        {
            Finish();
        }

        private void GetValueFromIntent()
        {
            var score = Intent.GetIntExtra("Score", -1);
            var count = Intent.GetIntExtra("QuestionCount", -1);
            percentageScore = ( (double)score / (double)count ) * 100;

            var playerName = Intent.GetStringExtra("PlayerName");
            var branchId = Intent.GetIntExtra("BranchId", -1);

            var dbSupport = new HistoryDatabase(Resources.GetString(Resource.String.historyDatabaseName));
            dbSupport.Add(playerName, percentageScore, branchId);
        }
    }
}