using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotate : MonoBehaviour
{
    public float RotateSpeed;
    public float Y;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Abs(transform.rotation.eulerAngles.y-Y)>0.1f)
        {
            Vector3 rot = new Vector3(transform.rotation.eulerAngles.x,Y,transform.rotation.eulerAngles.z);
            
            transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.Euler(rot),RotateSpeed*Time.deltaTime);
        }
        else
        {
            Y = Random.Range(0,360);
            RotateSpeed = Random.Range(2,7);
        }

    }
}
