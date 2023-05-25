using Assignment.Models;
namespace Assignment.IServices
{
	public interface IRole_Service
	{
        bool Add(Role_Model model);
        bool Update(Role_Model model);
        bool DeleteById(Guid id);
        public Role_Model GetById(Guid id);
        public List<Role_Model> GetAll();
        public List<Role_Model> GetByName(string name);
    }
}
