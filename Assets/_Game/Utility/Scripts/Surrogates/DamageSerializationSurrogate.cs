using UnityEngine;
using System.Runtime.Serialization;

namespace LOK1game.Tools
{
    public class DamageSerializationSurrogate : ISerializationSurrogate
    {
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            var damage = (Damage)obj;
            var hitPoint = damage.HitPoint;

            info.AddValue("d", damage.Value);
            info.AddValue("x", hitPoint.x);
            info.AddValue("y", hitPoint.y);
            info.AddValue("z", hitPoint.z);
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            var damage = (Damage)obj;

            damage.Value = info.GetInt32("d");
            damage.HitPoint = new Vector3((float)info.GetValue("x", typeof(float)), (float)info.GetValue("y", typeof(float)),
                (float)info.GetValue("z", typeof(float)));

            obj = damage;

            return obj;
        }
    }
}