namespace Assignment.Models
{
    public class Product_Model
    {
        public Guid Id { get; set; }

        public Guid CateId { get; set; }

        public string Name { get; set; }

        public long Price { get; set; }

        public int AvailableQuantity { get; set; }

        public string LinkImg { get; set; }

        public string Supplier { get; set; }

        public string Description { get; set; }

        public int Status { get; set; }

        public string Author { get; set; }

        public string Publisher { get; set; }

        public int YearOfPublication { get; set; }

        public int Weight { get; set; }

        public string Size { get; set; }

        public int Page { get; set; }

        public virtual ICollection<BillDetail_Model>? BillDetail_Models { get; set; }
        public virtual ICollection<CartDetail_Model>? CartDetail_Models { get; set; }
        public virtual Category_Model Category_Model { get; set; }
    }
}
