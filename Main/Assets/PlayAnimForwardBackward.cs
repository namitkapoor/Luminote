using UnityEngine;

public class PlayAnimationOnButtonPress : MonoBehaviour
{
    public Animator targetAnimator; // Assign this in the inspector
    public string triggerName = "Activate"; // Name of the trigger parameter

    public void PlayAnimation()
    {
        if (targetAnimator)
        {
            targetAnimator.SetTrigger(triggerName);
        }
    }
}
