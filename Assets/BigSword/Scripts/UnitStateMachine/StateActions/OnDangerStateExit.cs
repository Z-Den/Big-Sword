using UnityEngine;

namespace UnitStateMachine.UpdateActions
{
    [CreateAssetMenu(menuName = "State Machine/Update Actions/On Danger State Exit")]
    public class OnDangerStateExit : StateAction
    {
        public override void DoAction()
        {
            StateMachine.DangerStateChanged?.Invoke(false);
        }
    }
}