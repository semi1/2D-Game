using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class TrapScript : MonoBehaviour
{

    public Transform[] points;
    public int Indexpoint = 0;
    public float speed = 3f;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, points[Indexpoint].position, speed * Time.deltaTime);

        
        if (Vector2.Distance(transform.position, points[Indexpoint].position) < 0.1f)
        {
            Indexpoint++;
            if (Indexpoint == points.Length)
            {
                Indexpoint = 0;
            }
        }
       
    }
}
