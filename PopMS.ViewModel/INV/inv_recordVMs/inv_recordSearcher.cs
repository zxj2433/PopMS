using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using PopMS.Model;


namespace PopMS.ViewModel.INV.inv_recordVMs
{
    public partial class inv_recordSearcher : BaseSearcher
    {
        [Display(Name = "类型")]
        public RecordType? Type { get; set; }
        [Display(Name = "操作人")]
        public String UserName { get; set; }
        [Display(Name = "操作时间")]
        public DateRange UpdateTime { get; set; }

        protected override void InitVM()
        {
        }

    }
}
