using FinalWebProject.Data;
using FinalWebProject.Utils;
using FinalWebProject.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace FinalWebProject.Pages.ResellerSite
{
    public class CartModel : PageModel
    {
        private readonly FinalWebProject.Data.FinalDbContext _dbContext;
		private readonly IConfiguration _configuration;
        public CartModel(FinalWebProject.Data.FinalDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
			_configuration = configuration;
        }
		private Reseller Reseller { get; set; }
        public IActionResult OnGet()
        {
            Reseller reseller = SessionHelpers.GetObjectFromJson<Reseller>(HttpContext.Session, "resellerUser");
            if (reseller == null || SessionHelpers.GetObjectFromJson<Reseller>(HttpContext.Session, "resellerUser") == null) {
                return RedirectToPage("./Index");
            }
			Reseller = reseller;
            return Page();
        }
		public List<ResellerCartItem> GetCartItems()
		{
			if (SessionHelpers.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart") != null)
			{
				return SessionHelpers.GetObjectFromJson<List<ResellerCartItem>>(HttpContext.Session, "cart");
			}
			return new List<ResellerCartItem>();
		}

		private void ClearCart()
		{
			HttpContext.Session.Remove("cart");
		}

		private void SaveCart(List<ResellerCartItem> cartItems)
		{
			SessionHelpers.SetObjectAsJson(HttpContext.Session, "cart", cartItems);
		}

		public IActionResult OnPostUpdate(int[] quantities)
		{
			var cart = GetCartItems();
			for (int i = 0; i < cart.Count; i++)
			{
				cart[i].Quantity = quantities[i];
			}
			SaveCart(cart);
			return Page();
		}
		public IActionResult OnGetRemove(int? phoneId, int? warehouseId)
		{
			if (phoneId == null || warehouseId == null)
			{
				return NotFound("Id is not found");
			}
			var cart = GetCartItems();
			var cartItem = cart.Find(c => c.Phone.PhoneId == phoneId.Value && c.Warehouse.WarehouseId == warehouseId);
			if(cartItem != null)
			{
				cart.Remove(cartItem);
			}
			SaveCart(cart);
			return Page();
		}
		public IActionResult OnGetAdd(int? phoneId, int? warehouseId)
		{
			if(phoneId == null || warehouseId == null)
			{
				return NotFound("Id are invalid");
			}
			var phone = _dbContext.Phone.FirstOrDefault(p => p.PhoneId == phoneId);
			var warehouse = _dbContext.Warehouse.FirstOrDefault(w=> w.WarehouseId== warehouseId);
			if(phone == null || warehouse == null)
			{
				return NotFound("Either phone or warehouse are not found");
			}
			var cart = GetCartItems();
			var cartItem = cart.Find(c => c.Phone.PhoneId == phone.PhoneId && c.Warehouse.WarehouseId == warehouse.WarehouseId);
			if(cartItem != null)
			{
				cartItem.Quantity++;
			}
			else
			{
				cart.Add(new ResellerCartItem (){ Phone = phone, Warehouse = warehouse, Quantity = 1});
			}
			SaveCart(cart);
			return Page();
		}
	}
}
