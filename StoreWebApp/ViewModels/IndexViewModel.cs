using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StoreWebApp.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace StoreWebApp.ViewModels
{
    public class IndexViewModel
    {
        public List<Product> Products { get; set; }
        public List<Category> Categories { get; set; }
        public SelectList SelectCategories { get; set; }

        //public static implicit operator IndexViewModel(IndexViewModel v)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
