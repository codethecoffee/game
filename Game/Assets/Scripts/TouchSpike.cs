using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchSpike : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Destroy player when it comes in contact


        if (collision.gameObject.tag != "Ground" && collision.gameObject.tag != "Spike")
        {
            Debug.Log("Player touched a spike");
            Destroy(collision.transform.root.gameObject);
        }
    }
}
