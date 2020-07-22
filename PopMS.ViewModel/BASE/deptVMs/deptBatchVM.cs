using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using PopMS.Model;


namespace PopMS.ViewModel.BASE.deptVMs
{
    public partial class deptBatchVM : BaseBatchVM<dept, dept_BatchEdit>
    {
        public deptBatchVM()
        {
            ListVM = new deptListVM();
            LinkedVM = new dept_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class dept_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
