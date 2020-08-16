using StoreWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreWebApp.ViewModels
{
    public class ProductViewModel
    {
        public Product Product { get; set; }
        public List<Product> RecommendedProducts { get; set; }
    }
}
