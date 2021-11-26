using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public const float SPEED = 5.0f;
    private Vector3 _velocity;
    private Reel.OnSlotCollisionWithBottomCallback _onSlotCollisionWithBottomCallback;
    private string _collisionTag; // The tag that which handles the collision
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move(Vector3 distance)
    {
        transform.Translate(distance);
    }

    public void ResetToPosition(Vector3 position)
    {
        transform.position = position;
    }

    public void SetOnCollisionBottomCallback(Reel.OnSlotCollisionWithBottomCallback callback)
    {
        _onSlotCollisionWithBottomCallback = callback;
    }

    public void SetCollisionTag(string tag)
    {
        _collisionTag = tag;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(_collisionTag))
        {
            _onSlotCollisionWithBottomCallback?.Invoke(this);
        }
    }
}
