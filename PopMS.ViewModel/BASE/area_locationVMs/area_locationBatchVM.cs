using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using PopMS.Model;


namespace PopMS.ViewModel.BASE.area_locationVMs
{
    public partial class area_locationBatchVM : BaseBatchVM<area_location, area_location_BatchEdit>
    {
        public area_locationBatchVM()
        {
            ListVM = new area_locationListVM();
            LinkedVM = new area_location_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class area_location_BatchEdit : BaseVM
    {
        [Display(Name = "可混放")]
        public Boolean? isMix { get; set; }

        protected override void InitVM()
        {
        }

    }

}
