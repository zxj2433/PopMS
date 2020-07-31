using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using PopMS.Model;


namespace PopMS.ViewModel.BASE.area_locationVMs
{
    public partial class area_locationListVM : BasePagedListVM<area_location_View, area_locationSearcher>
    {
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeStandardAction("area_location", GridActionStandardTypesEnum.Create, Localizer["Create"],"BASE", dialogWidth: 800),
                this.MakeStandardAction("area_location", GridActionStandardTypesEnum.Edit, Localizer["Edit"],"BASE", dialogWidth: 800),
                this.MakeStandardAction("area_location", GridActionStandardTypesEnum.Delete, Localizer["Delete"], "BASE",dialogWidth: 800),
                this.MakeStandardAction("area_location", GridActionStandardTypesEnum.Details, Localizer["Details"],"BASE", dialogWidth: 800),
                this.MakeStandardAction("area_location", GridActionStandardTypesEnum.BatchEdit, Localizer["BatchEdit"],"BASE", dialogWidth: 800),
                this.MakeStandardAction("area_location", GridActionStandardTypesEnum.BatchDelete, Localizer["BatchDelete"],"BASE", dialogWidth: 800),
                this.MakeStandardAction("area_location", GridActionStandardTypesEnum.Import, Localizer["Import"],"BASE", dialogWidth: 800),
                this.MakeStandardAction("area_location", GridActionStandardTypesEnum.ExportExcel, Localizer["Export"],"BASE"),
            };
        }

        protected override IEnumerable<IGridColumn<area_location_View>> InitGridHeader()
        {
            return new List<GridColumn<area_location_View>>{
                this.MakeGridHeader(x => x.Area_view),
                this.MakeGridHeader(x => x.Location),
                this.MakeGridHeader(x => x.isMix),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<area_location_View> GetSearchQuery()
        {
            var query = DC.Set<area_location>()
                .Include("Area")
                .DPWhere(LoginUserInfo?.DataPrivileges, x => x.Area.DCID)
                .CheckEqual(Searcher.AreaID, x=>x.AreaID)
                .CheckEqual(Searcher.isMix, x=>x.isMix)
                .Select(x => new area_location_View
                {
				    ID = x.ID,
                    Area_view = x.Area.Area,
                    Location = x.Location,
                    isMix = x.isMix,
                })
                .OrderBy(x => x.Area_view).ThenBy(x=>x.Location);
            return query;
        }

    }

    public class area_location_View : area_location{
        [Display(Name = "区域")]
        public String Area_view { get; set; }

    }
}
