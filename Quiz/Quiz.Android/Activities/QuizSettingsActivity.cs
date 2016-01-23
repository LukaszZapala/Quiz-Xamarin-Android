using System;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Android.Content.PM;

namespace Quiz.Android.Activities
{
    [Activity(Label = "Quiz", Icon = "@mipmap/icon", ScreenOrientation = ScreenOrientation.Portrait)]
    public class QuizSettingsActivity : Activity
    {
        EditText txtPlayerName;
        Button btnPlay;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.QuizSettings);

            txtPlayerName = FindViewById<EditText>(Resource.Id.editTextPlayerName);

            btnPlay = FindViewById<Button>(Resource.Id.buttonPlay);
            btnPlay.Click += StartGame;
        }

        private void StartGame(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtPlayerName.Text))
            {
                var newIntent = new Intent(this, typeof(GameActivity));
                newIntent.PutExtra("BranchId", Intent.GetIntExtra("BranchId", -1));
                newIntent.PutExtra("PlayerName", txtPlayerName.Text);
                StartActivity(newIntent);
                Finish();
            }            
        }
    }
}