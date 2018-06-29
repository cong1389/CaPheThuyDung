using System.Collections.Generic;
using App.Core.Extensions;

namespace App.Framework.Templating.Liquid.Drops
{
    internal class DictionaryDrop : SafeDropBase
    {
        private readonly IDictionary<string, object> _inner;

        public DictionaryDrop(IDictionary<string, object> data)
        {
            _inner = data;
        }

        public override bool ContainsKey(object key)
        {
            return key is string s && _inner.ContainsKey(s);
        }

        protected override object InvokeMember(string name)
        {
            return _inner.Get(name);
        }

        public override object GetWrappedObject()
        {
            return _inner;
        }
    }
}
