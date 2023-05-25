namespace Assignment.Models
{
    public class BillDetail_Model
    {
        public Guid Id { get; set; }
        public Guid BillId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public long Price { get; set; }

        public virtual Bill_Model Bill_Model { get; set; }
        public virtual Product_Model Product_Model { get; set; }
    }
}
