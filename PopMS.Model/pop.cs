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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PopIndex { get; set; }
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
    }
}
