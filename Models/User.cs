﻿using System.Collections.Generic;

namespace HPlusSport.API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public List<Order> Orders { get; set; }
    }
}