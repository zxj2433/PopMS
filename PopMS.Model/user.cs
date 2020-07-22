using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WalkingTec.Mvvm.Core;

namespace PopMS.Model
{
    public class user:FrameworkUserBase
    {
        public dc DC { get; set; }
        [Display(Name = "仓库")]
        [Required]
        public Guid? DCID { get; set; }
        public dept Dept { get; set; }
        [Display(Name ="部门")]
        [Required]
        public Guid? DeptID { get; set; }
    }
}
