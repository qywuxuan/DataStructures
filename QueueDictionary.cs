﻿using System.Collections.Generic;
using System;

namespace StalkerME.DataStructures
{
    public class QueueDictionary<T, U> : CompoundDictionary<T, U, Queue<U>>
    {
        enum GetOperationTypeEnum
        {
            Peek,
            Dequeue,
        }

        public void Enqueue(T key, U value)
        {
            var queue = GetIEnumerable(key);

            if (queue.Contains (value))
            {
                //do nothing
            }
            else
            {
                queue.Enqueue (value);
            }
        }

        public U Dequeue(T key)
        {
            return GetOperation(key, GetOperationTypeEnum.Dequeue);
        }

        public U Peek(T key)
        {
            return GetOperation(key, GetOperationTypeEnum.Peek);
        }

        public bool Contains(T key, U item)
        {
            if (ContainsKey (key))
            {
                var queue = GetIEnumerable(key);;

                return queue.Contains (item);
            }
            else
            {
                return false;
            }
        }

        U GetOperation(T key, GetOperationTypeEnum readOperationType)
        {
            if (ContainsKey (key))
            {
                var queue = GetIEnumerable(key);;

                switch (readOperationType)
                {
                    case GetOperationTypeEnum.Peek:
                        return queue.Peek ();
                    case GetOperationTypeEnum.Dequeue:
                        return queue.Dequeue ();
                    default:
                        throw new Exception("Wrong " + typeof(GetOperationTypeEnum));
                }
            }
            else
            {
                throw new InvalidOperationException("Operation is not valid due to the current state of the object");
            }
        }
    }
}