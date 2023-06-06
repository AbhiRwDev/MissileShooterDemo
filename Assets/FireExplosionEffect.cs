using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExplosionEffect : MonoBehaviour
{
    public LayerMask affectedLayers;
    public float range;
    public float EffectForce;
    
    private List<GameObject> affectedObjects = new List<GameObject>();
    public CameraShake cameraShake;
    private void OnEnable()
    {
        Invoke(nameof(DisableObject), 7f);
    }
    private void Start()
    {
        cameraShake = FindObjectOfType<CameraShake>();
        Explode();
    }

    public void Explode()
    {
        cameraShake.Shake(2,0.3f);
        Collider[] colliders = Physics.OverlapSphere(transform.position, range, affectedLayers);
        foreach (var collider in colliders)
        {
            GameObject affectedObject = collider.gameObject;

            // Skip the object if it is already being affected
            if (affectedObjects.Contains(affectedObject))
                continue;

            affectedObjects.Add(affectedObject);
        }
        foreach (var item in colliders)
        {
            if(item.GetComponent<Rigidbody>()!=null)
            {
                item.GetComponent<Rigidbody>().AddForce((item.transform.position-transform.position).normalized*EffectForce,ForceMode.Impulse);
            }
        }
    }
    public void DisableObject()
    {

        gameObject.SetActive(false);

    }
    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.SetActive(false);
    }
}
