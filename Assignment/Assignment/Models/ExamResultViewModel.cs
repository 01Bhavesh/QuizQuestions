namespace Assignment.Models
{
    public class ExamResultViewModel
    {
        public string Email { get; set; }
        public List<QuestionAnswerResult> QuestionAnswerResults { get; set; }
        public int CorrectAnswers { get; set; }
        public int TotalQuestions { get; set; }
    }
}
