using Assignment.Models;
namespace Assignment.IServices
{
    public interface ICategory_Service
    {
        bool Add(Category_Model model);
        bool Update(Category_Model model);
        bool DeleteById(Guid id);
        public Category_Model GetById(Guid id);
        public List<Category_Model> GetAll();
        public Category_Model GetByName(string name);
    }
}
