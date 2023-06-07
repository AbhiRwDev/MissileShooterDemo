using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    public float Duration;
    public float ShrinkSpeed;
    public Vector3 Scale;

    private float shrinkSpeed;
    private float duration;
    private void Awake()
    {
        Scale = transform.localScale;
    }
    private void Start()
    {
      
        duration = Duration;
        shrinkSpeed = ShrinkSpeed;
    }
    // Update is called once per frame
    void Update()
    {
        if(duration>=0)
        {
            duration -= Time.deltaTime;
        }
        else
        {
            transform.localScale = Vector3.Lerp(transform.localScale,Vector3.zero,Time.deltaTime*shrinkSpeed);
        }
    }
    private void OnDisable()
    {
        transform.localScale = Scale;
        duration = Duration;
        shrinkSpeed = ShrinkSpeed;
    }

}
