using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleEffect : MonoBehaviour
{
    public LayerMask affectedLayers;
    public float range;
    public float AffectSpeed;
    public float suctionForce;
    private List<GameObject> affectedObjects = new List<GameObject>();
    public GameObject BlackHole;
    private void OnEnable()
    {
        Invoke(nameof(DisableObject), 7f);
    }
    private void Start()
    {
       
    }

    private void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, range, affectedLayers);

        foreach (var collider in colliders)
        {
            GameObject affectedObject = collider.gameObject;

            // Skip the object if it is already being affected
            if (affectedObjects.Contains(affectedObject))
                continue;

            affectedObjects.Add(affectedObject);
        }

        foreach (var affectedObject in affectedObjects)
        {
            Vector3 direction = (transform.position - affectedObject.transform.position).normalized;
            affectedObject.transform.position += direction * suctionForce * Time.deltaTime;
          //  affectedObject.transform.localScale = Vector3.Lerp(affectedObject.transform.localScale,Vector3.zero,Time.deltaTime*AffectSpeed);
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
