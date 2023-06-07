using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiTargetMissile : MonoBehaviour
{
    public LayerMask affectedLayers;
    public float range;
    public float MinDistance;
    public float EffectForce;
    public int MaxTargets;
    public List<GameObject> affectedObjects = new List<GameObject>();
    CameraShake cameraShake;
    public LineRenderer LineRenderers;
    public float LineSpeed;
    public float Duration=7;
    private void OnEnable()
    {
        Invoke(nameof(DisableObject), Duration);
    }
    private void Start()
    {
        
      
        cameraShake = FindObjectOfType<CameraShake>();
        cameraShake.Shake(2,0.2f);
    }
    private void Update()
    {
        RenderLines();
        // cameraShake.Shake(2, 0.3f);
       
        PullObjects();
    }
    public void RenderLines()
    {
        affectedObjects = new List<GameObject>();
        Collider[] colliders = Physics.OverlapSphere(transform.position, range, affectedLayers);
        foreach (var collider in colliders)
        {
            GameObject affectedObject = collider.gameObject;

            // Skip the object if it is already being affected
            if (affectedObjects.Contains(affectedObject))
                continue;

            if (affectedObjects.Count < MaxTargets)
                affectedObjects.Add(affectedObject);
        }
        LineRenderers.positionCount = affectedObjects.Count * 2;

        Debug.Log((affectedObjects.Count) * 2);
        for (int i = 0; i < affectedObjects.Count * 2; i++)
        {

            LineRenderers.SetPosition(i, transform.position);
            i++;
            if (LineRenderers.GetPosition(i).normalized.magnitude <= 0)
            {
                LineRenderers.SetPosition(i, transform.position);
            }
            LineRenderers.SetPosition(i, Vector3.Lerp(LineRenderers.GetPosition(i), affectedObjects[i / 2].transform.position, LineSpeed * Time.deltaTime));
        }
        if(affectedObjects.Count<=0)
        {
            LineRenderers.enabled = false;
        }
        else
        {
            LineRenderers.enabled = true;
        }

    }

    public void DisableObject()
    {

        gameObject.SetActive(false);

    }
    private void OnDisable()
    {
        
    }
    public void PullObjects()
    {
        foreach (var item in affectedObjects)
        {
            if(item.GetComponent<Rigidbody>())
            {
                if (Vector3.Distance(transform.position, item.transform.position) >= MinDistance)
                {
                    Rigidbody rb = item.GetComponent<Rigidbody>();

                    rb.velocity = (transform.position - item.transform.position).normalized * EffectForce;
                }
            }
        }
    }
    
}
