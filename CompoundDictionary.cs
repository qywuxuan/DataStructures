using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.CompilerServices;

namespace StalkerME.DataStructures
{
    /// <summary>
    /// T:KeyType
    /// U:ValueType
    /// V:IEnumerableType
    /// </summary>
    public class CompoundDictionary<T, U, V> where V : class, IEnumerable<U>, new()
    {
        public CompoundDictionary()
        {
            map = new ImprovedDictionary<T, V>();
        }

        protected internal ImprovedDictionary<T, V> map;

        protected V GetIEnumerable(T key)
        {
            V enumerable = null;

            if (map.ContainsKey (key))
            {
                enumerable = map[key];
            }
            else
            {
                enumerable = new V();
                map.Add (key, enumerable);
            }

            return enumerable;
        }
    }
}