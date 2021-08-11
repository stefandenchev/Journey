namespace Journey.Tests.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Journey.Data.Models;
    using Journey.Web.ViewModels.Profile;

    public static class CreditCards
    {
        public static IEnumerable<CreditCard> ThreeCards
          => Enumerable.Range(1, 3).Select(i => new CreditCard
          {
              Id = i,
              CardNumber = $"444455556666777{i}",
          });

        public static CreateCardInputModel GetCardInModel(string userId)
        {
            CreateCardInputModel card = new()
            {
                CardNumber = "4567465745674561",
                ExpirationDate = new DateTime(2022, 10, 10).ToString(),
                UserId = userId,
            };

            return card;
        }
    }
}
