using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreWebApp.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } //Название категории

        public List<Product> Products { get; set; }
    }
}
