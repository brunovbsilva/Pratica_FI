using FI.AtividadeEntrevista.DAL;
using FI.AtividadeEntrevista.DML;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FI.AtividadeEntrevista.BLL
{
    public class BoBeneficiario
    {
        private readonly DaoBeneficiario _beneficiario = new DaoBeneficiario();

        /// <summary>
        /// Inclui um novo beneficiario
        /// </summary>
        /// <param name="cliente">Objeto de beneficiario</param>
        public long Incluir(Beneficiario beneficiario)
        {
            return _beneficiario.Incluir(beneficiario);
        }

        /// <summary>
        /// Altera um beneficiario
        /// </summary>
        /// <param name="beneficiario">Objeto de beneficiario</param>
        public void Alterar(Beneficiario beneficiario)
        {
            _beneficiario.Alterar(beneficiario);
        }

        /// <summary>
        /// Excluir o beneficiario
        /// </summary>
        /// <param name="id">id do beneficiario</param>
        /// <returns></returns>
        public void Excluir(long id)
        {
            _beneficiario.Excluir(id);
        }

        /// <summary>
        /// Lista os beneficiarios do cliente
        /// <param name="idCliente">Objeto de idCliente</param>
        /// </summary>
        public List<Beneficiario> Listar(long idCliente)
        {
            return _beneficiario.Pesquisa(idCliente);
        }

        /// <summary>
        /// Verifica Existencia 
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="CPF"></param>
        /// <param name="IdCliente"></param>
        /// <returns></returns>
        public bool CheckExistence(Beneficiario beneficiario)
        {
            return _beneficiario.VerificarExistencia(beneficiario); ;
        }

        /// <summary>
        /// Atualiza/Insere/Exclui beneficiarios
        /// </summary>
        /// <param name="list">Objeto lista beneficiarios</param>
        /// <param name="idCliente">Objeto de idCliente</param>
        public void Atualizar(List<Beneficiario> list)
        {
            List<Beneficiario> beneficiarios = Listar(list.First().Id);
            Update(list.Where(x => x.Id > 0).ToList());
            Insert(list.Where(x => x.Id == 0).ToList());
            beneficiarios.Where(x => !list.Select(y => y.Id).Contains(x.Id))
                .ToList()
                .ForEach(x => Excluir(x.Id));
        }

        /// <summary>
        /// Insere beneficiarios
        /// </summary>
        /// <param name="idCliente">Objeto de idCliente</param>
        /// <param name="beneficiariosList">Objeto lista beneficiarios</param>

        private void Insert(List<Beneficiario> beneficiariosList)
        {
            beneficiariosList?.ForEach(beneficiario =>
            {
                if (CheckExistence(beneficiario))
                    throw new Exception($"CPF({beneficiario.CPF}) já cadastrado para o cliente.");
            });
            beneficiariosList?.ForEach(x => Incluir(x));
        }

        /// <summary>
        /// Atualiza beneficiarios
        /// </summary>
        /// <param name="idCliente">Objeto de idCliente</param>
        /// <param name="beneficiariosList">Objeto lista beneficiarios</param>
        private void Update(List<Beneficiario> beneficiariosList)
        {
            beneficiariosList.ForEach(beneficiario =>
            {
                if (CheckExistence(beneficiario))
                    throw new Exception($"CPF {beneficiario.CPF} já cadastrado para o cliente.");
            });
            beneficiariosList.ForEach(x => Alterar(x));
        }

        public void DeletarDeCliente(long clientId)
        {
            List<Beneficiario> beneficiarios = Listar(clientId);
            beneficiarios.ForEach(beneficiario => Excluir(beneficiario.Id));
        }
    }
}
