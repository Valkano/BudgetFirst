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
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using SharedInterfaces.Annotations;
    using SharedInterfaces.ReadModel;

    /// <summary>
    /// List view models wrap list read models (i.e. observable collections).
    /// Implementation details: a view model must implement the same properties as the read model. 
    /// </summary>
    public abstract class ListViewModel<TListReadModel, TListItemReadModel, TListItemViewModel> : ObservableCollection<TListItemViewModel>
        where TListReadModel : ObservableCollection<TListItemReadModel>
        where TListItemReadModel : ReadModel
        where TListItemViewModel : ViewModel<TListItemReadModel>
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="ListViewModel{TListReadModel,TListItemReadModel,TListItemViewModel}"/> class.
        /// </summary>
        /// <param name="listReadModel">Read model to wrap and listen for events to.</param>
        protected ListViewModel(TListReadModel listReadModel)
        {
            this.ListReadModel = listReadModel;
            this.ListReadModel.CollectionChanged += this.ListReadModel_CollectionChanged;
            
            // init list
            foreach (var item in listReadModel)
            {
                this.Add(this.GetListItem(item)); // TODO: virtual call in constructor. Must assure that inheriting class doesn't use any state in that method
            }
        }

        /// <summary>
        /// Gets the list read model
        /// </summary>
        protected TListReadModel ListReadModel { get; private set; }

        private void ListReadModel_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            // TODO: this might not be fully implemented yet. It assumes that this event is handled for a single item change only
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (var newItem in e.NewItems.OfType<TListItemReadModel>())
                    {
                        this.Add(this.GetListItem(newItem));
                    }
                    break;

                case NotifyCollectionChangedAction.Move:
                    // TODO: handle multiple items
                    throw new NotImplementedException();
                    this.Move(e.OldStartingIndex, e.NewStartingIndex);
                    break;

                case NotifyCollectionChangedAction.Remove:
                    // TODO: handle multiple items
                    throw new NotImplementedException();
                    this.RemoveAt(e.OldStartingIndex);
                    break;

                case NotifyCollectionChangedAction.Replace:
                    // TODO: handle multiple items
                    throw new NotImplementedException();
                    var newItemReplaced = e.NewItems.OfType<TListItemReadModel>().Single();
                    this[e.OldStartingIndex] = this.GetListItem(newItemReplaced);
                    break;

                case NotifyCollectionChangedAction.Reset:
                    this.Clear();
                    break;
            }
        }

        protected abstract TListItemViewModel GetListItem(TListItemReadModel itemReadModel);
    }
}
