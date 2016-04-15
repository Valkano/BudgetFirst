namespace BudgetFirst.SharedInterfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// A bootstrap performs necessary run-once operations such as registering classes in inversion of control containers etc.
    /// </summary>
    public interface IBootstrap
    {
        /// <summary>
        /// Perform all necessary run-once operations
        /// </summary>
        void Initialise();
    }
}
