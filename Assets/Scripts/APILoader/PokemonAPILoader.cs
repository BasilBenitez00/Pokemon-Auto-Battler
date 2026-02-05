
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PokemonLoader : MonoBehaviour
{
    private const string API = "https://pokeapi.co/api/v2/pokemon/";
    private const int TIMEOUT = 10;

    public Sprite defaultSprite;

    public IEnumerator LoadPokemon(int id, System.Action<PokemonStats> onSuccess, System.Action<string> onError)
{
    // -------- CHECK INTERNET --------
    if (Application.internetReachability == NetworkReachability.NotReachable)
    {
        if (onError != null)
            onError("No internet connection.");
        yield break;
    }

    UnityWebRequest request = UnityWebRequest.Get(API + id);

    yield return request.SendWebRequest();

    if (request.result != UnityWebRequest.Result.Success)
    {
        Debug.LogWarning("Pokemon ID " + id + " not found. Retrying...");
        if (onError != null)
            onError("Retry");
        yield break;
    }

    PokemonAPIData data = JsonUtility.FromJson<PokemonAPIData>(request.downloadHandler.text);

    if (data == null || data.stats == null)
    {
        if (onError != null)
            onError("Invalid data");
        yield break;
    }

    // -------- GET STATS --------
    PokemonStats stats = new PokemonStats(data);

    // -------- FETCH SPRITE --------
        if (!string.IsNullOrEmpty(data.sprites.front_default))
        {
            UnityWebRequest spriteRequest = UnityWebRequestTexture.GetTexture( data.sprites.front_default);

            spriteRequest.timeout = TIMEOUT;
            yield return spriteRequest.SendWebRequest();

            if (spriteRequest.result == UnityWebRequest.Result.Success)
            {
                Texture2D tex = DownloadHandlerTexture.GetContent(spriteRequest);

                stats.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
            }
            else
            {
                stats.sprite = defaultSprite;
            }
        }
        else
        {
            stats.sprite = defaultSprite;
        }

        if (onSuccess != null)
            onSuccess(stats);
        }

}
