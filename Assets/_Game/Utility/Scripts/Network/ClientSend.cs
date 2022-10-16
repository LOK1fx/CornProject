using UnityEngine;

namespace LOK1game.Networking
{
    public class ClientSend
    {
        private static void SendTCPData(Packet packet)
        {
            packet.WriteLength();

            Client.Instance.Tcp.SendData(packet);
        }

        private static void SendUDPData(Packet packet)
        {
            packet.WriteLength();

            Client.Instance.Udp.SendData(packet);
        }

        #region Packets

        public static void WelcomeReceived()
        {
            using (Packet packet = new Packet((int)EClientPackets.WelcomeReceived))
            {
                packet.Write(Client.Instance.LocalId);
                packet.Write("Player" + Random.Range(0, 15));

                SendTCPData(packet);
            }
        }

        public static void PlayerMovement(Vector3 position, Quaternion rotation)
        {
            using (Packet packet = new Packet((int)EClientPackets.PlayerMovement))
            {
                packet.Write(position);
                packet.Write(rotation);

                SendTCPData(packet);
            }
        }

        #endregion
    }
}