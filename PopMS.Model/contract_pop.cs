using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WalkingTec.Mvvm.Core;

namespace PopMS.Model
{
    public class contract_pop:BasePoco
    {
        public pop Pop { get; set; }
        [Display(Name ="物料名")]
        public Guid PopID { get; set; }
        [Display(Name = "单位类型")]
        [StringLength(50)]
        [Required]
        public string UnitPack { get; set; }
        [Display(Name = "箱规")]
        public int Cnt { get; set; }
        [Display(Name ="单价")]
        [Required]
        public double Price { get; set; }
        public contract Contract { get; set; }
        [Display(Name = "合同")]
        [Required]
        public Guid? ContractID { get; set; }
    }
}
