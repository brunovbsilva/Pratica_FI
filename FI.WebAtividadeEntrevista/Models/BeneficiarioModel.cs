using System.ComponentModel.DataAnnotations;
using FI.AtividadeEntrevista.DML;

namespace WebAtividadeEntrevista.Models
{
    public class BeneficiarioModel : BaseModel
    {
        /// <summary>
        /// Nome
        /// </summary>
        [Required]
        public string Nome { get; set; }

        /// <summary>
        /// CPF
        /// </summary>
        [Required]
        [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$", ErrorMessage = "Digite um CPF válido")]
        public string CPF { get; set; }

        /// <summary>
        /// Id do Cliente
        /// </summary>
        [Required]
        public long IdCliente { get; private set; }

        #region Methods

        public void SetClientId(long id) => IdCliente = id;
        #endregion

        #region Constructors
        public BeneficiarioModel() {}
        public BeneficiarioModel(long id, string name, string cpf, long idCliente)
        {
            Id = id;
            Nome = name;
            CPF = cpf;
            IdCliente = idCliente;
        }
        public BeneficiarioModel(string name, string cpf, long idCliente)
        {
            Id = 0;
            Nome = name;
            CPF = cpf;
            IdCliente = idCliente;
        }
        #endregion

        #region Operators
        public static implicit operator BeneficiarioModel(Beneficiario beneficiario)
        {
            return new BeneficiarioModel(beneficiario.Id, beneficiario.Nome, beneficiario.CPF, beneficiario.IdCliente);
        }

        public static explicit operator Beneficiario(BeneficiarioModel beneficiario)
        {
            return new Beneficiario(beneficiario.Id, beneficiario.Nome, beneficiario.CPF, beneficiario.IdCliente);
        }
        #endregion
    }
}