using SQLite;

namespace Quiz.Android.Database
{
    [Table("Question")]
    public class Question
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Content { get; set; }
        public string CorrectAnswers { get; set; }
        public int BranchId { get; set; }

        public Question() { }

        public Question(int branchId, string content, string correctAnswers)
        {
            BranchId = branchId;
            Content = content.Trim();
            CorrectAnswers = correctAnswers.Trim();
        }
    }
}