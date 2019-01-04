using System.Collections.Generic;
using System.Linq;
using TattooShop.Data.Models;

namespace TattooShop.Services.Contracts
{
    public interface IHomeService
    {
        IQueryable<Tattoo> RecentTattoos();

        IQueryable<Artist> AllArtists();

        void RegisterFeedBack(string firstName, string lastName, string message, string email, string phone);

        IEnumerable<ContactInfo> AllFeedback();

    }
}
