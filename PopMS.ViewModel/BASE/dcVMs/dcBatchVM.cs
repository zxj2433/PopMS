using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using PopMS.Model;


namespace PopMS.ViewModel.BASE.dcVMs
{
    public partial class dcBatchVM : BaseBatchVM<dc, dc_BatchEdit>
    {
        public dcBatchVM()
        {
            ListVM = new dcListVM();
            LinkedVM = new dc_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class dc_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
