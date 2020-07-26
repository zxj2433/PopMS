using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WalkingTec.Mvvm.Core;

namespace PopMS.Model
{
    public enum ShipStatus
    {
        /// <summary>
        /// 新建
        /// </summary>
        [Display(Name ="新建")]
        NEW,
        /// <summary>
        /// 已取消
        /// </summary>
        [Display(Name = "已取消")]
        CANCEL,
        /// <summary>
        /// 正在发放
        /// </summary>
        [Display(Name = "正在发放")]
        ING,
        /// <summary>
        /// 已发放
        /// </summary>
        [Display(Name = "已发放")]
        FINISH
    }
    public class ship_pop : BasePoco
    {
        public user User { get; set; }
        [Display(Name = "申请人")]
        public Guid UserID { get; set; }
        [Display(Name = "申请物料")]
        public pop Pop { get; set; }
        [Display(Name = "申请物料")]
        [Required]
        public Guid PopID { get; set; }
        [Display(Name = "申请数量")]
        [Required]
        public int? OrderQty { get; set; }
        [Display(Name = "分配数量")]
        public int AlcQty { get; set; }
        [Display(Name ="实发数量")]
        public int ShipQty { get; set; }
        [Display(Name ="最大可用量")]
        public int EnableQty { get; set; }
        [Display(Name = "状态")]
        public ShipStatus Status { get; set; }
        [Display(Name = "派发人")]
        [StringLength(50)]
        public string ShipUser { get; set; }
        [Display(Name = "派发时间")]
        public DateTime? ShipTime { get; set; }
        public ship_pop_sum Ship_Pop_Sum { get; set; }
        [Display(Name ="申请单号")]
        public Guid? Ship_Pop_SumID { get; set; }

        public List<inventoryout> ShipIn { get; set; }
    }
}
