using UnityEngine;

public class RotateGrapplingGun : MonoBehaviour
{
    public Gun gun;
    public Grapple grappling;
    public Animator gunRec;
    public Quaternion desiredRot;

    void Update()
    {
        if(!grappling.IsGrappling())
        {
            gun.enabled = true;
            desiredRot = transform.parent.rotation;           
            if (!gunRec.enabled)
            {
                gunRec.enabled = true;               
            }            
            
        }
        else
        {
            if(grappling.ConnectedBody == null)
            {
                gun.enabled = false;
                if (gunRec.enabled)
                {
                    gunRec.enabled = false;
                }
                desiredRot = Quaternion.LookRotation(grappling.GetGrapplePoint() - transform.position);
            }
            else
            {
                gun.enabled = false;
                if (gunRec.enabled)
                {
                    gunRec.enabled = false;                    
                }
                desiredRot = transform.parent.rotation;
            }
            
        }
        transform.rotation = Quaternion.Lerp(transform.rotation, desiredRot, Time.deltaTime * 5f);       
    }

    
}
