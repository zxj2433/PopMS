using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WalkingTec.Mvvm.Core;

namespace PopMS.Model
{
    public class area:BasePoco
    {
        public dc DC { get; set; }
        [Display(Name ="仓库")]
        public Guid DCID { get; set; }
        [Display(Name ="区域")]
        [StringLength(50)]
        [Required]
        public string Area { get; set; }
        [Display(Name ="备注")]
        [StringLength(50)]
        public string AreaRemark { get; set; }
    }
}
