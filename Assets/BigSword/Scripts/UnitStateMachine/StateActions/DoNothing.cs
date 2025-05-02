using UnityEngine;

namespace UnitStateMachine.UpdateActions
{
    [CreateAssetMenu(menuName = "State Machine/Update Actions/Do Nothing")]
    public class DoNothing : StateAction
    {
        public override void DoAction()
        {
        }
    }
}