using System.Collections.Generic;
using UnityEngine;

namespace PivotConnection
{
    public interface IPivotFollower
    {
        protected string PivotKey { get; }
        public Transform PivotTransform { get; protected set; }

        public void SetPivot(IPivotHolder pivotHolder)
        {
            PivotTransform = pivotHolder.GetPivotByKey(PivotKey);
            
        }
    }
}