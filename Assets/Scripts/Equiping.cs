using UnityEngine;

public class Equiping : MonoBehaviour
{
    public GameObject[] WeaponHolder;
    public bool GunEquipped;
    public bool SwordEquipped;
    public bool ShotgunEquipped;
    public int currentlyEquipped = 0;
    public Animator gunEquiping, Sword, Shotgun;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SearchWeapon", 0, 0.7f);
        GunEquipped = false;
        SwordEquipped = false;
        ShotgunEquipped = false;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeWeapon();
    }

    public void ChangeWeapon()
    {
        if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if(currentlyEquipped < WeaponHolder.Length)
            {
                currentlyEquipped++;
            }
            else
            {
                currentlyEquipped = 0;
            }
        }
        else if(Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (currentlyEquipped > 0)
            {
                currentlyEquipped--;
            }
            else
            {
                currentlyEquipped = WeaponHolder.Length - 1;
            }
        }
        
    }
    
    public void SearchWeapon()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            WeaponHolder[i] = transform.GetChild(i).gameObject;
        }
    }

    public void SwitchWeapon()
    {
        switch (WeaponHolder[currentlyEquipped].name)
        {
            case "GunEquiping":
                WeaponHolder[currentlyEquipped].GetComponent<Animator>().Play("");
                break;
            default:
                break;
                            
        }
    }
}
