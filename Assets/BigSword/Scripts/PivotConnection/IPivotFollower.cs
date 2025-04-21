using UnityEngine;

namespace PivotConnection
{
    public interface IPivotFollower
    {
        protected Transform PivotTransform { get; set; }
        public Vector3 Offset { get; }

        public void SetPivot(IPivot pivot) => PivotTransform = pivot.PivotTransform;
    }
}