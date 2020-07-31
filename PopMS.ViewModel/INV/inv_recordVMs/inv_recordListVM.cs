using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using PopMS.Model;


namespace PopMS.ViewModel.INV.inv_recordVMs
{
    public partial class inv_recordListVM : BasePagedListVM<inv_record_View, inv_recordSearcher>
    {
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                //this.MakeStandardAction("inv_record", GridActionStandardTypesEnum.Create, Localizer["Create"],"INV", dialogWidth: 800),
                this.MakeStandardAction("inv_record", GridActionStandardTypesEnum.Delete, Localizer["Delete"], "INV",dialogWidth: 800),
                this.MakeStandardAction("inv_record", GridActionStandardTypesEnum.Details, Localizer["Details"],"INV", dialogWidth: 800),
                this.MakeStandardAction("inv_record", GridActionStandardTypesEnum.BatchDelete, Localizer["BatchDelete"],"INV", dialogWidth: 800),
                this.MakeStandardAction("inv_record", GridActionStandardTypesEnum.Import, Localizer["Import"],"INV", dialogWidth: 800),
                this.MakeStandardAction("inv_record", GridActionStandardTypesEnum.ExportExcel, Localizer["Export"],"INV"),
            };
        }

        protected override IEnumerable<IGridColumn<inv_record_View>> InitGridHeader()
        {
            return new List<GridColumn<inv_record_View>>{
                this.MakeGridHeader(x => x.DCName),
                this.MakeGridHeader(x => x.Type),
                this.MakeGridHeader(x => x.PopName),
                this.MakeGridHeader(x => x.FromLocation),
                this.MakeGridHeader(x => x.Location_view),
                this.MakeGridHeader(x => x.Qty),
                this.MakeGridHeader(x => x.UserName),
                this.MakeGridHeader(x => x.Time),
                this.MakeGridHeaderAction(width: 150)
            };
        }

        public override IOrderedQueryable<inv_record_View> GetSearchQuery()
        {
            var query = DC.Set<inv_record>()
                .Include("FromLoc.Area")
                .DPWhere(LoginUserInfo?.DataPrivileges,x=>x.FromLoc.Area.DCID)
                .CheckEqual(Searcher.Type, x=>x.Type)
                .CheckContain(Searcher.UserName, x=>x.UserName)
                .CheckBetween(Searcher.UpdateTime?.GetStartTime(), Searcher.UpdateTime?.GetEndTime(), x => x.UpdateTime, includeMax: false)
                .Select(x => new inv_record_View
                {
				    ID = x.ID,
                    DCName=x.FromLoc.Area.DC.Name,
                    PopName =x.Inv.InvIn.First().OrderPop.ContractPop.Pop.PopName,
                    FromLocation = x.FromLoc.Location,
                    Type = x.Type,
                    Location_view = x.ToLoc.Location,
                    Qty = x.Qty,
                    UserName = x.UserName,
                    Time = x.UpdateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class inv_record_View : inv_record{
        [Display(Name = "从货位")]
        public String FromLocation { get; set; }
        [Display(Name = "至货位")]
        public String Location_view { get; set; }
        [Display(Name = "物料")]
        public string PopName { get; set; }
        [Display(Name ="操作时间")]
        public string Time { get; set; }
        [Display(Name ="仓库")]
        public string DCName { get; set; }

    }
}
