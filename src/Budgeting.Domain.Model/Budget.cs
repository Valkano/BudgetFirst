namespace BudgetFirst.Budgeting.Domain.Model
{
    using System;
    using System.Runtime.InteropServices;

    using BudgetFirst.Budgeting.Domain.Events;
    using BudgetFirst.Common.Domain.Model.Identifiers;
    using BudgetFirst.Common.Infrastructure.Domain.Model;
    using BudgetFirst.Common.Infrastructure.Persistency;

    /// <summary>
    /// A budget for a single currency
    /// </summary>
    [ComVisible(false)]
    public class Budget : AggregateRoot<BudgetId>
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="Budget"/> class.
        /// Create a new budget
        /// </summary>
        /// <param name="id">Budget id</param>
        /// <param name="name">Budget name</param>
        /// <param name="currencyCode">Currency code</param>
        /// <param name="unitOfWork">Unit of work</param>
        internal Budget(BudgetId id, string name, string currencyCode, IUnitOfWork unitOfWork) : this(id, unitOfWork, false)
        {
            if (id == null || id.ToGuid() == Guid.Empty)
            {
                throw new ArgumentException("Id must be set");
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Budget must have a name");
            }

            if (string.IsNullOrWhiteSpace(currencyCode))
            {
                throw new ArgumentException("Currency code must be set");
            }

            this.Apply(new AddedBudget(name, currencyCode));
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="Budget"/> class.
        /// Load budget from history
        /// </summary>
        /// <param name="id">Budget Id</param>
        /// <param name="unitOfWork">Unit of work</param>
        internal Budget(BudgetId id, IUnitOfWork unitOfWork) : this(id, unitOfWork, true)
        {
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="Budget"/> class.
        /// Serves as a base for all <see cref="Budget"/> constructors.
        /// </summary>
        /// <param name="id">Budget Id</param>
        /// <param name="unitOfWork">Unit of work</param>
        /// <param name="loadFromHistory">Load the aggregate state from history</param>
        /// <remarks>Load from history cannot be part of the base class because we must define the event handlers first</remarks>
        private Budget(BudgetId id, IUnitOfWork unitOfWork, bool loadFromHistory) : base(id, unitOfWork)
        {
            this.Handles<AddedBudget>(this.When);

            if (loadFromHistory)
            {
                this.LoadFrom(unitOfWork.GetEventsForAggregate(id));
            }
        }

        /// <summary>
        /// When a budget has been added
        /// </summary>
        /// <param name="e">Budget added event</param>
        public void When(AddedBudget e)
        {
            // Nothing to track yet. Will come later  
        }
    }
}