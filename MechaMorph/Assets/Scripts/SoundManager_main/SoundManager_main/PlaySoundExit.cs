//Author: Small Hedge Games
//Updated: 13/06/2024

using UnityEngine;

namespace TrippleTrinity.MechaMorph.SoundManager_main.SoundManager_main
{
    public class PlaySoundExit : StateMachineBehaviour
    {
        [SerializeField] private SoundType sound;
        [SerializeField, Range(0, 1)] private float volume = 1;
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            SoundManager.PlaySound(sound, null, volume);
        }
    }
}