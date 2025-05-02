using UnityEngine;

namespace UnitStateMachine.UpdateActions
{
    [CreateAssetMenu(menuName = "State Machine/Update Actions/On Danger State Enter")]
    public class OnDangerStateEnter : StateAction
    {
        public override void DoAction()
        {
            StateMachine.DangerStateChanged?.Invoke(true);
        }
    }
}