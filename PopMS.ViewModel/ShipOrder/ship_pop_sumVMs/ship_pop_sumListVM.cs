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

namespace PopMS.ViewModel.ShipOrder.ship_pop_sumVMs
{
    public partial class ship_pop_sumListVM : BasePagedListVM<ship_pop_sum_View, ship_pop_sumSearcher>
    {
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeStandardAction("ship_pop_sum", GridActionStandardTypesEnum.Create, Localizer["Create"],"ShipOrder", dialogWidth: 800),
                this.MakeStandardAction("ship_pop_sum", GridActionStandardTypesEnum.Edit, Localizer["Edit"],"ShipOrder", dialogWidth: 800),
                this.MakeStandardAction("ship_pop_sum", GridActionStandardTypesEnum.Delete, Localizer["Delete"], "ShipOrder",dialogWidth: 800),
                this.MakeStandardAction("ship_pop_sum", GridActionStandardTypesEnum.Details, Localizer["Details"],"ShipOrder", dialogWidth: 800),
                this.MakeStandardAction("ship_pop_sum", GridActionStandardTypesEnum.BatchEdit, Localizer["BatchEdit"],"ShipOrder", dialogWidth: 800),
                this.MakeStandardAction("ship_pop_sum", GridActionStandardTypesEnum.BatchDelete, Localizer["BatchDelete"],"ShipOrder", dialogWidth: 800),
                this.MakeStandardAction("ship_pop_sum", GridActionStandardTypesEnum.Import, Localizer["Import"],"ShipOrder", dialogWidth: 800),
                this.MakeStandardAction("ship_pop_sum", GridActionStandardTypesEnum.ExportExcel, Localizer["Export"],"ShipOrder"),
            };
        }

        protected override IEnumerable<IGridColumn<ship_pop_sum_View>> InitGridHeader()
        {
            return new List<GridColumn<ship_pop_sum_View>>{
                this.MakeGridHeader(x => x.OrderDate),
                this.MakeGridHeader(x => x.OrderRemark),
                 this.MakeGridHeader(x => x.PopName),
                this.MakeGridHeader(x => x.Location),
                 this.MakeGridHeader(x => x.PickQty),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<ship_pop_sum_View> GetSearchQuery()
        {
            if(FC.ContainsKey("Ids[]"))
            {
                var query = DC.Set<ship_pop_sum>()
                         .Select(x => new ship_pop_sum_View
                         {
                             ID =x.ID,
                             OrderDate = x.OrderDate,
                             OrderRemark = x.OrderRemark,
                         });
                return query.OrderBy(x => x.OrderDate);
            }
            else
            {
                var query = DC.Set<inventoryout>()
                        .CheckBetween(Searcher.OrderDate?.GetStartTime(), Searcher.OrderDate?.GetEndTime(), x => x.sp.Ship_Pop_Sum.OrderDate)
                        .CheckEqual(Searcher.Status, x => x.sp.Status)
                        .GroupBy(x => new {
                            x.sp.Ship_Pop_SumID,
                            x.sp.Ship_Pop_Sum.OrderDate,
                            x.sp.Ship_Pop_Sum.OrderRemark
                        ,
                            x.Inv.Location.Location,
                            x.sp.Pop.PopName
                        })
                        .Select(x => new ship_pop_sum_View
                        {
                            ID = x.Key.Ship_Pop_SumID.Value,
                            OrderDate = x.Key.OrderDate,
                            OrderRemark = x.Key.OrderRemark,
                            PopName = x.Key.PopName,
                            Location = x.Key.Location,
                            PickQty = x.Sum(r => r.OutQty)
                        });
                return query.OrderBy(x => x.Location);
            }            
        }

    }

    public class ship_pop_sum_View : ship_pop_sum{
        [Display(Name ="物料")]
        public string PopName { get; set; }
        [Display(Name = "货位")]
        public string Location { get; set; }
        [Display(Name = "拣货数量")]
        public int PickQty { get; set; }
    }
}
