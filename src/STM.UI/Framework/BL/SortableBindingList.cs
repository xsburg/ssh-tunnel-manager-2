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

namespace STM.UI.Framework.BL
{
    public class ExtendedBindingList<T>
        : IList<T>,
            IReadOnlyList<T>,
            IBindingList,
            ICancelAddNew,
            IRaiseItemChangedEvents
    {
        private readonly Dictionary<Type, PropertyComparer<T>> comparers;
        private readonly List<T> innerList;
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
            this.innerList = new List<T>();
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
                var allowNew = this.AllowNew;
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

        public int Count
        {
            get
            {
                return this.GetList().Count;
            }
        }

        private IList<T> GetList()
        {
            return (this.filteredList ?? this.innerList);
        }

        public bool IsFixedSize
        {
            get
            {
                return false;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return this.innerList.IsReadOnly;
            }
        }

        public bool IsSynchronized
        {
            get
            {
                return ((ICollection)this.innerList).IsSynchronized;
            }
        }

        public bool RaiseListChangedEvents { get; set; }

        public object SyncRoot
        {
            get
            {
                return ((ICollection)this.innerList).SyncRoot;
            }
        }

        bool IBindingList.IsSorted
        {
            get
            {
                return this.isSorted;
            }
        }

        ListSortDirection IBindingList.SortDirection
        {
            get
            {
                return this.listSortDirection;
            }
        }

        PropertyDescriptor IBindingList.SortProperty
        {
            get
            {
                return this.propertyDescriptor;
            }
        }

        bool IBindingList.SupportsChangeNotification
        {
            get
            {
                return true;
            }
        }

        bool IBindingList.SupportsSearching
        {
            get
            {
                return true;
            }
        }

        bool IBindingList.SupportsSorting
        {
            get
            {
                return true;
            }
        }


        bool IRaiseItemChangedEvents.RaisesItemChangedEvents
        {
            get
            {
                return this.raiseItemChangedEvents;
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

        public T this[int index]
        {
            get
            {
                return this.GetList()[index];
            }
            set
            {
                this.SetItem(index, value);
            }
        }

        object IList.this[int index]
        {
            get
            {
                return this[index];
            }
            set
            {
                this[index] = (T)value;
            }
        }

        public void Add(T item)
        {
            this.Insert(this.Count, item);
        }

        public int Add(object value)
        {
            this.Add((T)value);
            return this.Count - 1;
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

        public void Clear()
        {
            this.ClearItems();
        }

        public bool Contains(object item)
        {
            return this.Contains((T)item);
        }

        public bool Contains(T item)
        {
            return GetList().Contains(item);
        }

        public void CopyTo(Array array, int index)
        {
            this.CopyTo((T[])array, index);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            GetList().CopyTo(array, arrayIndex);
        }

        public virtual void EndNew(int itemIndex)
        {
            if (this.addNewPos < 0 || this.addNewPos != itemIndex)
            {
                return;
            }
            this.addNewPos = -1;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.GetList().GetEnumerator();
        }

        public int IndexOf(object value)
        {
            return this.IndexOf((T)value);
        }

        public int IndexOf(T item)
        {
            return this.GetList().IndexOf(item);
        }

        public void Insert(int index, object value)
        {
            this.Insert(index, (T)value);
        }

        public void Insert(int index, T item)
        {
            this.EndNew(this.addNewPos);
            this.GetList().Insert(index, item);
            if (this.raiseItemChangedEvents)
            {
                this.HookPropertyChanged(item);
            }

            this.FireListChanged(ListChangedType.ItemAdded, index);
        }

        public void Remove(object value)
        {
            this.Remove((T)value);
        }

        public bool Remove(T item)
        {
            var index = this.IndexOf(item);
            if (index == -1)
            {
                return false;
            }

            this.RemoveItem(index);
            return true;
        }

        public void RemoveAt(int index)
        {
            this.RemoveItem(index);
        }

        public void ResetBindings()
        {
            this.FireListChanged(ListChangedType.Reset, -1);
        }

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
                ? this.GetList().IndexOf((T)obj)
                : -1;
            return obj;
        }

        void IBindingList.ApplySort(PropertyDescriptor prop, ListSortDirection direction)
        {
            this.ApplySortCore(prop, direction);
        }

        int IBindingList.Find(PropertyDescriptor prop, object key)
        {
            return this.FindCore(prop, key);
        }

        void IBindingList.RemoveIndex(PropertyDescriptor prop)
        {
        }

        void IBindingList.RemoveSort()
        {
            this.isSorted = false;
            this.propertyDescriptor = null;
            this.listSortDirection = ListSortDirection.Ascending;

            this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
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
            var obj = this.FireAddingNew() ?? CreateInstance(typeof(T));
            this.Add((T)obj);
            return obj;
        }

        protected void ApplySortCore(PropertyDescriptor property, ListSortDirection direction)
        {
            var propertyType = property.PropertyType;
            PropertyComparer<T> comparer;
            if (!this.comparers.TryGetValue(propertyType, out comparer))
            {
                comparer = new PropertyComparer<T>(property, direction);
                this.comparers.Add(propertyType, comparer);
            }

            comparer.SetPropertyAndDirection(property, direction);

            innerList.Sort(comparer);
            if (filteredList != null)
            {
                innerList.Sort(comparer);
            }

            this.propertyDescriptor = property;
            this.listSortDirection = direction;
            this.isSorted = true;

            this.ResetBindings();
        }

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
            this.filteredList = null;
            this.FireListChanged(ListChangedType.Reset, -1);
        }

        protected int FindCore(PropertyDescriptor property, object key)
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

        private IList<T> filteredList;

        public void Filter(Func<T, bool> predicate)
        {
            this.filteredList = this.innerList.Where(predicate).ToList();
            this.ResetBindings();
        }
    }
}
