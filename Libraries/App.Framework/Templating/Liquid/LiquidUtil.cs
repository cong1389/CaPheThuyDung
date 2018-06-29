using App.Core.Extensions;
using App.Framework.Templating.Liquid.Drops;
using System;
using System.Collections;
using System.Collections.Generic;

namespace App.Framework.Templating.Liquid
{
    internal static class LiquidUtil
    {
        private static readonly IDictionary<Type, Func<object, object>> TypeWrapperCache
            = new Dictionary<Type, Func<object, object>>();

        internal static object CreateSafeObject(object value)
        {
            if (value == null)
            {
                return null;
            }

            if (value is TestDrop)
            {
                return value;
            }

            var valueType = value.GetType();

            if (!TypeWrapperCache.TryGetValue(valueType, out var fn))
            {
                if (value is IDictionary<string, object>)
                {
                    fn = x => new DictionaryDrop((IDictionary<string, object>)x);
                }
                else if (valueType.IsSequenceType())
                {
                    var genericArgs = valueType.GetGenericArguments();
                    var isEnumerable = genericArgs.Length == 1 && valueType.IsSubClass(typeof(IEnumerable<>));
                    if (isEnumerable)
                    {
                        var seqType = genericArgs[0];
                        if (!IsSafeType(seqType))
                        {
                            fn = x => new EnumerableWrapper((IEnumerable)x);
                        }
                    }
                }
                //else if (valueType.IsPlainObjectType())
                //{
                //    fn = x => new ObjectDrop(x);
                //}

                TypeWrapperCache[valueType] = fn;
            }

            return fn?.Invoke(value) ?? value;
        }

        public static bool IsSafeType(Type type)
        {
            return type.IsPredefinedType();
        }
    }
}
