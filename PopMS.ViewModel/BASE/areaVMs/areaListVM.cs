using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using PopMS.Model;


namespace PopMS.ViewModel.BASE.areaVMs
{
    public partial class areaListVM : BasePagedListVM<area_View, areaSearcher>
    {
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeStandardAction("area", GridActionStandardTypesEnum.Create, Localizer["Create"],"BASE", dialogWidth: 800),
                this.MakeStandardAction("area", GridActionStandardTypesEnum.Edit, Localizer["Edit"],"BASE", dialogWidth: 800),
                this.MakeStandardAction("area", GridActionStandardTypesEnum.Delete, Localizer["Delete"], "BASE",dialogWidth: 800),
                this.MakeStandardAction("area", GridActionStandardTypesEnum.Details, Localizer["Details"],"BASE", dialogWidth: 800),
                this.MakeStandardAction("area", GridActionStandardTypesEnum.BatchEdit, Localizer["BatchEdit"],"BASE", dialogWidth: 800),
                this.MakeStandardAction("area", GridActionStandardTypesEnum.BatchDelete, Localizer["BatchDelete"],"BASE", dialogWidth: 800),
                this.MakeStandardAction("area", GridActionStandardTypesEnum.Import, Localizer["Import"],"BASE", dialogWidth: 800),
                this.MakeStandardAction("area", GridActionStandardTypesEnum.ExportExcel, Localizer["Export"],"BASE"),
            };
        }

        protected override IEnumerable<IGridColumn<area_View>> InitGridHeader()
        {
            return new List<GridColumn<area_View>>{
                this.MakeGridHeader(x => x.Area),
                this.MakeGridHeader(x => x.AreaRemark),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<area_View> GetSearchQuery()
        {
            var query = DC.Set<area>()
                .DPWhere(LoginUserInfo?.DataPrivileges,x=>x.DCID)
                .CheckContain(Searcher.Area, x=>x.Area)
                .Select(x => new area_View
                {
				    ID = x.ID,
                    Area = x.Area,
                    AreaRemark = x.AreaRemark,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class area_View : area{

    }
}
