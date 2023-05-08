using FinalWebProject.Data;
using FinalWebProject.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace FinalWebProject.Pages.AccountantSite
{
    public class ConfirmOrderModel : PageModel
    {
        private readonly FinalWebProject.Data.FinalDbContext _dbContext;
        public SelectList DeliveryStatusList { get; set; }
        public ConfirmOrderModel(FinalWebProject.Data.FinalDbContext dbContext)
        {
            _dbContext = dbContext;
            DeliveryStatusList = new SelectList(_dbContext.DeliveryStatus.Where(ds => ds.DeliveryStatusId == 2), "DeliveryStatusId", "DeliveryStatusName");
        }
        public IList<ResellerImportReceiptDetails> ResellerImportReceiptDetails { get; set; } = default!;
        public ResellerImportReceipt ResellerImportReceipt { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var accountant = SessionHelpers.GetObjectFromJson<Accountant>(HttpContext.Session, "accountantUser");
            if(accountant == null)
            {
                return RedirectToPage("./Index");
            }
            if (id == null)
            {
                return NotFound();
            }
            ResellerImportReceipt = await _dbContext.ResellerImportReceipt.FirstOrDefaultAsync(r => r.ResellerImportReceiptId == id.Value);
            if (ResellerImportReceipt == null)
            {
                return RedirectToPage("./Index");
            }
            ResellerImportReceiptDetails = await _dbContext.ResellerImportReceiptDetail.Where(r => r.ResellerImportReceiptId == ResellerImportReceipt.ResellerImportReceiptId).Include(r => r.Phone).ThenInclude(p=>p.Manufacturer).Include(r => r.Warehouse).ToListAsync();
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int paymentStatus, int deliveryStatus, int? orderId)
        {
			var accountant = SessionHelpers.GetObjectFromJson<Accountant>(HttpContext.Session, "accountantUser");
			if (accountant == null)
			{
				return RedirectToPage("./Index");
			}
			if (orderId == null)
            {
                return NotFound("Order was not found");
            }
			ResellerImportReceipt resellerImportReceipt = await _dbContext.ResellerImportReceipt.FirstOrDefaultAsync(r=>r.ResellerImportReceiptId == orderId);
            
            if(resellerImportReceipt == null)
            {
                return RedirectToPage("./ImportOrderListing");
            }
            ResellerImportReceipt = resellerImportReceipt;
			var resellerImportOrderDetails = await _dbContext.ResellerImportReceiptDetail.Where(d => d.ResellerImportReceiptId == resellerImportReceipt.ResellerImportReceiptId).Include(d=>d.Warehouse).Include(d=>d.Phone).ToListAsync();
            ResellerImportReceiptDetails = resellerImportOrderDetails;
            //Check warehouse products quantity
            foreach(var item in resellerImportOrderDetails)
            {
                var warehouseProductData = await _dbContext.WarehouseProducts.FirstOrDefaultAsync(wp => wp.WarehouseId == item.WarehouseId && wp.PhoneId == item.PhoneId && wp.Quantity >= item.Quantity);
                if (warehouseProductData == null)
                {
                    ViewData["Message"] = "Warehouse does not have enough phones";
                    return Page();
                }
            }

            //Deduct from warehouse products
            foreach(var item in resellerImportOrderDetails)
            {
                var warehouseProductsData = await _dbContext.WarehouseProducts.FirstOrDefaultAsync(wp => wp.WarehouseId == item.WarehouseId && wp.PhoneId == item.PhoneId);
                var newWarehouseProduct = new WarehouseProducts
                {
                    WarehouseId = warehouseProductsData.WarehouseId,
                    PhoneId = warehouseProductsData.PhoneId,
                    Quantity = warehouseProductsData.Quantity - item.Quantity,
                };
                _dbContext.Entry(warehouseProductsData).CurrentValues.SetValues(newWarehouseProduct);
                await _dbContext.SaveChangesAsync();
            }

			var newResellerImportReceipt = new ResellerImportReceipt
            {
                ResellerImportReceiptId = resellerImportReceipt.ResellerImportReceiptId,
                TotalPrice = resellerImportReceipt.TotalPrice,
                DateCreated = resellerImportReceipt.DateCreated,
                PaymentMethod = resellerImportReceipt.PaymentMethod,
                PaymentStatus = paymentStatus,
                DeliveryStatusId = deliveryStatus,
                ResellerId = resellerImportReceipt.ResellerId,
            };

            //Update Reseller Import Receipt

            _dbContext.Entry(resellerImportReceipt).CurrentValues.SetValues(newResellerImportReceipt);
            await _dbContext.SaveChangesAsync();

			//Create export receipt
			ExportReceipt exportReceipt = new ExportReceipt
            {
                TotalPrice = newResellerImportReceipt.TotalPrice,
                DateCreated = DateTime.Now,
                AccountantId = accountant.AccountantID,
			};
            _dbContext.ExportReceipt.Add(exportReceipt);
            await _dbContext.SaveChangesAsync();

            //Create exxport receipt details
            var res =  resellerImportOrderDetails.GroupBy(d => d.Phone.PhoneId).Select(Group => new {PhoneId = Group.Key, Quantity = Group.Sum(d => d.Quantity), Price = Group.Sum(d=>d.Price) });
            
            foreach(var item in res)
            {
                var exportReceiptDetails = new ExportReceiptDetails
                {
                    ResellerId = newResellerImportReceipt.ResellerId,
                    PhoneId = item.PhoneId,
                    Quantity = item.Quantity,
                    Price = item.Price,
                    ExportReceiptId = exportReceipt.ExportReceiptId,
				};
                _dbContext.ExportReceiptDetails.Add(exportReceiptDetails);
                await _dbContext.SaveChangesAsync();
            }

            foreach(var item in res)
            {
                var resellerStorage = await _dbContext.ResellerStorage.FirstOrDefaultAsync(rs => rs.PhoneId == item.PhoneId && rs.ResellerId == newResellerImportReceipt.ResellerId);
                if(resellerStorage == null)
                {
					var newResellerStorage = new ResellerStorage
					{
						ResellerId = newResellerImportReceipt.ResellerId,
						PhoneId = item.PhoneId,
						Quantity = item.Quantity,
					};
                    _dbContext.ResellerStorage.Add(newResellerStorage);
                    await _dbContext.SaveChangesAsync();
				}
                else
                {
                    var newResellerStorage = new ResellerStorage
                    {
                        ResellerId = newResellerImportReceipt.ResellerId,
                        PhoneId = item.PhoneId,
                        Quantity = item.Quantity + resellerStorage.Quantity,
                    };
                    _dbContext.Entry(resellerStorage).CurrentValues.SetValues(newResellerStorage);
                    await _dbContext.SaveChangesAsync();
                }
            }
            ViewData["Message"] = "Confirm Reseller Order Successfully";
            return Page();
        }
    }
}
