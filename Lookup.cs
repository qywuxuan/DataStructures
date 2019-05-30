using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.CompilerServices;

namespace StalkerME.DataStructures
{
    public class Lookup<T, U> : CompoundDictionary<T, U, List<U>>
	{
//		public static void ListDictionaryForeach(ListDictionary<T, U> listDictionary, ImprovedDictionary<T, List<U>>.DictionaryForeachHandler handler)
//		{
//			var map = listDictionary.map;
//
//			ImprovedDictionary<T, List<U>>.DictionaryForeach (map, handler);
//		}

		public void Add(T key, U value)
		{
            var list = GetIEnumerable(key);

			if (list.Contains (value))
			{
				//do nothing
			}
			else
			{
				list.Add (value);
			}
		}

		public bool Remove(T key, U item)
		{
			if (map.ContainsKey (key))
			{
                var list = GetIEnumerable(key);

				return list.Remove (item);
			}
			else
			{
				return false;
			}
		}

		public U Find(T key, Predicate<U> match)
		{
			if (map.ContainsKey (key))
			{
                var list = GetIEnumerable(key);;

				return list.Find (match);
			}
			else
			{
				return default(U);
			}
		}

		public bool Contains(T key, U item)
		{
			if (map.ContainsKey (key))
			{
                var list = GetIEnumerable(key);;

				return list.Contains (item);
			}
			else
			{
				return false;
			}
		}
	}

	public static class LookupExpend
	{
		public static void ListDictionaryForeach<T, U>(this Lookup<T, U> listDictionary, ImprovedDictionaryExpend.DictionaryForeachHandler<T, List<U>> handler)
		{
			var map = listDictionary.map;

			map.DictionaryForeach (handler);
		}
	}
}