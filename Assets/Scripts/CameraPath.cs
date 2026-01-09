using UnityEngine;
using UnityEngine.Splines;

public class CameraPath : MonoBehaviour
{
    [SerializeField] private SplineContainer spline;
    public float speed;
    private float t = 0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector3 pos = spline.EvaluatePosition(t);
        Vector3 pos2 = spline.EvaluatePosition(t + 0.01f);
        t += Time.fixedDeltaTime * speed / 100;
        transform.position = pos;
        transform.LookAt(pos2);
    }
}
