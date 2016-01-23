using SQLite;

namespace Quiz.Android.Database
{
    [Table("OptionAnswers")]
    public class OptionAnswers
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string FirstOption { get; set; }
        public string SecondOption { get; set; }
        public string ThirdOption { get; set; }
        public string FourthOption { get; set; }
        public int QuestionId { get; set; }

        public OptionAnswers() { }

        public OptionAnswers(int questionId, params string[] options)
        {
            QuestionId = questionId;
            FirstOption = options[0].Trim();
            SecondOption = options[1].Trim();
            ThirdOption = options[2].Trim();
            FourthOption = options[3].Trim();
        }
    }
}