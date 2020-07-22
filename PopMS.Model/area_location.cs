using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WalkingTec.Mvvm.Core;

namespace PopMS.Model
{
    public class area_location:BasePoco
    {
        [Display(Name ="区域")]
        public area Area { get; set; }
        [Display(Name = "区域")]
        [Required]
        public Guid? AreaID { get; set; }
        [Display(Name = "货位")]
        [Required]
        [StringLength(20)]
        public string Location { get; set; }
        [Display(Name = "可混放")]
        [Required]
        public bool? isMix { get; set; } = true;
    }
}
