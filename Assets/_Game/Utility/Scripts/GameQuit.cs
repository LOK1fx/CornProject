using UnityEngine;

namespace LOK1game.Utils
{
    public class GameQuit : MonoBehaviour
    {
        public void Quit()
        {
            App.Quit();
        }

        public void Quit(int exitCode)
        {
            App.Quit(exitCode);
        }
    }
}

