using Assignment.Models;
using Assignment.IServices;
using System.Linq;

namespace Assignment.Services
{
	public class BillDetail_Service : IBillDetail_Service
	{
		AssDbContext context;

		public BillDetail_Service()
		{
			context = new AssDbContext();
		}

		public bool Add(BillDetail_Model model)
		{
            try
            {
                context.BillDetail_Models.Add(model);
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

		public List<BillDetail_Model> GetAll()
		{
			return context.BillDetail_Models.ToList();
		}

		public BillDetail_Model GetById(Guid id)
		{
			return context.BillDetail_Models.Find(id);
		}

		public List<BillDetail_Model> GetByBillId(Guid billId)
		{
			return context.BillDetail_Models.Where(x=>x.BillId == billId).ToList();
		}
        public BillDetail_Model GetByProId(Guid proId)
        {
            return context.BillDetail_Models.FirstOrDefault(x => x.ProductId == proId);
        }
        public bool Update(BillDetail_Model model)
		{
			throw new NotImplementedException();
		}
	}
}
