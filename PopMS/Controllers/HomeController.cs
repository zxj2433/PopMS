using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WalkingTec.Mvvm.Core.Auth;
using WalkingTec.Mvvm.Mvc;
using PopMS.ViewModel.HomeVMs;
using System.Security.Policy;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.Extensions.Hosting.Internal;

namespace PopMS.Controllers
{
    public class HomeController : BaseController
    {
        [AllRights]
        public IActionResult Index()
        {
            ViewData["title"] = "屈臣氏物料管理系统";
            return View();
        }

        [Public]
        public IActionResult PIndex()
        {
            return View();
        }

        [AllRights]
        [ActionDescription("FrontPage")]
        public IActionResult FrontPage()
        {
            var areas = GlobaInfo.AllModule.Select(x => x.Area).Distinct();
            var legend = new List<string>();
            var series = new List<object>();
            foreach (var area in areas)
            {
                var legendName = area?.AreaName ?? "Default";
                var controllers = GlobaInfo.AllModule.Where(x => x.Area == area);
                legend.Add(legendName);
                series.Add(new
                {
                    name = legendName,
                    type = "bar",
                    data = new int[] {
                        controllers.Count(),
                        controllers.SelectMany(x => x.Actions).Count()
                    },
                });
            }

            var otherLegend = new List<string>() { "Info" };
            var otherSeries = new List<object>()
            {
                new {
                    name = "Info",
                    type = "bar",
                    data = new int[] {
                        GlobaInfo.AllModels.Count(),
                        GlobaInfo.AllAssembly.Count(),
                        ConfigInfo.DataPrivilegeSettings.Count(),
                        ConfigInfo.ConnectionStrings.Count(),
                        ConfigInfo.AppSettings.Count()
                    },
                }
            };

            ViewData["controller.legend"] = legend;
            ViewData["controller.series"] = series;
            ViewData["other.legend"] = otherLegend;
            ViewData["other.series"] = otherSeries;

            return PartialView();
        }

        [AllRights]
        [ActionDescription("Layout")]
        public IActionResult Layout()
        {
            ViewData["debug"] = ConfigInfo.IsQuickDebug;
            return PartialView();
        }

        [AllRights]
        public IActionResult UserInfo()
        {
            if (HttpContext.Request.Cookies.TryGetValue(CookieAuthenticationDefaults.CookiePrefix + AuthConstants.CookieAuthName, out string cookieValue))
            {
                var protectedData = Base64UrlTextEncoder.Decode(cookieValue);
                var dataProtectionProvider = HttpContext.RequestServices.GetRequiredService<IDataProtectionProvider>();
                var _dataProtector = dataProtectionProvider
                                        .CreateProtector(
                                            "Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationMiddleware",
                                            CookieAuthenticationDefaults.AuthenticationScheme,
                                            "v2");
                var unprotectedData = _dataProtector.Unprotect(protectedData);

                string cookieData = Encoding.UTF8.GetString(unprotectedData);
                return Json(cookieData);
            }
            else
                return Json("No Data");
        }
        [Public]
        public IActionResult Download(string name)
        {
            var s = Directory.GetCurrentDirectory();
            string Path = string.Format(ConfigInfo.AppSettings[name], s);
            string filename = System.IO.Path.GetFileName(Path);
            //FileStream fs = System.IO.File.Open(Path, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            //fs.Position = 0;
            return File(Path, "application/text", filename);
        }
        [Public]
        public IActionResult PalyVideo()
        {
            var s = Directory.GetCurrentDirectory();
            string Path =string.Format(ConfigInfo.AppSettings["VedioPath"],s);
            FileStream fs = System.IO.File.Open(Path, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            fs.Position = 0;
            return File(fs,"Vedio/mp4","物料管理系统培训视频.mp4");
        }
        [Public]
        public IActionResult SOPWord()
        {
            var s = Directory.GetCurrentDirectory();
            string Path =string.Format(ConfigInfo.AppSettings["SOPPath"],s);
            FileStream fs = System.IO.File.Open(Path, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            fs.Position = 0;
            return File(fs, "application/octet-stream", "SOP.rar");
        }

        //[AllowAnonymous]
        //[ResponseCache(Duration = 3600)]
        //public github GetGithubInfo()
        //{
        //    var rv = ReadFromCache<github>("githubinfo", () =>
        //    {
        //        var s = ConfigInfo.Domains["github"].CallAPI<github>("repos/dotnetcore/wtm", null, null, 60).Result;
        //        return s;
        //    }, 1800);

        //    return rv;
        //}

        //public class github
        //{
        //    public int stargazers_count { get; set; }
        //    public int forks_count { get; set; }
        //    public int subscribers_count { get; set; }
        //    public int open_issues_count { get; set; }
        //}

    }
}
