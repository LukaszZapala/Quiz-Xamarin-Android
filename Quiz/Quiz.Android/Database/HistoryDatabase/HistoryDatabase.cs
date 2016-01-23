using SQLite;
using System;
using System.IO;

namespace Quiz.Android.Database
{
    class HistoryDatabase
    {
        private SQLiteConnection connection;
        private bool isExists = false;

        public TableQuery<History> HistoryQuery
        {
            get
            {
                return connection.Table<History>();
            }
        }

        public HistoryDatabase(string _databaseName)
        {
            var databaseName = _databaseName;
            var documentsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal));
            var databasePath = Path.Combine(documentsPath, databaseName);

            isExists = File.Exists(databasePath);

            connection = new SQLiteConnection(databasePath);

            if (!isExists) connection.CreateTable<History>();
        }

        public void Add(string playerName, double scores, int branchId)
        {
            var query = HistoryQuery.Where(x => x.PlayerName == playerName && x.BranchId == branchId);

            if (query.Count() == 1)
            {
                foreach (var item in query)
                {
                    if (item.Scores < scores) item.Scores = scores;
                    connection.Update(item);
                }
            }
            else
            {
                var history = new History
                {
                    PlayerName = playerName,
                    Scores = scores,
                    BranchId = branchId,
                };

                connection.Insert(history);
                connection.Update(history);
            }            
        }
    }
}