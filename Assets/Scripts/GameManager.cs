using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{

    public static event System.Action<DamageEvent> OnDamageDealt;

    public static event System.Action<PokemonStats> OnBattleEnded;

    public PokemonLoader loader;
    
    public UIManager uiManager;

    [SerializeField]
    private PokemonStats p1;
    [SerializeField]
    private PokemonStats p2;

    void Start()
    {
        //BeginBattle();
    }

    public void BeginBattle()
    {
        StartCoroutine(StartBattle());
    }
// ---------------- BATTLE SETUP ----------------
    IEnumerator StartBattle()
    {
        uiManager.ShowLoading(true);

        int id1 = Random.Range(1, 1011);
        int id2 = Random.Range(1, 1011);

        yield return loader.LoadPokemon(id1, OnP1Loaded, OnError);
        yield return loader.LoadPokemon(id2, OnP2Loaded, OnError);

        uiManager.SetPokemon1(p1);
        uiManager.SetPokemon2(p2);

        yield return new WaitForSeconds(3f);
        uiManager.ShowLoading(false);


        yield return new WaitForSeconds(1.5f);
        if (p1 != null && p2 != null)
            StartCoroutine(BattleLoop());
    }

    void OnP1Loaded(PokemonStats stats)
    {
        p1 = stats;
    }

    void OnP2Loaded(PokemonStats stats)
    {
        p2 = stats;
    }

    void OnError(string message)
    {
        Debug.LogError(message);
        
    }
// ---------------- BATTLE LOGIC ----------------
    IEnumerator BattleLoop()
    {
        PokemonStats first;
        PokemonStats second;

        
        if (p1.speed >= p2.speed)
        {
            first = p1;
            second = p2;
        }
        else
        {
            first = p2;
            second = p1;
        }

        while (!p1.IsDead && !p2.IsDead)
        {
            yield return Attack(first, second);
            if (second.IsDead)
                break;

            yield return Attack(second, first);
        }

        string winner;

        if (p1.IsDead)
        {
            winner = p2.name;
            if (OnBattleEnded != null)
            {
                OnBattleEnded(p2);
            }
                
        }      
        else
        {
            
            winner = p1.name;
            if (OnBattleEnded != null)
            {
                OnBattleEnded(p1);
            }
                
        }


        uiManager.winnerText.text = winner.ToUpper() + " WINS!";
        Debug.Log(winner + " WINS!");



    }
    // ---------------- ATTACK LOGIC ----------------
    IEnumerator Attack(PokemonStats attacker, PokemonStats defender)
    {
        int damage = attacker.attack - defender.defense / 2;
        if (damage < 1)
            damage = 1;

        defender.currentHP -= damage;
        Debug.Log(attacker.name + " hits " + defender.name + " for " + damage);

         if (OnDamageDealt != null)
            OnDamageDealt(new DamageEvent(attacker, defender, damage));

        yield return new WaitForSeconds(3f);
    }
}
