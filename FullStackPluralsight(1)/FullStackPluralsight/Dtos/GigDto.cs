using FullStackPluralsight.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FullStackPluralsight.Dtos
{
    public class GigDto
    {
        public int Id { get; set; }         //usefull as potentially we awnt to return this to the user

        public bool IsCanceled { get; set; }        //This is for delete function

        public UserDto Artist { get; set; }

        public DateTime DateTime { get; set; }

        public string Venue { get; set; }

        public GenreDto Genre { get; set; }

    }
}