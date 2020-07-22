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
    public partial class areaBatchVM : BaseBatchVM<area, area_BatchEdit>
    {
        public areaBatchVM()
        {
            ListVM = new areaListVM();
            LinkedVM = new area_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class area_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
