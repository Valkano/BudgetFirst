namespace BudgetFirst.ViewModel.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using ReadSide.ReadModel;
    using ReadSide.Repositories;
    using SharedInterfaces.Commands;

    /// <summary>
    /// Account view model repository
    /// </summary>
    public class AccountViewModelRepository
    {
        /// <summary>
        /// Account read model repository
        /// </summary>
        private readonly AccountReadModelRepository readModelRepository;

        /// <summary>
        /// Command bus
        /// </summary>
        private readonly ICommandBus commandBus;

        /// <summary>
        /// Initialises a new instance of the <see cref="AccountViewModelRepository"/> class.
        /// </summary>
        /// <param name="readModelRepository">Read model repository (to get and wrap read models)</param>
        /// <param name="commandBus">Command bus</param>
        public AccountViewModelRepository(AccountReadModelRepository readModelRepository, ICommandBus commandBus)
        {
            this.readModelRepository = readModelRepository;
            this.commandBus = commandBus;
        }

        /// <summary>
        /// Find an account with a specific Id
        /// </summary>
        /// <param name="id">Account Id</param>
        /// <returns>Account view model, or <c>null</c> if not found.</returns>
        public AccountViewModel Find(Guid id)
        {
            var readModel = this.readModelRepository.Find(id);
            if (readModel == null)
            {
                return null;
            }

            return new AccountViewModel(readModel, this.commandBus);
        }
    }
}
