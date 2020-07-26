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
    public partial class pop_groupSearcher : BaseSearcher
    {
        public List<ComboSelectListItem> AllDCs { get; set; }
        [Display(Name = "仓库")]
        public Guid? DCID { get; set; }

        protected override void InitVM()
        {
            AllDCs = DC.Set<dc>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.Name);
        }

    }
}
