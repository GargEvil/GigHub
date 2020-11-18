using GigHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace GigHub.DataAccess
{
    public static class AttendanceManager
    {
        public static List<Gig> Attending(string userId)
        {
            using(ApplicationDbContext _context = new ApplicationDbContext())
            {
                var gigs = _context.Attendances.
                Where(a => a.AttendeeId == userId)
                .Select(a => a.Gig)
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .ToList();

                return gigs;
            }
        }
    }
}