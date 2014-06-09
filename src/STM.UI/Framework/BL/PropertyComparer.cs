// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2014. All rights reserved.
// </copyright>
// <summary>
//   PropertyComparer.cs
// </summary>
// ***********************************************************************

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace STM.UI.Framework.BL
{
    public class PropertyComparer<T> : IComparer<T>
    {
        private readonly IComparer comparer;
        private PropertyDescriptor propertyDescriptor;
        private int reverse;

        public PropertyComparer(PropertyDescriptor property, ListSortDirection direction)
        {
            this.propertyDescriptor = property;
            Type comparerForPropertyType = typeof(Comparer<>).MakeGenericType(property.PropertyType);
            this.comparer =
                (IComparer)
                    comparerForPropertyType.InvokeMember(
                        "Default",
                        BindingFlags.Static | BindingFlags.GetProperty | BindingFlags.Public,
                        null,
                        null,
                        null);
            this.SetListSortDirection(direction);
        }

        public int Compare(T x, T y)
        {
            var xValue = this.propertyDescriptor.GetValue(x);
            var yValue = this.propertyDescriptor.GetValue(y);
            if (!(xValue is IComparable) && !(yValue is IComparable))
            {
                return 0;
            }

            return this.reverse * this.comparer.Compare(xValue, yValue);
        }

        public void SetPropertyAndDirection(PropertyDescriptor descriptor, ListSortDirection direction)
        {
            this.SetPropertyDescriptor(descriptor);
            this.SetListSortDirection(direction);
        }

        private void SetListSortDirection(ListSortDirection direction)
        {
            this.reverse = direction == ListSortDirection.Ascending
                ? 1
                : -1;
        }

        private void SetPropertyDescriptor(PropertyDescriptor descriptor)
        {
            this.propertyDescriptor = descriptor;
        }
    }
}
