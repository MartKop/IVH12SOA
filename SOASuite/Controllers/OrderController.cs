using SOASuite.Models;
using SOASuite.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SOASuite.Controllers
{
    public class OrderController : Controller
    {
        //private itemtype[] itemlist = new itemtype[3];

        //public OrderController() {
        //    itemList[0] = new ItemType {
        //        SKU = "32779",
        //        Brand = "Slaker",
        //        Category = 2008,
        //        Description = "Slaker Water Bottle",
        //        Model = "Water bottle",
        //        Quantity = 5,
        //        UnitPrice = 4.72
        //    };
        //    itemList[1] = new ItemType {
        //        SKU = "30421",
        //        Brand = "Grand Prix",
        //        Category = 2005,
        //        Description = "Grand Prix Bicycle Tires",
        //        Model = "Bicycle Tires",
        //        Quantity = 2,
        //        UnitPrice = 10.72
        //    };
        //    itemList[3] = new ItemType {
        //        SKU = "32861",
        //        Brand = "Safe-T",
        //        Category = 1829,
        //        Description = "Safe-T Helmet",
        //        Model = "Bicycle helmet",
        //        Quantity = 1,
        //        UnitPrice = 60.72
        //    };

        //}
        // GET: Order
        [HttpGet]
        public ActionResult Index() {


            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(OrderModel model) {
            List<ItemType> items = new List<ItemType>();
            ItemType type = new ItemType {
                SKU = "32861",
                Brand = "Safe-T",
                Category = 1829,
                Description = "Safe-T Helmet",
                Model = "Bicycle helmet",
                Quantity = 1,
                UnitPrice = 60.72
            };
            ItemType type1 = new ItemType {
                SKU = "32779",
                Brand = "Slaker",
                Category = 2008,
                Description = "Slaker Water Bottle",
                Model = "Water bottle",
                Quantity = 5,
                UnitPrice = 4.72
            };
            ItemType type2 = new ItemType {
                SKU = "30421",
                Brand = "Grand Prix",
                Category = 2005,
                Description = "Grand Prix Bicycle Tires",
                Model = "Bicycle Tires",
                Quantity = 2,
                UnitPrice = 10.72
            };
            items.Add(type);
            items.Add(type1);
            items.Add(type2);
            ItemType[] itemListClone = items.ToArray();

            List<string> adresgui = new List<string>();
            adresgui.Add(model.AddressLine);
            string[] adressclone = adresgui.ToArray();

            AddressType addressType = new AddressType {             
                City = model.City,
                AddressLine = adressclone,
                FirstName = model.Firstname,
                LastName = model.Lastname,
                PhoneNumber = model.Phonenumber,
                State = model.State,
                ZipCode = model.ZipCode
            };

            ShippingProviderType shippingProviderType = new ShippingProviderType {
                Name = "USPS"
            };

            ShippingType shippingType = new ShippingType {
                Address = addressType,
                ShipMethod = 1,
                ShippingSpeed = ShippingSpeedType.Oneday,
                ShipMethodSpecified = true,
                ShippingSpeedSpecified = true,
                ShippingProvider = shippingProviderType
            };

            PaymentType paymentType = new PaymentType {
                AuthorizationAmount = 12.12,
                AuthorizationAmountSpecified = true,
                AuthorizationDate = DateTime.Now,
                AuthorizationDateSpecified = true,
                BillingAddress = addressType,
                CardName = "AMEX",
                CardNum = "1234123412341234",
                CardPaymentType = 1,
                ExpireDate = "0316"
            };

            OrderType order = new OrderType {               
                Email = model.Email,
                OrderDate = new DateTime().ToUniversalTime(),
                Shipping = shippingType,
                Billing = paymentType,
                Items = itemListClone,
                OrderDateSpecified = true
            };

            processOrderPortTypeClient client = new processOrderPortTypeClient();
            processResponse response = await client.processAsync(order);
            order.OrderNumber = response.OrderAck.OrderNumber;


            return View("OrderSuccess", order);

        }

        public ViewResult OrderSuccess(OrderType model) {
            return View(model);
        }
    }
}