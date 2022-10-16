namespace LOK1game
{
    public class PlayerController : Controller
    {
        protected override void Awake()
        {
            
        }

        protected override void Update()
        {
            ControlledPawn?.OnInput(this);
        }
    }
}