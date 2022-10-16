using UnityEngine;
using System;

namespace LOK1game
{
    [Obsolete]
    public abstract class BootstrapBase<T> : MonoBehaviour where T : BootstrapBase<T>
    {
        protected static bool _bootstrapped { get; private set; }

        protected static void Bootstrap(T app)
        {
            if(_bootstrapped)
                throw new ApplicationException($"{app.name} is already bootstrapped!");

            Debug.Log("Bootstrap!");

            var newApp = Instantiate(Resources.Load<T>(app.name));

            if (newApp == null)
                throw new ApplicationException($"object name of {app.name} is not found!");

            newApp.name = app.name;
            newApp.InitializeComponents();

            DontDestroyOnLoad(newApp);

            _bootstrapped = true;
        }

        protected abstract void InitializeComponents();
    }
}