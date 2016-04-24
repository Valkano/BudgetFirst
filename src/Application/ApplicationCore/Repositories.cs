namespace BudgetFirst.ApplicationCore
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using ViewModel.Repository;

    /// <summary>
    /// Singleton for view model repositories
    /// </summary>
    public static class Repositories
    {
        /// <summary>
        /// Bootstrap which handles all initialisation of the application core
        /// </summary>
        private static readonly Bootstrap Bootstrap;

        /// <summary>
        /// Initialises static members of the <see cref="Repositories"/> class.
        /// </summary>
        static Repositories()
        {
            Bootstrap = new Bootstrap();
        }

        /// <summary>
        /// Gets the account view model repository
        /// </summary>
        public static AccountViewModelRepository AccountViewModelRepository => Bootstrap.AccountViewModelRepository;
    }
}
