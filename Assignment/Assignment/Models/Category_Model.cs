namespace Assignment.Models
{
    public class Category_Model
    {
        public Guid Id { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }

        public virtual IEnumerable<Product_Model> Product_Models { get; set; }
    }
}
