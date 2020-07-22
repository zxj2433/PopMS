using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using PopMS.Model;


namespace PopMS.ViewModel.BASE.areaVMs
{
    public partial class areaSearcher : BaseSearcher
    {
        [Display(Name = "区域")]
        public String Area { get; set; }

        protected override void InitVM()
        {
        }

    }
}
