using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WalkingTec.Mvvm.Core;

namespace PopMS.Model
{
    public class contract:TopBasePoco
    {
        public dc DC { get; set; }
        [Display(Name = "仓库")]
        public Guid DCID { get; set; }
        [Display(Name = "合同编号")]
        public string ContractID { get; set; }
        [Display(Name ="合同名")]
        [StringLength(50)]
        [Required]
        public string Name { get; set; }
        [Display(Name ="供应商")]
        [StringLength(50)]
        [Required]
        public string Vendor { get; set; }
        [Display(Name ="备注")]
        [StringLength(500)]
        public string Remark { get; set; }
        [Display(Name ="开始日期")]
        public DateTime StartDate { get; set; }
        public List<contract_pop> Pops { get; set; }
        [Display(Name = "失效日期")]
        public DateTime EndDate { get; set; }        
        public FileAttachment ContractFile { get; set; }
        [Display(Name ="合同文件")]
        public Guid? ContractFileID { get; set; }
        [Display(Name = "添加人")]
        [StringLength(50)]
        public string UserCode { get; set; }
        [Display(Name ="导入日期")]
        public DateTime ImportTime { get; set; }
        [Display(Name ="合同金额")]
        public double MaxCost { get; set; }
    }
}
