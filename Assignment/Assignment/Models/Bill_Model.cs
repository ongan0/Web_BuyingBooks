namespace Assignment.Models
{
    public class Bill_Model
    {
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid UserId { get; set; }
        public int Status { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public long Payment { get; set; }

        public List<BillDetail_Model> BillDetail_Models { get; set; }
        public virtual User_Model User_Model { get; set; }
    }
}
