﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using PopMS.Model;


namespace PopMS.ViewModel.BASE.popVMs
{
    public partial class popListVM : BasePagedListVM<pop_View, popSearcher>
    {
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeStandardAction("pop", GridActionStandardTypesEnum.Create, Localizer["Create"],"BASE", dialogWidth: 800),
                this.MakeStandardAction("pop", GridActionStandardTypesEnum.Edit, Localizer["Edit"],"BASE", dialogWidth: 800),
                this.MakeStandardAction("pop", GridActionStandardTypesEnum.Delete, Localizer["Delete"], "BASE",dialogWidth: 800),
                this.MakeStandardAction("pop", GridActionStandardTypesEnum.Details, Localizer["Details"],"BASE", dialogWidth: 800),
                this.MakeStandardAction("pop", GridActionStandardTypesEnum.BatchEdit, Localizer["BatchEdit"],"BASE", dialogWidth: 800),
                this.MakeStandardAction("pop", GridActionStandardTypesEnum.BatchDelete, Localizer["BatchDelete"],"BASE", dialogWidth: 800),
                this.MakeStandardAction("pop", GridActionStandardTypesEnum.Import, Localizer["Import"],"BASE", dialogWidth: 800),
                this.MakeStandardAction("pop", GridActionStandardTypesEnum.ExportExcel, Localizer["Export"],"BASE"),
            };
        }

        protected override IEnumerable<IGridColumn<pop_View>> InitGridHeader()
        {
            return new List<GridColumn<pop_View>>{
                this.MakeGridHeader(x => x.PopNo),
                this.MakeGridHeader(x => x.PopName),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<pop_View> GetSearchQuery()
        {
            var query = DC.Set<pop>()
                .CheckContain(Searcher.PopName, x=>x.PopName)
                .Select(x => new pop_View
                {
				    ID = x.ID,
                    PopNo = x.PopNo,
                    PopName = x.PopName,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class pop_View : pop{

    }
}
