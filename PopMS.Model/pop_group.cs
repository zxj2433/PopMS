using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WalkingTec.Mvvm.Core;

namespace PopMS.Model
{
    public class pop_group:BasePoco
    {
        [Display(Name ="组别")]
        [StringLength(50)]
        [Required]
        public string Name { get; set; }
        [Display(Name = "序号")]
        public int? Index { get; set; }
    }
}
