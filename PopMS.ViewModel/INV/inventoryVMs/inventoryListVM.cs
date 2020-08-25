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
                //this.MakeStandardAction("inventory", GridActionStandardTypesEnum.Create, Localizer["Create"],"INV", dialogWidth: 800),
                //this.MakeStandardAction("inventory", GridActionStandardTypesEnum.Edit, Localizer["Edit"],"INV", dialogWidth: 800),
                //this.MakeStandardAction("inventory", GridActionStandardTypesEnum.Delete, Localizer["Delete"], "INV",dialogWidth: 800),
                //this.MakeStandardAction("inventory", GridActionStandardTypesEnum.Details, Localizer["Details"],"INV", dialogWidth: 800),
                //this.MakeStandardAction("inventory", GridActionStandardTypesEnum.BatchEdit, Localizer["BatchEdit"],"INV", dialogWidth: 800),
                this.MakeStandardAction("inventory", GridActionStandardTypesEnum.BatchDelete, Localizer["BatchDelete"],"INV", dialogWidth: 800),
                //this.MakeStandardAction("inventory", GridActionStandardTypesEnum.Import, Localizer["Import"],"INV", dialogWidth: 800),
                this.MakeStandardAction("inventory", GridActionStandardTypesEnum.ExportExcel, Localizer["Export"],"INV"),
                this.MakeAction("inv_record","Create","修改","库存操作",GridActionParameterTypesEnum.SingleId,"INV",dialogWidth:800).SetShowInRow(true).SetHideOnToolBar(true),
                this.MakeAction("inv_record","BatchEdit","批量转移","库存批量转移",GridActionParameterTypesEnum.MultiIds,"INV",dialogWidth:800).SetShowInRow(false).SetHideOnToolBar(false),
            };
        }

        protected override IEnumerable<IGridColumn<inventory_View>> InitGridHeader()
        {
            return new List<GridColumn<inventory_View>>{
                this.MakeGridHeader(x => x.DCName_VIew),
                this.MakeGridHeader(x => x.Location_view).SetSort(true),
                this.MakeGridHeader(x => x.PopName).SetSort(true),
                this.MakeGridHeader(x => x.Pack),
                this.MakeGridHeader(x => x.Cnt),
                this.MakeGridHeader(x => x.Price),
                this.MakeGridHeader(x => x.Stock).SetSort(true).SetShowTotal(true),
                //this.MakeGridHeader(x => x.TotalPrice),    
                this.MakeGridHeader(x => x.EnableQty).SetShowTotal(true),
                //this.MakeGridHeader(x => x.EnablePrice),
                this.MakeGridHeader(x => x.PutUser),
                this.MakeGridHeader(x => x.PutTime).SetSort(true),
                this.MakeGridHeader(x => x.OutDate).SetSort(true),
                this.MakeGridHeaderAction(width: 100)
            };
        }

        public override IOrderedQueryable<inventory_View> GetSearchQuery()
        {
            var query = DC.Set<inventoryIn>()
                .Include("Inv.Location.Area")
                .DPWhere(LoginUserInfo?.DataPrivileges,x=>x.Inv.Location.Area.DCID)
                .CheckEqual(Searcher.DCID, x => x.Inv.Location.Area.DCID)
                .CheckEqual(Searcher.LocationID, x => x.Inv.LocationID)
                .CheckEqual(Searcher.PopID,x=>x.OrderPop.ContractPop.PopID)
                .Select(x => new inventory_View
                {
                    ID = x.Inv.ID,
                    DCName_VIew=x.Inv.Location.Area.DC.Name,
                    Location_view = x.Inv.Location.Location,
                    PopName = x.OrderPop.ContractPop.Pop.PopName,
                    OutDate = x.Inv.InvOut.Max(r => r.CreateTime),
                    Stock = x.Inv.Stock,
                    UsedQty = x.Inv.InvOut.Sum(r=>r.OutQty),
                    PutUser = x.Inv.PutUser,
                    PutTime = x.Inv.PutTime,
                    Price=x.OrderPop.Price,
                    Pack = x.OrderPop.ContractPop.UnitPack,
                    Cnt = x.OrderPop.ContractPop.Cnt
                })
                .OrderBy(x => x.PopName);
            return query;
        }

    }

    public class inventory_View : inventory
    {
        [Display(Name ="仓库")]
        public string DCName_VIew { get; set; }
        [Display(Name = "货位")]
        public String Location_view { get; set; }
        [Display(Name = "批次")]
        public String Lot_view { get; set; }
        [Display(Name = "最后一次出库日期")]
        public DateTime? OutDate { get; set; }
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
        public double TotalPrice { get; set; }
        [Display(Name = "使用资产")]
        public double UsedPrice { get; set; }
        [Display(Name = "可用资产")]
        public double EnablePrice {
            get
            {
                return TotalPrice - UsedPrice;
            }
        }
        [Display(Name ="最近入库时间")]
        public DateTime? InDate { get; set; }
        [Display(Name ="已订数量")]
        public int OrderQty { get; set; }
        [Display(Name = "在途数量")]
        public int OnHandQty { get; set; }
        [Display(Name ="物料分组")]
        public string PopGroup { get; set; }
    }
}
