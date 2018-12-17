using System.Collections.Generic;
using TattooShop.Data.Models;

namespace TattooShop.Services.Contracts
{
    public interface IHomeService
    {
        IEnumerable<Tattoo> RecentTattoos();

        IEnumerable<Artist> AllArtists();

        void RegisterFeedBack(string firstName, string lastName, string message, string email, string phone);

    }
}
