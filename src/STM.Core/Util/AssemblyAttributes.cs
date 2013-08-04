// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   AssemblyAttributes.cs
// </summary>
// ***********************************************************************

namespace STM.Core.Util
{
    /// <summary>
    ///     A static wrapper for the <c>AssemblyAttributesReader</c> class.
    /// </summary>
    public static class AssemblyAttributes
    {
        private static readonly AssemblyAttributesReader Reader = new AssemblyAttributesReader();

        /// <summary>Gets the company produced the assembly.</summary>
        /// <value>The assembly company.</value>
        public static string AssemblyCompany
        {
            get
            {
                return Reader.AssemblyCompany;
            }
        }

        /// <summary>Gets the assembly configuration.</summary>
        /// <value>The assembly configuration.</value>
        public static string AssemblyConfiguration
        {
            get
            {
                return Reader.AssemblyConfiguration;
            }
        }

        /// <summary>Gets the assembly copyright text.</summary>
        /// <value>The assembly copyright text.</value>
        public static string AssemblyCopyright
        {
            get
            {
                return Reader.AssemblyCopyright;
            }
        }

        /// <summary>Gets the information describing the assembly.</summary>
        /// <value>Information describing the assembly.</value>
        public static string AssemblyDescription
        {
            get
            {
                return Reader.AssemblyDescription;
            }
        }

        /// <summary>Gets the product containing the assembly.</summary>
        /// <value>The product containing the assembly.</value>
        public static string AssemblyProduct
        {
            get
            {
                return Reader.AssemblyProduct;
            }
        }

        /// <summary>Gets the assembly title.</summary>
        /// <value>The assembly title.</value>
        public static string AssemblyTitle
        {
            get
            {
                return Reader.AssemblyTitle;
            }
        }

        /// <summary>Gets the assembly version.</summary>
        /// <value>The assembly version.</value>
        public static string AssemblyVersion
        {
            get
            {
                return Reader.AssemblyVersion;
            }
        }
    }
}
