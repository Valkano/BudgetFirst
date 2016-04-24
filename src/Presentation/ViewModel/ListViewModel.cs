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
    using ReadSide.Repositories;
    using Repository;
    using SharedInterfaces.Annotations;
    using SharedInterfaces.ReadModel;

    /// <summary>
    /// List view models wrap list read models (i.e. observable collections).
    /// Implementation details: a view model must implement the same properties as the read model. 
    /// </summary>
    /// <typeparam name="TListReadModel">List read model type</typeparam>
    /// <typeparam name="TListItemReadModel">List item read model type</typeparam>
    /// <typeparam name="TListItemViewModel">List item view model type</typeparam>
    public abstract class ListViewModel<TListReadModel, TListItemReadModel, TListItemViewModel> : ObservableCollection<TListItemViewModel>
        where TListReadModel : ObservableCollection<TListItemReadModel>
        where TListItemReadModel : ReadModel
        where TListItemViewModel : ViewModel<TListItemReadModel>
    {
        /// <summary>
        /// View model repository
        /// </summary>
        private IViewModelRepository<TListItemReadModel, TListItemViewModel> viewModelRepository;

        /// <summary>
        /// Initialises a new instance of the <see cref="ListViewModel{TListReadModel,TListItemReadModel,TListItemViewModel}"/> class.
        /// </summary>
        /// <param name="listReadModel">Read model to wrap and listen for events to.</param>
        /// <param name="viewModelRepository">List item view model repository</param>
        protected ListViewModel(TListReadModel listReadModel, IViewModelRepository<TListItemReadModel, TListItemViewModel> viewModelRepository)
        {
            this.viewModelRepository = viewModelRepository;
            this.ListReadModel = listReadModel;
            this.ListReadModel.CollectionChanged += this.ListReadModel_CollectionChanged;

            // init list
            foreach (var item in listReadModel)
            {
                this.Add(this.GetListItem(item));
            }
        }

        /// <summary>
        /// Gets the list read model
        /// </summary>
        protected TListReadModel ListReadModel { get; private set; }

        /// <summary>
        /// Maps the read model list item to the view model list item (using the identity-mapped repository)
        /// </summary>
        /// <param name="listItemReadModel">List item read model</param>
        /// <returns>Mapped view model</returns>
        private TListItemViewModel GetListItem(TListItemReadModel listItemReadModel)
        {
            // read and view models share the same Id; Uses caching
            return this.viewModelRepository.Find(listItemReadModel.Id);
        }

        /// <summary>
        /// Handles the collection changed event for the list read model
        /// </summary>
        /// <param name="sender">Sender of the event</param>
        /// <param name="e">Event arguments</param>
        private void ListReadModel_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            // TODO: this might not be fully implemented yet. It assumes that this event is handled for a single item change only
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    if (e.NewItems == null || e.NewItems.Count != 1)
                    {
                        break;
                    }

                    this.Insert(e.NewStartingIndex, this.GetListItem((TListItemReadModel)e.NewItems[0]));
                    return;
                case NotifyCollectionChangedAction.Move:
                    if (e.NewItems == null || e.NewItems.Count != 1 || e.OldItems == null || e.OldItems.Count != 1)
                    {
                        break;
                    }

                    this.Move(e.OldStartingIndex, e.NewStartingIndex);
                    return;
                case NotifyCollectionChangedAction.Remove:
                    if (e.OldItems == null || e.OldItems.Count != 1)
                    {
                        break;
                    }

                    this.RemoveAt(e.OldStartingIndex);
                    return;
                case NotifyCollectionChangedAction.Replace:
                    if (e.NewItems == null || e.NewItems.Count != 1 || e.OldItems == null || e.OldItems.Count != 1 ||
                        e.OldStartingIndex != e.NewStartingIndex)
                    {
                        break;
                    }

                    this[e.OldStartingIndex] = this.GetListItem((TListItemReadModel)e.NewItems[0]);
                    return;

                case NotifyCollectionChangedAction.Reset:
                    this.Clear();
                    return;
            }
        }
    }
}
