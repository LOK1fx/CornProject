namespace LOK1game.Weapon
{
    public interface IWeapon
    {
        bool CanBeUsed { get; }
        void Use(Player.Player sender);
        void AltUse(Player.Player sender);
        void Equip(Player.Player sender);
    }
}