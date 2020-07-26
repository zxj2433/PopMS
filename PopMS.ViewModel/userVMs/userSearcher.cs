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
    public partial class userSearcher : BaseSearcher
    {
        public List<ComboSelectListItem> AllDCs { get; set; }
        [Display(Name ="仓库")]
        public Guid? DCID { get; set; }
        public List<ComboSelectListItem> AllDepts { get; set; }
        [Display(Name = "部门")]
        public Guid? DeptID { get; set; }
        [Display(Name = "用户名")]
        public String ITCode { get; set; }

        protected override void InitVM()
        {
            AllDCs = DC.Set<dc>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.Name);
            AllDepts = DC.Set<dept>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.DeptName);
        }

    }
}
