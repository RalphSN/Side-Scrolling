using UnityEngine;

public class IdleStateLog : StateMachineBehaviour
{
    public override void OnStateEnter(
        Animator animator,
        AnimatorStateInfo stateInfo,
        int layerIndex
    )
    {
        // Debug.Log($"[{Time.time:F3}] Idle 動畫狀態真正進入");
    }
}
