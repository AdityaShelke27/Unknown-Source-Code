using UnityEngine;

public class SpikeDamage : MonoBehaviour
{
    private bool WillTakeDamage;
    public float DamageRate = 2;
    public float DamageCooldown = 1;
    public TakeDamage player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<TakeDamage>();
    }

    // Update is called once per frame
    void Update()
    {
        if (WillTakeDamage)
        {
            if (DamageCooldown > 0)
            {
                player.Health -= DamageRate * Time.deltaTime;
            }
            else
            {
                DamageCooldown = 1;
            }

            DamageCooldown -= Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.name == "Player")
        {
            WillTakeDamage = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.gameObject.name == "Player")
        {
            WillTakeDamage = false;
        }
    }
}
