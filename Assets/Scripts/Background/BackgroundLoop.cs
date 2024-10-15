using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{
    private Transform cameraTransform;

    private float textureUnitSizeX;
    private float textureUnitSizeY;

    void Start()
    {
        cameraTransform = Camera.main.transform;

        Sprite sprite = GetComponent<SpriteRenderer>().sprite;

        Texture2D texture = sprite.texture;

        textureUnitSizeX = texture.width / sprite.pixelsPerUnit;
        textureUnitSizeY = texture.height / sprite.pixelsPerUnit;
    }

    void FixedUpdate()
    {
        if(Mathf.Abs(cameraTransform.position.x - transform.position.x) >= textureUnitSizeX)
        {
            transform.position = new Vector3(cameraTransform.position.x , transform.position.y);
        }

        if (Mathf.Abs(cameraTransform.position.y - transform.position.y) >= textureUnitSizeY)
        {
            transform.position = new Vector3(transform.position.x, cameraTransform.position.y);
        }
    }

}
