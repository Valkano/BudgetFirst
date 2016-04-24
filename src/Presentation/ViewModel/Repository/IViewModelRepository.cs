namespace BudgetFirst.ViewModel.Repository
{
    using System;
    using SharedInterfaces.ReadModel;

    /// <summary>
    /// Represents a view model repository
    /// </summary>
    /// <typeparam name="TReadModel">Read model type the view model is based on</typeparam>
    /// <typeparam name="TViewModel">View model type</typeparam>
    public interface IViewModelRepository<TReadModel, TViewModel>
        where TReadModel : ReadModel
        where TViewModel : ViewModel<TReadModel>
    {
        /// <summary>
        /// Get a view model for the given Id.
        /// Uses caching to return same view model every time.
        /// </summary>
        /// <param name="id">View model Id</param>
        /// <returns>View model, if found. <c>null</c> otherwise</returns>
        TViewModel Find(Guid id);
    }
}