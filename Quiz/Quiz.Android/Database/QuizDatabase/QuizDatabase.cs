using Android.App;
using Android.Content.Res;
using Quiz.Android.Database;
using SQLite;
using System;
using System.IO;

namespace Quiz.Android
{
    public class QuizDatabase
    {
        private SQLiteConnection connection;
        private bool isExists;
        private int questionCount = 0;
        public TableQuery<Branch> BranchQuery { get; set; }
        public TableQuery<Question> QuestionQuery { get; set; }
        public TableQuery<OptionAnswers> AnswersQuery { get; set; }

        public QuizDatabase(string _databaseName)
        {
            var databaseName = _databaseName;
            var documentsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal));
            var databasePath = Path.Combine(documentsPath, databaseName);

            isExists = File.Exists(databasePath);

            connection = new SQLiteConnection(databasePath);

            if (!isExists) RecoverDatabase();

            BranchQuery = connection.Table<Branch>();
            QuestionQuery = connection.Table<Question>();
            AnswersQuery = connection.Table<OptionAnswers>();
        }

        private void RecoverDatabase()
        {
            // Create Table
            connection.CreateTable<Branch>();
            connection.CreateTable<Question>();
            connection.CreateTable<OptionAnswers>();

            // Adds record to Branch table
            var history = new Branch("Historia");
            InsertAndUpdate(history);
            ReadItemFromFile("history.txt", 1);

            var geography = new Branch("Geografia");
            InsertAndUpdate(geography);
            ReadItemFromFile("geography.txt", 2);
        }

        public void InsertAndUpdate(object table)
        {
            connection.Insert(table);
            connection.Update(table);
        }

        private void ReadItemFromFile(string fileName, int id)
        {
            string line = null;

            using (var reader = new StreamReader(Application.Context.Assets.Open(fileName)))
            {
                while((line = reader.ReadLine()) != null)
                {
                    if (!String.IsNullOrWhiteSpace(line))
                    {
                        var rows = line.Split(';');

                        if (rows.Length == 2)
                        {
                            var question = new Question(id, rows[0], rows[1]);
                            InsertAndUpdate(question);
                            questionCount++;
                        }
                        if (rows.Length == 4)
                        {
                            var option = new OptionAnswers(questionCount, rows);
                            InsertAndUpdate(option);                            
                        }
                    }
                }
            }
        }


    }
}