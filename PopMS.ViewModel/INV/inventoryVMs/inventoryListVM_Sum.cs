using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using PopMS.Model;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Internal;
using PopMS.ViewModel.ShipOrder.ship_popVMs;

namespace PopMS.ViewModel.INV.inventoryVMs
{
    public partial class inventoryListVM_Sum : BasePagedListVM<inventory_View, inventorySearcher>
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
                this.MakeStandardAction("inventory", GridActionStandardTypesEnum.ExportExcel, Localizer["Export"],"INV"),
                this.MakeAction("inv_record","Create","修改","库存操作",GridActionParameterTypesEnum.SingleId,"INV",dialogWidth:800).SetShowInRow(true).SetHideOnToolBar(true),
                this.MakeAction("inv_record","BatchEdit","批量转移","库存批量转移",GridActionParameterTypesEnum.MultiIds,"INV",dialogWidth:800).SetShowInRow(false).SetHideOnToolBar(false),
            };
        }

        protected override IEnumerable<IGridColumn<inventory_View>> InitGridHeader()
        {
            return new List<GridColumn<inventory_View>>{
                this.MakeGridHeader(x => x.PopName).SetSort(true),
                this.MakeGridHeader(x => x.OrderQty),
                this.MakeGridHeader(x => x.UsedQty),
                this.MakeGridHeader(x => x.OnHandQty),
                this.MakeGridHeader(x => x.EnableQty),
                this.MakeGridHeader(x => x.InDate).SetSort(true),
                this.MakeGridHeader(x => x.OutDate).SetSort(true),
                this.MakeGridHeaderAction(width: 200).SetHide(true)
            };
        }

        public override IOrderedQueryable<inventory_View> GetSearchQuery()
        {
            var Query = from x in DC.Set<pop>().AsNoTracking()
                        .Include("Group")
                        .CheckEqual(Searcher.PopID, x => x.ID)
                        join I in DC.Set<order_pop>().AsNoTracking()
                        .Include("ContractPop").Include("ContractPop.Contract")
                        .DPWhere(LoginUserInfo?.DataPrivileges, x => x.ContractPop.Contract.DCID)
                        .CheckBetween(Searcher.InvDate?.GetStartTime(), Searcher.InvDate?.GetEndTime(), x => x.CreateTime)
                        .CheckEqual(Searcher.PopID, x => x.ContractPop.PopID)
                        .CheckEqual(Searcher.DCID, x => x.ContractPop.Contract.DCID)
                        .GroupBy(x => x.ContractPop.PopID)
                        .Select(x => new order_pop
                        {
                            ContractPopID = x.Key,
                            OrderQty = x.Sum(x => x.OrderQty),
                            RecQty = x.Sum(x => x.RecQty),
                            RecTime = x.Max(x => x.RecTime)
                        })
                        on x.ID equals I.ContractPopID into INs
                        from IN in INs.DefaultIfEmpty()
                        join O in DC.Set<ship_pop>().AsNoTracking()
                        .DPWhere(LoginUserInfo?.DataPrivileges, x => x.User.DCID)
                        .CheckBetween(Searcher.InvDate?.GetStartTime(), Searcher.InvDate?.GetEndTime(), x => x.CreateTime)
                        .CheckEqual(Searcher.PopID, x => x.PopID)
                        .CheckEqual(Searcher.DCID,x=>x.User.DCID)
                        .GroupBy(x =>x.PopID)
                        .Select(x => new ship_pop_View
                        {
                            PopID = x.Key,
                            AlcQty = x.Sum(x => x.AlcQty),
                            CreateTime = x.Max(x => x.CreateTime)
                        })
                        on x.ID equals O.PopID  into OUTs
                        from OUT in OUTs.DefaultIfEmpty()
                        select new inventory_View
                        {
                            ID = x.ID,
                            PopGroup = x.Group.Name,
                            PopName = x.PopName,
                            InDate = IN.RecTime,
                            OutDate = OUT.CreateTime,
                            OrderQty = IN.OrderQty,
                            OnHandQty=IN.OrderQty-IN.RecQty,
                            Stock=IN.RecQty,
                            UsedQty = OUT.AlcQty
                        };
            return Query.AsQueryable().OrderBy(r=>r.PopName);
        }

    }
}
