namespace BudgetFirst.ReadSide.ReadModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using SharedInterfaces.ReadModel;

    /// <summary>
    /// Account read model
    /// </summary>
    public class Account : ReadModel
    {
        /// <summary>
        /// Account name
        /// </summary>
        private string name;

        /// <summary>
        /// Gets or sets the account name
        /// </summary>
        public string Name
        {
            get { return this.name; }
            set { this.SetProperty(ref this.name, value); }
        }
    }
}
