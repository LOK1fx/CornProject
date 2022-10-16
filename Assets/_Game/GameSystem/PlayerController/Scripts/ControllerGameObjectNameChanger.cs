using UnityEngine;

namespace LOK1game
{
    [RequireComponent(typeof(Controller))]
    public class ControllerGameObjectNameChanger : MonoBehaviour
    {
        private Controller _controller;
        private Pawn _currentPawn;

        private void Awake()
        {
            _controller = GetComponent<Controller>();
            _controller.OnControlledPawnChanged += OnControlledPawnChanged;
            
            SetActualName();
        }

        private void OnControlledPawnChanged(IPawn pawn)
        {
            if(_controller.ControlledPawn is Pawn == false) { return;;}
            
            _currentPawn = pawn as Pawn;
            
            SetActualName();
        }

        private void SetActualName()
        {
            if(_currentPawn == false) { return; }
            
            gameObject.name = $"{gameObject.name} ({_currentPawn.name})";
        }

        private void OnDestroy()
        {
            _controller.OnControlledPawnChanged -= OnControlledPawnChanged;
        }
    }
}