namespace Assignment.Models
{
    public class Role_Model
    {
        public Guid Id { get; set; }
        public string RoleName { get; set; }
        public string? Description { get; set; }
        public int Status { get; set; }

        //public virtual User User { get; set; }
        public virtual IEnumerable<User_Model> User_Models { get; set; }
    }
}
