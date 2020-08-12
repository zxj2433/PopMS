using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [NotMapped]
        public string PopNo
        {
            get
            {
                return (10000 + PopIndex).ToString();
            }
        }
        [Display(Name ="序号")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PopIndex { get; set; }
        [Display(Name ="外部编码")]
        [StringLength(20)]
        public string OutID { get; set; }
        [Display(Name = "物料名称")]
        [StringLength(100)]
        [Required]
        public string PopName { get; set; }
        [Display(Name ="序号")]
        public int? index { get; set; }
        [NotMapped]
        [Display(Name ="物料名")]
        public string PopNoName
        {
            get
            {
                return PopNo +"-"+ PopName;
            }
        }
        [Display(Name ="规格")]
        [StringLength(50)]
        public string Pack { get; set; }
        [Display(Name = "单位")]
        [StringLength(20)]
        public string Unit { get; set; }
        [Display(Name ="重量")]
        public double Weight { get; set; }
        [Display(Name = "图片")]
        public FileAttachment Image { get; set; }
        [Display(Name ="图片")]
        public Guid? ImageID { get; set; }
    }
}
