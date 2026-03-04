using UnityEngine;

public class ComputerCursorControl : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    void Start()
    {
        cam = Camera.main;
    }
    public GameObject cursor;
    Camera cam;
    public Vector2 offset;
    public float distance;
    public Vector2 min;
    public Vector2 max;
    // Update is called once per frame
    void FixedUpdate()
    {
        //RaycastHit hit;
        Ray ray1 = cam.ScreenPointToRay( Input.mousePosition );
        Vector3 CursorPosition = ray1.GetPoint(distance);
        CursorPosition.y += offset.y;
        CursorPosition.x += offset.x;
        CursorPosition.z = cursor.transform.position.z;
        //screen limits
        if (CursorPosition.y < min.y)
        {
            CursorPosition.y = min.y;
        }
        else if (CursorPosition.y > max.y)
        {
            CursorPosition.y = max.y;
        }
        if (CursorPosition.x < min.x)
        {
            CursorPosition.x = min.x;
        }
        else if (CursorPosition.x > max.x)
        {
            CursorPosition.x = max.x;
        }

        cursor.transform.position = CursorPosition;


        
    }
}
