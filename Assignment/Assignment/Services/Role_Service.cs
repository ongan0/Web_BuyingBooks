using Assignment.Models;
using Assignment.IServices;
using System.Linq;

namespace Assignment.Services
{
	public class Role_Service : IRole_Service
	{
        AssDbContext context;

        public Role_Service()
        {
            context = new AssDbContext();
        }

        public bool Add(Role_Model model)
		{
			throw new NotImplementedException();
		}

		public bool DeleteById(Guid id)
		{
			throw new NotImplementedException();
		}

		public List<Role_Model> GetAll()
		{
            return context.Role_Models.ToList();
        }

		public Role_Model GetById(Guid id)
		{
            return context.Role_Models.Find(id);
        }

        public List<Role_Model> GetByName(string name)
		{
			throw new NotImplementedException();
		}

		public bool Update(Role_Model model)
		{
			throw new NotImplementedException();
		}
	}
}
