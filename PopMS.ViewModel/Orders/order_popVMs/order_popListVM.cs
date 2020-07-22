using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using PopMS.Model;


namespace PopMS.ViewModel.Orders.order_popVMs
{
    public partial class order_popListVM : BasePagedListVM<order_pop_View, order_popSearcher>
    {
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeStandardAction("order_pop", GridActionStandardTypesEnum.Create, Localizer["Create"],"Orders", dialogWidth: 800),
                //this.MakeStandardAction("order_pop", GridActionStandardTypesEnum.Edit, Localizer["Edit"],"Orders", dialogWidth: 800),
                this.MakeStandardAction("order_pop", GridActionStandardTypesEnum.Delete, Localizer["Delete"], "Orders",dialogWidth: 800),
                //this.MakeStandardAction("order_pop", GridActionStandardTypesEnum.Details, Localizer["Details"],"Orders", dialogWidth: 800),
                this.MakeStandardAction("order_pop", GridActionStandardTypesEnum.BatchEdit, Localizer["BatchEdit"],"Orders", dialogWidth: 800),
                this.MakeStandardAction("order_pop", GridActionStandardTypesEnum.BatchDelete, Localizer["BatchDelete"],"Orders", dialogWidth: 800),
                this.MakeStandardAction("order_pop", GridActionStandardTypesEnum.Import, Localizer["Import"],"Orders", dialogWidth: 800),
                this.MakeStandardAction("order_pop", GridActionStandardTypesEnum.ExportExcel, Localizer["Export"],"Orders"),
                this.MakeAction("order_pop","RecPop","收货","收货",GridActionParameterTypesEnum.SingleId,"Orders",dialogWidth:800).SetShowInRow(true).SetHideOnToolBar(false)
            };
        }

        protected override IEnumerable<IGridColumn<order_pop_View>> InitGridHeader()
        {
            return new List<GridColumn<order_pop_View>>{
                this.MakeGridHeader(x => x.ContractName),
                this.MakeGridHeader(x => x.PopName),
                 this.MakeGridHeader(x => x.CreateBy),
                this.MakeGridHeader(x => x.CreateTime),
                this.MakeGridHeader(x => x.UnitCost),
                this.MakeGridHeader(x => x.OrderQty),
                this.MakeGridHeader(x => x.TotalCost),
                this.MakeGridHeader(x => x.RecQty),
                this.MakeGridHeader(x => x.RecCost),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<order_pop_View> GetSearchQuery()
        {
            var query = DC.Set<order_pop>()
                .Select(x => new order_pop_View
                {
				    ID = x.ID,
                    PopName = x.ContractPop.Pop.PopName,
                    ContractName=x.ContractPop.Contract.Name,
                    UnitCost=x.ContractPop.Price,
                    OrderQty = x.OrderQty,
                    RecQty = x.RecQty,
                    RecUser = x.RecUser,
                    RecTime = x.RecTime,
                    CreateBy=x.CreateBy,
                    CreateTime=x.CreateTime
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class order_pop_View : order_pop{
        [Display(Name = "物料")]
        public String PopName { get; set; }
        [Display(Name = "合同")]
        public String ContractName { get; set; }
        [Display(Name = "单价")]
        public double UnitCost { get; set; }
        [Display(Name = "总金额")]
        public double TotalCost {
            get
            {
                return UnitCost * OrderQty;
            }
        }
        [Display(Name = "总金额")]
        public double RecCost
        {
            get
            {
                return UnitCost * RecQty;
            }
        }

    }
}
