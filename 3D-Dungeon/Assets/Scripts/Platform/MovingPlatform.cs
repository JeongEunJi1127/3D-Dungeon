using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private Vector3 startPos;
    private Transform curPos;

    private bool isRight;

    public float maxMovingDistance;
    public float movingSpeed;

    private void Start()
    {
        startPos = transform.position;
        curPos = transform;
        isRight = true;
    }
    private void Update()
    {
        Moving();
    }

    void Moving()
    {
        // 오른쪽으로 움직여야하면 x값을 줄여야함
        if (isRight)
        {
            curPos.position = new Vector3(curPos.position.x - movingSpeed, transform.position.y,transform.position.z);
            if (startPos.x - curPos.position.x  >= maxMovingDistance) isRight = false;
        }
        else
        {
            curPos.position = new Vector3(curPos.position.x + movingSpeed, transform.position.y, transform.position.z);
            if (startPos.x <= curPos.position.x) isRight = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        collision.transform.parent = curPos;
    }
    private void OnCollisionExit(Collision collision)
    {
        collision.transform.parent = null;
    }
}
