using UnityEngine;
using LOK1game.Tools;

namespace LOK1game
{
    public enum EDamageType : ushort
    {
        Normal = 1,
        Lazer,
        Void,
        Hit,
        Drill,
    }

    public struct Damage
    {
        public Actor Sender { get; set; }
        public EDamageType DamageType { get; set; }
        public int Value { get; set; }

        public Vector3 HitPoint { get; set; }
        public Vector3 HitNormal { get; set; }

        public Damage(int value)
        {
            Sender = null;
            Value = value;
            DamageType = EDamageType.Normal;

            HitPoint = Vector3.zero;
            HitNormal = Vector3.zero;
        }

        public Damage(int value, EDamageType type)
        {
            Sender = null;
            Value = value;
            DamageType = type;

            HitPoint = Vector3.zero;
            HitNormal = Vector3.zero;
        }

        public Damage(int value, EDamageType type, Actor sender)
        {
            Sender = sender;
            Value = value;
            DamageType = type;

            HitPoint = Vector3.zero;
            HitNormal = Vector3.zero;
        }

        public Damage(int value, Actor sender)
        {
            Sender = sender;
            Value = value;
            DamageType = EDamageType.Normal;

            HitPoint = Vector3.zero;
            HitNormal = Vector3.zero;
        }

        public Vector3 GetHitDirection(bool drawDebugLines = false)
        {
            if (HitPoint == Vector3.zero)
                return Vector3.zero;

            var originPosition = Sender.transform.position;

            if (drawDebugLines)
            {
                Debug.DrawRay(originPosition, Vector3.up, Color.green, 4f);
                Debug.DrawRay(HitPoint, Vector3.up, Color.red, 4f);
                Debug.DrawLine(originPosition, HitPoint, Color.yellow, 4f);
            }

            return HitPoint.GetDirectionTo(originPosition).normalized;
        }
    }
}