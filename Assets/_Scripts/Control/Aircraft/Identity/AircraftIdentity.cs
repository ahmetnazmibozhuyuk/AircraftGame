using UnityEngine;

namespace Aircraft.Control
{
    [CreateAssetMenu(fileName = "newAircraft", menuName = "Aircraft Identity/Aircraft Data")]
    public class AircraftIdentity : ScriptableObject
    {
        public float maxSpeed;

        [Range(0.0002f, 0.01f)]
        [Tooltip("How quickly the plane reaches its max speed.")]
        public float accelerationMultiplier;

        [Tooltip("Up - down maneuver multiplier.")]
        public float xTorqueMultiplier = 1;

        [Tooltip("Left - right maneuver multiplier.")]
        public float yTorqueMultiplier = 1;

        [Tooltip("Roll maneuver multiplier.")]
        public float zTorqueMultiplier = 1;

        [Tooltip("How quickly the aircraft readjusts its roll when the input is released.")]
        public float noInputReadjustSpeed = 1;
    }
}
