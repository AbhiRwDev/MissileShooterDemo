using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExplosionEffect : MonoBehaviour
{
    public LayerMask affectedLayers;
    public float range;
    public float EffectForce;
    
    private List<GameObject> affectedObjects = new List<GameObject>();
    CameraShake cameraShake;
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
                Rigidbody rb = item.GetComponent<Rigidbody>();
                rb.constraints = RigidbodyConstraints.None;
                //rb.AddForce(((item.transform.position-transform.position).normalized + Vector3.up )* EffectForce,ForceMode.Impulse);
                rb.AddForceAtPosition(((item.transform.position-transform.position).normalized )* EffectForce,rb.transform.position+Vector3.up,ForceMode.Impulse);
                rb.gameObject.GetComponent<EnemyController>().Damage(20);
            }
        }
    }
    public void DisableObject()
    {

        gameObject.SetActive(false);

    }
    

}
