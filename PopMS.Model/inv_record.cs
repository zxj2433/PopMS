using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Text;
using WalkingTec.Mvvm.Core;

namespace PopMS.Model
{
    public enum RecordType
    {
        /// <summary>
        /// 调整
        /// </summary>
        [Display(Name="调整")]
        ADJ,
        /// <summary>
        /// 转移
        /// </summary>
        [Display(Name="转移")]
        TSF
    }
    public class inv_record : TopBasePoco
    {
        public inventory Inv { get; set; }
        [Display(Name = "库存")]
        public Guid InvID { get; set; }

        public inventory NewInv { get; set; }
        public Guid? NewInvID { get; set; }
        [Display(Name = "类型")]
        [Required]
        public RecordType? Type { get; set; }
        public area_location FromLoc { get; set; }
        [Display(Name = "从货位")]
        public Guid? FromLocID{get;set;}
        public area_location ToLoc { get; set; }
        [Display(Name = "至货位")]
        public Guid? ToLocID { get; set; }
        [Display(Name = "数量")]
        public int Qty { get; set; }
        [Display(Name = "操作人")]
        public string UserName { get; set; }
        [Display(Name = "操作时间")]
        public DateTime UpdateTime { get; set; }
    }
}
