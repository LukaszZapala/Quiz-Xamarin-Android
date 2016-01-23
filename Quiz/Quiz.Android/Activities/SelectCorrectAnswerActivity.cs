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
    public class SelectCorrectAnswerActivity : Activity
    {
        QuizDatabase dbSupport;
        RadioButton [] rbtnOption = new RadioButton[4];
        TextView tvQuestion;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.SelectCorrectAnswer);

            tvQuestion = FindViewById<TextView>(Resource.Id.textViewQuestion);
            tvQuestion.Text = Intent.GetStringExtra("Question");

            rbtnOption[0] = FindViewById<RadioButton>(Resource.Id.radioButtonFirst);
            rbtnOption[0].Text = Intent.GetStringExtra("Option1");
            rbtnOption[0].Click += SelectFirstRadioButton;

            rbtnOption[1] = FindViewById<RadioButton>(Resource.Id.radioButtonSecond);
            rbtnOption[1].Text = Intent.GetStringExtra("Option2");
            rbtnOption[1].Click += SelectSecondRadioButton;

            rbtnOption[2] = FindViewById<RadioButton>(Resource.Id.radioButtonThird);
            rbtnOption[2].Text = Intent.GetStringExtra("Option3");
            rbtnOption[2].Click += SelectThirdRadioButton;

            rbtnOption[3] = FindViewById<RadioButton>(Resource.Id.radioButtonFourth);
            rbtnOption[3].Text = Intent.GetStringExtra("Option4");
            rbtnOption[3].Click += SelectFourthRadioButton;
        }


        private void SelectFirstRadioButton(object sender, EventArgs e)
        {
            AddToDatabase(0);
        }

        private void SelectSecondRadioButton(object sender, EventArgs e)
        {
            AddToDatabase(1);
        }


        private void SelectThirdRadioButton(object sender, EventArgs e)
        {
            AddToDatabase(2);
        }

        private void SelectFourthRadioButton(object sender, EventArgs e)
        {
            AddToDatabase(3);
        }

        private void AddToDatabase(int checkedOption)
        {
            AlertDialog.Builder alert = new AlertDialog.Builder(this);
            alert.SetTitle(Resource.String.AddRecordToDatabase);

            alert.SetPositiveButton(Resource.String.closeDialogPositive, (senderAlert, args) =>
            {
                dbSupport = new QuizDatabase(Resources.GetString(Resource.String.quizDatabaseName));

                Question question = new Question
                {
                    BranchId = Intent.GetIntExtra("BranchId", -1),
                    Content = tvQuestion.Text,
                    CorrectAnswers = rbtnOption[checkedOption].Text
                };
                dbSupport.InsertAndUpdate(question);

                OptionAnswers options = new OptionAnswers
                {
                    QuestionId = GetQuestionCount(question.BranchId) + 1,
                    FirstOption = rbtnOption[0].Text,
                    SecondOption = rbtnOption[1].Text,
                    ThirdOption = rbtnOption[2].Text,
                    FourthOption = rbtnOption[3].Text
                };
                dbSupport.InsertAndUpdate(options);

                Finish();
            });

            alert.SetNegativeButton(Resource.String.closeDialogNegative, (senderAlert, args) => 
            {
                rbtnOption[checkedOption].Checked = false;
            });

            Dialog dialog = alert.Create();
            dialog.Show();
        }

        private int GetQuestionCount(int id)
        {
            int count = 0;

            foreach (var item in dbSupport.BranchQuery)
            {
                if (item.Id < id) count += dbSupport.QuestionQuery.Where(x => x.BranchId == item.Id).Count();
                else return count;
            }
            return count;
        }
    }
}