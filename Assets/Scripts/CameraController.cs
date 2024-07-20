using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 touchStart;
    [SerializeField] 
    private float zoomOutMin = 30;
    [SerializeField] 
    private float zoomOutMax = 150;
    [SerializeField] 
    private float dragSpeed = 0.01f;
    [SerializeField] 
    private float zoomSpeed = 0.01f;


    [SerializeField] 
    private float minX = -500f;
    [SerializeField] 
    private float maxX = 500f;
    [SerializeField] 
    private float minY = -500f;
    [SerializeField] 
    private float maxY = 500f;

    void Update()
    {
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            touchStart = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
        }

        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector3 direction = touchStart - Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            Camera.main.transform.position += direction * dragSpeed;
            ClampCamera();
        }

        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;

            Zoom(difference * zoomSpeed);
            ClampCamera();
        }
    }

    void Zoom(float increment)
    {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, zoomOutMin, zoomOutMax);
    }

    void ClampCamera()
    {
        Vector3 pos = Camera.main.transform.position;
        float vertExtent = Camera.main.orthographicSize;
        float horzExtent = vertExtent * Screen.width / Screen.height;

        pos.x = Mathf.Clamp(pos.x, minX + horzExtent, maxX - horzExtent);
        pos.y = Mathf.Clamp(pos.y, minY + vertExtent, maxY - vertExtent);

        Camera.main.transform.position = pos;
    }
}
