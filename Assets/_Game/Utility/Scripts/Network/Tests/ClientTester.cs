using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LOK1game.Networking;

public class ClientTester : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Client.Instance.ConnectToServer();
        }
    }
}
