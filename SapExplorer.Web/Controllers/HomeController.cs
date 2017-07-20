using SapExplorer.Models;
using SapExplorer.Services;
using SapExplorer.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SapExplorer.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(DatasheetModel model)
        {
            model.Destinations = new DestinationService().GetAllDestinations();

            if (!string.IsNullOrEmpty(model.SearchFunction))
            {
                var service = new SapRfcService();
                service.SetDestination(model.SelectedDestination);

                var result = service.GetFunctionParameters(model.SearchFunction.ToUpper());
                if (result.Success)
                {
                    model.Parameters = result.Export;
                }
                else
                {
                    ViewBag.Error = result.Message;
                }
            }
            
            return View(model);
        }
        
        [HttpPost]
        public ActionResult Code(DatasheetModel model)
        {
            var service = new SapRfcService();
            service.SetDestination(model.SelectedDestination);

            var result = service.GetFunctionParameters(model.SearchFunction);
            if (result.Success)
            {
                var codeDomService = new CodeDomService();
                var code = codeDomService.Generate(model.SearchFunction, result.Export);

                return File(Encoding.UTF8.GetBytes(code), "text/plain", string.Format("{0}.cs", model.SearchFunction));
            }

            return new EmptyResult();
        }

        public ActionResult Read(string destination, string structure)
        {
            var service = new SapRfcService();
            service.SetDestination(destination);

            var model = new TableModel();
            var result = service.GetTable(tableName: structure, rowCount: 1000);
            if (result.Success)
            {
                model.Table = result.Export;
            }
            else
            {
                ViewBag.Error = result.Message;
            }

            return View(model);
        }

        public ActionResult GetFunctions(string destination, string query)
        {
           var service = new SapRfcService();
            service.SetDestination(destination);

            var result = service.GetFunctions(string.Format("{0}*", query));
            if (result.Success)
            {
                var names = result.Export
                    .Select(x => x.Name)
                    .OrderBy(x => x)
                    .Take(50);

                return Json(names, JsonRequestBehavior.AllowGet);
            }

            return Json(false);
        }
    }
}