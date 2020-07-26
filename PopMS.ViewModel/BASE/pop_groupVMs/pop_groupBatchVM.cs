using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using PopMS.Model;


namespace PopMS.ViewModel.BASE.pop_groupVMs
{
    public partial class pop_groupBatchVM : BaseBatchVM<pop_group, pop_group_BatchEdit>
    {
        public pop_groupBatchVM()
        {
            ListVM = new pop_groupListVM();
            LinkedVM = new pop_group_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class pop_group_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
