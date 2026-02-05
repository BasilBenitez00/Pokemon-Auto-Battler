using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour, IBattleEventObserver
{
    [Header("Pokemon 1 UI")]
    public Image p1Sprite;
    public Text p1Name;
    public Text p1HP;
    public Text p1Attack;
    public Text p1Defense;
    public Text p1Speed;

    [Header("Pokemon 2 UI")]
    public Image p2Sprite;
    public Text p2Name;
    public Text p2HP;
    public Text p2Attack;
    public Text p2Defense;
    public Text p2Speed;

    [Header("General UI")]
    public GameObject loadingPanel;
    public GameObject winnerPanel;
    public Text errorText;
    public Text damageText;
    public Text winnerText;


    private PokemonStats p1Stats;
    private PokemonStats p2Stats;

    // ---------------- UNITY EVENTS ----------------

    void OnEnable()
    {
        //GameManager.OnDamageDealt += OnDamageDealt;
        //GameManager.OnBattleEnded += HandleWinner;

        GameManager.Instance.RegisterObserver(this);
    }

    void OnDisable()
    {
        //GameManager.OnDamageDealt -= OnDamageDealt;
        //GameManager.OnBattleEnded -= HandleWinner;

        GameManager.Instance.RemoveObserver(this);
    
    }

    // ---------------- PUBLIC API ----------------

    public void ShowLoading(bool show)
    {
        if (loadingPanel != null)
            loadingPanel.SetActive(show);
    }

    public void ShowError(string message)
    {
        if (errorText != null)
            errorText.text = message;
    }

    public void SetPokemon1(PokemonStats stats)
    {
        p1Stats = stats;
        UpdatePokemonUI(stats, p1Sprite, p1Name, p1HP, p1Attack, p1Defense, p1Speed);
    }

    public void SetPokemon2(PokemonStats stats)
    {
        p2Stats = stats;
        UpdatePokemonUI(stats, p2Sprite, p2Name, p2HP, p2Attack, p2Defense, p2Speed);
    }

    // ---------------- INTERNAL ----------------

    void UpdatePokemonUI(PokemonStats stats, Image sprite, Text name,Text hp,Text atk, Text def,Text spd)
    {
        if (stats == null)
            return;

        name.text = stats.name.ToUpper();
        hp.text = "HP: " + stats.currentHP;
        atk.text = "ATK: " + stats.attack;
        def.text = "DEF: " + stats.defense;
        spd.text = "SPD: " + stats.speed;

        if (stats.sprite != null)
            sprite.sprite = stats.sprite;
    }

    public void OnDamageEvent(DamageEvent damageEvent)
    {
        if (damageEvent.defender == p1Stats)
        {
            p1HP.text = "HP: " + p1Stats.currentHP;
        }
        if (damageEvent.defender == p2Stats)
        {
             p2HP.text = "HP: " + p2Stats.currentHP;
        }
           
        damageText.text = damageEvent.attacker.name.ToUpper() + " dealt " + damageEvent.damage + " damage to " + damageEvent.defender.name.ToUpper() + "!";
    }

    public void OnBattleEndEvent(PokemonStats winner)
    {
        damageText.text = "";
        if (winnerPanel != null)
            winnerPanel.SetActive(true);

        if (winnerText != null)
            winnerText.text = winner.name.ToUpper() + " WINS!";
    }
}
