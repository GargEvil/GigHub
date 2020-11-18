using GigHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using GigHub.ViewModels;

namespace GigHub.DataAccess
{
    public static class GigsManager
    {
        public static List<Gig> GetAll()
        {
            using (ApplicationDbContext _context = new ApplicationDbContext())
            {
                var gigs = _context.Gigs
                    .Include(g => g.Genre)
                    .Include(g => g.Artist)
                    .ToList();

                return gigs;
            }
        }

        public static List<Gig> GetUpcoming()
        {
            using (ApplicationDbContext _context = new ApplicationDbContext())
            {
                var gigs = _context.Gigs
                    .Include(g => g.Genre)
                    .Include(g => g.Artist)
                    .Where(g => g.DateTime > DateTime.Now && !g.IsCanceled)
                    .ToList();

                return gigs;
            }
        }

        public static Gig GetById(int id)
        {
            using (ApplicationDbContext _context = new ApplicationDbContext())
            {
                var gig = _context.Gigs.Include(g => g.Genre).Where(g => g.Id == id).First();

                return gig;
            }
        }

        public static List<Gig> QueryGigs(string query)
        {

            var upcomingGigs = GetUpcoming()
                .Where(g =>
                        g.Artist.Name.Contains(query) ||
                        g.Genre.Name.Contains(query) ||
                        g.Venue.Contains(query))
                .ToList();

            return upcomingGigs;

        }

        public static List<Gig> Mine(string userId)
        {
            using (ApplicationDbContext _context = new ApplicationDbContext())
            {
                var gigs = _context
                .Gigs
                .Where(g => g.ArtistId == userId && g.DateTime > DateTime.Now && !g.IsCanceled)
                .Include(g => g.Genre)
                .ToList();

                return gigs;
            }
        }

        public static GigFormViewModel EditGigFormVM(string userId, int id)
        {
            using (ApplicationDbContext _context = new ApplicationDbContext())
            {
                var gig = _context.Gigs.Single(g => g.Id == id && g.ArtistId == userId);

                var viewModel = new GigFormViewModel
                {
                    Id = gig.Id,
                    Date = gig.DateTime.ToString("d MMM yyyy"),
                    Time = gig.DateTime.ToString("HH:mm"),
                    Genre = gig.GenreId,
                    Venue = gig.Venue,
                    Genres = _context.Genres.ToList(),
                    Heading = "Edit Gig"
                };

                return viewModel;
            }
        }

        public static void Add(GigFormViewModel viewModel, string userId)
        {
            using (ApplicationDbContext _context = new ApplicationDbContext())
            {


                var gig = new Gig
                {
                    ArtistId = userId,
                    DateTime = viewModel.GetDateTime(),
                    GenreId = viewModel.Genre,
                    Venue = viewModel.Venue
                };

                _context.Gigs.Add(gig);
                _context.SaveChanges();
            }
        }
    }
}