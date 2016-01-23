using System;
using System.Collections.Generic;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Quiz.Android.Database.Quiz;
using Android.Content.PM;

namespace Quiz.Android.Activities
{
    [Activity(Label = "Quiz", Icon = "@mipmap/icon", ScreenOrientation = ScreenOrientation.Portrait)]
    public class GameActivity : Activity
    {
        private TextView tvCount, tvQuestion;
        private RadioButton rbtnFirst, rbtnSecond, rbtnThird, rbtnFourth;
        private Button btnNext, btnPrevious;
        private QuizDatabase dbSupport;
        private int branchId, questionCount, currentCount = 0;
        private List<Item> items;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Game);

            dbSupport = new QuizDatabase(Resources.GetString(Resource.String.quizDatabaseName));
            branchId = Intent.GetIntExtra("BranchId", -1);
            questionCount = dbSupport.QuestionQuery.Where(x => x.BranchId == branchId).Count();

            FillItemList();


            tvCount = FindViewById<TextView>(Resource.Id.textViewCount);

            tvQuestion = FindViewById<TextView>(Resource.Id.textViewQuestion);

            rbtnFirst = FindViewById<RadioButton>(Resource.Id.radioButtonFirst);
            rbtnFirst.Click += SelectFirstRadioButton;

            rbtnSecond = FindViewById<RadioButton>(Resource.Id.radioButtonSecond);
            rbtnSecond.Click += SelectSecondRadioButton;

            rbtnThird = FindViewById<RadioButton>(Resource.Id.radioButtonThird);
            rbtnThird.Click += SelectThirdRadioButton;

            rbtnFourth = FindViewById<RadioButton>(Resource.Id.radioButtonFourth);
            rbtnFourth.Click += SelectFourthRadioButton;

            btnNext = FindViewById<Button>(Resource.Id.buttonNext);
            btnNext.Click += NextQuestion;

            btnPrevious = FindViewById<Button>(Resource.Id.buttonPrevious);
            btnPrevious.Click += PreviousQuestion;

            FillForm(currentCount);
        }       

        private void SelectFirstRadioButton(object sender, EventArgs e)
        {
            rbtnFirst.Checked = true;
            rbtnSecond.Checked = false;
            rbtnThird.Checked = false;
            rbtnFourth.Checked = false;

            items[currentCount].CheckedIndex = 0;
        }

        private void SelectSecondRadioButton(object sender, EventArgs e)
        {
            rbtnFirst.Checked = false;
            rbtnSecond.Checked = true;
            rbtnThird.Checked = false;
            rbtnFourth.Checked = false;

            items[currentCount].CheckedIndex = 1;
        }

        private void SelectThirdRadioButton(object sender, EventArgs e)
        {
            rbtnFirst.Checked = false;
            rbtnSecond.Checked = false;
            rbtnThird.Checked = true;
            rbtnFourth.Checked = false;

            items[currentCount].CheckedIndex = 2;
        }

        private void SelectFourthRadioButton(object sender, EventArgs e)
        {
            rbtnFirst.Checked = false;
            rbtnSecond.Checked = false;
            rbtnThird.Checked = false;
            rbtnFourth.Checked = true;

            items[currentCount].CheckedIndex = 3;
        }

        private void NextQuestion(object sender, EventArgs e)
        {
            if (currentCount < questionCount - 1)
            {
                FillForm(++currentCount);
                btnPrevious.Enabled = true;
            }
            else
            {
                int score = 0;
                foreach (var item in items)
                {
                    if (item[item.CheckedIndex] == item.CorrectAnswers)
                    {
                        score++;
                    }
                }

                var intent = new Intent(this, typeof(ScoreActivity));
                intent.PutExtra("PlayerName", Intent.GetStringExtra("PlayerName"));
                intent.PutExtra("Score", score);
                intent.PutExtra("QuestionCount", questionCount);
                intent.PutExtra("BranchId", branchId);
                StartActivity(intent);
                Finish();
            }
        }

        private void PreviousQuestion(object sender, EventArgs e)
        {
            FillForm(--currentCount);
            if (currentCount == 0) btnPrevious.Enabled = false;
        }

        private int StartPoint(int id)
        {
            int count = 0;

            foreach (var item in dbSupport.BranchQuery)
            {
                if (item.Id < id) count += dbSupport.QuestionQuery.Where(x => x.BranchId == item.Id).Count();
                else return count;
            }
            return count;
        }

        private void FillItemList()
        {
            items = new List<Item>();

            for (int i = StartPoint(branchId) + 1; i <= (questionCount + StartPoint(branchId)); i++)
            {
                var newItem = new Item();

                var question = dbSupport.QuestionQuery.Where(x => x.BranchId == branchId && x.Id == i);
                foreach (var item in question)
                {
                    newItem.Question = item.Content;
                    newItem.CorrectAnswers = item.CorrectAnswers;
                }

                var options = dbSupport.AnswersQuery.Where(x => x.QuestionId == i);
                foreach (var item in options)
                {
                    newItem[0] = item.FirstOption;
                    newItem[1] = item.SecondOption;
                    newItem[2] = item.ThirdOption;
                    newItem[3] = item.FourthOption;
                }

                items.Add(newItem);
            }
        }

        private void FillForm(int id)
        {
            tvCount.Text = (id + 1) + " z " + questionCount;
            tvQuestion.Text = items[id].Question;

            rbtnFirst.Text = items[id][0];
            rbtnSecond.Text = items[id][1];
            rbtnThird.Text = items[id][2];
            rbtnFourth.Text = items[id][3];

            if (items[id].CheckedIndex == 0)
            {
                rbtnFirst.Checked = true;
                rbtnSecond.Checked = false;
                rbtnThird.Checked = false;
                rbtnFourth.Checked = false;
            }
            if (items[id].CheckedIndex == 1)
            {
                rbtnFirst.Checked = false;
                rbtnSecond.Checked = true;
                rbtnThird.Checked = false;
                rbtnFourth.Checked = false;
            }
            if (items[id].CheckedIndex == 2)
            {
                rbtnFirst.Checked = false;
                rbtnSecond.Checked = false;
                rbtnThird.Checked = true;
                rbtnFourth.Checked = false;
            }
            if (items[id].CheckedIndex == 3)
            {
                rbtnFirst.Checked = false;
                rbtnSecond.Checked = false;
                rbtnThird.Checked = false;
                rbtnFourth.Checked = true;
            }
        }
    }
}