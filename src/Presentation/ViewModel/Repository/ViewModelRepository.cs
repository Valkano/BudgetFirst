// This file is part of BudgetFirst.
//
// BudgetFirst is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// BudgetFirst is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with Budget First.  If not, see<http://www.gnu.org/licenses/>.
// ===================================================================
namespace BudgetFirst.ViewModel.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using ReadSide.Repositories;
    using SharedInterfaces.Commands;
    using SharedInterfaces.ReadModel;

    /// <summary>
    /// Base class for view model repositories
    /// </summary>
    /// <typeparam name="TReadModelRepository">Read model repository type</typeparam>
    /// <typeparam name="TReadModel">Read model type</typeparam>
    /// <typeparam name="TViewModel">View model type</typeparam>
    public abstract class ViewModelRepository<TReadModelRepository, TReadModel, TViewModel> : IViewModelRepository<TReadModel, TViewModel> 
        where TReadModelRepository : IReadModelRepository<TReadModel>
        where TReadModel : ReadModel
        where TViewModel : ViewModel<TReadModel>
    {
        /// <summary>
        /// Cache for created view models
        /// </summary>
        private readonly Dictionary<Guid, TViewModel> identityMap;

        /// <summary>
        /// Initialises a new instance of the <see cref="ViewModelRepository{TReadModelRepository,TReadModel,TViewModel}"/> class.
        /// </summary>
        /// <param name="readModelRepository">Read model repository</param>
        /// <param name="commandBus">Command bus</param>
        protected ViewModelRepository(TReadModelRepository readModelRepository, ICommandBus commandBus)
        {
            this.ReadModelRepository = readModelRepository;
            this.CommandBus = commandBus;
            this.identityMap = new Dictionary<Guid, TViewModel>();
        }
        
        /// <summary>
        /// Gets the command bus
        /// </summary>
        protected ICommandBus CommandBus { get; private set; }

        /// <summary>
        /// Gets the read model repository
        /// </summary>
        protected TReadModelRepository ReadModelRepository { get; private set; }

        /// <summary>
        /// Get a view model for the given Id.
        /// Uses caching to return same view model every time.
        /// </summary>
        /// <param name="id">View model Id</param>
        /// <returns>View model, if found. <c>null</c> otherwise</returns>
        public TViewModel Find(Guid id)
        {
            TViewModel viewModel;
            if (this.identityMap.TryGetValue(id, out viewModel))
            {
                return viewModel;
            }

            var readModel = this.ReadModelRepository.Find(id);
            if (readModel == null)
            {
                return null;
            }

            return this.Map(readModel);
        }

        /// <summary>
        /// Maps the read model to the view model
        /// </summary>
        /// <param name="readModel">Read model</param>
        /// <returns>Mapped view model</returns>
        protected abstract TViewModel Map(TReadModel readModel);
    }
}
