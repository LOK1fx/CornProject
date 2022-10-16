using UnityEngine;

namespace LOK1game.Tools
{
    public enum ETransformCopierMode
    {
        All,
        OnlyRotation,
        OnlyPosition,
        OnlyScale,
        OnlyPositionAndRotation,
        OnlyPositionAndScale,
        OnlyRotationAndScale,
        Inactive,
    }

    public class TransformCopier : MonoBehaviour
    {
        public ETransformCopierMode Mode;
        public Transform Target;

        [Space]
        public Vector3 PositionOffset;
        public Vector3 RotationOffset;
        public Vector3 ScaleOffset;

        private void LateUpdate()
        {
            if(Mode == ETransformCopierMode.Inactive || Target == null) { return; }

            switch (Mode)
            {
                case ETransformCopierMode.All:
                    CopyPosition();
                    CopyRotation();
                    CopyScale();
                    break;
                case ETransformCopierMode.OnlyRotation:
                    CopyRotation();
                    break;
                case ETransformCopierMode.OnlyPosition:
                    CopyPosition();
                    break;
                case ETransformCopierMode.OnlyScale:
                    CopyScale();
                    break;
                case ETransformCopierMode.OnlyPositionAndRotation:
                    CopyPosition();
                    CopyRotation();
                    break;
                case ETransformCopierMode.OnlyPositionAndScale:
                    CopyPosition();
                    CopyScale();
                    break;
                case ETransformCopierMode.OnlyRotationAndScale:
                    CopyRotation();
                    CopyScale();
                    break;
            }
        }

        private void CopyPosition()
        {
            transform.position = Target.position + PositionOffset;
        }

        private void CopyRotation()
        {
            var targetEuler = Target.eulerAngles + RotationOffset;

            transform.rotation = Quaternion.Euler(targetEuler.x, targetEuler.y, targetEuler.z);
        }

        private void CopyScale()
        {
            transform.localScale = Target.localScale + ScaleOffset;
        }
    }
}