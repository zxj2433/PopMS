using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using PopMS.Model;


namespace PopMS.ViewModel.ShipOrder.ship_pop_sumVMs
{
    public partial class ship_pop_sumSearcher : BaseSearcher
    {
        [Display(Name = "日期")]
        public DateRange OrderDate { get; set; }
        [Display(Name ="状态")]
        public ShipStatus? Status { get; set; }
        protected override void InitVM()
        {
        }

    }
}
