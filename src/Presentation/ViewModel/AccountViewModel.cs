namespace BudgetFirst.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using ReadSide.ReadModel;

    /// <summary>
    /// Account view model
    /// </summary>
    public class AccountViewModel : ViewModel<Account>
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="AccountViewModel"/> class.
        /// </summary>
        /// <param name="readModel">Account read model to base the view model on.</param>
        public AccountViewModel(Account readModel) : base(readModel)
        {
        }

        /// <summary>
        /// Gets or sets the account name
        /// </summary>
        public string Name
        {
            get
            {
                return this.ReadModel.Name;
            }

            set
            {
                // TODO 
            }
        }
    }
}
