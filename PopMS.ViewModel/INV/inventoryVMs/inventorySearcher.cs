﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using PopMS.Model;


namespace PopMS.ViewModel.INV.inventoryVMs
{
    public partial class inventorySearcher : BaseSearcher
    {
        public List<ComboSelectListItem> AllLocations { get; set; }
        [Display(Name ="货位")]
        public Guid? LocationID { get; set; }
        public List<ComboSelectListItem> AllPops { get; set; }
        [Display(Name = "物料")]
        public Guid? PopID { get; set; }
        [Display(Name ="日期")]
        public DateRange InvDate { get; set; }
        public List<ComboSelectListItem> AllGroups { get; set; }

        [Display(Name = "物料类型")]
        public Guid? GroupID { get; set; }
        public List<ComboSelectListItem> AllDCs { get; set; }

        [Display(Name = "仓库")]
        public Guid? DCID { get; set; }

        protected override void InitVM()
        {
            AllLocations = DC.Set<area_location>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.Location);
            AllPops = DC.Set<pop>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.PopNoName);
            AllGroups=DC.Set<pop_group>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.Name);
            AllDCs = DC.Set<dc>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, x => x.Name);
        }

    }
}
