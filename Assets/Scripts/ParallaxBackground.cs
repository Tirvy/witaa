using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour {

    private Vector2 bounds;
    private Vector3 startPos, cameraStartPos;
	
    public GameObject playerCamera;
    public float parallaxEffectX;
    public float parallaxEffectY;


    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        cameraStartPos = playerCamera.transform.position;
        bounds = GetComponent<SpriteRenderer>().bounds.size;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 cameraOffset = playerCamera.transform.position - cameraStartPos; 
        Vector2 dist = new Vector2(cameraOffset.x * parallaxEffectX,
                                   cameraOffset.y * parallaxEffectY);

        transform.position = new Vector3(startPos.x + dist.x, 
                                         startPos.y + dist.y, 
                                         transform.position.z);

        float tempX = (playerCamera.transform.position.x * (1 - parallaxEffectX));
        if (tempX > startPos.x + bounds.x)
        {
            startPos.x += bounds.x;
        }
        else if (tempX < startPos.x - bounds.x)
        {
            startPos.x -= bounds.x;
        }
    }
}
