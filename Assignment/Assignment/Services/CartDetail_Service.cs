using Assignment.Models;
using Assignment.IServices;
using System.Linq;

namespace Assignment.Services
{
	public class CartDetail_Service : ICartDetail_Service
	{

        AssDbContext context;

        public CartDetail_Service()
        {
            context = new AssDbContext();
        }


        public bool Add(CartDetail_Model model)
		{
            try
            {
                context.CartDetail_Models.Add(model);
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
            try
            {
                var model = context.CartDetail_Models.FirstOrDefault(x => x.Id == id);
                context.CartDetail_Models.Remove(model);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteByCartId(Guid cartId)
        {
            try
            {
                var model = context.CartDetail_Models.Where(x => x.UserId == cartId);
                context.CartDetail_Models.RemoveRange(model);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<CartDetail_Model> GetAll()
		{
            return context.CartDetail_Models.ToList();
		}

		public CartDetail_Model GetById(Guid id)
		{
            return context.CartDetail_Models.Find(id);
        }

        public List<CartDetail_Model> GetByCartId(Guid id)
        {
            return context.CartDetail_Models.Where(x => x.UserId == id).ToList();
        }

        public CartDetail_Model GetByProductId(Guid id)
        {
            return context.CartDetail_Models.FirstOrDefault(x => x.ProductId == id);
        }

        public bool UpdateQuantity(CartDetail_Model model)
		{
            try
            {
                var cartDetail = context.CartDetail_Models.Find(model.Id);
                cartDetail.Quantity = model.Quantity;
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
