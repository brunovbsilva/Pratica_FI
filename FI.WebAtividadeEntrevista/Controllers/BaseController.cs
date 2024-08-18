using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace FI.WebAtividadeEntrevista.Controllers
{
    public abstract class BaseController : Controller
    {
        public JsonResult CheckModelValidity()
        {
            List<string> erros = (from item in ModelState.Values
                from error in item.Errors
                select error.ErrorMessage).ToList();

            Response.StatusCode = 400;
            return Json(string.Join(Environment.NewLine, erros));
        }
    }
}