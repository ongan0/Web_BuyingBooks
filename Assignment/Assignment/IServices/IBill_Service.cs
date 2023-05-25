using Assignment.Models;

namespace Assignment.IServices
{
	public interface IBill_Service
	{
        bool Add(Bill_Model model);
        bool Update(Bill_Model model);
        bool DeleteById(Guid id);
        public Bill_Model GetById(Guid id);
        public List<Bill_Model> GetAll();
        public List<Bill_Model> GetByName(string name);
    }
}
