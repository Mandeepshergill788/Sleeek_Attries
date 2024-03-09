using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using RehmatCreations.Models;
using Stripe.BillingPortal;


namespace RehmatCreations.Controllers

{
    public class CheckoutController : Controller
    {
        public IActionResult Index()
        {
            List<ProductEntity> productList = new List<ProductEntity>();

            productList = new List<ProductEntity>
            {
                new ProductEntity
                {
                    ProductName = "Black Print",
                    Title = "Beautiful design on cotton printed fabric summer",
                    Price = 1600,
                    Quanity = 1,
                    ImagePath = "Images/BlackDress.jpg"

                },
                new ProductEntity
                {
                    ProductName = "Peach Suit",
                    Title = "Beautiful suit designed on double shaded silk",
                    Price = 400,
                    Quanity = 1,
                    ImagePath = "Images/Suit.jpeg"

                }

            };
            return View(productList);
        }
        public IActionResult OrderConfirmation()
        {
            var service = new Stripe.Checkout.SessionService();
            Stripe.Checkout.Session session = service.Get(TempData["Session"].ToString());
            if (session.PaymentStatus == "paid")
            {
                var transaction = session.PaymentIntentId.ToString();
                return View("Success");
            }
            return View("Login");

        }
        public IActionResult Success()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult CheckOut()
        {
            List<ProductEntity> productList = new List<ProductEntity>();

            productList = new List<ProductEntity>
            {
                new ProductEntity
                {
                     ProductName = "Peach Suit",
                    Title = "Beautiful design on cotton printed fabric summer",
                    Price = 16000,
                    Quanity = 1,
                    ImagePath = "Images/BlackDress.jpg"

                },
                new ProductEntity
                {
                    ProductName = "Black Print",
                    Title = "Beautiful suit designed on double shaded silk",
                    Price = 4000,
                    Quanity = 1,
                    ImagePath = "Images/Suit.jpeg"


                }

            };

            var domain = "http://localhost:5169/";

            var options = new Stripe.Checkout.SessionCreateOptions
            {
                //if you used home change back to Checkout
                SuccessUrl = domain + $"CheckOut/OrderConfirmation",
                CancelUrl = domain + $"CheckOut/Login",
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
                // CustomerEmail="digtal@gmail.com"

            };
            foreach (var item in productList)
            {
                var sessionListItem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(item.Price * item.Quanity),
                        Currency = "cad",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.ProductName.ToString(),
                        }

                    },
                    Quantity = item.Quanity
                };
                options.LineItems.Add(sessionListItem);
            }
            var service = new Stripe.Checkout.SessionService();
            Stripe.Checkout.Session session = service.Create(options);
            TempData["Session"] = session.Id;

            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);

        }
    }
}

