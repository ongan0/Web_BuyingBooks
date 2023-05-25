using Assignment.Models;
using Assignment.IServices;
using System.Linq;

namespace Assignment.Services
{
	public class Bill_Service : IBill_Service
	{
        AssDbContext context;

        public Bill_Service()
        {
            context = new AssDbContext();
        }

        public bool Add(Bill_Model model)
		{
            try
            {
                context.Bill_Models.Add(model);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

		public bool DeleteById(Guid id)
		{
			throw new NotImplementedException();
		}

		public List<Bill_Model> GetAll()
		{
			return context.Bill_Models.ToList();
		}

		public Bill_Model GetById(Guid id)
		{
			return context.Bill_Models.Find(id);
		}

		public List<Bill_Model> GetByName(string name)
		{
			throw new NotImplementedException();
		}

		public bool Update(Bill_Model model)
		{
            try
            {
                var bill = GetById(model.Id);
                bill.Address = model.Address;
                bill.PhoneNumber = model.PhoneNumber;
                bill.Status = model.Status;
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
	}
}
