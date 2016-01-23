using System;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Android.Content.PM;

namespace Quiz.Android.Activities
{
    [Activity(Label = "Quiz", Icon = "@mipmap/icon", ScreenOrientation = ScreenOrientation.Portrait)]
    public class AddQuestionActivity : Activity
    {
        EditText[] txtValue = new EditText[5];
        Button btnContinue;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.AddQuestion);

            txtValue[0] = FindViewById<EditText>(Resource.Id.editTextNewQuestion);
            txtValue[1] = FindViewById<EditText>(Resource.Id.editTextOption1); 
            txtValue[2] = FindViewById<EditText>(Resource.Id.editTextOption2);
            txtValue[3] = FindViewById<EditText>(Resource.Id.editTextOption3);
            txtValue[4] = FindViewById<EditText>(Resource.Id.editTextOption4);

            btnContinue = FindViewById<Button>(Resource.Id.buttonContinue);
            btnContinue.Click += Continue;
        }

        private void Continue(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(SelectCorrectAnswerActivity));

            intent.PutExtra("BranchId", Intent.GetIntExtra("BranchId", -1));
            for (int i = 0; i < 5; i++)
            {
                if (i == 0) intent.PutExtra("Question", txtValue[i].Text);
                else intent.PutExtra("Option" + i, txtValue[i].Text);
            }

            StartActivity(intent);
            Finish();
        }
    }
}