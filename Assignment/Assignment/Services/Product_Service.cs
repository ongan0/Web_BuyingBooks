using Assignment.Models;
using Assignment.IServices;
using System.Linq;

namespace Assignment.Services
{
	public class Product_Service : IProduct_Service
	{
		AssDbContext context;

		public Product_Service()
		{
			context = new AssDbContext();
		}

		public bool Add(Product_Model model)
		{
			try
			{
				//if (context.Product_Models.FirstOrDefault(x => x.Name == model.Name) != null) return false;
				if (!validate(model)) return false;
                context.Product_Models.Add(model);
                context.SaveChanges();
                return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

        bool validate(Product_Model model)
		{
			int checkNumber;

            if (model.CateId.ToString() == "") return false;
            if (model.Price <= 0) return false;
            if (String.IsNullOrEmpty(model.Supplier)) return false;
            if (model.YearOfPublication > DateTime.Now.Year) return false;
			// tách kích thước
			var arr = model.Size.Split("x");
            var arr1 = model.Size.Split(" x ");
            if (arr.Length != 2 && arr1.Length != 2) {  return false; }
            if (arr.Length == 2)
            {
                if (!int.TryParse(arr[0],out checkNumber) || !int.TryParse(arr[1], out checkNumber))
                {
                    return false;
                }
            }
            else
            {
                if (!int.TryParse(arr1[0], out checkNumber) || !int.TryParse(arr1[1], out checkNumber))
                {
                    return false;
                }
            }
            return true;
        }
        public bool DeleteById(Guid id)
		{
            try
            {
				var model = context.Product_Models.FirstOrDefault(x => x.Id == id);
                context.Product_Models.Remove(model);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

		public List<Product_Model> GetAll()
		{
			return context.Product_Models.ToList();
		}

        public List<Product_Model> GetAllPro()
        {
            return context.Product_Models.Where(x=>x.Status==0).ToList();
        }

        public Product_Model GetById(Guid id)
		{
			return context.Product_Models.FirstOrDefault(x => x.Id == id);
		}

		public bool Update(Product_Model model)
		{
            try
            {
				var product = context.Product_Models.Find(model.Id);
                if (String.IsNullOrEmpty(model.Name)) return false;
                if (context.Product_Models.FirstOrDefault(x => x.Name == model.Name) == null) return false;
                if (!validate(model)) return false;
                product.Name = model.Name;
                product.CateId = model.CateId;
                product.Price = model.Price;
                product.AvailableQuantity = model.AvailableQuantity;
                product.LinkImg = model.LinkImg;
                product.Supplier = model.Supplier;
                product.Description = model.Description;
                product.Status = model.Status;
                product.Author = model.Author;
                product.Publisher = model.Publisher;
                product.YearOfPublication = model.YearOfPublication;
                product.Weight = model.Weight;
                product.Size = model.Size;
                product.Page = model.Page;
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
		public List<Product_Model> GetByName(string name)
        {
			List<Product_Model>? products = new List<Product_Model>();
            if (!String.IsNullOrEmpty(name))
			{
				products = context.Product_Models.Where(x => x.Name.Contains(name.Trim())).ToList();
				
			}
			else
				products = context.Product_Models.ToList();
			return products;
		}
        public List<Product_Model> GetByCategory(Guid cateId)
        {
            List<Product_Model>? products = new List<Product_Model>();
                products = context.Product_Models.Where(x => x.CateId == cateId).ToList();
            return products;
        }
    }
}
