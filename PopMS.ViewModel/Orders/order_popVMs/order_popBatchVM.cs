using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using PopMS.Model;


namespace PopMS.ViewModel.Orders.order_popVMs
{
    public partial class order_popBatchVM : BaseBatchVM<order_pop, order_pop_BatchEdit>
    {
        public order_popBatchVM()
        {
            ListVM = new order_popListVM();
            LinkedVM = new order_pop_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class order_pop_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
