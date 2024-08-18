using System.Collections.Generic;
using FI.AtividadeEntrevista.DAL;

namespace FI.AtividadeEntrevista.BLL
{
    public class BoCliente
    {
        private readonly DaoCliente _cli = new DaoCliente();
        /// <summary>
        /// Inclui um novo cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        public long Incluir(DML.Cliente cliente) => _cli.Incluir(cliente);

        /// <summary>
        /// Altera um cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        public void Alterar(DML.Cliente cliente) => _cli.Alterar(cliente);

        /// <summary>
        /// Consulta o cliente pelo id
        /// </summary>
        /// <param name="id">id do cliente</param>
        /// <returns></returns>
        public DML.Cliente Consultar(long id) => _cli.Consultar(id);

        /// <summary>
        /// Excluir o cliente pelo id
        /// </summary>
        /// <param name="id">id do cliente</param>
        /// <returns></returns>
        public void Excluir(long id) => _cli.Excluir(id);

        /// <summary>
        /// Lista os clientes
        /// </summary>
        public List<DML.Cliente> Listar() => _cli.Listar();

        /// <summary>
        /// Lista os clientes
        /// </summary>
        public List<DML.Cliente> Pesquisa(int iniciarEm, int quantidade, string campoOrdenacao, bool crescente, out int qtd)
            => _cli.Pesquisa(iniciarEm,  quantidade, campoOrdenacao, crescente, out qtd);
    }
}
