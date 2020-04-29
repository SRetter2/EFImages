using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFImages.Data;

namespace EFImages.Web.Models
{
    public class DisplayImageViewModel
    { 
        public Image Image { get; set; }
        public bool Liked { get; set; }
    }
}
