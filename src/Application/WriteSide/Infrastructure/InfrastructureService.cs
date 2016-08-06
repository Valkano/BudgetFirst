namespace BudgetFirst.WriteSide.Infrastructure
{
    using BudgetFirst.Infrastructure.Commands;
    using BudgetFirst.Infrastructure.Messaging;
    using BudgetFirst.Infrastructure.Persistency;

    using GalaSoft.MvvmLight.Messaging;

    /// <summary>
    /// Infrastructure command service
    /// </summary>
    public class InfrastructureService : ICommandHandler<SaveApplicationState>
    {
        /// <summary>
        /// Application state repository
        /// </summary>
        private IPersistedApplicationStateRepository repository;

        /// <summary>
        /// Factory for current application state
        /// </summary>
        private ICurrentApplicationStateFactory factory;

        /// <summary>
        /// Initialises a new instance of the <see cref="InfrastructureService"/> class.
        /// </summary>
        /// <param name="repository">Application state repository</param>
        /// <param name="factory">Current application state factory</param>
        public InfrastructureService(IPersistedApplicationStateRepository repository, ICurrentApplicationStateFactory factory)
        {
            this.repository = repository;
            this.factory = factory;
        }

        /// <summary>
        /// Handle the save application state command
        /// </summary>
        /// <param name="command">Save application state command</param>
        /// <param name="unitOfWork">Unit of work (is not used in this case)</param>
        public void Handle(SaveApplicationState command, IUnitOfWork unitOfWork)
        {
            this.repository.Save(this.factory.GetCurrentApplicationState(), command.Location);
        }
    }
}