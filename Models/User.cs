namespace KUSYS_Demo.Models
{
    public class User
    {
        public int UserId { get; set; }
        public int? StudentId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public short Role { get; set; }//0:Student  1:Admin

    }
}
