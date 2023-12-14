using UnityEngine;

namespace BraveBloodMonsterHunt.Utility
{
    public static class AnimationUtility
    {
        private static void AddAnimationEvent(Animator anim, string clipName, string eventFunctionName, float time)
        {
            AnimationClip[] clips = anim.runtimeAnimatorController.animationClips;
            for (int i = 0; i < clips.Length; i++)
            {
                if (clips[i].name == clipName)
                {
                    AnimationEvent animEvent = new AnimationEvent
                    {
                        functionName = eventFunctionName,
                        time = time
                    };
                    clips[i].AddEvent(animEvent);
                    break;
                }
                anim.Rebind();
            }
        }

        private static void CleanAllEvent(Animator anim)
        {
            
        }
    }
}