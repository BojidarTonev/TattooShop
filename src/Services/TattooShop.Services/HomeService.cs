using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TattooShop.Data.Contracts;
using TattooShop.Data.Models;
using TattooShop.Services.Contracts;

namespace TattooShop.Services
{
    public class HomeService : IHomeService
    {
        private readonly IRepository<Tattoo> _tattoosRepository;
        private readonly IRepository<Artist> _artistsRepository;
        private readonly IRepository<ContactInfo> _feedbackRepository;
        private const int TattoosToTake = 12;

        public HomeService(IRepository<Tattoo> tattoosRepository, 
            IRepository<Artist> artistsRepository, 
            IRepository<ContactInfo> feedbackRepository)
        {
            this._tattoosRepository = tattoosRepository;
            this._artistsRepository = artistsRepository;
            this._feedbackRepository = feedbackRepository;
        }

        public IEnumerable<Tattoo> RecentTattoos()
        {
            return this._tattoosRepository.All().Include(t => t.TattooStyle).Take(TattoosToTake).OrderBy(x => x.DoneOn).ToList();
        }

        public IEnumerable<Artist> AllArtists()
        {
            return this._artistsRepository.All().Include(a => a.TattooCollection).ToList();
        }

        public void RegisterFeedBack(string firstName, string lastName, string message, string email, string phone)
        {
            var info = new ContactInfo()
            {
                FirstName = firstName,
                LastName = lastName,
                Message = message,
                SenderEmail = email,
                SenderPhoneNumber = phone,
                SendOn = DateTime.UtcNow
            };

            this._feedbackRepository.AddAsync(info);
            this._feedbackRepository.SaveChangesAsync();
        }

        public IEnumerable<ContactInfo> AllFeedback()
        {
            return this._feedbackRepository.All();
        }
    }
}
