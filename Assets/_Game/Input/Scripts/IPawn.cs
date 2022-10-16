namespace LOK1game
{
    public interface IPawn : IInputabe
    {
        Controller Controller { get; }
        void OnPocces(Controller sender);
        void OnUnpocces();
    }
}