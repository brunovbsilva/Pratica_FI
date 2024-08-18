namespace FI.AtividadeEntrevista.DML
{
    /// <summary>
    /// Classe de beneficiário que representa o registo na tabela Beneficiario do Banco de Dados
    /// </summary>
    public class Beneficiario : BaseEntity
    {
        /// <summary>
        /// Nome
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// CPF
        /// </summary>
        public string CPF { get; set; }

        /// <summary>
        /// IdCliente
        /// </summary>
        public long IdCliente { get; set; }

        #region Constructors
        public Beneficiario() {}
        public Beneficiario(long id, string name, string cpf, long idCliente)
        {
            Id = id;
            Nome = name;
            CPF = cpf.Replace(".", "").Replace("-", "");
            IdCliente = idCliente;
        }
        public Beneficiario(long id, string name, string cpf)
        {
            Id = id;
            Nome = name;
            CPF = cpf.Replace(".", "").Replace("-", "");
            IdCliente = 0;
        }
        
        public Beneficiario(string name, string cpf)
        {
            Id = 0;
            Nome = name;
            CPF = cpf.Replace(".", "").Replace("-", "");
            IdCliente = 0;
        }
        #endregion
    }
}