namespace Assignment.Models
{
    public class Cart_Model
    {
        public Guid UserId { get; set; }
        public string? Description { get; set; }

        //public ICollection<CartDetail> CartDetails { get; set; }
        public virtual List<CartDetail_Model> CartDetail_Models { get; set; }
        public virtual User_Model User_Model { get; set; }
    }
}
