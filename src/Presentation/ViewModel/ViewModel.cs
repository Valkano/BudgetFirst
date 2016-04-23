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
// along with Foobar.  If not, see<http://www.gnu.org/licenses/>.
// ===================================================================
namespace BudgetFirst.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using SharedInterfaces.Annotations;
    using SharedInterfaces.ReadModel;

    /// <summary>
    /// View models wrap read models.
    /// Implementation details: a view model must implement the same properties as the read model. 
    /// <see cref="PropertyChanged"/> events are raised for the containing read model properties.
    /// </summary>
    /// <typeparam name="TReadModel">Read model that this view model is based on</typeparam>
    public abstract class ViewModel<TReadModel> : INotifyPropertyChanged where TReadModel : ReadModel
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="ViewModel{TReadModel}"/> class.
        /// </summary>
        /// <param name="readModel">Read model to wrap and listen for events to.</param>
        protected ViewModel(TReadModel readModel)
        {
            this.ReadModel = readModel;
            this.ReadModel.PropertyChanged += this.ReadModel_PropertyChanged;
        }

        /// <summary>
        /// Property changed event
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets the read model
        /// </summary>
        protected TReadModel ReadModel { get; private set; }
        
        /// <summary>
        /// Invokes all event handlers for the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="propertyName">Name of the property</param>
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        /// <summary>
        /// Forwards the <see cref="PropertyChanged"/> event from the read model.
        /// </summary>
        /// <param name="sender">Sender of the event</param>
        /// <param name="e">Event arguments</param>
        private void ReadModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.OnPropertyChanged(e.PropertyName);
        }
    }
}
