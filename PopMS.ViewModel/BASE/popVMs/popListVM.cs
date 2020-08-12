using System;
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
                this.MakeGridHeader(x => x.Name_view),
                this.MakeGridHeader(x => x.PopNo),
                this.MakeGridHeader(x => x.OutID),
                this.MakeGridHeader(x => x.PopName),
                this.MakeGridHeader(x => x.index),
                this.MakeGridHeader(x => x.Pack),
                this.MakeGridHeader(x => x.Unit),
                this.MakeGridHeader(x => x.Weight),
                this.MakeGridHeader(x => x.ImageID).SetFormat(ImageIDFormat),
                this.MakeGridHeaderAction(width: 200)
            };
        }
        private List<ColumnFormatInfo> ImageIDFormat(pop_View entity, object val)
        {
            return new List<ColumnFormatInfo>
            {
                ColumnFormatInfo.MakeDownloadButton(ButtonTypesEnum.Button,entity.ImageID),
                ColumnFormatInfo.MakeViewButton(ButtonTypesEnum.Button,entity.ImageID,640,480),
            };
        }


        public override IOrderedQueryable<pop_View> GetSearchQuery()
        {
            var query = DC.Set<pop>()
                .CheckEqual(Searcher.GroupID, x=>x.GroupID)
                .CheckContain(Searcher.PopName, x=>x.PopName)
                .Select(x => new pop_View
                {
				    ID = x.ID,
                    Name_view = x.Group.Name,
                    PopIndex = x.PopIndex,
                    OutID = x.OutID,
                    PopName = x.PopName,
                    index = x.index,
                    Pack = x.Pack,
                    Unit = x.Unit,
                    Weight = x.Weight,
                    ImageID = x.ImageID,
                })
                .OrderBy(x => x.Name_view).ThenBy(x=>x.index).ThenBy(x=>x.PopName);
            return query;
        }

    }

    public class pop_View : pop{
        [Display(Name = "组别")]
        public String Name_view { get; set; }

    }
}
