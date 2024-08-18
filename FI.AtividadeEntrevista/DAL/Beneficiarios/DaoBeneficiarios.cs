using FI.AtividadeEntrevista.DML;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace FI.AtividadeEntrevista.DAL
{
    /// <summary>
    /// Classe de acesso a dados de Beneficiario
    /// </summary>
    internal class DaoBeneficiario : AcessoDados
    {
        /// <summary>
        /// Inclui um novo beneficiario
        /// </summary>
        /// <param name="beneficiario">Objeto de beneficiario</param>
        internal long Incluir(Beneficiario beneficiario)
        {
            DataSet ds = base.Consultar("FI_SP_IncBeneficiario", GetParams(beneficiario));
            long ret = 0;
            if (ds.Tables[0].Rows.Count > 0)
                long.TryParse(ds.Tables[0].Rows[0][0].ToString(), out ret);
            return ret;
        }

        /// <summary>
        /// Inclui um novo beneficiario
        /// </summary>
        /// <param name="beneficiario">Objeto de beneficiario</param>
        internal List<Beneficiario> Consultar(long Id)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("Id", Id));

            DataSet ds = base.Consultar("FI_SP_ConsBeneficiario", parametros);
            List<Beneficiario> list = Converter(ds);

            return list;
        }

        internal bool VerificarExistencia(Beneficiario beneficiario)
        {
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            sqlParams.Add(new SqlParameter("ID", beneficiario.Id));
            sqlParams.Add(new SqlParameter("CPF", beneficiario.CPF));
            sqlParams.Add(new SqlParameter("IdCliente", beneficiario.IdCliente));
            DataSet ds = base.Consultar("FI_SP_VerificaBeneficiario", sqlParams);
            return ds.Tables[0].Rows.Count > 0;
        }

        internal List<Beneficiario> Pesquisa(long idCliente)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("idCliente", idCliente));

            DataSet ds = base.Consultar("FI_SP_PesqBeneficiario", parametros);
            List<Beneficiario> bene = Converter(ds);

            int iQtd = 0;

            if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                int.TryParse(ds.Tables[1].Rows[0][0].ToString(), out iQtd);

            return bene;
        }

        /// <summary>
        /// Lista todos os clientes
        /// </summary>
        internal List<Beneficiario> Listar()
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("Id", 0));

            DataSet ds = base.Consultar("FI_SP_Consbeneficiario", parametros);
            List<Beneficiario> bene = Converter(ds);

            return bene;
        }

        /// <summary>
        /// Inclui um novo cliente
        /// </summary>
        /// <param name="beneficiario">Objeto de cliente</param>
        internal void Alterar(Beneficiario beneficiario)
        {
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            sqlParams.Add(new SqlParameter("Nome", beneficiario.Nome));
            sqlParams.Add(new SqlParameter("CPF", beneficiario.CPF));
            sqlParams.Add(new SqlParameter("Id", beneficiario.Id));
            Executar("FI_SP_AltBeneficiario", sqlParams);
        }


        /// <summary>
        /// Excluir Beneficiario
        /// </summary>
        /// <param name="cliente">Objeto de beneficiario</param>
        internal void Excluir(long Id)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();

            parametros.Add(new SqlParameter("Id", Id));

            Executar("FI_SP_DelBeneficiario", parametros);
        }

        private List<Beneficiario> Converter(DataSet ds)
        {
            List<Beneficiario> lista = new List<Beneficiario>();
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    lista.Add(new Beneficiario(
                        row.Field<long>("Id"),
                        row.Field<string>("Nome"),
                        row.Field<string>("CPF"),
                        row.Field<long>("IdCliente")
                    ));
                }
            }

            return lista;
        }

        private List<SqlParameter> GetParams(Beneficiario beneficiario)
        {
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            sqlParams.Add(new SqlParameter("Nome", beneficiario.Nome));
            sqlParams.Add(new SqlParameter("CPF", beneficiario.CPF));
            sqlParams.Add(new SqlParameter("IdCliente", beneficiario.IdCliente));
            return sqlParams;
        }
    }
}
