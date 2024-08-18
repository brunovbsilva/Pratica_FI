using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using FI.AtividadeEntrevista.DML;

namespace WebAtividadeEntrevista.Models
{
    /// <summary>
    /// Classe de Modelo de Cliente
    /// </summary>
    public class ClienteModel : BaseModel
    {
        /// <summary>
        /// CEP
        /// </summary>
        [Required]
        public string CEP { get; set; }

        /// <summary>
        /// Cidade
        /// </summary>
        [Required]
        public string Cidade { get; set; }

        /// <summary>
        /// E-mail
        /// </summary>
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Digite um e-mail válido")]
        public string Email { get; set; }

        /// <summary>
        /// Estado
        /// </summary>
        [Required]
        [MaxLength(2)]
        public string Estado { get; set; }

        /// <summary>
        /// Logradouro
        /// </summary>
        [Required]
        public string Logradouro { get; set; }

        /// <summary>
        /// Nacionalidade
        /// </summary>
        [Required]
        public string Nacionalidade { get; set; }

        /// <summary>
        /// Nome
        /// </summary>
        [Required]
        public string Nome { get; set; }

        /// <summary>
        /// Sobrenome
        /// </summary>
        [Required]
        public string Sobrenome { get; set; }

        /// <summary>
        /// Telefone
        /// </summary>
        public string Telefone { get; set; }

        /// <summary>
        /// CPF
        /// </summary>
        [Required]
        [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$", ErrorMessage = "Digite um CPF válido")]
        public string CPF { get; set; }

        /// <summary>
        /// Lista de beneficiários
        /// </summary>
        public List<BeneficiarioModel> Beneficiarios { get; private set; } = new List<BeneficiarioModel>();

        #region Methods
        public void AddBeneficiario(BeneficiarioModel beneficiario) => Beneficiarios.Add(beneficiario);

        public void UpdateId(long id)
        {
            Id = id;
            Beneficiarios.ForEach(beneficiario => beneficiario.SetClientId(id));
        }
        #endregion

        #region Constructors
        public ClienteModel() {}
        public ClienteModel(long id, string cep, string city, string email, string state, string lograd,
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
        public ClienteModel(long id, string cep, string city, string email, string state, string lograd,
            string native, string name, string lastName, string phone, string cpf, List<BeneficiarioModel> beneficiarios)
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
        public ClienteModel(string cep, string city, string email, string state, string lograd,
            string native, string name, string lastName, string phone, string cpf, List<BeneficiarioModel> beneficiarios)
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
        public ClienteModel(string cep, string city, string email, string state, string lograd,
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

        #region Operators
        public static implicit operator ClienteModel(Cliente client)
        {
            return new ClienteModel(
                client.Id,
                client.CEP,
                client.Cidade,
                client.Email,
                client.Estado,
                client.Logradouro,
                client.Nacionalidade,
                client.Nome,
                client.Sobrenome,
                client.Telefone,
                client.CPF.Replace(".", "").Replace("-", ""),
                client.Beneficiarios.Select(x => (BeneficiarioModel)x).ToList()
            );
        }

        public static explicit operator Cliente(ClienteModel client)
        {
            return new Cliente(
                client.Id,
                client.CEP,
                client.Cidade,
                client.Email,
                client.Estado,
                client.Logradouro,
                client.Nacionalidade,
                client.Nome,
                client.Sobrenome,
                client.Telefone,
                client.CPF.Replace(".", "").Replace("-", ""),
                client.Beneficiarios.Select(x => (Beneficiario)x).ToList()
            );
        }   
        #endregion
    }    
}