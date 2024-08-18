namespace LOK1game.Weapon
{
    public interface IWeapon
    {
        bool CanBeUsed { get; }
        void Use(PlayerDomain.Player sender);
        void AltUse(PlayerDomain.Player sender);
        void Equip(PlayerDomain.Player sender);
    }
}