using UnityEngine;

namespace LOK1game.PlayerDomain
{
    [CreateAssetMenu(fileName = "new MoveData", menuName = "ReCode/Player/MoveData")]
    public class PlayerMovementParams : ScriptableObject
    {
        public float Friction;

        public float WalkGroundAccelerate;
        public float WalkGroundMaxVelocity;

        public float CrouchGroundAccelerate;
        public float CrouchGroundMaxVelocity;

        public float SprintGoundAccelerate;
        public float SprintGoundMaxVelocity;

        public float AirAccelerate;
        public float AirMaxVelocity;
        
        public float JumpForce;
    }
}