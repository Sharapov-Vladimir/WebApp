using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Areas.Admin.Models.ViewModels
{
    public class ListViewModel
    {
        public List<Category> Categories { get; set; }

        public Category SelectedCategory { get; set; }
    }
}
