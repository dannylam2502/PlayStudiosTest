using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reel : MonoBehaviour
{
    [SerializeField]
    private List<Slot> _slots;

    [Header("The position the slot will reset")]
    public Vector3 topPosition;

    public GameObject bottomCollider;

    [SerializeField]
    private Vector3 _slotVelocity;

    public delegate void OnSlotCollisionWithBottomCallback(Slot slot);

    // The current slot on the top of the reel
    private Slot _topSlot;

    // Start is called before the first frame update
    void Start()
    {
        string tag = bottomCollider.tag;
        // set up callback and some data
        foreach (Slot slot in _slots)
        {
            slot.SetOnCollisionBottomCallback(OnSlotCollisionBottom);
            slot.SetCollisionTag(tag);
        }
        _topSlot = _slots[0];
    }

    // Update is called once per frame
    void Update()
    {
        var distance = _slotVelocity * Time.deltaTime;
        foreach (Slot slot in _slots)
        {
            slot.Move(distance);
        }
    }

    public void OnSlotCollisionBottom(Slot slot)
    {
        // reset the position to be on top of the current top slot
        // assign new top slot to this slot
        slot.ResetToPosition(_topSlot.transform.position + new Vector3(0.0f, 2.5f, 0.0f));
        _topSlot = slot;
    }
}
