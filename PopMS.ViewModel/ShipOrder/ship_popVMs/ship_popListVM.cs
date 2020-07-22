using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using PopMS.Model;


namespace PopMS.ViewModel.ShipOrder.ship_popVMs
{
    public partial class ship_popListVM : BasePagedListVM<ship_pop_View, ship_popSearcher>
    {
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeStandardAction("ship_pop", GridActionStandardTypesEnum.Create, Localizer["Create"],"ShipOrder", dialogWidth: 800),
                this.MakeStandardAction("ship_pop", GridActionStandardTypesEnum.Edit, Localizer["Edit"],"ShipOrder", dialogWidth: 800),
                this.MakeStandardAction("ship_pop", GridActionStandardTypesEnum.Delete, Localizer["Delete"], "ShipOrder",dialogWidth: 800),
                this.MakeStandardAction("ship_pop", GridActionStandardTypesEnum.Details, Localizer["Details"],"ShipOrder", dialogWidth: 800),
                this.MakeStandardAction("ship_pop", GridActionStandardTypesEnum.BatchEdit, Localizer["BatchEdit"],"ShipOrder", dialogWidth: 800),
                this.MakeStandardAction("ship_pop", GridActionStandardTypesEnum.BatchDelete, Localizer["BatchDelete"],"ShipOrder", dialogWidth: 800),
                this.MakeStandardAction("ship_pop", GridActionStandardTypesEnum.Import, Localizer["Import"],"ShipOrder", dialogWidth: 800),
                this.MakeStandardAction("ship_pop", GridActionStandardTypesEnum.ExportExcel, Localizer["Export"],"ShipOrder"),
                this.MakeAction("ship_pop_sum","Create","生成拣货","生成拣货单",GridActionParameterTypesEnum.MultiIds,"ShipOrder",dialogWidth:800).SetShowInRow(false).SetHideOnToolBar(false)
            };
        }

        protected override IEnumerable<IGridColumn<ship_pop_View>> InitGridHeader()
        {
            return new List<GridColumn<ship_pop_View>>{
                this.MakeGridHeader(x => x.CodeAndName_view),
                this.MakeGridHeader(x => x.PopName_view),
                this.MakeGridHeader(x => x.OrderQty),
                this.MakeGridHeader(x => x.Status),
                this.MakeGridHeader(x => x.ShipUser),
                this.MakeGridHeader(x => x.ShipTime),
                this.MakeGridHeader(x => x.OrderRemark_view),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<ship_pop_View> GetSearchQuery()
        {
            var query = DC.Set<ship_pop>()
                .CheckEqual(Searcher.Status, x => x.Status)
                .CheckEqual(Searcher.Ship_Pop_SumID, x => x.Ship_Pop_SumID)
                .DPWhere(LoginUserInfo?.DataPrivileges,x=>x.Pop.DCID)
                .Select(x => new ship_pop_View
                {
				    ID = x.ID,
                    CodeAndName_view = x.User.CodeAndName,
                    PopName_view = x.Pop.PopName,
                    OrderQty = x.OrderQty,
                    Status = x.Status,
                    ShipUser = x.ShipUser,
                    ShipTime = x.ShipTime
                })
                .OrderBy(x => x.ID);
            var data = query.ToList();
            return query;
        }

    }

    public class ship_pop_View : ship_pop{
        [Display(Name = "申请人")]
        public String CodeAndName_view { get; set; }
        [Display(Name = "物料名称")]
        public String PopName_view { get; set; }
        [Display(Name = "备注")]
        public String OrderRemark_view { get; set; }

    }
}
