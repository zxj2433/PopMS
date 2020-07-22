using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using PopMS.Model;


namespace PopMS.ViewModel.CTT.contractVMs
{
    public partial class contractListVM : BasePagedListVM<contract_View, contractSearcher>
    {
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeStandardAction("contract", GridActionStandardTypesEnum.Create, Localizer["Create"],"CTT", dialogWidth: 800),
                this.MakeStandardAction("contract", GridActionStandardTypesEnum.Edit, Localizer["Edit"],"CTT", dialogWidth: 800),
                this.MakeStandardAction("contract", GridActionStandardTypesEnum.Delete, Localizer["Delete"], "CTT",dialogWidth: 800),
                this.MakeStandardAction("contract", GridActionStandardTypesEnum.Details, Localizer["Details"],"CTT", dialogWidth: 800),
                this.MakeStandardAction("contract", GridActionStandardTypesEnum.BatchEdit, Localizer["BatchEdit"],"CTT", dialogWidth: 800),
                this.MakeStandardAction("contract", GridActionStandardTypesEnum.BatchDelete, Localizer["BatchDelete"],"CTT", dialogWidth: 800),
                this.MakeStandardAction("contract", GridActionStandardTypesEnum.Import, Localizer["Import"],"CTT", dialogWidth: 800),
                this.MakeStandardAction("contract", GridActionStandardTypesEnum.ExportExcel, Localizer["Export"],"CTT"),
            };
        }

        protected override IEnumerable<IGridColumn<contract_View>> InitGridHeader()
        {
            return new List<GridColumn<contract_View>>{
                this.MakeGridHeader(x => x.Name_view),
                this.MakeGridHeader(x => x.Name),
                this.MakeGridHeader(x => x.Vendor),
                this.MakeGridHeader(x => x.Remark),
                this.MakeGridHeader(x => x.StartDate),
                this.MakeGridHeader(x => x.EndDate),
                this.MakeGridHeader(x => x.ImportTime),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<contract_View> GetSearchQuery()
        {
            var query = DC.Set<contract>()
                .CheckEqual(Searcher.DCID, x=>x.DCID)
                .CheckContain(Searcher.Name, x=>x.Name)
                .CheckContain(Searcher.Vendor, x=>x.Vendor)
                .CheckBetween(Searcher.ImportTime?.GetStartTime(), Searcher.ImportTime?.GetEndTime(), x => x.ImportTime, includeMax: false)
                .Select(x => new contract_View
                {
				    ID = x.ID,
                    Name_view = x.DC.Name,
                    Name = x.Name,
                    Vendor = x.Vendor,
                    Remark = x.Remark,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    ImportTime = x.ImportTime,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class contract_View : contract{
        [Display(Name = "仓库名")]
        public String Name_view { get; set; }

    }
}
