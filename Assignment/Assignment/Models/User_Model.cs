using System.Data;

namespace Assignment.Models
{
    public class User_Model
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string FullName { get; set; }

        public Guid RoleId { get; set; }

        public int Status { get; set; }

        public virtual IEnumerable<Bill_Model> Bill_Models { get; set; }
        public virtual Role_Model Role_Model { get; set; }

        //public virtual ICollection<Role> Roles { get; set; }
        public virtual Cart_Model Cart_Model { get; set; }
    }
}
