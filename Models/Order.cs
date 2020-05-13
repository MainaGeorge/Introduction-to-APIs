using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace HPlusSport.API.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }

        public int UserId { get; set; }

        [JsonIgnore]
        public virtual User User { get; set; }

        public List<Product> Products { get; set; }


    }
}
