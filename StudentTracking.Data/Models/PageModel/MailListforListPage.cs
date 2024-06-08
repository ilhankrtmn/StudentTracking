namespace StudentTracking.Data.Models.PageModel
{
    public class MailListforListPage
    {
        public List<MailList> MailLists { get; set; }
    }

    public class MailList
    {
        public int Id { get; set; }
        public string TeacherName { get; set; }
        public string TeacherSurname { get; set; }
        public string Subject { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}