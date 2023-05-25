using Assignment.Models;

namespace Assignment.IServices
{
	public interface IBillDetail_Service
	{
        bool Add(BillDetail_Model model);
        bool Update(BillDetail_Model model);
        bool DeleteById(Guid id);
        public BillDetail_Model GetById(Guid id);
        public List<BillDetail_Model> GetAll();
        public List<BillDetail_Model> GetByBillId(Guid billId);
        public BillDetail_Model GetByProId(Guid proId);
    }
}
