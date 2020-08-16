using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StoreWebApp.Models
{
    public class Product
    {
        public int Id { get; set; } //ID
        public string Name { get; set; } //Название
        public int Article { get; set; } //Артикул
        public int Price { get; set; } //Цена
        public int Availability { get; set; } //Наличие
        public string Description { get; set; } //Описание
        public string Image { get; set; } //Изображение
        [ForeignKey("Category")]
        public int IdCategory { get; set; } //Категория
        public int IdRecommendation1 { get; set; } //Рекомендация 1
        public int IdRecommendation2 { get; set; } //Рекомендация 2
        public int IdRecommendation3 { get; set; } //Рекомендация 3

        public Category Category { get; set; }

    }
}
