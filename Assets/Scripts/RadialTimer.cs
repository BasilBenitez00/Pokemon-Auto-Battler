using UnityEngine;
using UnityEngine.UI;

public class RadialTimer : MonoBehaviour
{
    public Image radialImage;
    public float maxTime;

    float timer;

    void Update()
    {
        if (timer < maxTime)
        {
            timer += Time.deltaTime;
            radialImage.fillAmount = timer / maxTime;
        }
    }

    void OnEnable()
    {
        ResetTimer();
    }

    void ResetTimer()
    {
        timer = 0f;
        radialImage.fillAmount = 0f;
    }
}
