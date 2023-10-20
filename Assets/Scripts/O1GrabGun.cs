using UnityEngine;

public class O1GrabGun : MonoBehaviour
{
    public GameObject gun, player;
    public Equiping gunEquipped;
    public float range;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null)
        {
            return;
        }
        if (Vector3.Distance(gun.transform.position, player.transform.position) <= range && Input.GetKeyDown(KeyCode.E))
        {
            gun.transform.SetParent(player.transform.GetChild(0));
            gun.GetComponent<Animator>().Play("Gun_Equip");
            gunEquipped.GunEquipped = true;
            this.enabled = false;
        }
    }
}
