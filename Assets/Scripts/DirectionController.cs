using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionController : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform visualRoot;
    public int facingDirection = 1;
    private Vector3 _originalScale;


    private void Awake()
    {
        if(visualRoot == null)
        {
            visualRoot = transform;
        }

        _originalScale = visualRoot.localScale;
        
    }


    public void FaceDirection(float moveX)
    {
        if(moveX == 0)
        {
            return;
        }

        int newDirection = moveX > 0 ? 1 : -1;

        if(newDirection != facingDirection)
        {
            facingDirection = newDirection;

            Vector3 scale = _originalScale;
            scale.x *= facingDirection;
            visualRoot.localScale = scale;

        }

    }
}
