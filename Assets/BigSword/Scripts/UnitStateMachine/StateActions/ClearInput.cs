using UnityEngine;

namespace UnitStateMachine.UpdateActions
{
    [CreateAssetMenu(menuName = "State Machine/Update Actions/Clear Input")]
    public class ClearInput : StateAction
    {
        public override void DoAction()
        {
            StateMachine.MoveDirection = Vector2.zero;
            StateMachine.Rotation = 0;
        }
    }
}