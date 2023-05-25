using Assignment.Models;
namespace Assignment.IServices
{
	public interface ICartDetail_Service
	{
        bool Add(CartDetail_Model model);
        bool UpdateQuantity(CartDetail_Model model);
        bool DeleteById(Guid id);
        public CartDetail_Model GetById(Guid id);
        public CartDetail_Model GetByProductId(Guid id);
        public List<CartDetail_Model> GetAll();
        public List<CartDetail_Model> GetByCartId(Guid id);
        public bool DeleteByCartId(Guid cartId);
    }
}
