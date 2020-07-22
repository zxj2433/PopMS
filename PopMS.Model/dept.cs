using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Text;
using WalkingTec.Mvvm.Core;

namespace PopMS.Model
{
    public class dept:BasePoco
    {
        [Display(Name="部门")]
        [StringLength(50)]
        public string DeptName { get; set; }
        [Display(Name = "备注")]
        [StringLength(50)]
        public string DeptRemark { get; set; }
        [Display(Name = "序号")]
        public int Index { get; set; }
    }
}
