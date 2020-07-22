using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using PopMS.Model;


namespace PopMS.ViewModel.INV.inventoryVMs
{
    public partial class inventoryBatchVM : BaseBatchVM<inventory, inventory_BatchEdit>
    {
        public inventoryBatchVM()
        {
            ListVM = new inventoryListVM();
            LinkedVM = new inventory_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class inventory_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
