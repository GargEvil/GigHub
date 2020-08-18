using GigHub.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GigHub.Dtos
{

    public class GenreDto
    {
        public byte Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}