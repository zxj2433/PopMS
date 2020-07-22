using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using PopMS.Model;


namespace PopMS.ViewModel.BASE.popVMs
{
    public partial class popSearcher : BaseSearcher
    {
        [Display(Name = "物料名称")]
        public String PopName { get; set; }

        protected override void InitVM()
        {
        }

    }
}
