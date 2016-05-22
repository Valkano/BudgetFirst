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
namespace BudgetFirst.ViewModel.Shared
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;

    /// <summary>
    /// A base ViewModel that has a header and knows how to close itself, 
    /// to represent windows or Closable tabs.
    /// </summary>
    public class ClosableViewModel : ViewModelBase
    {
        /// <summary>
        /// The ClosableViewModel's Header.
        /// </summary>
        private string header;

        /// <summary>
        /// Whether or not the <see cref="ClosableViewModel"/> is closable.
        /// </summary>
        private bool closable;

        /// <summary>
        /// Initialises a new instance of the <see cref="ClosableViewModel"/> class.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors", 
            Justification = "The method is not called, it is just initialised as an action.")]
        public ClosableViewModel()
        {
            this.CloseCommand = new RelayCommand(() => this.Close(), () => this.Closable);
            this.Closable = true;
        }

        /// <summary>
        /// An event that is called when the <see cref="ClosableViewModel"/> would like to be closed.
        /// </summary>
        public event EventHandler RequestClose;

        /// <summary>
        /// Gets or sets the Header.
        /// </summary>
        public string Header
        {
            get
            {
                return this.header;
            }

            set
            {
                this.header = value;
                this.RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the <see cref="ClosableViewModel"/> is closable.
        /// </summary>
        public bool Closable
        {
            get
            {
                return this.closable;
            }

            set
            {
                this.closable = value;
                this.CloseCommand.RaiseCanExecuteChanged();
                this.RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets the command to close the <see cref="ClosableViewModel"/>
        /// </summary>
        public RelayCommand CloseCommand { get; private set; }

        /// <summary>
        /// Closes the the <see cref="ClosableViewModel"/>.
        /// </summary>
        protected virtual void Close()
        {
            this.RaiseRequestClose();
        }

        /// <summary>
        /// Raises the RequestClose event.
        /// </summary>
        private void RaiseRequestClose()
        {
            this.RequestClose?.Invoke(this, EventArgs.Empty);
        }
    }
}