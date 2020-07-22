using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Text;
using WalkingTec.Mvvm.Core;

namespace PopMS.Model
{
  
    public class ship_pop_sum:TopBasePoco
    {
        [Display(Name ="日期")]
        public DateTime OrderDate { get; set; }
        [Display(Name = "备注")]
        [StringLength(50)]
        [Required]
        public string OrderRemark { get; set; }
        public List<ship_pop> ShipPops { get; set; }
    }
}
