using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using PopMS.Model;


namespace PopMS.ViewModel.userVMs
{
    public partial class userListVM : BasePagedListVM<user_View, userSearcher>
    {
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeStandardAction("user", GridActionStandardTypesEnum.Create, Localizer["Create"],"", dialogWidth: 800),
                this.MakeStandardAction("user", GridActionStandardTypesEnum.Edit, Localizer["Edit"],"", dialogWidth: 800),
                this.MakeStandardAction("user", GridActionStandardTypesEnum.Delete, Localizer["Delete"], "",dialogWidth: 800),
                //this.MakeStandardAction("user", GridActionStandardTypesEnum.Details, Localizer["Details"],"", dialogWidth: 800),
                this.MakeStandardAction("user", GridActionStandardTypesEnum.BatchEdit, Localizer["BatchEdit"],"", dialogWidth: 800),
                this.MakeStandardAction("user", GridActionStandardTypesEnum.BatchDelete, Localizer["BatchDelete"],"", dialogWidth: 800),
                this.MakeStandardAction("user", GridActionStandardTypesEnum.Import, Localizer["Import"],"", dialogWidth: 800),
                this.MakeStandardAction("user", GridActionStandardTypesEnum.ExportExcel, Localizer["Export"],""),
            };
        }

        protected override IEnumerable<IGridColumn<user_View>> InitGridHeader()
        {
            return new List<GridColumn<user_View>>{
                this.MakeGridHeader(x => x.Name_view),
                this.MakeGridHeader(x => x.DeptName_view),
                this.MakeGridHeader(x => x.CodeAndName),
                this.MakeGridHeader(x => x.Sex),
                this.MakeGridHeader(x => x.CellPhone),
                this.MakeGridHeader(x => x.PhotoId).SetFormat(PhotoIdFormat),
                this.MakeGridHeader(x => x.IsValid),
                this.MakeGridHeaderAction(width: 200)
            };
        }
        private List<ColumnFormatInfo> PhotoIdFormat(user_View entity, object val)
        {
            return new List<ColumnFormatInfo>
            {
                ColumnFormatInfo.MakeDownloadButton(ButtonTypesEnum.Button,entity.PhotoId),
                ColumnFormatInfo.MakeViewButton(ButtonTypesEnum.Button,entity.PhotoId,640,480),
            };
        }
        public override IOrderedQueryable<user_View> GetSearchQuery()
        {
            var query = DC.Set<user>()
                .DPWhere(LoginUserInfo?.DataPrivileges,x=>x.DCID)
                .CheckEqual(Searcher.DCID,x=>x.DCID)
                .CheckEqual(Searcher.DeptID, x=>x.DeptID)
                .CheckContain(Searcher.ITCode, x=>x.ITCode)
                .CheckEqual(Searcher.IsValid, x=>x.IsValid)
                .Select(x => new user_View
                {
				    ID = x.ID,
                    Name_view=x.DC.Name,
                    DeptName_view = x.Dept.DeptName,
                    ITCode = x.ITCode,
                    Password = x.Password,
                    Email = x.Email,
                    Name = x.Name,
                    Sex = x.Sex,
                    CellPhone = x.CellPhone,
                    PhotoId = x.PhotoId,
                    IsValid = x.IsValid,
                    RoleName_view = x.UserRoles.Select(y=>y.Role.RoleName).ToSpratedString(null,","), 
                    GroupName_view = x.UserGroups.Select(y=>y.Group.GroupName).ToSpratedString(null,","), 
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class user_View : user{
        [Display(Name = "仓库名")]
        public String Name_view { get; set; }
        [Display(Name = "部门")]
        public String DeptName_view { get; set; }
        [Display(Name = "角色")]
        public String RoleName_view { get; set; }
        [Display(Name = "组别")]
        public String GroupName_view { get; set; }

    }
}
