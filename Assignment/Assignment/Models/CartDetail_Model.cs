namespace Assignment.Models
{
    public class CartDetail_Model
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }

        public virtual Cart_Model Cart_Model { get; set; }
        public virtual Product_Model Product_Model { get; set; }
    }
}
