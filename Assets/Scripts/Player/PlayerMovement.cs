using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private float pushPower = 100;
    [SerializeField] private float chainPower = 10;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        transform.Rotate(0, 0, Mathf.Abs(rb.velocity.x) + Mathf.Abs(rb.velocity.y), Space.Self);
    }

    public void PushOff(float power, Vector2 direction) => rb.AddForce(direction * power * pushPower);

    public void ThrowChain(Vector2 pos)
    {
        //Debug.Log("CHAAAIIIINNNN");
        //Vector2 direction =  Camera.main.ScreenToWorldPoint(pos) - transform.position;
        //float distance = direction.magnitude;
        //direction /= distance;

        //rb.AddForce(direction * chainPower);
    }
}
