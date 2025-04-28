using UnityEngine;

namespace TheDeveloperTrain.SciFiGuns
{
    /// <summary>
    /// A ScriptableObject defining recoil behavior for a gun.
    /// Includes position and rotation jolts, duration, and curve settings for both recoil and recovery.
    /// </summary>
    [CreateAssetMenu(menuName = "SciFiGuns/Recoil Profile")]
    public class RecoilProfile : ScriptableObject
    {
        [Header("Amplitude Settings")]

        /// <summary>
        /// How much the gun kicks backward (in meters) when firing a single shot.
        /// </summary>
        [Tooltip("How much the gun kicks backward (in meters) when firing a single shot.")]
        public float movementAmplitude = 0.05f;

        /// <summary>
        /// How much the gun rotates (in degrees) when firing a single shot.
        /// </summary>
        [Tooltip("How much the gun rotates (in degrees) when firing a single shot.")]
        public float rotationAmplitude = 2f;


        [Header("Offset Limits")]

        /// <summary>
        /// The maximum total movement (in meters) the gun can reach from stacking recoil.
        /// </summary>
        [Tooltip("The maximum total movement (in meters) the gun can reach from stacking recoil.")]
        public float maxMovementOffset = 0.15f;

        /// <summary>
        /// The maximum total rotation (in degrees) the gun can reach from stacking recoil.
        /// </summary>
        [Tooltip("The maximum total rotation (in degrees) the gun can reach from stacking recoil.")]
        public float maxRotationOffset = 6f;


        [Header("Timing")]

        /// <summary>
        /// Time (in seconds) it takes for the recoil to fully apply after a shot.
        /// </summary>
        [Tooltip("Time (in seconds) it takes for the recoil to fully apply after a shot.")]
        public float recoilDuration = 0.05f;

        /// <summary>
        /// Time (in seconds) it takes for the gun to return to its original position after recoil.
        /// </summary>
        [Tooltip("Time (in seconds) it takes for the gun to return to its original position after recoil.")]
        public float recoveryDuration = 0.2f;


        [Header("Curves")]

        /// <summary>
        /// Controls the interpolation shape of the recoil jolt. X = time (0-1), Y = strength (0-1).
        /// </summary>
        [Tooltip("Controls the interpolation shape of the recoil jolt. X = time (0-1), Y = strength (0-1).")]
        public AnimationCurve recoilCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

        /// <summary>
        /// Controls the interpolation shape of the recovery back to the original state. X = time (0-1), Y = strength (1-0).
        /// </summary>
        [Tooltip("Controls the interpolation shape of the recovery back to the original state. X = time (0-1), Y = strength (1-0).")]
        public AnimationCurve recoveryCurve = AnimationCurve.EaseInOut(0, 1, 1, 0);
    }
}
