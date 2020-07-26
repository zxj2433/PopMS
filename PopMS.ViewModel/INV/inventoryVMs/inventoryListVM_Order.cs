using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using PopMS.Model;


namespace PopMS.ViewModel.INV.inventoryVMs
{
    public partial class inventoryListVM_Order : BasePagedListVM<inventory_View, inventorySearcher>
    {
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                //this.MakeStandardAction("inventory", GridActionStandardTypesEnum.Create, Localizer["Create"],"INV", dialogWidth: 800),
                //this.MakeStandardAction("inventory", GridActionStandardTypesEnum.Edit, Localizer["Edit"],"INV", dialogWidth: 800),
                //this.MakeStandardAction("inventory", GridActionStandardTypesEnum.Delete, Localizer["Delete"], "INV",dialogWidth: 800),
                //this.MakeStandardAction("inventory", GridActionStandardTypesEnum.Details, Localizer["Details"],"INV", dialogWidth: 800),
                //this.MakeStandardAction("inventory", GridActionStandardTypesEnum.BatchEdit, Localizer["BatchEdit"],"INV", dialogWidth: 800),
                //this.MakeStandardAction("inventory", GridActionStandardTypesEnum.BatchDelete, Localizer["BatchDelete"],"INV", dialogWidth: 800),
                //this.MakeStandardAction("inventory", GridActionStandardTypesEnum.Import, Localizer["Import"],"INV", dialogWidth: 800),
                this.MakeAction("ship_pop","Create","订货","订货",GridActionParameterTypesEnum.SingleId,"ShipOrder",dialogWidth:800).SetShowInRow(true).SetHideOnToolBar(true)
            };
        }

        protected override IEnumerable<IGridColumn<inventory_View>> InitGridHeader()
        {
            return new List<GridColumn<inventory_View>>{
                this.MakeGridHeader(x => x.PopName),
                this.MakeGridHeader(x => x.Pack),
                this.MakeGridHeader(x => x.Cnt),
                this.MakeGridHeader(x => x.UsedQty),
                this.MakeGridHeader(x => x.EnableQty),
                this.MakeGridHeader(x => x.OrderQty),
                this.MakeGridHeaderAction(width: 100)
            };
        }

        public override IOrderedQueryable<inventory_View> GetSearchQuery()
        {
            var query = DC.Set<order_pop>()
                .Include("ContractPop.Pop")
                .CheckEqual(Searcher.GroupID, x => x.ContractPop.Pop.GroupID)
                .GroupBy(x=> new { x.ContractPop.PopID, x.ContractPop.Pop.PopNo, x.ContractPop.Pop.PopName, x.ContractPop.UnitPack, x.ContractPop.Cnt })
                .Select(x => new inventory_View
                {
                    ID = x.Key.PopID,
                    PopName = x.Key.PopName,
                    Stock = x.Sum(r=>r.RecQty),
                    UsedQty =DC.Set<ship_pop>().Where(r=>r.PopID==x.Key.PopID).Sum(r=>r.AlcQty),
                    OrderQty= DC.Set<ship_pop>().Where(r => r.PopID == x.Key.PopID)
                    .Where(r=>r.User.DeptID==DC.Set<user>()
                    .Where(r=>r.ID==LoginUserInfo.Id).FirstOrDefault().DeptID)
                    .Where(r=>r.Status==ShipStatus.NEW)
                    .Sum(r=>r.OrderQty.Value),
                    Pack = x.Key.UnitPack,
                    Cnt = x.Key.Cnt
                })
                .OrderBy(x => x.PopName);
            return query;
        }
    }
}
