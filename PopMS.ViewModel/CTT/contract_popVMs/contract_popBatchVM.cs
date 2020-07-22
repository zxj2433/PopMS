using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using PopMS.Model;


namespace PopMS.ViewModel.CTT.contract_popVMs
{
    public partial class contract_popBatchVM : BaseBatchVM<contract_pop, contract_pop_BatchEdit>
    {
        public contract_popBatchVM()
        {
            ListVM = new contract_popListVM();
            LinkedVM = new contract_pop_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class contract_pop_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
