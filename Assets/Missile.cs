using UnityEngine;



public class Missile : MonoBehaviour
{
    public float speed = 5f;
    float timer;
    public float rotationSpeed = 10f;

    [SerializeField] private Vector3[] path;
    private int currentPathIndex = 0;
    public Vector3 startPos;
    public Vector3 targetPos;
    public float curveMagnitude;
    public int resolution;
   
    public GameObject[] sphere50;
    bool bHasCollided=false;
    public GameObject Effect;
   
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
           
            GeneratePath(resolution);
            if (path == null || path.Length == 0)
                return;

            MoveAlongPath();
        }
    }

    private void MoveAlongPath()
    {
        Vector3 targetPosition = path[currentPathIndex];
        Vector3 direction = (targetPosition - transform.position).normalized;


        transform.rotation = Quaternion.LookRotation(direction);
        timer -= Time.deltaTime*speed;
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
        Instantiate(Effect, transform.position, Quaternion.identity);
        DestroyMissile();
    }
   

    private void DestroyMissile()
    {
      
        Destroy(gameObject);
    }
   
}



