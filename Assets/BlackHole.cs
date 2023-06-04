using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    public float Duration;
    public float ShrinkSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Duration>=0)
        {
            Duration -= Time.deltaTime;
        }
        else
        {
            transform.localScale = Vector3.Lerp(transform.localScale,Vector3.zero,Time.deltaTime*ShrinkSpeed);
        }
    }
    
}
