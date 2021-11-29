using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public SpriteRenderer spr;
    public const float SPEED = 5.0f;
    private Reel.OnSlotCollisionWithBottomCallback _onSlotCollisionWithBottomCallback;
    private string _collisionTag; // The tag that which handles the collision
    private int _slotIndex;

    private Coroutine _routineFadeEffect;
    public int GetSlotIndex()
    {
        return _slotIndex;
    }

    private void Awake()
    {
        if (spr == null)
        {
            spr = GetComponent<SpriteRenderer>();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.D))
        {
            PlayFadeAnimation();
        }
#endif
    }

    public void OnSpin()
    {
        if (_routineFadeEffect != null)
        {
            StopCoroutine(_routineFadeEffect);
        }
        spr.color = new Color(1f, 1f, 1f, 1.0f);
    }

    // @index: the current index of this slot in reel strip data
    // @spriteSymbol: the current symbol of the slot
    public void SetSlotData(int index, Sprite spriteSymbol)
    {
        _slotIndex = index;
        spr.sprite = spriteSymbol;
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

    public void PlayFadeAnimation()
    {
        if (_routineFadeEffect != null)
        {
            StopCoroutine(_routineFadeEffect);
        }
        _routineFadeEffect = StartCoroutine(RoutineFadeEffect());
    }

    public IEnumerator RoutineFadeEffect()
    {
        while (true)
        {
            spr.color = new Color(1f, 1f, 1f, Mathf.Repeat(Time.time, 1.0f));
            yield return new WaitForEndOfFrame();
        }
    }
}
