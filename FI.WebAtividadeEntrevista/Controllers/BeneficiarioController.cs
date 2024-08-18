using System;
using System.Linq;
using System.Web.Mvc;
using FI.AtividadeEntrevista.BLL;
using WebAtividadeEntrevista.Models;

namespace FI.WebAtividadeEntrevista.Controllers
{
    public class BeneficiarioController : BaseController
    {
        private readonly BoBeneficiario _bo = new BoBeneficiario();
        [HttpGet]
        public JsonResult Get(long id)
        {
            try
            {
                var model = _bo.Listar(id).Select(x => (BeneficiarioModel)x).ToList();
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
    }
}