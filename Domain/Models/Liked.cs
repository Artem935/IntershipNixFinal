﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Liked
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public int? CarId { get; set; }
    }
}
