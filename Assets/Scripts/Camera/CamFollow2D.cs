using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow2D : MonoBehaviour
{
    public Transform objectToFollow;
    [SerializeField] private float speed;
    private Vector3 wantedPosition { get => objectToFollow.position + (Vector3.back * 10); set { wantedPosition = wantedPosition; } }
    private float startTime;
    private float distance { get => Mathf.Abs(Vector3.Distance(transform.position, wantedPosition)); }

    private void Start()
    {
        OnPlayerMove(0, Vector2.zero);
    }

    private void FixedUpdate()
    {
        float coveredDist = (Time.time - startTime) * speed;

        float fractionOfDist = coveredDist / distance;

        transform.position = Vector3.Lerp(transform.position, wantedPosition, fractionOfDist);
    }

    public void OnPlayerMove(float p, Vector2 d)
    {
        startTime = Time.time - speed * 2;
    }
}
