using UnityEngine;

public class PokemonAPIData
{
    public string name;
    public StatHolder[] stats;

     public PokemonSprite sprites;
}

[System.Serializable]
public class StatHolder
{
    public int base_stat;
    public Stat stat;
}

[System.Serializable]
public class Stat
{
    public string name;
}

[System.Serializable]
public class PokemonSprite
{
    public string front_default;
}