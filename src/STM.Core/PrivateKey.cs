// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   PrivateKey.cs
// </summary>
// ***********************************************************************

using System;
using System.IO;
using System.Text;

namespace STM.Core
{
    public class PrivateKey : IDisposable
    {
        private readonly string content;
        private readonly string filename;

        public PrivateKey(string content)
        {
            this.content = content;
            this.filename = Path.GetTempFileName();
            File.WriteAllText(this.Filename, content, Encoding.ASCII);
        }

        public string Content
        {
            get
            {
                if (this.IsDisposed)
                {
                    throw new ObjectDisposedException("PrivateKey");
                }

                return this.content;
            }
        }

        public string Filename
        {
            get
            {
                if (this.IsDisposed)
                {
                    throw new ObjectDisposedException("PrivateKey");
                }

                return this.filename;
            }
        }

        public bool IsDisposed { get; private set; }

        public void Dispose()
        {
            if (!this.IsDisposed)
            {
                File.Delete(this.Filename);
                this.IsDisposed = true;
            }
        }

        public override bool Equals(object obj)
        {
            var other = obj as PrivateKey;
            if (other == null)
            {
                return false;
            }

            return this.Content.Equals(other.Content);
        }

        public override int GetHashCode()
        {
            return this.Content.GetHashCode();
        }
    }
}
