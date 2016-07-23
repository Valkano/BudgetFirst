namespace BudgetFirst.ReadSide.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using BudgetFirst.Infrastructure.ReadModel;

    /// <summary>
    /// Represents a read model repository
    /// </summary>
    /// <typeparam name="TReadModel">Read model type</typeparam>
    public interface IReadModelRepository<TReadModel> where TReadModel : ReadModel
    {
        /// <summary>
        /// Retrieve a read model instance from the repository.
        /// </summary>
        /// <param name="id">Read model Id</param>
        /// <returns>Reference to the read model in the repository, if found. <c>null</c> otherwise.</returns>
        TReadModel Find(Guid id);
    }
}
