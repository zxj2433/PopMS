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
        [Display(Name = "物料名称")]
        public String PopName { get; set; }
        public List<ComboSelectListItem> PopAllGroups { get; set; }
        [Display(Name = "物料类型")]
        public Guid? PopGroup { get; set; }

        protected override void InitVM()
        {
            PopAllGroups = DC.Set<pop_group>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, x => x.Name);
        }

    }
}
