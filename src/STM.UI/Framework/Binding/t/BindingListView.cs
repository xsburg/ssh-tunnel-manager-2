using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections;
using System.Reflection;
using System.Reflection.Emit;
using System.Diagnostics;
using STM.UI.Properties;

namespace SSHTunnelManager.Ext.BLW
{
    public class BindingListView<T> : Component, IBindingListView, IRaiseItemChangedEvents, ITypedList
    {
        #region Constructors

        public BindingListView(IList list)
            : this()
        {
            this.DataSource = list;
        }

        public BindingListView()
        {
            _savedSourceLists = new List<IList>();
            _sourceIndices = new MultiSourceIndexList<T>();
            // Start with a filter that includes all items.
            _filter = IncludeAllItemFilter<T>.Instance;
            // Start with no sorts applied.
            _sorts = new ListSortDescriptionCollection();
            _objectViewCache = new Dictionary<T,ObjectView<T>>();
        }

        public BindingListView(IContainer container)
            : this()
        {
            container.Add(this);

            if (Site is ISynchronizeInvoke)
            {
                SynchronizingObject = Site as ISynchronizeInvoke;
            }

            this.DataSource = null;
        }

        #endregion

        #region Private Member Fields

        /// <summary>
        /// The sorted, filtered list of item indices in _sourceList.
        /// </summary>
        private MultiSourceIndexList<T> _sourceIndices;
        /// <summary>
        /// The current filter applied to the view.
        /// </summary>
        private IItemFilter<T> _filter;
        /// <summary>
        /// The current sorts applied to the view.
        /// </summary>
        private ListSortDescriptionCollection _sorts;
        /// <summary>
        /// The <see cref="System.Collection.Generic.IComparer">IComparer</see> used to compare items when sorting.
        /// </summary>
        private IComparer<KeyValuePair<ListItemPair<T>, int>> _comparer;
        /// <summary>
        /// The item in the process of being added to the view.
        /// </summary>
        private ObjectView<T> _newItem;

        /// <summary>
        /// The object used to marshal event-handler calls that are invoked on a non-UI thread.
        /// </summary>
        private ISynchronizeInvoke _synchronizingObject;
        /// <summary>
        /// A copy of the source lists so when a list is removed from SourceLists
        /// we still have a reference to use for unhooking events, etc.
        /// </summary>
        private List<IList> _savedSourceLists;
        /// <summary>
        /// ObjectView cache used to prevent re-creation of existing object wrappers when 
        /// in FilterAndSort().
        /// </summary>
        private Dictionary<T, ObjectView<T>> _objectViewCache;

        #endregion
        
        /// <summary>
        /// Gets the ObjectView&lt;T&gt; of the item at the given index in the view.
        /// </summary>
        /// <param name="index">The item index.</param>
        /// <returns>The ObjectView&lt;T&gt; of the item.</returns>
        public ObjectView<T> this[int index]
        {
            get
            {
                return _sourceIndices[index].Key.Item;
            }
        }
        
        /// <summary>
        /// Re-applies any current filter and sorts to refresh the current view.
        /// </summary>
        public void Refresh()
        {
            FilterAndSort();
            // Get any bound objects to refresh everything as well.
            OnListChanged(ListChangedType.Reset, -1);
        }

        /// <summary>
        /// Gets or sets the object used to marshal event-handler calls that are invoked on a non-UI thread.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ISynchronizeInvoke SynchronizingObject
        {
            get
            {
                return _synchronizingObject;
            }
            set
            {
                _synchronizingObject = value;
            }
        }

        /// <summary>
        /// Updates the _sourceIndices list to contain the items that are current viewed
        /// according to applied filter and sorts.
        /// </summary>
        protected void FilterAndSort()
        {
            // The view contains items from the source list
            // and possibly a new items that are not yet committed.
            // Therefore we can't just clear the list and start over
            // as we would lose the new items. So we have to to insert
            // filtered source list items into a new list first.
            // New items can then be pulled out of the current view
            // and appended to the new list.
            var newList = new MultiSourceIndexList<T>();

            // Get items from the source list that are included by the current filter.
            var sourceList = this.GetSourceList();
            if (sourceList == null)
            {
                return;
            }

            for (var i = 0; i < sourceList.Count; i++)
            {
                var item = (T)sourceList[i];
                ObjectView<T> editableObject;
                if (_filter.Include(item))
                {
                    if (_objectViewCache.ContainsKey(item))
                    {
                        editableObject = _objectViewCache[item];                            
                    }
                    else
                    {
                        editableObject = new ObjectView<T>(item, this);
                        _objectViewCache.Add(item, editableObject);
                        // Listen to the editing notification and property changed events.
                        HookPropertyChangedEvent(editableObject);
                    }
                        
                    // Add the editable object along with the index of the item in the source list.
                    newList.Add(sourceList, editableObject, i);
                }
                else
                {
                    if (_objectViewCache.ContainsKey(item))
                    {
                        editableObject = _objectViewCache[item];
                        UnHookPropertyChangedEvent(editableObject);
                        _objectViewCache.Remove(item);
                    }
                }
            }

            // If we have sorts to apply, do them now
            if (_comparer != null)
            {
                newList.Sort(_comparer);
            }

            // Now we can append any new items to the end of the view.
            foreach (var kvp in _sourceIndices)
            {
                // New items have a source list index of -1 since they are not
                // yet in the source list.
                if (kvp.Value == -1)
                {
                    newList.Add(kvp);
                }
            }

            // Set our view now
            _sourceIndices = newList;

            // Note: We do not raise the ListChanged event with ListChangeType.Reset
            // since the view may not have changed that much. It is better to let
            // the calling code decide what has happened and raise events accordingly.
        }

        /// <summary>
        /// Event handler for when a source list changes.
        /// </summary>
        private void SourceListChanged(object sender, ListChangedEventArgs e)
        {
            int oldIndex;
            int newIndex;
            var sourceList = sender as IBindingList;
            switch (e.ListChangedType)
            {
                case ListChangedType.ItemAdded:
                    FilterAndSort();
                    // Get the index of the newly sorted item
                    newIndex = _sourceIndices.IndexOfSourceIndex(sourceList, e.NewIndex);
                    if (newIndex > -1)
                    {
                        OnListChanged(ListChangedType.ItemAdded, newIndex);
                        // Other items have moved down the list
                        for (var i = newIndex + 1; i < Count; i++)
                        {
                            OnListChanged(ListChangedType.ItemMoved, i - 1, i);
                        }
                    }
                    else
                    {
                        // The item was excluded by the filter,
                        // so to the viewer the item has been "deleted".
                        // The new item will have been added at the end of the view
                        OnListChanged(ListChangedType.ItemDeleted, Math.Max(Count - 1, 0));
                    }
                    break;

                case ListChangedType.ItemChanged:
                    // Check if filtering will remove the item from view
                    // by getting the index before and after
                    oldIndex = _sourceIndices.IndexOfSourceIndex(sourceList, e.NewIndex);

                    // Is the object in our view?
                    if (oldIndex < 0)
                    {
                        return;
                    }

                    FilterAndSort();
                    newIndex = _sourceIndices.IndexOfSourceIndex(sourceList, e.NewIndex);
                    // if item was filtered out then the newIndex == -1
                    // otherwise we can say that the item was changed.
                    if (newIndex > -1)
                    {
                        if (newIndex == oldIndex)
                        {
                            OnListChanged(ListChangedType.ItemChanged, newIndex);
                        }
                        else
                        {
                            // Two items will have changed places
                            OnListChanged(ListChangedType.ItemMoved, newIndex, oldIndex);
                        }
                    }
                    else
                    {
                        OnListChanged(ListChangedType.ItemDeleted, oldIndex);
                    }
                    break;

                case ListChangedType.ItemDeleted:
                    // Find the deleted index
                    newIndex = _sourceIndices.IndexOfSourceIndex(sourceList, e.NewIndex);

                    // Did we have the object in our view?
                    if (newIndex < 0)
                    {
                        return;
                    }

                    // Stop listening to it's events
                    UnHookPropertyChangedEvent(_sourceIndices[newIndex].Key.Item);
                    // Remove its index
                    _sourceIndices.RemoveAt(newIndex);
                    // Move up indices after removed item
                    for (var i = 0; i < _sourceIndices.Count; i++)
                    {
                        if (_sourceIndices[i].Value > e.NewIndex)
                        {
                            _sourceIndices[i] = new KeyValuePair<ListItemPair<T>, int>(_sourceIndices[i].Key, _sourceIndices[i].Value - 1);
                        }
                    }
                    // Inform listeners that an item has been deleted from this view
                    OnListChanged(ListChangedType.ItemDeleted, newIndex);
                    break;

                case ListChangedType.ItemMoved:
                    if (!IsSorted && (Filter is IncludeAllItemFilter<T>))
                    {
                        // We can move the item in the view
                        // note indicies match those in _sourceList
                        OnListChanged(ListChangedType.ItemMoved, e.NewIndex, e.OldIndex);
                    }
                    // Otherwise it makes no sense to move due to sort and/or filter
                    break;

                case ListChangedType.Reset:
                    // Most of the source list has changed
                    // so re-sort and filter
                    FilterAndSort();
                    // The view is most likely to have changed lots as well
                    OnListChanged(ListChangedType.Reset, -1);
                    break;
            }
        }

        /// <summary>
        /// Event handler for when an item in the view changes.
        /// </summary>
        /// <param name="sender">The item that changed.</param>
        private void ItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // The changed item may not actually be present in the view
            var index = _sourceIndices.IndexOfItem((T)sender);
            // Test the returned index, -1 => not in the view.
            if (index > -1)
            {
                // Tell listeners that an item has changed.
                // This is inline with the IRaiseItemChangedEvents implementation.
                OnListChanged(ListChangedType.ItemChanged, index);
            }
        }

        #region ListChanged Event

        /// <summary>
        /// Occurs when the list changes or an item in the list changes.
        /// </summary>
        public event ListChangedEventHandler ListChanged;

        /// <summary>
        /// Raises the ListChanged event with the given event arguments.
        /// </summary>
        /// <param name="e">The ListChangedEventArgs to raise the event with.</param>
        protected virtual void OnListChanged(ListChangedEventArgs e)
        {
            if (ListChanged != null)
            {
                // Check if we need to invoke on the UI thread or not
                if (SynchronizingObject != null && SynchronizingObject.InvokeRequired)
                {
                    SynchronizingObject.Invoke(ListChanged, new object[] { this, e });
                }
                else
                {
                    ListChanged(this, e);
                }
            }
        }

        /// <summary>
        /// Helper method to build the ListChangedEventArgs needed for the ListChanged event.
        /// </summary>
        /// <param name="listChangedType">The type of change that  occured.</param>
        /// <param name="newIndex">The index of the changed item.</param>
        private void OnListChanged(ListChangedType listChangedType, int newIndex)
        {
            OnListChanged(new ListChangedEventArgs(listChangedType, newIndex));
        }

        /// <summary>
        /// Helper method to build the ListChangedEventArgs needed for the ListChanged event.
        /// </summary>
        /// <param name="listChangedType">The type of change that  occured.</param>
        /// <param name="newIndex">The index of the item after the change.</param>
        /// <param name="oldIndex">The index of the iem before the change.</param>
        private void OnListChanged(ListChangedType listChangedType, int newIndex, int oldIndex)
        {
            OnListChanged(new ListChangedEventArgs(listChangedType, newIndex, oldIndex));
        }

        #endregion

        #region Filtering

        public void ApplyFilter(IItemFilter<T> filter)
        {
            Filter = filter;
        }

        public void ApplyFilter(Predicate<T> includeItem)
        {
            if (includeItem == null)
            {
                throw new ArgumentNullException("includeItem", "includeDelegate cannot be null.");
            }

            Filter = CreateItemFilter(includeItem);
        }

        /// <summary>
        /// Gets if this view supports filtering of items. Always returns true.
        /// </summary>
        bool IBindingListView.SupportsFiltering
        {
            get { return true; }
        }

        /// <remarks>Explicitly implemented to expose the stronger Filter property instead.</remarks>
        string IBindingListView.Filter
        {
            get
            {
                return Filter.ToString();
            }
            set
            {
                throw new NotSupportedException(@"Cannot set filter from string expression.");
            }
        }

        /// <summary>
        /// Gets or sets the filter currently applied to the view.
        /// </summary>
        public IItemFilter<T> Filter
        {
            get
            {
                return _filter;
            }
            set
            {
                // Do not allow a null filter. Instead, use the "include all items" filter.
                if (value == null) value = IncludeAllItemFilter<T>.Instance;
                if (_filter != value)
                {
                    _filter = value;
                    FilterAndSort();
                    // The list has probably changed a lot, so get bound controls to reset.
                    OnListChanged(ListChangedType.Reset, -1);
                }
            }
        }

        public static IItemFilter<T> CreateItemFilter(Predicate<T> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }
            return new PredicateItemFilter<T>(predicate);
        }

        /// <summary>
        /// Removes any currently applied filter so that all items are displayed by the view.
        /// </summary>
        public void RemoveFilter()
        {
            // Set filter back to including all items.
            Filter = IncludeAllItemFilter<T>.Instance;
        }

        #endregion

        #region Sorting

        /// <summary>
        /// Used to signal that a sort on a property is to be descending, not ascending.
        /// </summary>
        public readonly string SortDescendingModifier = @"DESC";
        /// <summary>
        /// The character used to seperate sorts by multiple properties.
        /// </summary>
        public readonly char SortDelimiter = ',';

        /// <summary>
        /// Gets if this view supports sorting. Always returns true.
        /// </summary>
        bool IBindingList.SupportsSorting
        {
            get { return true; }
        }

        /// <summary>
        /// Gets if this view supports advanced sorting. Always returns true.
        /// </summary>
        bool IBindingListView.SupportsAdvancedSorting
        {
            get { return true; }
        }

        /// <summary>
        /// Sorts the view by a single property in a given direction.
        /// This will remove any existing sort.
        /// </summary>
        /// <param name="property">A property of <typeparamref name="T"/> to sort by.</param>
        /// <param name="direction">The direction to sort in.</param>
        public void ApplySort(PropertyDescriptor property, ListSortDirection direction)
        {
            // Apply sort by setting the current sort descriptions
            // to be a collection containing just one SortDescription.
            SortDescriptions = new ListSortDescriptionCollection(
                new[]
                    {
                        new ListSortDescription(property, direction)
                    });
        }

        /// <summary>
        /// Sorts the view by the given collection of sort descriptions.
        /// </summary>
        /// <param name="sorts">The sorts to apply.</param>
        public void ApplySort(ListSortDescriptionCollection sorts)
        {
            SortDescriptions = sorts;
        }

        public void ApplySort(IComparer<T> comparer)
        {
            if (comparer == null)
            {
                throw new ArgumentNullException("comparer");
            }

            // Clear any current sorts
            _sorts = new ListSortDescriptionCollection();
            // Sort with this new comparer
            _comparer = new ExternalSortComparer<T>(comparer);
            FilterAndSort();
            OnListChanged(ListChangedType.Reset, -1);
        }

        public void ApplySort(Comparison<T> comparison)
        {
            if (comparison == null)
            {
                throw new ArgumentNullException("comparison");
            }

            // Clear any current sorts
            _sorts = new ListSortDescriptionCollection();
            // Sort with this new comparer
            _comparer = new ExternalSortComparison<T>(comparison);
            FilterAndSort();
            OnListChanged(ListChangedType.Reset, -1);
        }

        /// <summary>
        /// Removes any sort currently applied to the view, restoring it to the order of the source list.
        /// </summary>
        public void RemoveSort()
        {
            // An empty collection of sorts will achieve what we need.
            SortDescriptions = new ListSortDescriptionCollection();
        }

        /// <summary>
        /// Gets if the view is currently sorted.
        /// </summary>
        [Browsable(false)]
        public bool IsSorted
        {
            get
            {
                // To be sorted there must be some sorts applied.
                return (SortDescriptions.Count > 0);
            }
        }

        /// <summary>
        /// Gets the direction in which the view is sorted.
        /// If more than one sort is applied, the direction of the first is returned.
        /// </summary>
        [Browsable(false)]
        public ListSortDirection SortDirection
        {
            get
            {
                if (IsSorted)
                {
                    return SortDescriptions[0].SortDirection;
                }
                else
                {
                    // We don't really want to throw exceptions.
                    // Calling code should have checked IsSorted to know the true situation.
                    return ListSortDirection.Ascending;
                }
            }
        }

        /// <summary>
        /// Gets the property the view is currently sorted by.
        /// If more than one sort is applied, the property of the first is returned.
        /// </summary>
        [Browsable(false)]
        public PropertyDescriptor SortProperty
        {
            get
            {
                if (IsSorted)
                {
                    return SortDescriptions[0].PropertyDescriptor;
                }
                else
                {
                    // We don't really want to throw exceptions.
                    // Calling code should have checked IsSorted to know the true situation.
                    return null;
                }
            }
        }

        /// <summary>
        /// Gets the sorts currently applied to the view.
        /// </summary>
        [Browsable(false)]
        public ListSortDescriptionCollection SortDescriptions
        {
            get
            {
                return _sorts;
            }
            private set
            {
                _sorts = value;
                _comparer = new SortComparer(value);
                FilterAndSort();
                // Most of the list will have probably changed, so get bound objects to reset.
                OnListChanged(ListChangedType.Reset, -1);
            }
        }

        /// <summary>
        /// Used to compare items in the view when sorting the _sourceIndices list.
        /// It supports mutliple sorts by different properties and directions.
        /// </summary>
        private class SortComparer : IComparer<KeyValuePair<ListItemPair<T>, int>>
        {
            private Dictionary<ListSortDescription, Comparison<T>> _comparisons;

            /// <summary>
            /// Creates a new SortComparer that will use the given sorts.
            /// </summary>
            /// <param name="sorts">The sorts to apply to the view.</param>
            public SortComparer(ListSortDescriptionCollection sorts)
            {
                _sorts = sorts;

                // Build the delegates used to compare properties of objects
                _comparisons = new Dictionary<ListSortDescription, Comparison<T>>();
                foreach (ListSortDescription sort in sorts)
                {
                    _comparisons[sort] = BuildComparison(sort.PropertyDescriptor.Name, sort.SortDirection);
                }
            }

            private ListSortDescriptionCollection _sorts;

            /// <summary>
            /// Compares two items according to the defined sorts.
            /// </summary>
            /// <remarks>
            /// Use of light-weight code generation comparison delegates gives ~10x speed up
            /// compared to the pure reflection based implementation.
            /// </remarks>
            /// <param name="x">The first item to compare.</param>
            /// <param name="y">The second item to compare.</param>
            /// <returns>-1 if x &lt; y, 0 if x = y and 1 if x &gt; y.</returns>
            public int Compare(KeyValuePair<ListItemPair<T>, int> x, KeyValuePair<ListItemPair<T>, int> y)
            {
                foreach (ListSortDescription sort in _sorts)
                {
                    var result = _comparisons[sort](x.Key.Item.Object, y.Key.Item.Object);
                    if (result != 0)
                    {
                        return result;
                    }
                }
                return 0;
            }

            // Old SLOW version of Compare method
            ///// <summary>
            ///// Compares two items according to the defined sorts.
            ///// </summary>
            ///// <param name="x">The first item to compare.</param>
            ///// <param name="y">The second item to compare.</param>
            ///// <returns>-1 if x &lt; y, 0 if x = y and 1 if x &gt; y.</returns>
            //public int Compare(KeyValuePair<ListItemPair<T>, int> x, KeyValuePair<ListItemPair<T>, int> y)
            //{
            //    foreach (ListSortDescription sort in _sorts)
            //    {
            //        // Get the two values to compare.
            //        object valueX = sort.PropertyDescriptor.GetValue(x.Key.Item);
            //        object valueY = sort.PropertyDescriptor.GetValue(y.Key.Item);

            //        // Special treatment of nulls
            //        if (valueX == null && valueY == null)
            //        {
            //            // null && null are equal, so no sorting applied here
            //            continue;
            //        }
            //        if (valueX == null && valueY != null)
            //        {
            //            // null < object
            //            if (sort.SortDirection == ListSortDirection.Ascending)
            //            {
            //                return -1;
            //            }
            //            else
            //            {
            //                return 1;
            //            }
            //        }
            //        if (valueX != null && valueY == null)
            //        {
            //            // object > null
            //            if (sort.SortDirection == ListSortDirection.Ascending)
            //            {
            //                return 1;
            //            }
            //            else
            //            {
            //                return -1;
            //            }
            //        }

            //        // valueX and valueY are of the same type so if valueX is comparable then so is valueY.
            //        if (valueX is IComparable)
            //        {
            //            int compare = ((IComparable)valueX).CompareTo(valueY);
            //            if (compare < 0)
            //            {
            //                if (sort.SortDirection == ListSortDirection.Ascending)
            //                {
            //                    return -1;
            //                }
            //                else
            //                {
            //                    return 1;
            //                }
            //            }
            //            else if (compare > 0)
            //            {
            //                if (sort.SortDirection == ListSortDirection.Ascending)
            //                {
            //                    return 1;
            //                }
            //                else
            //                {
            //                    return -1;
            //                }
            //            }
            //        }
            //        else
            //        {
            //            if (valueX.Equals(valueY))
            //            {
            //                return 0;
            //            }
            //            else
            //            {
            //                // Last resort.
            //                // Try to compare their string representations
            //                return valueX.ToString().CompareTo(valueY.ToString());
            //            }
            //        }
            //    }
            //    // Exhausted all sort criteria, so objects must be equal (under this sort).
            //    return 0;
            //}

            private static Comparison<T> BuildComparison(string propertyName, ListSortDirection direction)
            {
                var pi = typeof(T).GetProperty(propertyName);
                Debug.Assert(pi != null, string.Format(@"Property '{0}' is not a member of type '{1}'", propertyName, typeof(T).FullName));

                if (typeof(IComparable).IsAssignableFrom(pi.PropertyType))
                {
                    if (pi.PropertyType.IsValueType)
                    {
                        return BuildValueTypeComparison(pi, direction);
                    }
                    else
                    {
                        var getProperty = BuildGetPropertyMethod(pi);
                        return delegate(T x, T y)
                        {
                            int result;
                            var value1 = getProperty(x);
                            var value2 = getProperty(y);
                            if (value1 != null && value2 != null)
                            {
                                result = (value1 as IComparable).CompareTo(value2);
                            }
                            else if (value1 == null && value2 != null)
                            {
                                result = -1;
                            }
                            else if (value1 != null && value2 == null)
                            {
                                result = 1;
                            }
                            else
                            {
                                result = 0;
                            }

                            if (direction == ListSortDirection.Descending)
                            {
                                result *= -1;
                            }
                            return result;
                        };
                    }
                }
                else if (pi.PropertyType.IsGenericType && pi.PropertyType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                {
                    return BuildNullableComparison(pi, direction);
                }
                else
                {
                    return delegate(T o1, T o2)
                    {
                        if (o1.Equals(o2))
                        {
                            return 0;
                        }
                        else
                        {
                            return o1.ToString().CompareTo(o2.ToString());
                        }
                    };
                }
            }

            private delegate object GetPropertyDelegate(T obj);

            private static GetPropertyDelegate BuildGetPropertyMethod(PropertyInfo pi)
            {
                var getMethod = pi.GetGetMethod();
                Debug.Assert(getMethod != null);

                var dm = new DynamicMethod("__blw_get_" + pi.Name, typeof(object), new[] { typeof(T) }, typeof(T), true);
                var il = dm.GetILGenerator();

                il.Emit(OpCodes.Ldarg_0);
                il.EmitCall(OpCodes.Call, getMethod, null);

                // Return the result of the comparison.
                il.Emit(OpCodes.Ret);

                // Create the delegate pointing at the dynamic method.
                return (GetPropertyDelegate)dm.CreateDelegate(typeof(GetPropertyDelegate));
            }

            private static Comparison<T> BuildRefTypeComparison(PropertyInfo pi, ListSortDirection direction)
            {
                var getMethod = pi.GetGetMethod();
                Debug.Assert(getMethod != null);

                var dm = new DynamicMethod("Get" + pi.Name, typeof(int), new[] { typeof(T), typeof(T) }, typeof(T), true);
                var il = dm.GetILGenerator();

                // Get the value of the first object's property.
                il.Emit(OpCodes.Ldarg_0);
                il.EmitCall(OpCodes.Call, getMethod, null);

                // Get the value of the second object's property.
                il.Emit(OpCodes.Ldarg_1);
                il.EmitCall(OpCodes.Call, getMethod, null);

                // Cast the first value to IComparable and call CompareTo,
                // passing the second value as the argument.
                il.Emit(OpCodes.Castclass, typeof(IComparable));
                il.EmitCall(OpCodes.Call, typeof(IComparable).GetMethod("CompareTo"), null);

                // If descending then multiply comparison result by -1
                // to reverse the ordering.
                if (direction == ListSortDirection.Descending)
                {
                    il.Emit(OpCodes.Ldc_I4_M1);
                    il.Emit(OpCodes.Mul);
                }

                // Return the result of the comparison.
                il.Emit(OpCodes.Ret);

                // Create the delegate pointing at the dynamic method.
                return (Comparison<T>)dm.CreateDelegate(typeof(Comparison<T>));
            }

            private static Comparison<T> BuildValueTypeComparison(PropertyInfo pi, ListSortDirection direction)
            {
                var getMethod = pi.GetGetMethod();
                Debug.Assert(getMethod != null);

                var dm = new DynamicMethod("Get" + pi.Name, typeof(int), new[] { typeof(T), typeof(T) }, typeof(T), true);
                var il = dm.GetILGenerator();

                // Get the value of the first object's property.
                il.Emit(OpCodes.Ldarg_0);
                il.EmitCall(OpCodes.Call, getMethod, null);
                // Box the value type
                il.Emit(OpCodes.Box, pi.PropertyType);

                // Get the value of the second object's property.
                il.Emit(OpCodes.Ldarg_1);
                il.EmitCall(OpCodes.Call, getMethod, null);
                // Box the value type
                il.Emit(OpCodes.Box, pi.PropertyType);

                // Cast the first value to IComparable and call CompareTo,
                // passing the second value as the argument.
                il.Emit(OpCodes.Castclass, typeof(IComparable));
                il.EmitCall(OpCodes.Call, typeof(IComparable).GetMethod("CompareTo"), null);

                // If descending then multiply comparison result by -1
                // to reverse the ordering.
                if (direction == ListSortDirection.Descending)
                {
                    il.Emit(OpCodes.Ldc_I4_M1);
                    il.Emit(OpCodes.Mul);
                }

                // Return the result of the comparison.
                il.Emit(OpCodes.Ret);

                // Create the delegate pointing at the dynamic method.
                return (Comparison<T>)dm.CreateDelegate(typeof(Comparison<T>));
            }

            private static Comparison<T> BuildNullableComparison(PropertyInfo pi, ListSortDirection direction)
            {
                var getMethod = pi.GetGetMethod();
                Debug.Assert(getMethod != null);

                //Type nullableType = typeof(Nullable<>).MakeGenericType(pi.PropertyType.GetGenericArguments()[0]);

                var dm = new DynamicMethod("Get" + pi.Name, typeof(int), new[] { typeof(T), typeof(T) }, typeof(T), true);
                var il = dm.GetILGenerator();

                // Get the value of the first object's property.
                il.Emit(OpCodes.Ldarg_0); 
                il.EmitCall(OpCodes.Call, getMethod, null);

                // Get the value of the second object's property.
                il.Emit(OpCodes.Ldarg_1);
                il.EmitCall(OpCodes.Call, getMethod, null);
                
                // Call Nullable.Compare
                il.EmitCall(OpCodes.Call, typeof(Nullable).GetMethod("Compare", BindingFlags.Static | BindingFlags.Public).MakeGenericMethod(pi.PropertyType.GetGenericArguments()[0]), null);

                // If descending then multiply comparison result by -1
                // to reverse the ordering.
                if (direction == ListSortDirection.Descending)
                {
                    il.Emit(OpCodes.Ldc_I4_M1);
                    il.Emit(OpCodes.Mul);
                }

                // Return the result of the comparison.
                il.Emit(OpCodes.Ret);

                // Create the delegate pointing at the dynamic method.
                return (Comparison<T>)dm.CreateDelegate(typeof(Comparison<T>));
            }
        }

        private class ExternalSortComparer<U> : IComparer<KeyValuePair<ListItemPair<U>, int>>
        {
            public ExternalSortComparer(IComparer<U> comparer)
            {
                _comparer = comparer;
            }

            private IComparer<U> _comparer;

            public int Compare(KeyValuePair<ListItemPair<U>, int> x, KeyValuePair<ListItemPair<U>, int> y)
            {
                return _comparer.Compare(x.Key.Item.Object, y.Key.Item.Object);
            }
        }

        private class ExternalSortComparison<U> : IComparer<KeyValuePair<ListItemPair<U>, int>>
        {
            public ExternalSortComparison(Comparison<U> comparison)
            {
                _comparison = comparison;
            }

            private Comparison<U> _comparison;

            public int Compare(KeyValuePair<ListItemPair<U>, int> x, KeyValuePair<ListItemPair<U>, int> y)
            {
                return _comparison(x.Key.Item.Object, y.Key.Item.Object);
            }
        }

        #endregion

        #region Searching

        /// <summary>
        /// Gets if this view supports searching using the Find method. Always returns true.
        /// </summary>
        bool IBindingList.SupportsSearching
        {
            get { return true; }
        }

        /// <summary>
        /// Returns the index of the first item in the view who's property equals the given value.
        /// -1 is returned if no item is found.
        /// </summary>
        /// <param name="property">The property of each item to check.</param>
        /// <param name="key">The value being sought.</param>
        /// <returns>The index of the item, or -1 if not found.</returns>
        public int Find(PropertyDescriptor property, object key)
        {
            for (var i = 0; i < _sourceIndices.Count; i++)
            {
                if (property.GetValue(_sourceIndices[i].Key.Item.Object).Equals(key))
                {
                    return i;
                }
            }
            return -1;
        }

        #endregion

        #region IBindingList Members

        /// <summary>
        /// Gets if this view raises the ListChanged event. Always returns true.
        /// </summary>
        bool IBindingList.SupportsChangeNotification
        {
            get { return true; }
        }

        /// <remarks>Explicitly implemented so the type safe AddNew method is exposed instead.</remarks>
        object IBindingList.AddNew()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Gets if this view allows items to be edited.
        /// </summary>
        /// <remarks>Delegates to the source list.</remarks>
        bool IBindingList.AllowEdit
        {
            get
            {
                return !(this.dataSource is IBindingList) || (this.dataSource as IBindingList).AllowEdit;
            }
        }

        /// <summary>
        /// Gets if this view allows new items to be added using AddNew().
        /// </summary>
        /// <remarks>Delegates to the source list.</remarks>
        bool IBindingList.AllowNew
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Gets if this view allows items to be removed.
        /// </summary>
        /// <remarks>Delegates to the source list.</remarks>
        bool IBindingList.AllowRemove
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Not implemented.
        /// </summary>
        /// <exception cref="System.NotImplementedException">Method not implemented.</exception>
        void IBindingList.AddIndex(PropertyDescriptor property)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Not implemented.
        /// </summary>
        /// <exception cref="System.NotImplementedException">Method not implemented.</exception>
        void IBindingList.RemoveIndex(PropertyDescriptor property)
        {
            throw new NotImplementedException();
        }
        
        #endregion

        #region IRaiseItemChangedEvents Members

        /// <summary>
        /// Gets if this view raises the ListChanged event when an item changes. Always returns true.
        /// </summary>
        [Browsable(false)]
        public bool RaisesItemChangedEvents
        {
            get { return true;  }
        }

        #endregion

        #region IList Members

        /// <exception cref="System.ArgumentException">
        /// value is of the wrong type.
        /// </exception>
        /// <exception cref="System.InvalidOperationException">
        /// <see cref="NewItemsList"/> is null, so an item cannot be added.
        /// </exception>
        int IList.Add(object value)
        {
            throw new NotSupportedException("Cannot add an external item to the view. Add to DataSource list instead.");
        }

        /// <summary>
        /// Cannot clear this view.
        /// </summary>
        /// <exception cref="System.ArgumentException">
        /// Cannot clear this view.
        /// </exception>
        void IList.Clear()
        {
            throw new NotSupportedException("Cannot clear this view.");
        }

        /// <summary>
        /// Checks if this view contains the given item.
        /// Note that items excluded by current filter are not searched.
        /// </summary>
        /// <param name="item">The item to search for.</param>
        /// <returns>True if the item is in the view, else false.</returns>
        bool IList.Contains(object item)
        {
            // See if the source indices contain the item
            if (item is ObjectView<T>)
            {
                return _sourceIndices.ContainsKey((ObjectView<T>)item);
            }
            else if (item is T)
            {
                return _sourceIndices.ContainsItem((T)item);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Gets the index in the view of an item.
        /// </summary>
        /// <param name="item">The item to search for</param>
        /// <returns>The index of the item, or -1 if not found.</returns>
        int IList.IndexOf(object item)
        {
            if (item is ObjectView<T>)
            {
                return _sourceIndices.IndexOfKey(item as ObjectView<T>);
            }
            else if (item is T)
            {
                return _sourceIndices.IndexOfItem((T)item);
            }
            return -1;
        }

        /// <summary>
        /// Cannot insert an external item into this collection.
        /// </summary>
        /// <exception cref="System.ArgumentException">
        /// Cannot insert an external item into this collection.
        /// </exception>
        void IList.Insert(int index, object value)
        {
            throw new NotSupportedException("Cannot insert an external item into this collection.");
        }

        /// <summary>
        /// Gets a value indicating if this view is read-only.
        /// </summary>
        /// <remarks>Delegates to the source list.</remarks>
        bool IList.IsReadOnly
        {
            get
            {
                return this.dataSource is IBindingList && (this.dataSource as IBindingList).IsReadOnly;
            }
        }

        /// <summary>
        /// Always returns <code>false</code> because the view can change size when
        /// source lists are added.
        /// </summary>
        bool IList.IsFixedSize
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Removes the given item from the view and underlying source list.
        /// </summary>
        /// <param name="value">Either an ObjectView&lt;T&gt; or T to remove.</param>
        void IList.Remove(object value)
        {
            var index = (this as IList).IndexOf(value);
            (this as IList).RemoveAt(index);
        }

        /// <summary>
        /// Removes the item from the view at the given index.
        /// </summary>
        /// <param name="index">The index of the item to remove.</param>
        void IList.RemoveAt(int index)
        {
            // Get the index in the source list.
            var sourceIndex = _sourceIndices[index].Value;
            var sourceList = _sourceIndices[index].Key.List;
            if (sourceIndex > -1)
            {
                sourceList.RemoveAt(sourceIndex);
                if (!(sourceList is IBindingList) || !(sourceList as IBindingList).SupportsChangeNotification)
                {
                    FilterAndSort();
                    OnListChanged(ListChangedType.Reset, -1);
                }
            }
        }

        /// <summary>
        /// Gets the <see cref="ObjectView&lt;T&gt;"/> at the given index.
        /// </summary>
        /// <param name="index">The index of the item to retrieve.</param>
        /// <returns>An <see cref="ObjectView&lt;T&gt;"/> object.</returns>
        /// <exception cref="System.NotSupportException">
        /// Cannot set an item in the view.
        /// </exception>
        object IList.this[int index]
        {
            get
            {
                return this[index];
            }
            set
            {
                // The interface requires we supply a setter
                // But we don't want external code modifying the view
                // in this manner.
                throw new NotSupportedException("Cannot set an item in the view.");
            }
        }

        #endregion

        #region ICollection Members

        /// <summary>
        /// Copies the <see cref="ObjectView&lt;T&gt;"/> objects of the view to an <see cref="System.Array"/>, starting at a particular System.Array index.
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="System.Array"/> that is the destination of the elements copied from view. The System.Array must have zero-based indexing.</param>
        /// <param name="index">The zero-based index in array at which copying begins. </param>
        void ICollection.CopyTo(Array array, int index)
        {
            _sourceIndices.Keys.CopyTo(array, index);
        }

        /// <summary>
        /// Gets a value indicating whether access to the <see cref="System.Collections.ICollection" /> is synchronized (thread safe).
        /// </summary>
        bool ICollection.IsSynchronized
        {
            get { return false; }
        }

        /// <summary>
        /// Not supported.
        /// </summary>
        object ICollection.SyncRoot
        {
            get { throw new NotSupportedException("Synchronized access to the view is not supported."); }
        }

        /// <summary>
        /// Gets the number of items currently in the view. This does not include those items
        /// excluded by the current filter.
        /// </summary>
        [Browsable(false)]
        public int Count
        {
            get { return _sourceIndices.Count; }
        }

        private IList dataSource;

        [DefaultValue(null)]
        [AttributeProvider(typeof(IListSource))]
        public IList DataSource
        {
            get
            {
                return dataSource;
            }
            set
            {
                this.dataSource = null;

                if (value == null)
                {
                    // Clear all current data
                    this.dataSource = null;
                    this.FilterAndSort();
                    this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
                    return;
                }

                if (!(value is ICollection<T>))
                {
                    // list is not a strongy-type collection.
                    // Check that items in list are all of type T
                    foreach (var item in value)
                    {
                        if (!(item is T))
                        {
                            throw new ArgumentException(string.Format("Item in list is not of type {0}.", typeof(T).FullName), "DataSource");
                        }
                    }
                }

                this.dataSource = value;

                if (value is IBindingList)
                {
                    // We need to know when the source list changes
                    (value as IBindingList).ListChanged += this.SourceListChanged;
                }
                _savedSourceLists.Add(value);
                FilterAndSort();
                OnListChanged(ListChangedType.Reset, -1);
            }
        }

        #endregion

        #region IEnumerable Members

        /// <summary>
        /// Returns an enumerator that iterates through all the <see cref="ObjectView&lt;T&gt;"/> items in the view.
        /// This does not include those items excluded by the current filter.
        /// </summary>
        /// <returns>An IEnumerator to iterate with.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _sourceIndices.GetKeyEnumerator();
        }

        #endregion

        #region ITypedList Members

        /// <summary>
        /// Returns the <see cref="System.ComponentModel.PropertyDescriptorCollection"/> that represents the properties on each item used to bind data.
        /// </summary>
        /// <param name="listAccessors">Array of property descriptors to navigate object hirerachy to actual item object. It can be null.</param>
        /// <returns>The System.ComponentModel.PropertyDescriptorCollection that represents the properties on each item used to bind data.</returns>
        PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] listAccessors)
        {
            PropertyDescriptorCollection originalProps;

            if (this.DataSource is ITypedList)
            {
                // Ask the source list for the properties.
                originalProps = (this.DataSource as ITypedList).GetItemProperties(listAccessors);
            }
            else
            {
                // Get the properties ourself.
                originalProps = System.Windows.Forms.ListBindingHelper.GetListItemProperties(typeof(T), listAccessors);
            }

            if (listAccessors != null && listAccessors.Length > 0)
            {
                var type = originalProps[0].ComponentType;
                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(ObjectView<>))
                {
                    originalProps = originalProps[0].GetChildProperties();
                }
            }

            var newProps = new List<PropertyDescriptor>();
            foreach (PropertyDescriptor pd in originalProps)
            {
                newProps.Add(pd);
            }
            foreach (var pd in GetProvidedViews(originalProps))
            {
                newProps.Add(pd);                
            }
            return new PropertyDescriptorCollection(newProps.ToArray());
        }

        protected internal bool ShouldProvideView(PropertyDescriptor property)
        {
            return ProvidedViewPropertyDescriptor<T>.CanProvideViewOf(property);
        }

        protected internal string GetProvidedViewName(PropertyDescriptor sourceListProperty)
        {
            return sourceListProperty.Name + @"View";
        }

        protected internal object CreateProvidedView(ObjectView<T> @object, PropertyDescriptor sourceListProperty)
        {
            var list = sourceListProperty.GetValue(@object);
            var viewType = GetProvidedViewType(sourceListProperty);
            return Activator.CreateInstance(viewType, list);
        }

        private static Type GetProvidedViewType(PropertyDescriptor sourceListProperty)
        {
            var viewTypeDef = typeof(BindingListView<object>).GetGenericTypeDefinition();
            var typeParam = sourceListProperty.PropertyType.GetGenericArguments()[0];
            var viewType = viewTypeDef.MakeGenericType(typeParam);
            return viewType;
        }

        private IEnumerable<PropertyDescriptor> GetProvidedViews(PropertyDescriptorCollection properties)
        {
            var count = properties.Count;
            for (var i = 0; i < count; i++)
            {
                if (ShouldProvideView(properties[i]))
                {
                    var name = GetProvidedViewName(properties[i]);
                    yield return new ProvidedViewPropertyDescriptor<T>(name, GetProvidedViewType(properties[i]));
                }
            }
        }

        /// <summary>
        /// Gets the name of the view.
        /// </summary>
        /// <param name="listAccessors">Unused. Can be null.</param>
        /// <returns>The name of the view.</returns>
        string ITypedList.GetListName(PropertyDescriptor[] listAccessors)
        {
            return GetType().Name;
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Creates a new <see cref="System.ComponentModel.ListSortDescription"/> for given property name and sort direction.
        /// </summary>
        /// <param name="propertyName">The name of the property to sort by.</param>
        /// <param name="direction">The direction in which to sort.</param>
        /// <returns>A ListSortDescription.</returns>
        /// <remarks>
        /// Used by external code to simplify sorting the view.
        /// </remarks>
        public ListSortDescription CreateListSortDescription(string propertyName, ListSortDirection direction)
        {
            var pd = GetPropertyDescriptor(propertyName);
            if (pd == null)
            {
                throw new ArgumentException(string.Format("Property {0} does not exist in type {1}.", propertyName, typeof(T).FullName), "propertyName");
            }

            return new ListSortDescription(pd, direction);
        }

        /// <summary>
        /// Gets the property descriptor for a given property name.
        /// </summary>
        /// <param name="propertyName">The name of a property of <typeparamref name="T"/>.</param>
        /// <returns>The <see cref="System.ComponentModel.PropertyDescriptor"/>.</returns>
        private PropertyDescriptor GetPropertyDescriptor(string propertyName)
        {
            return TypeDescriptor.GetProperties(typeof(T)).Find(propertyName, false);
        }

        /// <summary>
        /// Attaches an event handler to the <see cref="ObjectView&lt;T&gt;"/>'s PropertyChanged event.
        /// </summary>
        /// <param name="objectView">The <see cref="ObjectView&lt;T&gt;"/> to listen to.</param>
        private void HookPropertyChangedEvent(ObjectView<T> editableObject)
        {
            editableObject.PropertyChanged += this.ItemPropertyChanged;
        }

        /// <summary>
        /// Detaches the event handler from the <see cref="ObjectView&lt;T&gt;"/>'s PropertyChanged event.
        /// </summary>
        /// <param name="objectView">The <see cref="ObjectView&lt;T&gt;"/> to stop listening to.</param>
        private void UnHookPropertyChangedEvent(ObjectView<T> editableObject)
        {
            editableObject.PropertyChanged -= this.ItemPropertyChanged;
        }

        protected IList GetSourceList()
        {
            var obj = DataSource as IListSource;
            if (obj != null)
            {
                if (obj.ContainsListCollection)
                {
                    var list = obj.GetList();
                    if (list != null && list.Count > 0)
                    {
                        list = list[0] as IList;
                        return list;
                    }
                    return null;
                }
                return obj.GetList();
            }

            return DataSource;
        }

        #endregion
        
    }
}
