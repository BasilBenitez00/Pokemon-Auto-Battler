using UnityEngine;
using UnityEngine.UI;

[System.Serializable]    
public class PokemonStats
{
    public string name;
    public int maxHP;
    public int currentHP;
    public int attack;
    public int defense;
    public int speed;

    public Sprite sprite;

    public bool IsDead
{
    get
    {
        return currentHP <= 0;
    }
}


    public PokemonStats(PokemonAPIData data)
    {
        name = data.name;

        foreach (var s in data.stats)
        {
            switch (s.stat.name)
            {
                case "hp":
                    maxHP = s.base_stat;
                    currentHP = maxHP;
                    break;
                case "attack":
                    attack = s.base_stat;
                    break;
                case "defense":
                    defense = s.base_stat;
                    break;
                case "speed":
                    speed = s.base_stat;
                    break;
            }
        }
    }

   
}

