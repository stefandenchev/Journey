namespace Journey.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;

    using Journey.Data;
    using Journey.Data.Models;
    using Journey.Web.ViewModels.Profile;
    using Microsoft.AspNetCore.Mvc;

    public class ProfileController : BaseController
    {
        private readonly ApplicationDbContext db;

        public ProfileController(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult Payment()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var creditCards = new List<CreditCard>();

            creditCards = this.db.CreditCards.Where(c => c.UserId == userId).ToList();

            var model = new PaymentViewModel
            {
                CreditCards = creditCards ?? new List<CreditCard>(),
            };

            return this.View(model);
        }

        [HttpGet]
        public JsonResult GetCreditCards()
        {
            string currentUserId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            List<CreditCard> creditCards = new List<CreditCard>();

            creditCards = this.db.CreditCards.Where(c => c.UserId == currentUserId).ToList();

            return this.Json(creditCards);
        }

        [HttpPost]
        public ActionResult AddCreditCard([FromBody]CreditCard creditCard)
        {
            creditCard.UserId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            try
            {
                string cardNumberFormatted = creditCard.CardNumber.Replace(" ", string.Empty);
                cardNumberFormatted = cardNumberFormatted.Length == 13 ? string.Format("{0:0000 0000 0000 0}", long.Parse(cardNumberFormatted)) :
                    cardNumberFormatted.Length == 15 ? string.Format("{0:0000 0000 0000 000}", long.Parse(cardNumberFormatted)) :
                        string.Format("{0:0000 0000 0000 0000}", long.Parse(cardNumberFormatted));
                creditCard.CardNumber = cardNumberFormatted;

                this.db.CreditCards.Add(creditCard);
                this.db.SaveChanges();
                return this.Json(new { Success = true });
            }
            catch
            {
                return this.Json(new { Success = false, Error = "Error occurred while saving card information" });
            }
        }

        public ActionResult DeleteCreditCard(int? id)
        {
            if (id == null)
            {
                return this.Json(new { Success = false, Error = "Information not received" });
            }

            try
            {
                var creditCard = this.db.CreditCards.FirstOrDefault(c => c.Id == id);

                if (creditCard == null)
                {
                    return this.Json(new { Success = false, Error = "Credit Card not found" });
                }

                this.db.CreditCards.Remove(creditCard);
                this.db.SaveChanges();

                return this.RedirectToAction("Payment", "Profile");
            }
            catch
            {
                return this.Json(new { Success = false, Error = "Error occurred while deleting credit card record" });
            }
        }

        public IActionResult Wishlist()
        {
            return this.View();
        }

        public IActionResult Purchases()
        {
            return this.View();
        }

        public IActionResult Orders()
        {
            return this.View();
        }
    }
}
