﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using PopMS.Model;


namespace PopMS.ViewModel.INV.inventoryVMs
{
    public partial class inventoryVM : BaseCRUDVM<inventory>
    {
        public List<ComboSelectListItem> AllLocations { get; set; }
        public List<ComboSelectListItem> AllOrderPops { get; set; }

        public inventoryVM()
        {
            SetInclude(x => x.Location);
        }

        protected override void InitVM()
        {
            AllLocations = DC.Set<area_location>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.Location);
            AllOrderPops = DC.Set<order_pop>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.Lot);
        }

        public override void DoAdd()
        {           
            base.DoAdd();
        }

        public override void DoEdit(bool updateAllFields = false)
        {
            base.DoEdit(updateAllFields);
        }

        public override void DoDelete()
        {
            base.DoDelete();
        }
    }
}
