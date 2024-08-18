using System.Collections.Generic;

namespace FI.AtividadeEntrevista.DML
{
    /// <summary>
    /// Classe de cliente que representa o registo na tabela Cliente do Banco de Dados
    /// </summary>
    public class Cliente : BaseEntity
    {
        
        /// <summary>
        /// CEP
        /// </summary>
        public string CEP { get; set; }

        /// <summary>
        /// Cidade
        /// </summary>
        public string Cidade { get; set; }

        /// <summary>
        /// E-mail
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Estado
        /// </summary>
        public string Estado { get; set; }

        /// <summary>
        /// Logradouro
        /// </summary>
        public string Logradouro { get; set; }

        /// <summary>
        /// Nacionalidade
        /// </summary>
        public string Nacionalidade { get; set; }

        /// <summary>
        /// Nome
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Sobrenome
        /// </summary>
        public string Sobrenome { get; set; }

        /// <summary>
        /// Telefone
        /// </summary>
        public string Telefone { get; set; }

        /// <summary>
        /// CPF
        /// </summary>
        public string CPF { get; set; }

        /// <summary>
        /// Lista de beneficiarios
        /// </summary>
        public List<Beneficiario> Beneficiarios { get; private set; } = new List<Beneficiario>();

        #region Methods
        public void AddBeneficiario(Beneficiario beneficiario) => Beneficiarios.Add(beneficiario);
        #endregion

        #region Constructors
        public Cliente() {}
        public Cliente(long id, string cep, string city, string email, string state, string lograd,
            string native, string name, string lastName, string phone, string cpf)
        {
            Id = id;
            CEP = cep;
            Cidade = city;
            Email = email;
            Estado = state;
            Logradouro = lograd;
            Nacionalidade = native;
            Nome = name;
            Sobrenome = lastName;
            Telefone = phone;
            CPF = cpf;
        }
        public Cliente(long id, string cep, string city, string email, string state, string lograd,
            string native, string name, string lastName, string phone, string cpf, List<Beneficiario> beneficiarios)
        {
            Id = id;
            CEP = cep;
            Cidade = city;
            Email = email;
            Estado = state;
            Logradouro = lograd;
            Nacionalidade = native;
            Nome = name;
            Sobrenome = lastName;
            Telefone = phone;
            CPF = cpf;
            beneficiarios.ForEach(AddBeneficiario);
        }
        public Cliente(string cep, string city, string email, string state, string lograd,
            string native, string name, string lastName, string phone, string cpf, List<Beneficiario> beneficiarios)
        {
            Id = 0;
            CEP = cep;
            Cidade = city;
            Email = email;
            Estado = state;
            Logradouro = lograd;
            Nacionalidade = native;
            Nome = name;
            Sobrenome = lastName;
            Telefone = phone;
            CPF = cpf;
            beneficiarios.ForEach(AddBeneficiario);
        }
        public Cliente(string cep, string city, string email, string state, string lograd,
            string native, string name, string lastName, string phone, string cpf)
        {
            Id = 0;
            CEP = cep;
            Cidade = city;
            Email = email;
            Estado = state;
            Logradouro = lograd;
            Nacionalidade = native;
            Nome = name;
            Sobrenome = lastName;
            Telefone = phone;
            CPF = cpf;
        }
        #endregion
    }    
}
