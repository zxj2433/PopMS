using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using PopMS.Model;


namespace PopMS.ViewModel.userVMs
{
    public partial class userBatchVM : BaseBatchVM<user, user_BatchEdit>
    {
        public userBatchVM()
        {
            ListVM = new userListVM();
            LinkedVM = new user_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class user_BatchEdit : BaseVM
    {
        public List<ComboSelectListItem> AllUserGroupss { get; set; }
        [Display(Name = "Group")]
        public List<Guid> SelectedUserGroupsIDs { get; set; }

        protected override void InitVM()
        {
            AllUserGroupss = DC.Set<FrameworkGroup>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.GroupName);
        }

    }

}
