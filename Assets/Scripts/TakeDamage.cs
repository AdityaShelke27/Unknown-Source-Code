using UnityEngine;
using UnityEngine.UI;

public class TakeDamage : MonoBehaviour
{
    public float Health = 10;
    public Text health_txt;

    // Update is called once per frame
    void Update()
    {
        if ((transform.position.y <= -30) || (Health <= 0))
        {
            Health = 0;
            Ded();
            
        }        
        health_txt.text = Health.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Sword1" || other.gameObject.name == "Sword2")
        {
            TakeDamageFromSword();

        }             
    }

    void TakeDamageFromSword()
    {
        Health--;
    }
    
    void Ded()
    {

        Time.timeScale = 0f;
        
    }


}
