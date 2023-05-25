using Assignment.Models;
namespace Assignment.IServices
{
	public interface IProduct_Service
	{
		bool Add(Product_Model model);
		bool Update(Product_Model model);
		bool DeleteById(Guid id);
		public Product_Model GetById(Guid id);
		public List<Product_Model> GetAll();
		public List<Product_Model> GetByName(string name);
        public List<Product_Model> GetByCategory(Guid cateId);
        public List<Product_Model> GetAllPro();
    }
}
