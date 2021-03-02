using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.ViewModels
{
    public class OrderFormViewModel
    {
        [Required(ErrorMessage ="Введите имя получателя")]
        public string Custumer { get; set; }

        [Required(ErrorMessage ="Введите адрес доставки")]

        public string Adress { get; set; }

        [Required(ErrorMessage = "Введите номер телефона получателя")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Неверный формат")]
        [RegularExpression(@"^\+[0-9]{2}([0-9]{10})$", ErrorMessage = "Неверный формат")]
        public string PhoneNumber { get; set; }

        public string Comment { get; set; }

        public string JsonLines { get; set; }

        public string Amount { get; set; }

        public string Date { get; set; }
    }
}
