public class DamageEvent
{
    public PokemonStats attacker;
    public PokemonStats defender;
    public int damage;

    public DamageEvent(PokemonStats attacker,PokemonStats defender,int damage)
    {
        this.attacker = attacker;
        this.defender = defender;
        this.damage = damage;
    }
}

public interface IBattleEventObserver
{
    void OnDamageEvent(DamageEvent damageEvent);
    void OnBattleEndEvent(PokemonStats winner);
}
