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
    public partial class popBatchVM : BaseBatchVM<pop, pop_BatchEdit>
    {
        public popBatchVM()
        {
            ListVM = new popListVM();
            LinkedVM = new pop_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class pop_BatchEdit : BaseVM
    {
        public List<ComboSelectListItem> AllGroups { get; set; }
        public Guid? GroupID { get; set; }

        protected override void InitVM()
        {
            AllGroups = DC.Set<pop_group>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.Name);
        }

    }

}
