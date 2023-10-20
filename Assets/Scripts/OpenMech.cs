using UnityEngine;

public class OpenMech : MonoBehaviour
{
    private LineRenderer line;
    public GameObject Ball;
    public Animator Portal;
    public GameObject PortalCam;
    private float timerCamClose = 3.5f;
    public GameObject PortalEff1;
    public GameObject PortalEff2;
    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {      
        DrawLine();
        OpenPortal();
    }

    public void DrawLine()
    {
        line.SetPosition(0, transform.position);
        line.SetPosition(1, Ball.transform.position);
    }

    public void OpenPortal()
    {
        float distance = Vector3.Distance(transform.position, Ball.transform.position);
        if (distance >= 3)
        {
            PortalCam.SetActive(true);
            Portal.enabled = true;           
        }
        if(Portal.enabled)
        {
            if (timerCamClose < 0)
            {
                PortalCam.SetActive(false);
            }
            else
            {
                if(timerCamClose < 1.4f)
                {
                    PortalEff1.SetActive(true);
                    PortalEff2.SetActive(true);
                }
                timerCamClose -= Time.deltaTime;
            }
        }
            
    }
}
