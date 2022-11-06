using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Colony : MonoBehaviour
{


    private bool conquered = false;
    private bool triggered = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collision.gameObject.name == "Teddy" && !conquered)
        {
            GameObject.Find("LevelController").GetComponent<LevelController>().StartCubaBattle();
        }
    }

}
