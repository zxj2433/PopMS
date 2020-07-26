using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WalkingTec.Mvvm.Core;

namespace PopMS.Model
{
    public class pop:BasePoco
    {
        public pop_group Group { get; set; }
        [Display(Name = "组别")]
        [Required]
        public Guid GroupID { get; set; }
        [Display(Name = "物料编号")]
        [RegularExpression("^[0-9A-Z]+",ErrorMessage ="只能是数字或字母")]
        [StringLength(50)]
        [Required]
        public string PopNo { get; set; }
        [Display(Name = "物料名称")]
        [StringLength(100)]
        [Required]
        public string PopName { get; set; }
        [Display(Name ="序号")]
        public int? index { get; set; }
    }
}
