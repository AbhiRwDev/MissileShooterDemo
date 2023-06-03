using UnityEngine;

public class Missile : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 10f;

    [SerializeField] private Vector3[] path;
    private int currentPathIndex = 0;
    public Vector3 startPos;
    public Vector3 targetPos;
    public float curveMagnitude;
    public int resolution;
    public Transform targetpos1;
    public GameObject[] sphere50;
   
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
            sphere50[i].transform.position = path[i];
        }
    }

    private Vector3 CalculateCurvePoint(float t)
    {
        // Calculate the interpolated position along the curve using t
        Vector3 point = Vector3.Lerp(startPos, targetPos, t);
        
        // Add a curve offset to the y-axis based on t to create a curved trajectory
        float yOffset = Mathf.Sin(t * Mathf.PI) * curveMagnitude;
        point += Vector3.up * yOffset;
      
        return point;
    }

    private void Update()
    {
        targetPos = targetpos1.position;
         GeneratePath(resolution);
        if (path == null || path.Length == 0)
            return;

        MoveAlongPath();
    }

    private void MoveAlongPath()
    {
        Vector3 targetPosition = path[currentPathIndex];
        Vector3 direction = (targetPosition - transform.position).normalized;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        float distanceToTarget = Vector3.Distance(transform.position, targetPosition);
        if (distanceToTarget <= 0.3f)
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

    private void OnCollisionEnter(Collision collision)
    {
        DestroyMissile();
    }

    private void DestroyMissile()
    {
        Debug.Log("Destroyed");
        ExplosionEffect();
        Destroy(gameObject);
    }

    private void ExplosionEffect()
    {
        // Add any explosion effect logic here
    }
}
