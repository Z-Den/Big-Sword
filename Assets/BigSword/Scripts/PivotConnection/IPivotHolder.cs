using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PivotConnection
{
    public interface IPivotHolder
    {
        public List<Pivot> PivotList { get; }

        public Transform GetPivotByKey(string key)
        {
            return (from pivot in PivotList where pivot.Key == key select pivot.Root).FirstOrDefault();
        }
    }

    [Serializable]
    public struct Pivot
    {
        public string Key;
        public Transform Root;
    }
}