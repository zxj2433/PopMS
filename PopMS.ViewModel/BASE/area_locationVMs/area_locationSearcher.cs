using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using PopMS.Model;


namespace PopMS.ViewModel.BASE.area_locationVMs
{
    public partial class area_locationSearcher : BaseSearcher
    {
        public List<ComboSelectListItem> AllAreas { get; set; }
        [Display(Name = "区域")]
        public Guid? AreaID { get; set; }
        [Display(Name = "可混放")]
        public Boolean? isMix { get; set; }

        protected override void InitVM()
        {
            AllAreas = DC.Set<area>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.Area);
        }

    }
}
