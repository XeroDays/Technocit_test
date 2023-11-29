using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Technocitt.Database.Tables;
using Technocitt.Models;

namespace Technocitt.ViewModels
{
    public class PropertiesViewModel
    {
        public PropertiesViewModel()
        {
            Properties = new List<MyProperty>();
        }
        public List<MyProperty> Properties { get; set; }
    }
}
