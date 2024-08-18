using FI.AtividadeEntrevista.BLL;
using WebAtividadeEntrevista.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using FI.AtividadeEntrevista.DML;
using FI.WebAtividadeEntrevista.Controllers;

namespace WebAtividadeEntrevista.Controllers
{
    public class ClienteController : BaseController
    {
        private readonly BoCliente _bo = new BoCliente();
        private readonly BoBeneficiario _boBeneficiario = new BoBeneficiario();
        public ActionResult Index() => View();
        public ActionResult Incluir() => View();

        [HttpPost]
        public JsonResult Incluir(ClienteModel model)
        {
            if (!ModelState.IsValid) return CheckModelValidity();

            try
            {
                model.UpdateId(_bo.Incluir((Cliente)model));
                if(model.Beneficiarios.Any())
                    _boBeneficiario.Atualizar(model.Beneficiarios.Select(beneficiario => (Beneficiario)beneficiario).ToList());
            }
            catch (SqlException e)
            {
                Response.StatusCode = 400;
                if (e.Message.Contains("UQ_CLIENTES_CPF"))
                    return Json($"Já existe um cliente com o CPF {model.CPF} cadastrado!");
            }
       
            return Json("Cadastro efetuado com sucesso");
        }

        [HttpPost]
        public JsonResult Alterar(ClienteModel model)
        {
            if (!ModelState.IsValid) return CheckModelValidity();

            model.UpdateId(model.Id);
            _bo.Alterar((Cliente)model);
            if (model.Beneficiarios.Any()) _boBeneficiario.Atualizar(model.Beneficiarios.Select(beneficiario => (Beneficiario)beneficiario).ToList());
            else _boBeneficiario.DeleteFromClient(model.Id);
            return Json("Cadastro alterado com sucesso");
        }

        [HttpGet]
        public ActionResult Alterar(long id) => View((ClienteModel)_bo.Consultar(id));

        [HttpPost]
        public JsonResult ClienteList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                int qtd = 0;
                string campo = string.Empty;
                string crescente = string.Empty;
                string[] array = jtSorting.Split(' ');

                if (array.Length > 0)
                    campo = array[0];

                if (array.Length > 1)
                    crescente = array[1];

                List<Cliente> clientes = new BoCliente().Pesquisa(jtStartIndex, jtPageSize, campo, crescente.Equals("ASC", StringComparison.InvariantCultureIgnoreCase), out qtd);

                //Return result to jTable
                return Json(new { Result = "OK", Records = clientes, TotalRecordCount = qtd });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
    }
}