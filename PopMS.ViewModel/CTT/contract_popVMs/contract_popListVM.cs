using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using PopMS.Model;


namespace PopMS.ViewModel.CTT.contract_popVMs
{
    public partial class contract_popListVM : BasePagedListVM<contract_pop_View, contract_popSearcher>
    {
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeStandardAction("contract_pop", GridActionStandardTypesEnum.Create, Localizer["Create"],"CTT", dialogWidth: 800),
                this.MakeStandardAction("contract_pop", GridActionStandardTypesEnum.Edit, Localizer["Edit"],"CTT", dialogWidth: 800),
                this.MakeStandardAction("contract_pop", GridActionStandardTypesEnum.Delete, Localizer["Delete"], "CTT",dialogWidth: 800),
                //this.MakeStandardAction("contract_pop", GridActionStandardTypesEnum.Details, Localizer["Details"],"CTT", dialogWidth: 800),
                this.MakeStandardAction("contract_pop", GridActionStandardTypesEnum.BatchEdit, Localizer["BatchEdit"],"CTT", dialogWidth: 800),
                this.MakeStandardAction("contract_pop", GridActionStandardTypesEnum.BatchDelete, Localizer["BatchDelete"],"CTT", dialogWidth: 800),
                this.MakeStandardAction("contract_pop", GridActionStandardTypesEnum.Import, Localizer["Import"],"CTT", dialogWidth: 800),
                this.MakeStandardAction("contract_pop", GridActionStandardTypesEnum.ExportExcel, Localizer["Export"],"CTT"),
                this.MakeAction("order_pop","Create","订货","订货",GridActionParameterTypesEnum.SingleId,"Orders",dialogWidth:800).SetShowInRow(true).SetHideOnToolBar(true)
            };
        }

        protected override IEnumerable<IGridColumn<contract_pop_View>> InitGridHeader()
        {
            return new List<GridColumn<contract_pop_View>>{
                this.MakeGridHeader(x=>x.DCName),
                this.MakeGridHeader(x => x.Name_view),
                this.MakeGridHeader(x => x.PopName_view),
                this.MakeGridHeader(x => x.UnitPack),
                this.MakeGridHeader(x => x.Cnt),
                this.MakeGridHeader(x => x.Price),
                this.MakeGridHeader(x => x.OrderedQty),
                this.MakeGridHeader(x => x.ShippedQty),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<contract_pop_View> GetSearchQuery()
        {
            var query = DC.Set<contract_pop>()
                .Include("Contract")
                .DPWhere(LoginUserInfo?.DataPrivileges,x=>x.Contract.DCID)
                .CheckEqual(Searcher.PopID, x=>x.PopID)
                .CheckEqual(Searcher.ContractID, x=>x.ContractID)
                .Select(x => new contract_pop_View
                {
				    ID = x.ID,
                    DCName=x.Contract.DC.Name,
                    PopName_view = x.Pop.PopName,
                    UnitPack = x.UnitPack,
                    Cnt = x.Cnt,
                    Price=x.Price,
                    Name_view = x.Contract.Name,
                    OrderedQty=DC.Set<order_pop>().Where(r=>r.ContractPopID==x.ID).Sum(r=>r.OrderQty),
                    ShippedQty=DC.Set<inventoryOut>().Where(r=>r.Inv.InvIn.First().OrderPop.ContractPopID==x.ID).Sum(r=>r.sp.AlcQty)
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class contract_pop_View : contract_pop{
        [Display(Name = "物料名称")]
        public String PopName_view { get; set; }
        [Display(Name = "合同名")]
        public String Name_view { get; set; }
        [Display(Name = "订货总数量")]
        public int OrderedQty { get; set; }
        [Display(Name = "已发放数量")]
        public int ShippedQty { get; set; }
        [Display(Name ="仓库")]
        public string DCName { get; set; }
    }
}
