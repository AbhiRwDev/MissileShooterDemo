using UnityEngine;



public class Missile : MonoBehaviour
{
    public float speed = 5f;
    public enum Missiletype
    {
        Fire,
        Chain,
        BlackHole
    }
    public Missiletype missileType;
    public float rotationSpeed = 10f;
    
    [SerializeField] private Vector3[] path;
    public int currentPathIndex = 0;
    public Vector3 startPos;
    public Vector3 targetPos;
    public float curveMagnitude;
    public int resolution;
    public Transform TargetIndicator;
    
    public bool bHasCollided=false;
    public GameObject Effect;
    private void OnEnable()
    {
        currentPathIndex = 0;
    }
    private void Start()
    {
        startPos = transform.position;
        
        GeneratePath(resolution);
        
     
    }

  

    private void GeneratePath(int resolution)
    {
        path = new Vector3[resolution];

        path[0] = startPos;
        path[resolution - 1] = targetPos;

        for (int i = 1; i < resolution - 1; i++)
        {
            float t = (float)i / (resolution - 1);
            path[i] = CalculateCurvePoint(t);
           
        }
    }

    private Vector3 CalculateCurvePoint(float t)
    {
        Vector3 point = Vector3.Lerp(startPos, targetPos, t);
        float yOffset = Mathf.Sin(t * Mathf.PI) * curveMagnitude;
        point += Vector3.up * yOffset;
        return point;
    }

    private void Update()
    {
        if (!bHasCollided)
        {
            TargetIndicator.transform.position = targetPos+new Vector3(0,0.5f,0);
            TargetIndicator.rotation = Quaternion.Euler(0,0,0);
          //  GeneratePath(resolution);
            if (path == null || path.Length == 0)
                return;

            MoveAlongPath();
        }
    }

    private void MoveAlongPath()
    {
        Vector3 targetPosition = path[currentPathIndex];
        Vector3 direction = (targetPosition - transform.position).normalized;


        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotationSpeed);
        
        float distanceToTarget = Vector3.Distance(transform.position, targetPosition);
        if (distanceToTarget <= 0.5f)
        {
            currentPathIndex++;

            if (currentPathIndex >= path.Length)
            {
                // Reached the end of the path
                return;
            }
        }
        

        transform.position += direction * speed * Time.deltaTime;

        
    }
    private void OnTriggerEnter(Collider other)
    {
            bHasCollided = true;
            CreateEffects();
            
            Disable();
           
        
    }
   
    public void Disable()
    {
        bHasCollided = false;
        switch (missileType)
        {
            case Missiletype.Fire:
                Objectpool.ObjectPool.AddToPool(Objectpool.ObjectType.FireMissile, gameObject);
                break;
            case Missiletype.Chain:
                Objectpool.ObjectPool.AddToPool(Objectpool.ObjectType.ChainMissile, gameObject);
                break;
            case Missiletype.BlackHole:
                Objectpool.ObjectPool.AddToPool(Objectpool.ObjectType.BlackHoleMissile, gameObject);
                break;
        }
    }
    public void CreateEffects()
    {
        GameObject G = null;
        switch (missileType)
        {
            case Missiletype.Fire:
                G = Objectpool.ObjectPool.GetFromPool(Objectpool.ObjectType.FireEffects);
                G.transform.position = transform.position;
                G.transform.rotation = Quaternion.identity;
                G.SetActive(true);
                
                break;
            case Missiletype.Chain:
                G = Objectpool.ObjectPool.GetFromPool(Objectpool.ObjectType.ChainEffects);
                G.transform.position = transform.position;
                G.transform.rotation = Quaternion.identity;
                G.SetActive(true);
                break;
            case Missiletype.BlackHole:
                G = Objectpool.ObjectPool.GetFromPool(Objectpool.ObjectType.BlackHoleEffects);
                G.transform.position = transform.position;
                G.transform.rotation = Quaternion.identity;
                G.SetActive(true);
                break;
        }
    }
  
}



