namespace Assignment.Models
{
    public class UserAnswer
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public int QuestionId { get; set; }
        public string SelectedAnswer { get; set; }
    }
}
