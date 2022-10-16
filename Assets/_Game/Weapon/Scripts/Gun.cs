using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LOK1game.Tools;
using LOK1game.Player;

namespace LOK1game.Weapon
{
    public class Gun : RaycastGun
    {
        public override void AltUse(Player.Player sender)
        {
            
        }

        public override void Use(Player.Player sender)
        {
            base.Use(sender);

            sender.Camera.AddCameraOffset(-sender.Camera.GetCameraTransform().forward * 0.1f);
            sender.Camera.TriggerRecoil(Data.Recoil);
        }

        public override void Equip(Player.Player sender)
        {
            Debug.Log($"Gun {Data.GunName} equipped");
        }
    }
}