using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rigidbody.AddForce(Vector2.left * 1000.0f * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rigidbody.AddForce(Vector2.right * 1000.0f * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            rigidbody.AddForce(Vector2.up * 1000.0f * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            rigidbody.AddForce(Vector2.down * 1000.0f * Time.deltaTime);
        }
    }
}
