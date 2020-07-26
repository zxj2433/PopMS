using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using PopMS.Model;


namespace PopMS.ViewModel.BASE.pop_groupVMs
{
    public partial class pop_groupListVM : BasePagedListVM<pop_group_View, pop_groupSearcher>
    {
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeStandardAction("pop_group", GridActionStandardTypesEnum.Create, Localizer["Create"],"BASE", dialogWidth: 800),
                this.MakeStandardAction("pop_group", GridActionStandardTypesEnum.Edit, Localizer["Edit"],"BASE", dialogWidth: 800),
                this.MakeStandardAction("pop_group", GridActionStandardTypesEnum.Delete, Localizer["Delete"], "BASE",dialogWidth: 800),
                this.MakeStandardAction("pop_group", GridActionStandardTypesEnum.Details, Localizer["Details"],"BASE", dialogWidth: 800),
                this.MakeStandardAction("pop_group", GridActionStandardTypesEnum.BatchEdit, Localizer["BatchEdit"],"BASE", dialogWidth: 800),
                this.MakeStandardAction("pop_group", GridActionStandardTypesEnum.BatchDelete, Localizer["BatchDelete"],"BASE", dialogWidth: 800),
                this.MakeStandardAction("pop_group", GridActionStandardTypesEnum.Import, Localizer["Import"],"BASE", dialogWidth: 800),
                this.MakeStandardAction("pop_group", GridActionStandardTypesEnum.ExportExcel, Localizer["Export"],"BASE"),
            };
        }

        protected override IEnumerable<IGridColumn<pop_group_View>> InitGridHeader()
        {
            return new List<GridColumn<pop_group_View>>{
                this.MakeGridHeader(x => x.Name_view),
                this.MakeGridHeader(x => x.Name),
                this.MakeGridHeader(x => x.Index),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<pop_group_View> GetSearchQuery()
        {
            var query = DC.Set<pop_group>()
                .CheckEqual(Searcher.DCID, x=>x.DCID)
                .Select(x => new pop_group_View
                {
				    ID = x.ID,
                    Name_view = x.DC.Name,
                    Name = x.Name,
                    Index = x.Index,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class pop_group_View : pop_group{
        [Display(Name = "仓库名")]
        public String Name_view { get; set; }

    }
}
