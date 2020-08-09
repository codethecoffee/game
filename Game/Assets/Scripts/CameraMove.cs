using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform player;
    public float xstart;
    public float xend;
    public float ystart;
    public float yend;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        float xpos = player.position.x;
        float ypos = player.position.y;
        //determine bounds for the camera in the editor
        if (xpos < xstart) xpos = xstart;
        if (xpos > xend) xpos = xend;
        if (ypos < ystart) ypos = ystart;
        if (ypos > yend) ypos = yend;

        
      transform.position = new Vector3(xpos, ypos, -1);
    }
}
