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
    public partial class contract_popSearcher : BaseSearcher
    {
        public List<ComboSelectListItem> AllPops { get; set; }
        [Display(Name ="物料")]
        public Guid? PopID { get; set; }
        public List<ComboSelectListItem> AllContracts { get; set; }
        [Display(Name = "合同")]
        public Guid? ContractID { get; set; }

        protected override void InitVM()
        {
            AllPops = DC.Set<pop>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.PopName);
            AllContracts = DC.Set<contract>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.Name);
        }

    }
}
