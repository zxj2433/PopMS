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
    public partial class inventoryListVM : BasePagedListVM<inventory_View, inventorySearcher>
    {
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeStandardAction("inventory", GridActionStandardTypesEnum.Create, Localizer["Create"],"INV", dialogWidth: 800),
                this.MakeStandardAction("inventory", GridActionStandardTypesEnum.Edit, Localizer["Edit"],"INV", dialogWidth: 800),
                this.MakeStandardAction("inventory", GridActionStandardTypesEnum.Delete, Localizer["Delete"], "INV",dialogWidth: 800),
                this.MakeStandardAction("inventory", GridActionStandardTypesEnum.Details, Localizer["Details"],"INV", dialogWidth: 800),
                this.MakeStandardAction("inventory", GridActionStandardTypesEnum.BatchEdit, Localizer["BatchEdit"],"INV", dialogWidth: 800),
                this.MakeStandardAction("inventory", GridActionStandardTypesEnum.BatchDelete, Localizer["BatchDelete"],"INV", dialogWidth: 800),
                this.MakeStandardAction("inventory", GridActionStandardTypesEnum.Import, Localizer["Import"],"INV", dialogWidth: 800),
                this.MakeStandardAction("inventory", GridActionStandardTypesEnum.ExportExcel, Localizer["Export"],"INV"),
            };
        }

        protected override IEnumerable<IGridColumn<inventory_View>> InitGridHeader()
        {
            return new List<GridColumn<inventory_View>>{
                this.MakeGridHeader(x => x.Location_view),
                this.MakeGridHeader(x => x.PopName),
                this.MakeGridHeader(x => x.Pack),
                this.MakeGridHeader(x => x.Cnt),
                this.MakeGridHeader(x => x.Price),
                this.MakeGridHeader(x => x.Stock),
                this.MakeGridHeader(x => x.TotalPrice),    
                this.MakeGridHeader(x => x.EnableQty),
                this.MakeGridHeader(x => x.EnablePrice),
                this.MakeGridHeader(x => x.PutUser),
                this.MakeGridHeader(x => x.PutTime),
                this.MakeGridHeader(x => x.OutDate),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<inventory_View> GetSearchQuery()
        {
            var query = DC.Set<inventoryIn>()
                .CheckEqual(Searcher.LocationID, x => x.Inv.LocationID)
                .CheckEqual(Searcher.PopID,x=>x.OrderPop.ContractPop.PopID)
                .Select(x => new inventory_View
                {
                    ID = x.Inv.ID,
                    Location_view = x.Inv.Location.Location,
                    PopName = x.OrderPop.ContractPop.Pop.PopName,
                    OutDate = x.Inv.InvOut.Max(r => r.CreateTime).Value.ToString("yyyy-MM-dd"),
                    Stock = x.Inv.Stock,
                    UsedQty = x.Inv.UsedQty,
                    PutUser = x.Inv.PutUser,
                    PutTime = x.Inv.PutTime,
                    Pack = x.OrderPop.ContractPop.UnitPack,
                    Cnt = x.OrderPop.ContractPop.Cnt,
                    Price = x.OrderPop.ContractPop.Price,

                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class inventory_View : inventory
    {
        [Display(Name = "货位")]
        public String Location_view { get; set; }
        [Display(Name = "批次")]
        public String Lot_view { get; set; }
        [Display(Name = "最后一次出库日期")]
        public string OutDate { get; set; }
        [Display(Name = "物料")]
        public string PopName { get; set; }
        [Display(Name = "可用数量")]
        public int EnableQty
        {
            get
            {
                return Stock - UsedQty;
            }
        }
        [Display(Name = "单位")]
        public string Pack { get; set; }
        [Display(Name = "规格")]
        public int Cnt { get; set; }
        [Display(Name = "单价")]
        public double Price { get; set; }
        [Display(Name = "总资产")]
        public string TotalPrice
        {
            get
            {
                return (Price * Stock).ToString("0.00");
            }
        }
        [Display(Name = "剩余资产")]
        public string EnablePrice
        {
            get
            {
                return (Price * EnableQty).ToString("0.00");
            }
        }
    }
}
