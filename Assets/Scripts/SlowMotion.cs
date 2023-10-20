using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class SlowMotion : MonoBehaviour
{
    public float SlowTimeAmount = 3;
    public float SlowTimeCoolDown = 0;
    private Vignette SlowTimeEffect;
    public GameObject SlowEffect;
    public bool CanSlowTime = false;
    // Start is called before the first frame update
    void Awake()
    {
        SlowTimeEffect = SlowEffect.GetComponent<PostProcessVolume>().profile.GetSetting<Vignette>();
    }

    // Update is called once per frame
    void Update()
    {
        SlowTime();
        if(Input.GetKeyDown(KeyCode.M))
        {
            if (SlowTimeCoolDown <= 0)
            {
                CanSlowTime = true;
                SlowTimeCoolDown = 8;
            }
        }
        
    }

    public void SlowTime()
    {
        if (transform.position.y >= 10)
        {
            if (CanSlowTime)
            {
                Time.timeScale = 0.5f;

                SlowTimeEffect.color.value = Color.Lerp(SlowTimeEffect.color.value, Color.red, 0.4f);
                SlowTimeEffect.intensity.value = Mathf.Lerp(SlowTimeEffect.intensity.value, 0.6f, 0.4f);
                SlowTimeEffect.smoothness.value = Mathf.Lerp(SlowTimeEffect.smoothness.value, 0.274f, 0.4f);
            }
        }
        if (SlowTimeAmount <= 0)
        {
            Time.timeScale = 1;
            SlowTimeAmount = 3f;

            SlowTimeEffect.color.value = Color.Lerp(SlowTimeEffect.color.value, Color.green, 1f);
            SlowTimeEffect.intensity.value = Mathf.Lerp(SlowTimeEffect.intensity.value, 0.342f, 1f);
            SlowTimeEffect.smoothness.value = Mathf.Lerp(SlowTimeEffect.smoothness.value, 0.2f, 1f);

            CanSlowTime = false;
        }
        if (Time.timeScale < 1)
        {
            SlowTimeAmount -= Time.deltaTime / Time.timeScale;
        }

        if (SlowTimeCoolDown > 0)
        {
            SlowTimeCoolDown -= Time.deltaTime / Time.timeScale;
        }
    }
}
