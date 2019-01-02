using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using ZooboxApplication.Models.Animals;

namespace ZooboxApplication.Models
{
    public class JobView  : Job
    {
        public List<SelectListItem> ApplicationUsers { get; set; }
    }
}
