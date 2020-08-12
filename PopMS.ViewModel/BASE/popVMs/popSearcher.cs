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
    public partial class popSearcher : BaseSearcher
    {
        public List<ComboSelectListItem> AllGroups { get; set; }
        [Display(Name = "物料类型")]
        public Guid? GroupID { get; set; }
        [Display(Name = "物料名称")]
        public String PopName { get; set; }

        protected override void InitVM()
        {
            AllGroups = DC.Set<pop_group>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.Name);
        }

    }
}
