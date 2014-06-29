// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2014. All rights reserved.
// </copyright>
// <summary>
//   SortableBindingList.cs
// </summary>
// ***********************************************************************

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime;

namespace STM.UI.Framework.BL
{
    public class ExtendedBindingList<T>
        : IList<T>,
            ICollection<T>,
            IList,
            ICollection,
            IReadOnlyList<T>,
            IReadOnlyCollection<T>,
            IEnumerable<T>,
            IEnumerable,
            IBindingList,
            ICancelAddNew,
            IRaiseItemChangedEvents
    {
        private const string Performance = "Performance critical to inline this type of method across NGen image boundaries";
        private readonly Dictionary<Type, PropertyComparer<T>> comparers;
        private readonly IList<T> innerList;
        private int addNewPos = -1;

        private bool allowEdit = true;
        private bool allowNew = true;
        private bool allowRemove = true;
        private bool isSorted;
        private PropertyDescriptorCollection itemTypeProperties;
        private int lastChangeIndex = -1;
        private ListSortDirection listSortDirection;
        private AddingNewEventHandler onAddingNew;
        private PropertyChangedEventHandler propertyChangedEventHandler;
        private PropertyDescriptor propertyDescriptor;
        private bool raiseItemChangedEvents;
        private bool userSetAllowNew;

        public ExtendedBindingList()
        {
            this.comparers = new Dictionary<Type, PropertyComparer<T>>();
            this.RaiseListChangedEvents = true;
            this.Initialize();
        }

        public ExtendedBindingList(IList<T> list)
        {
            this.innerList = list.ToList();
            this.comparers = new Dictionary<Type, PropertyComparer<T>>();
            this.RaiseListChangedEvents = true;
            this.Initialize();
        }

        public event AddingNewEventHandler AddingNew
        {
            add
            {
                bool allowNew = this.AllowNew;
                this.onAddingNew += value;
                if (allowNew == this.AllowNew)
                {
                    return;
                }
                this.FireListChanged(ListChangedType.Reset, -1);
            }
            remove
            {
                bool allowNew = this.AllowNew;
                this.onAddingNew -= value;
                if (allowNew == this.AllowNew)
                {
                    return;
                }
                this.FireListChanged(ListChangedType.Reset, -1);
            }
        }

        public event ListChangedEventHandler ListChanged;

        public bool AllowEdit
        {
            [TargetedPatchingOptOut(Performance)]
            get
            {
                return this.allowEdit;
            }
            set
            {
                if (this.allowEdit == value)
                {
                    return;
                }
                this.allowEdit = value;
                this.FireListChanged(ListChangedType.Reset, -1);
            }
        }

        public bool AllowNew
        {
            get
            {
                if (this.userSetAllowNew || this.allowNew)
                {
                    return this.allowNew;
                }

                return this.AddingNewHandled;
            }
            set
            {
                bool allowNew = this.AllowNew;
                this.userSetAllowNew = true;
                this.allowNew = value;
                if (allowNew == value)
                {
                    return;
                }
                this.FireListChanged(ListChangedType.Reset, -1);
            }
        }

        public bool AllowRemove
        {
            [TargetedPatchingOptOut(Performance)]
            get
            {
                return this.allowRemove;
            }
            set
            {
                if (this.allowRemove == value)
                {
                    return;
                }
                this.allowRemove = value;
                this.FireListChanged(ListChangedType.Reset, -1);
            }
        }

        public bool RaiseListChangedEvents { get; set; }

        bool IBindingList.AllowEdit
        {
            [TargetedPatchingOptOut(Performance)]
            get
            {
                return this.AllowEdit;
            }
        }

        bool IBindingList.AllowNew
        {
            [TargetedPatchingOptOut(Performance)]
            get
            {
                return this.AllowNew;
            }
        }

        bool IBindingList.IsSorted
        {
            [TargetedPatchingOptOut(Performance)]
            get
            {
                return this.IsSortedCore;
            }
        }

        ListSortDirection IBindingList.SortDirection
        {
            [TargetedPatchingOptOut(Performance)]
            get
            {
                return this.SortDirectionCore;
            }
        }

        PropertyDescriptor IBindingList.SortProperty
        {
            [TargetedPatchingOptOut(Performance)]
            get
            {
                return this.SortPropertyCore;
            }
        }

        bool IBindingList.SupportsChangeNotification
        {
            [TargetedPatchingOptOut(Performance)]
            get
            {
                return this.SupportsChangeNotificationCore;
            }
        }

        bool IBindingList.SupportsSearching
        {
            [TargetedPatchingOptOut(Performance)]
            get
            {
                return this.SupportsSearchingCore;
            }
        }

        bool IBindingList.SupportsSorting
        {
            [TargetedPatchingOptOut(Performance)]
            get
            {
                return this.SupportsSortingCore;
            }
        }


        bool IRaiseItemChangedEvents.RaisesItemChangedEvents
        {
            get
            {
                return this.raiseItemChangedEvents;
            }
        }

        protected bool IsSortedCore
        {
            get
            {
                return this.isSorted;
            }
        }

        protected ListSortDirection SortDirectionCore
        {
            get
            {
                return this.listSortDirection;
            }
        }

        protected PropertyDescriptor SortPropertyCore
        {
            get
            {
                return this.propertyDescriptor;
            }
        }

        protected virtual bool SupportsChangeNotificationCore
        {
            get
            {
                return true;
            }
        }

        protected bool SupportsSearchingCore
        {
            get
            {
                return true;
            }
        }

        protected bool SupportsSortingCore
        {
            get
            {
                return true;
            }
        }

        private bool AddingNewHandled
        {
            get
            {
                return this.onAddingNew != null && this.onAddingNew.GetInvocationList().Length > 0;
            }
        }

        private bool ItemTypeHasDefaultConstructor
        {
            get
            {
                Type type = typeof(T);
                return type.IsPrimitive ||
                       type.GetConstructor(
                           BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance,
                           null,
                           new Type[0],
                           null) != null;
            }
        }

        public T AddNew()
        {
            return (T)((IBindingList)this).AddNew();
        }

        public virtual void CancelNew(int itemIndex)
        {
            if (this.addNewPos < 0 || this.addNewPos != itemIndex)
            {
                return;
            }
            this.RemoveItem(this.addNewPos);
            this.addNewPos = -1;
        }

        public virtual void EndNew(int itemIndex)
        {
            if (this.addNewPos < 0 || this.addNewPos != itemIndex)
            {
                return;
            }
            this.addNewPos = -1;
        }

        [TargetedPatchingOptOut(Performance)]
        public void ResetBindings()
        {
            this.FireListChanged(ListChangedType.Reset, -1);
        }

        [TargetedPatchingOptOut(Performance)]
        public void ResetItem(int position)
        {
            this.FireListChanged(ListChangedType.ItemChanged, position);
        }

        void IBindingList.AddIndex(PropertyDescriptor prop)
        {
        }

        object IBindingList.AddNew()
        {
            object obj = this.AddNewCore();
            this.addNewPos = obj != null
                ? this.innerList.IndexOf((T)obj)
                : -1;
            return obj;
        }

        [TargetedPatchingOptOut(Performance)]
        void IBindingList.ApplySort(PropertyDescriptor prop, ListSortDirection direction)
        {
            this.ApplySortCore(prop, direction);
        }

        [TargetedPatchingOptOut(Performance)]
        int IBindingList.Find(PropertyDescriptor prop, object key)
        {
            return this.FindCore(prop, key);
        }

        void IBindingList.RemoveIndex(PropertyDescriptor prop)
        {
        }

        [TargetedPatchingOptOut(Performance)]
        void IBindingList.RemoveSort()
        {
            this.RemoveSortCore();
        }

        internal static object CreateInstance(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            return Activator.CreateInstance(
                type,
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance,
                null,
                null,
                null);
        }

        protected virtual object AddNewCore()
        {
            object obj = this.FireAddingNew() ?? CreateInstance(typeof(T));
            this.innerList.Add((T)obj);
            return obj;
        }

        protected void ApplySortCore(PropertyDescriptor property, ListSortDirection direction)
        {
            var itemsList = (List<T>)this.innerList;

            Type propertyType = property.PropertyType;
            PropertyComparer<T> comparer;
            if (!this.comparers.TryGetValue(propertyType, out comparer))
            {
                comparer = new PropertyComparer<T>(property, direction);
                this.comparers.Add(propertyType, comparer);
            }

            comparer.SetPropertyAndDirection(property, direction);
            itemsList.Sort(comparer);

            this.propertyDescriptor = property;
            this.listSortDirection = direction;
            this.isSorted = true;

            this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }

        /// <summary>
        ///     Removes all elements from the collection.
        /// </summary>
        protected void ClearItems()
        {
            this.EndNew(this.addNewPos);
            if (this.raiseItemChangedEvents)
            {
                foreach (T obj in this.innerList)
                {
                    this.UnhookPropertyChanged(obj);
                }
            }
            this.innerList.Clear();
            this.FireListChanged(ListChangedType.Reset, -1);
        }

        protected int FindCore(PropertyDescriptor property, object key)
        {
            int count = this.innerList.Count;
            for (int i = 0; i < count; ++i)
            {
                T element = this.innerList[i];
                if (property.GetValue(element).Equals(key))
                {
                    return i;
                }
            }

            return -1;
        }

        protected void InsertItem(int index, T item)
        {
            this.EndNew(this.addNewPos);
            this.innerList.Insert(index, item);
            if (this.raiseItemChangedEvents)
            {
                this.HookPropertyChanged(item);
            }

            this.FireListChanged(ListChangedType.ItemAdded, index);
        }

        protected virtual void OnAddingNew(AddingNewEventArgs e)
        {
            if (this.onAddingNew == null)
            {
                return;
            }
            this.onAddingNew(this, e);
        }

        protected virtual void OnListChanged(ListChangedEventArgs e)
        {
            if (this.ListChanged == null)
            {
                return;
            }
            this.ListChanged(this, e);
        }

        protected void RemoveItem(int index)
        {
            if (!this.allowRemove && (this.addNewPos < 0 || this.addNewPos != index))
            {
                throw new NotSupportedException();
            }
            this.EndNew(this.addNewPos);
            if (this.raiseItemChangedEvents)
            {
                this.UnhookPropertyChanged(this.innerList[index]);
            }
            this.innerList.RemoveAt(index);
            this.FireListChanged(ListChangedType.ItemDeleted, index);
        }

        protected void RemoveSortCore()
        {
            this.isSorted = false;
            this.propertyDescriptor = null;
            this.listSortDirection = ListSortDirection.Ascending;

            this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }

        protected void SetItem(int index, T item)
        {
            if (this.raiseItemChangedEvents)
            {
                this.UnhookPropertyChanged(this.innerList[index]);
            }
            this.innerList[index] = item;
            if (this.raiseItemChangedEvents)
            {
                this.HookPropertyChanged(item);
            }
            this.FireListChanged(ListChangedType.ItemChanged, index);
        }

        private void Child_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!this.RaiseListChangedEvents)
            {
                return;
            }
            if (sender != null && e != null)
            {
                if (!string.IsNullOrEmpty(e.PropertyName))
                {
                    T obj;
                    try
                    {
                        obj = (T)sender;
                    }
                    catch (InvalidCastException)
                    {
                        this.ResetBindings();
                        return;
                    }

                    int newIndex = this.lastChangeIndex;
                    if (newIndex < 0 || newIndex >= this.innerList.Count || !this.innerList[newIndex].Equals(obj))
                    {
                        newIndex = this.innerList.IndexOf(obj);
                        this.lastChangeIndex = newIndex;
                    }
                    if (newIndex == -1)
                    {
                        this.UnhookPropertyChanged(obj);
                        this.ResetBindings();
                        return;
                    }
                    else
                    {
                        if (this.itemTypeProperties == null)
                        {
                            this.itemTypeProperties = TypeDescriptor.GetProperties(typeof(T));
                        }
                        PropertyDescriptor propDesc = this.itemTypeProperties.Find(e.PropertyName, true);
                        this.OnListChanged(new ListChangedEventArgs(ListChangedType.ItemChanged, newIndex, propDesc));
                        return;
                    }
                }
            }
            this.ResetBindings();
        }

        private object FireAddingNew()
        {
            var e = new AddingNewEventArgs(null);
            this.OnAddingNew(e);
            return e.NewObject;
        }

        private void FireListChanged(ListChangedType type, int index)
        {
            if (!this.RaiseListChangedEvents)
            {
                return;
            }
            this.OnListChanged(new ListChangedEventArgs(type, index));
        }

        private void HookPropertyChanged(T item)
        {
            var notifyPropertyChanged = (object)item as INotifyPropertyChanged;
            if (notifyPropertyChanged == null)
            {
                return;
            }
            if (this.propertyChangedEventHandler == null)
            {
                this.propertyChangedEventHandler = this.Child_PropertyChanged;
            }

            notifyPropertyChanged.PropertyChanged += this.propertyChangedEventHandler;
        }

        private void Initialize()
        {
            this.allowNew = this.ItemTypeHasDefaultConstructor;
            if (!typeof(INotifyPropertyChanged).IsAssignableFrom(typeof(T)))
            {
                return;
            }
            this.raiseItemChangedEvents = true;
            foreach (T obj in this.innerList)
            {
                this.HookPropertyChanged(obj);
            }
        }

        private void UnhookPropertyChanged(T item)
        {
            var notifyPropertyChanged = (object)item as INotifyPropertyChanged;
            if (notifyPropertyChanged == null || this.propertyChangedEventHandler == null)
            {
                return;
            }

            notifyPropertyChanged.PropertyChanged -= this.propertyChangedEventHandler;
        }
    }

    public class SortableBindingList<T> : BindingList<T>
    {
        private readonly Dictionary<Type, PropertyComparer<T>> comparers;
        private bool isSorted;
        private ListSortDirection listSortDirection;
        private PropertyDescriptor propertyDescriptor;

        public SortableBindingList()
            : base(new List<T>())
        {
            this.comparers = new Dictionary<Type, PropertyComparer<T>>();
        }

        public SortableBindingList(IEnumerable<T> enumeration)
            : base(new List<T>(enumeration))
        {
            this.comparers = new Dictionary<Type, PropertyComparer<T>>();
        }

        protected override bool IsSortedCore
        {
            get
            {
                return this.isSorted;
            }
        }

        protected override ListSortDirection SortDirectionCore
        {
            get
            {
                return this.listSortDirection;
            }
        }

        protected override PropertyDescriptor SortPropertyCore
        {
            get
            {
                return this.propertyDescriptor;
            }
        }

        protected override bool SupportsSearchingCore
        {
            get
            {
                return true;
            }
        }

        protected override bool SupportsSortingCore
        {
            get
            {
                return true;
            }
        }

        public void Filter(Func<T, bool> predicate)
        {
            var unfilteredData = this.Items.ToArray();
            /*var itemsToFilter;
            if (unfilteredData != null)
            {
                itemsToFilter = unfilteredData;
            }
            else
            {
                itemsToFilter = Items;
            }*/
        }

        protected override void ApplySortCore(PropertyDescriptor property, ListSortDirection direction)
        {
            var itemsList = (List<T>)this.Items;

            Type propertyType = property.PropertyType;
            PropertyComparer<T> comparer;
            if (!this.comparers.TryGetValue(propertyType, out comparer))
            {
                comparer = new PropertyComparer<T>(property, direction);
                this.comparers.Add(propertyType, comparer);
            }

            comparer.SetPropertyAndDirection(property, direction);
            itemsList.Sort(comparer);

            this.propertyDescriptor = property;
            this.listSortDirection = direction;
            this.isSorted = true;

            this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }

        protected override int FindCore(PropertyDescriptor property, object key)
        {
            int count = this.Count;
            for (int i = 0; i < count; ++i)
            {
                T element = this[i];
                if (property.GetValue(element).Equals(key))
                {
                    return i;
                }
            }

            return -1;
        }

        protected override void RemoveSortCore()
        {
            this.isSorted = false;
            this.propertyDescriptor = base.SortPropertyCore;
            this.listSortDirection = base.SortDirectionCore;

            this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }
    }
}
