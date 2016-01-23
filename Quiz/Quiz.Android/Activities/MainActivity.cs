using Android.App;
using Android.OS;
using Android.Widget;
using System;
using Quiz.Android.Activities;
using Android.Content;
using Android.Content.PM;

namespace Quiz.Android
{
    [Activity (Label = "Quiz", MainLauncher = true, Icon = "@mipmap/icon", ScreenOrientation = ScreenOrientation.Portrait)]
	public class MainActivity : Activity
	{
        private Button btnStart, btnCreate, btnRanking, btnClose;
        private QuizDatabase dbSupport;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			SetContentView (Resource.Layout.Main);

            dbSupport = new QuizDatabase(Resources.GetString(Resource.String.quizDatabaseName));

            btnStart = FindViewById<Button>(Resource.Id.buttonStart);
            btnStart.Click += StartQuiz;

            btnCreate = FindViewById<Button>(Resource.Id.buttonEdit);
            btnCreate.Click += CreateQuiz;

            btnRanking = FindViewById<Button>(Resource.Id.buttonRanking);
            btnRanking.Click += OpenRanking;

            btnClose = FindViewById<Button>(Resource.Id.buttonClose);
            btnClose.Click += CloseActivity;
        }

        private void StartQuiz(object sender, System.EventArgs e)
        {
            StartActivity(typeof(SelectBranchActivity));
        }

        private void CreateQuiz(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(SelectBranchActivity));
            intent.PutExtra("Visible", true);
            StartActivity(intent);
        }

        private void OpenRanking(object sender, EventArgs e)
        {
            StartActivity(typeof(TopActivity));
        }

        private void CloseActivity(object sender, System.EventArgs e)
        {
            AlertDialog.Builder alert = new AlertDialog.Builder(this);
            alert.SetTitle(Resource.String.closeDialogTitle);
            alert.SetPositiveButton(Resource.String.closeDialogPositive, (senderAlert, args) =>
            {
                this.Finish();
            });
            alert.SetNegativeButton(Resource.String.closeDialogNegative, (senderAlert, args) => { });

            Dialog dialog = alert.Create();
            dialog.Show();
        }
    }
}


