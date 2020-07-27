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
                this.MakeGridHeader(x => x.PopName),
                this.MakeGridHeader(x => x.OrderQty),
                this.MakeGridHeader(x => x.TotalPrice),
                this.MakeGridHeader(x => x.UsedQty),
                this.MakeGridHeader(x => x.UsedPrice),
                this.MakeGridHeader(x => x.OnHandQty),
                this.MakeGridHeader(x => x.EnableQty),
                this.MakeGridHeader(x => x.InDate),
                this.MakeGridHeader(x => x.OutDate),
                this.MakeGridHeaderAction(width: 200).SetHide(true)
            };
        }

        public override IOrderedQueryable<inventory_View> GetSearchQuery()
        {
            var baseQuery = (from p in DC.Set<pop>().Include("Group")
                             //.DPWhere(LoginUserInfo?.DataPrivileges, x => x.Group.DCID)
                        join In in DC.Set<order_pop>().DPWhere(LoginUserInfo?.DataPrivileges, x => x.ContractPop.Contract.DCID)
                        .CheckBetween(Searcher.Date?.GetStartTime(), Searcher.Date?.GetEndTime(), x => x.CreateTime)
                        on p.ID equals In.ContractPop.PopID into Ins
                        from Insf in Ins.DefaultIfEmpty()
                        join Out in DC.Set<inventoryOut>().DPWhere(LoginUserInfo?.DataPrivileges, x => x.Inv.Location.Area.DCID)
                        .CheckBetween(Searcher.Date?.GetStartTime(), Searcher.Date?.GetEndTime(), x => x.sp.CreateTime)
                        on p.ID equals Out.sp.PopID into Outs
                        from Outsf in Outs.DefaultIfEmpty()
                        select new inventory_View
                        {
                            ID = p.ID,
                            PopName = p.PopName,
                            PutTime = Insf.RecTime,
                            OutDate = Outsf.sp.CreateTime,
                            OrderQty = Insf.OrderQty,
                            OnHandQty=Insf.OrderQty-Insf.RecQty,
                            Stock=Insf.RecQty,
                            UsedQty = Outsf.sp.AlcQty,
                            TotalPrice = Insf.ContractPop.Price * Insf.OrderQty,
                            UsedPrice = Outsf.sp.AlcQty * Outsf.Inv.InvIn.First().OrderPop.ContractPop.Price
                        }).ToList();
            var query = from q in baseQuery
                        group q by new { q.ID, q.PopName } into x
                        select new inventory_View
                        {
                            ID = x.Key.ID,
                            PopName = x.Key.PopName,
                            InDate = x.Max(r => r.PutTime),
                            OutDate = x.Max(r => r.OutDate),
                            OrderQty=x.Sum(r=>r.OrderQty),
                            OnHandQty=x.Sum(r=>r.OnHandQty),
                            Stock = x.Sum(r => r.Stock),
                            UsedQty = x.Sum(r => r.UsedQty),
                            TotalPrice = x.Sum(r => r.TotalPrice),
                            UsedPrice = x.Sum(r => r.UsedPrice)
                        };
            return query.AsQueryable().OrderBy(r=>r.PopName);
        }

    }
}
