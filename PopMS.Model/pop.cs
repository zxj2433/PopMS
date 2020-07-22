using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WalkingTec.Mvvm.Core;

namespace PopMS.Model
{
    public class pop:BasePoco
    {
        public dc DC { get; set; }
        [Display(Name = "仓库")]
        public Guid DCID { get; set; }
        [Display(Name = "物料编号")]
        [RegularExpression("^[0-9A-Z]+")]
        [StringLength(50)]
        public string PopNo { get; set; }
        [Display(Name = "物料名称")]
        [StringLength(100)]
        public string PopName { get; set; }
        [Display(Name ="序号")]
        public int index { get; set; }
    }
}
