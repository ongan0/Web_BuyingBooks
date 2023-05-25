using Assignment.IServices;
using Assignment.Models;
using System.Linq;

namespace Assignment.Services
{
    public class Category_Service : ICategory_Service
    {
        AssDbContext _context;
        public Category_Service()
        {
            _context = new AssDbContext();
        }
        public bool Add(Category_Model model)
        {
            try
            {
                if(String.IsNullOrEmpty(model.CategoryName)) return false;
                _context.Category_Models.Add(model);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex) 
            {
                return false;
            }
        }

        public bool DeleteById(Guid id)
        {
            try
            {
                var model = _context.Category_Models.FirstOrDefault(x => x.Id == id);
                _context.Category_Models.Remove(model);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<Category_Model> GetAll()
        {
            return _context.Category_Models.ToList();
        }

        public Category_Model GetById(Guid id)
        {
            return _context.Category_Models.Find(id);
        }

        public Category_Model GetByName(string name)
        {
            return _context.Category_Models.FirstOrDefault(x=>x.CategoryName == name);
        }

        public bool Update(Category_Model model)
        {
            try
            {
                var obj = _context.Category_Models.FirstOrDefault(x => x.Id == model.Id);
                obj.CategoryName = model.CategoryName;
                obj.Description = model.Description;
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
