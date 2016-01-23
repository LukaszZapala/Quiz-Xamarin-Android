using SQLite;

namespace Quiz.Android.Database
{
    [Table("Branch")]
    public class Branch
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }

        public Branch() { }

        public Branch(string name)
        {
            Name = name;
        }
    }
}