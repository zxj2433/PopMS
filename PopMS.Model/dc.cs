﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WalkingTec.Mvvm.Core;

namespace PopMS.Model
{
    public class dc:BasePoco
    {
        [Display(Name ="仓库号")]
        [StringLength(50)]
        [RegularExpression("^[0-9]+",ErrorMessage ="仓库号只能是数字")]
        [Required]
        public string DcNo { get; set; }
        [Display(Name = "仓库名")]
        [StringLength(50)]
        [Required]
        public string Name { get; set; }
        [Display(Name = "备注")]
        [StringLength(500)]
        public string Remark { get; set; }
    }
}
