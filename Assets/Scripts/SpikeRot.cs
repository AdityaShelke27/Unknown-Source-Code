using UnityEngine;

public class SpikeRot : MonoBehaviour
{
    private Transform Spike, SparkUp, SparkDown;
    // Start is called before the first frame update
    void Start()
    {
        Spike = transform.GetChild(2);
        SparkUp = transform.GetChild(0);
        SparkDown = transform.GetChild(1);
        
    }

    // Update is called once per frame
    void Update()
    {
        SparkUp.localPosition = new Vector3(Spike.localPosition.x, SparkUp.localPosition.y, SparkUp.localPosition.z);
        SparkDown.localPosition = new Vector3(Spike.localPosition.x, SparkDown.localPosition.y, SparkDown.localPosition.z);      
    }
}
