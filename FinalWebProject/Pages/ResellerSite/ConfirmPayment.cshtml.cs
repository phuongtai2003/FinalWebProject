using FinalWebProject.Data;
using FinalWebProject.Utils;
using FinalWebProject.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;
using System.Web;

namespace FinalWebProject.Pages.ResellerSite
{
    public class ConfirmPaymentModel : PageModel
    {
        private readonly IConfiguration _configuration;
        private readonly FinalWebProject.Data.FinalDbContext _dbContext;
        public ConfirmPaymentModel(IConfiguration configuration, FinalWebProject.Data.FinalDbContext dbContext)
        {
            _configuration = configuration;
            _dbContext = dbContext;
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

        public async Task<IActionResult> OnGetAsync()
        {
			Reseller reseller = SessionHelpers.GetObjectFromJson<Reseller>(HttpContext.Session, "resellerUser");
			if (reseller == null || SessionHelpers.GetObjectFromJson<Reseller>(HttpContext.Session, "resellerUser") == null)
			{
				return RedirectToPage("./Index");
			}

			if (Request.QueryString.HasValue)
            {
                var hashSecret = _configuration.GetValue<string>("Vnpay:HashSecret");
                VnPay vnPay = new VnPay();
                string vnPayString = Request.QueryString.Value;
                var col = HttpUtility.ParseQueryString(vnPayString);
                string[] queryParams = col.AllKeys;
                foreach(var param in queryParams)
                {
                    if (!string.IsNullOrEmpty(param) && param.StartsWith("vnp_"))
                    {
                        vnPay.AddResponseData(param, col.Get(param));
                    }
                }
                var transactionNo = vnPay.GetResponseData("vnp_TransactionNo");
                var transactionResCode = vnPay.GetResponseData("vnp_ResponseCode");
                var orderId = vnPay.GetResponseData("vnp_TxnRef");
                var cardType = vnPay.GetResponseData("vnp_CardType");
                var transactionStatus = vnPay.GetResponseData("vnp_TransactionStatus");
                var secureHash = vnPay.GetResponseData("vnp_SecureHash");
                bool validate = vnPay.ValidateSignature(secureHash, hashSecret);
                if (validate)
                {
                    if (transactionResCode.Equals("00") && transactionStatus.Equals("00"))
                    {
                        ViewData["Message"] = "Successful transaction for order " + orderId;
                        var cart = GetCartItems();
                        var total = 0;
                        foreach (var item in cart)
                        {
                            total = total + item.Quantity * item.Phone.Price;
                        }
                        var resellerReceipt = new ResellerImportReceipt
                        {
                            TotalPrice = total,
                            DateCreated = DateTime.Now,
                            PaymentMethod = cardType,
                            PaymentStatus = 0,
                            DeliveryStatusId = 1,
                            ResellerId = reseller.ResellerId,
						};
                        _dbContext.ResellerImportReceipt.Add(resellerReceipt);
                        await _dbContext.SaveChangesAsync();

                        var resellerReceiptId = resellerReceipt.ResellerImportReceiptId;

                        foreach(var item in cart)
                        {
                            var receiptDetails = new ResellerImportReceiptDetails
                            {
                                Quantity = item.Quantity,
                                Price = item.Quantity * item.Phone.Price,
                                ResellerImportReceiptId = resellerReceiptId,
                                PhoneId = item.Phone.PhoneId,
								WarehouseId = item.Warehouse.WarehouseId,
							};
                            _dbContext.ResellerImportReceiptDetail.Add(receiptDetails);
                            await _dbContext.SaveChangesAsync();
                        }
                        ClearCart();
                    }
                    else
                    {
                        ViewData["Message"] = "There has been an error for order " + orderId + " | Status code: " + transactionResCode;
                    }
                }
                else
                {
                    ViewData["Message"] = "Something went wrong";
                }
                return Page();
            }
            return RedirectToPage("/Index");
        }
    }
}
