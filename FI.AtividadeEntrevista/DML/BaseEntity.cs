namespace FI.AtividadeEntrevista.DML
{
    /// <summary>
    /// Classe abstrata de entidade base para entidades do banco de dados
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }
    }
}