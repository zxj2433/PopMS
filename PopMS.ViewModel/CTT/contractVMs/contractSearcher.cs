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
    public partial class contractSearcher : BaseSearcher
    {
        public List<ComboSelectListItem> AllDCs { get; set; }
        [Display(Name = "仓库")]
        public Guid? DCID { get; set; }
        [Display(Name = "合同名")]
        public String Name { get; set; }
        [Display(Name = "供应商")]
        public String Vendor { get; set; }
        [Display(Name = "导入日期")]
        public DateRange ImportTime { get; set; }

        protected override void InitVM()
        {
            AllDCs = DC.Set<dc>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.Name);
        }

    }
}
