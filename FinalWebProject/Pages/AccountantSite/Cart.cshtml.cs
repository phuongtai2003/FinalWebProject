using FinalWebProject.Data;
using FinalWebProject.Utils;
using FinalWebProject.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace FinalWebProject.Pages.AccountantSite
{
    public class CartModel : PageModel
    {
        private readonly FinalWebProject.Data.FinalDbContext _context;
        
        public CartModel(FinalWebProject.Data.FinalDbContext context)
        {
            _context = context;
        }
        public IActionResult OnGet()
        {
            Accountant accountant = SessionHelpers.GetObjectFromJson<Accountant>(HttpContext.Session, "accountantUser");
            if (accountant == null)
            {
                return RedirectToPage("../Index");
            }
            return Page();
        }

        public List<CartItem> GetCartItems()
        {
            if (SessionHelpers.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart") != null)
            {
                return SessionHelpers.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            }
            return new List<CartItem>();
        }

        private void ClearCart()
        {
            HttpContext.Session.Remove("cart");
        }

        private void SaveCart(List<CartItem> cartItems)
        {
            SessionHelpers.SetObjectAsJson(HttpContext.Session, "cart", cartItems);
        }


        public IActionResult OnGetAdd(int? id)
        {
            var phone = _context.Phone.FirstOrDefault(p => p.PhoneId == id);
            if (phone == null)
            {
                return NotFound("Product is not found");
            }
            var cart = GetCartItems();
            var cartItem = cart.Find(c => c.Phone.PhoneId == id);
            if(cartItem != null)
            {
                cartItem.Quantity++;
            }
            else
            {
                cart.Add(new CartItem (){ Phone = phone, Quantity = 1});
            }
            SaveCart(cart);
            return Page();
        }
        public IActionResult OnPostUpdate(int[] quantities)
        {
            var cart = GetCartItems();
            for(int i = 0; i < cart.Count; i++)
            {
                cart[i].Quantity = quantities[i];
            }
            SaveCart(cart);
            return Page();
        }
        public IActionResult OnGetRemove(int? id) {
            var phone = _context.Phone.FirstOrDefault(p => p.PhoneId == id);
            if (phone == null)
            {
                return NotFound("Product is not found");
            }
            var cart = GetCartItems();
            var cartItem = cart.Find(c => c.Phone.PhoneId ==id);
            if(cartItem != null)
            {
                cart.Remove(cartItem);
            }

            SaveCart(cart);
            return Page();
        }
        public async Task<IActionResult> OnGetConfirmAsync()
        {
            List<CartItem> cartItems = GetCartItems();
            if(cartItems.Count == 0)
            {
                ViewData["Error"] = "Cart is Empty";
                return Page();
            }
            Accountant currentAccountant = SessionHelpers.GetObjectFromJson<Accountant>(HttpContext.Session, "accountantUser");
            int totalPrice = 0;

            foreach(var item in cartItems) {
                totalPrice += item.Quantity * item.Phone.Price;
            }
            var receipt = new Receipt
            {
                AccountantId = currentAccountant.AccountantID,
                TotalPrice= totalPrice,
                DateCreated = DateTime.Now,
            };
            _context.Receipt.Add(receipt);
            await _context.SaveChangesAsync();

            int newReceiptId = receipt.ReceiptId;
            foreach(var item in cartItems)
            {
                var receiptDetails = new ReceiptDetails
                {
                    ReceiptId = newReceiptId,
                    PhoneId = item.Phone.PhoneId,
                    Quantity = item.Quantity,
                    Price = item.Phone.Price * item.Quantity,
                };
                _context.ReceiptDetails.Add(receiptDetails);
                await _context.SaveChangesAsync();
            }

            foreach (var item in cartItems)
            {
                int currentAccountantWarehouse = currentAccountant.WarehouseID;
                var warehouseProduct = _context.WarehouseProducts.FirstOrDefault(wp => wp.WarehouseId == currentAccountantWarehouse && wp.PhoneId == item.Phone.PhoneId);
                if (warehouseProduct != null)
                {
                    var newWarehouseProduct = new WarehouseProducts
                    {
                        WarehouseId = currentAccountantWarehouse,
                        PhoneId = item.Phone.PhoneId,
                        Quantity = item.Quantity + warehouseProduct.Quantity,
                    };
                    _context.Entry(warehouseProduct).CurrentValues.SetValues(newWarehouseProduct);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    var newWarehouseProduct = new WarehouseProducts
                    {
                        WarehouseId = currentAccountantWarehouse,
                        PhoneId = item.Phone.PhoneId,
                        Quantity = item.Quantity,
                    };
                    _context.WarehouseProducts.Add(newWarehouseProduct);
                    await _context.SaveChangesAsync();
                }
            }
            ClearCart();

            return RedirectToPage("./ReceiptListing");
        }
    }
}
