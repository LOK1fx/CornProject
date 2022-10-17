using UnityEngine;

namespace LOK1game.Weapon
{
    public class Gun : RaycastGun
    {
        public override void Use(Player.Player sender)
        {
            base.Use(sender);

            if (CanBeUsed == false)
                return;

            sender.Camera.AddCameraOffset(-sender.Camera.GetCameraTransform().forward * 0.1f);
            sender.Camera.TriggerRecoil(Data.Recoil);
        }

        public override void AltUse(Player.Player sender)
        {

        }

        public override void Equip(Player.Player sender)
        {
            Debug.Log($"Gun {Data.GunName} equipped");
        }
    }
}