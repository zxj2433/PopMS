using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WalkingTec.Mvvm.Core;

namespace PopMS.Model
{
    public enum RecStatus
    {
        /// <summary>
        /// 新建
        /// </summary>
        [Display(Name ="新建")]
        NEW,
        /// <summary>
        /// 正在入库
        /// </summary>
        [Display(Name = "正在入库")]
        ING,
        /// <summary>
        /// 已入库
        /// </summary>
        [Display(Name = "已入库")]
        FINISH
    }
    public class order_pop:BasePoco
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new int ID { get; set; }

        [Display(Name ="批次")]
        [NotMapped]
        public string Lot {
            get
            {
                return (1000 + ID).ToString();
            }
        }
        public contract_pop ContractPop { get; set; }
        [Display(Name = "物料")]
        public Guid ContractPopID { get; set; }
        [Display(Name = "备注")]
        public string Remark { get; set; }
        [Display(Name = "状态")]
        public RecStatus Status { get; set; } = RecStatus.NEW;
        [Display(Name = "订货数量")]
        public int OrderQty { get; set; }
        [Display(Name = "已收数量")]
        public int RecQty { get; set; }
        [Display(Name = "收货人")]
        [StringLength(50)]
        public string RecUser { get; set; }
        [Display(Name = "收货时间")]
        public DateTime? RecTime { get; set; }
        public double Price { get; set; }
    }
}
