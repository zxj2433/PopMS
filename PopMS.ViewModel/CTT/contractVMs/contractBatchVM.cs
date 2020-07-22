using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using PopMS.Model;


namespace PopMS.ViewModel.CTT.contractVMs
{
    public partial class contractBatchVM : BaseBatchVM<contract, contract_BatchEdit>
    {
        public contractBatchVM()
        {
            ListVM = new contractListVM();
            LinkedVM = new contract_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class contract_BatchEdit : BaseVM
    {
        [Display(Name = "优先级")]
        public Int32? Prority { get; set; }

        protected override void InitVM()
        {
        }

    }

}
