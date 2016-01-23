using System.Collections.Generic;

using Android.App;
using Android.OS;
using Android.Widget;
using Quiz.Android.Database;
using Quiz.Android.Adapters.HistoryListView;
using Android.Content.PM;

namespace Quiz.Android.Activities
{
    [Activity(Label = "Quiz", Icon = "@mipmap/icon", ScreenOrientation = ScreenOrientation.Portrait)]
    public class TopActivity : Activity
    {
        private HistoryDatabase dbSupport;
        private List<History> history;
        private ListView lvHistory;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.HistoryList);

            FillHistoryList();

            lvHistory = FindViewById<ListView>(Resource.Id.listViewHistory);
            lvHistory.Adapter = new HistoryListViewAdapter(this, history);
        }

        private void FillHistoryList()
        {
            dbSupport = new HistoryDatabase(Resources.GetString(Resource.String.historyDatabaseName));
            history = new List<History>();

            int k = 1;
            foreach (var record in dbSupport.HistoryQuery.OrderByDescending(x => x.Scores))
            {
                history.Add(new History
                {
                    Id = k,
                    PlayerName = record.PlayerName,
                    Scores = record.Scores
                });
                k++;
            }
        }
    }
}