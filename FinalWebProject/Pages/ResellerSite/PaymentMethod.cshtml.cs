using FinalWebProject.Data;
using FinalWebProject.Utils;
using FinalWebProject.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;
using System.Net.Mail;

namespace FinalWebProject.Pages.ResellerSite
{
    public class PaymentMethodModel : PageModel
    {
        private readonly FinalWebProject.Data.FinalDbContext _dbContext;
        private readonly IConfiguration _configuration;
        public PaymentMethodModel(IConfiguration configuration, FinalWebProject.Data.FinalDbContext dbContext)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }
        public Reseller Reseller { get; set; }
        public IActionResult OnGet()
        {
            Reseller reseller = SessionHelpers.GetObjectFromJson<Reseller>(HttpContext.Session, "resellerUser");
            if (reseller == null || SessionHelpers.GetObjectFromJson<Reseller>(HttpContext.Session, "resellerUser") == null)
            {
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

        public async Task<IActionResult> OnPostAsync(int totalPrice, string paymentMethod)
        {

			Reseller reseller = SessionHelpers.GetObjectFromJson<Reseller>(HttpContext.Session, "resellerUser");
			if (reseller == null || SessionHelpers.GetObjectFromJson<Reseller>(HttpContext.Session, "resellerUser") == null)
			{
				return RedirectToPage("./Index");
			}
			Reseller = reseller;


			int mode = int.Parse(paymentMethod);
			
			if(mode == 0)
			{
				var resellerImportReceipt = new ResellerImportReceipt
				{
					TotalPrice = totalPrice,
					DateCreated = DateTime.Now,
					PaymentMethod = "Cash",
					PaymentStatus = 0,
					DeliveryStatusId = 1,
					ResellerId = Reseller.ResellerId,
				};
				_dbContext.ResellerImportReceipt.Add(resellerImportReceipt);
				await _dbContext.SaveChangesAsync();

				var receiptId = resellerImportReceipt.ResellerImportReceiptId;

				var cart = GetCartItems();

				foreach(var item in cart)
				{
					var receiptDetails = new ResellerImportReceiptDetails
					{
						Quantity = item.Quantity,
						Price = item.Quantity * item.Phone.Price,
						ResellerImportReceiptId = receiptId,
						PhoneId = item.Phone.PhoneId,
						WarehouseId = item.Warehouse.WarehouseId,
					};
					_dbContext.ResellerImportReceiptDetail.Add(receiptDetails);
					await _dbContext.SaveChangesAsync();
				}
				ClearCart();
				return RedirectToPage("./Index");
			}
			else
			{
				var tmnCode = _configuration.GetValue<string>("Vnpay:TmnCode");
				var baseUrl = _configuration.GetValue<string>("Vnpay:BaseURL");
				var returnUrl = _configuration.GetValue<string>("Vnpay:ReturnURL");
				var hashSecret = _configuration.GetValue<string>("Vnpay:HashSecret");
				var command = _configuration.GetValue<string>("Vnpay:Command");
				VnPay vnPay = new VnPay();
				vnPay.AddRequestData("vnp_Version", VnPay.VERSION);
				vnPay.AddRequestData("vnp_Command", command);
				vnPay.AddRequestData("vnp_TmnCode", tmnCode);
				vnPay.AddRequestData("vnp_Amount", (totalPrice).ToString());
				vnPay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
				vnPay.AddRequestData("vnp_CurrCode", "VND");
				vnPay.AddRequestData("vnp_IpAddr", Utils.Utils.GetIpAddress());
				vnPay.AddRequestData("vnp_Locale", "vn");
				vnPay.AddRequestData("vnp_OrderInfo", "Reseller Import Receipt for " + Reseller.ResellerName);
				vnPay.AddRequestData("vnp_OrderType", "110000");
				vnPay.AddRequestData("vnp_ReturnUrl", returnUrl);
				vnPay.AddRequestData("vnp_TxnRef", DateTime.Now.Ticks.ToString());

				vnPay.AddRequestData("vnp_Bill_Mobile", "0375830891");
				vnPay.AddRequestData("vnp_Bill_Email", Reseller.ResellerEmail);
				vnPay.AddRequestData("vnp_Bill_FirstName", "Tai");
				vnPay.AddRequestData("vnp_Bill_LastName", "Nguyen");
				vnPay.AddRequestData("vnp_Bill_Address", Reseller.ResellerLocation);
				// Invoice
				vnPay.AddRequestData("vnp_Inv_Phone", "0375830891");
				vnPay.AddRequestData("vnp_Inv_Email", Reseller.ResellerEmail);
				vnPay.AddRequestData("vnp_Inv_Customer", Reseller.ResellerName);
				vnPay.AddRequestData("vnp_Inv_Address", Reseller.ResellerLocation);
				vnPay.AddRequestData("vnp_Inv_Company", "MMT Cooperation");
				vnPay.AddRequestData("vnp_Inv_Taxcode", "20180924080900");
				vnPay.AddRequestData("vnp_Inv_Type", "O");

				string paymentUrl = vnPay.CreateRequestUrl(baseUrl, hashSecret);
				return Redirect(paymentUrl);
			}
		}
    }
}
