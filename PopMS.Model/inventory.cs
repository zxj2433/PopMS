using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WalkingTec.Mvvm.Core;

namespace PopMS.Model
{
    public class inventory:TopBasePoco
    {
        public area_location Location { get; set; }
        [Display(Name = "货位")]
        public Guid LocationID { get; set; }
        [Display(Name ="库存")]
        public int Stock { get; set; }
        [Display(Name ="已使用库存")]
        public int UsedQty { get; set; }
        [Display(Name = "上架人")]
        [StringLength(50)]
        public string PutUser { get; set; }
        [Display(Name = "上架时间")]
        public DateTime? PutTime { get; set; }
        public List<inventoryout> InvOut { get; set; }
        public List<inventoryIn> InvIn { get; set; }
    }
}
