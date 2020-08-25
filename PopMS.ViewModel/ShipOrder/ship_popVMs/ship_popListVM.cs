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
                //this.MakeStandardAction("ship_pop", GridActionStandardTypesEnum.Create, Localizer["Create"],"ShipOrder", dialogWidth: 800),
                this.MakeStandardAction("ship_pop", GridActionStandardTypesEnum.Edit, Localizer["Edit"],"ShipOrder", dialogWidth: 800).SetBindVisiableColName("isNew"),
                this.MakeStandardAction("ship_pop", GridActionStandardTypesEnum.Delete, Localizer["Delete"], "ShipOrder",dialogWidth: 800).SetBindVisiableColName("isNew"),
                //this.MakeStandardAction("ship_pop", GridActionStandardTypesEnum.Details, Localizer["Details"],"ShipOrder", dialogWidth: 800),
                //this.MakeStandardAction("ship_pop", GridActionStandardTypesEnum.BatchEdit, Localizer["BatchEdit"],"ShipOrder", dialogWidth: 800),
                //this.MakeStandardAction("ship_pop", GridActionStandardTypesEnum.BatchDelete, Localizer["BatchDelete"],"ShipOrder", dialogWidth: 800),
                //this.MakeStandardAction("ship_pop", GridActionStandardTypesEnum.Import, Localizer["Import"],"ShipOrder", dialogWidth: 800),
                //this.MakeStandardAction("ship_pop", GridActionStandardTypesEnum.ExportExcel, Localizer["Export"],"ShipOrder"),
                this.MakeAction("inventoryOrder","Index","申请物料","物料领用",GridActionParameterTypesEnum.NoId,"INV",dialogWidth:900,dialogHeight:600).SetShowInRow(false).SetHideOnToolBar(false),
                this.MakeAction("ship_pop_sum","Create","生成拣货","生成拣货单",GridActionParameterTypesEnum.MultiIds,"ShipOrder",dialogWidth:800).SetShowInRow(false).SetHideOnToolBar(false),
                this.MakeAction("ship_pop","ShipPops","发放物料","更新发放状态",GridActionParameterTypesEnum.MultiIds,"ShipOrder",dialogWidth:800).SetShowInRow(false).SetHideOnToolBar(false)
            };
        }

        protected override IEnumerable<IGridColumn<ship_pop_View>> InitGridHeader()
        {
            return new List<GridColumn<ship_pop_View>>{
                this.MakeGridHeader(x => x.CreateTime),
                this.MakeGridHeader(x=>x.DCName),
                this.MakeGridHeader(x=>x.DeptName),
                this.MakeGridHeader(x => x.CodeAndName_view),
                this.MakeGridHeader(x => x.PopName_view).SetSort(true),
                this.MakeGridHeader(x => x.OrderQty),
                this.MakeGridHeader(x => x.Status),
                this.MakeGridHeader(x=>x.isNew).SetHide(true),
                this.MakeGridHeader(x => x.ShipUser),
                this.MakeGridHeader(x => x.ShipTime).SetSort(true),
                this.MakeGridHeader(x => x.OrderRemark_view),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<ship_pop_View> GetSearchQuery()
        {
            var query = DC.Set<ship_pop>()
                .Include("Pop.Group")
                .CheckBetween(Searcher.ShipDate?.GetStartTime(),Searcher.ShipDate?.GetEndTime(),x=>x.CreateTime)
                .CheckEqual(Searcher.Status, x => x.Status)
                .CheckEqual(Searcher.Ship_Pop_SumID, x => x.Ship_Pop_SumID)
                .DPWhere(LoginUserInfo?.DataPrivileges,x=>x.User.DCID)
                .CheckEqual(Searcher.DCID,x=>x.User.DCID)
                .CheckEqual(Searcher.PopID,x=>x.PopID)
                .Select(x => new ship_pop_View
                {
				    ID = x.ID,
                    DCName=x.User.DC.Name,
                    CodeAndName_view = x.User.CodeAndName,
                    PopName_view = x.Pop.PopName,
                    OrderQty = x.OrderQty,
                    Status = x.Status,
                    ShipUser = x.ShipUser,
                    ShipTime = x.ShipTime,
                    CreateTime=x.CreateTime,
                    DeptName=x.User.Dept.DeptName,
                    OrderRemark_view =x.Ship_Pop_Sum.OrderRemark
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

        public string isNew
        {
            get
            {
                return Status == ShipStatus.NEW ? "true" : "false";
            }
        }
        [Display(Name ="部门")]
        public string DeptName { get; set; }
        public Guid? DCID { get; set; }
        [Display(Name ="仓库")]
        public string DCName { get; set; }
    }
}
