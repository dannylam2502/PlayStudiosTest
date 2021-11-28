using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reel : MonoBehaviour
{
    public Sprite diamondSpr;
    public Sprite clubsSpr;
    public Sprite heartSpr;
    public Sprite spadesSpr;
    [SerializeField]
    private List<Slot> _slots;

    public GameObject bottomCollider;

    [SerializeField]
    private Vector3 _slotVelocity;

    public delegate void OnSlotCollisionWithBottomCallback(Slot slot);

    [Header("The current slot on the top of the reel, will change at runtime")]
    [SerializeField]
    private Slot _topSlot;
    private List<string> _reelStrip;
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
        // check if they forget to assign the first top slot, we will use the first slot by default
        if (_topSlot == null)
        {
            _topSlot = _slots[0];
        }
    }

    public void LoadReelStrip(List<string> reelStrip)
    {
        _reelStrip = reelStrip;
        // i for reelstrip index, j for slot, loop backward
        for (int i = reelStrip.Count - 1, j = _slots.Count - 1; i >= 0 && j >= 0; i--, j--)
        {
            _slots[j].SetSlotData(i, GetSpriteBySpring(reelStrip[i]));
        }
    }

    private Sprite GetSpriteBySpring(string s)
    {
        if (s.Equals(GameConst.CLUBS_SYMBOL))
        {
            return clubsSpr;
        }
        else if (s.Equals(GameConst.SPADES_SYMBOL))
        {
            return spadesSpr;
        }
        else if (s.Equals(GameConst.DIAMOND_SYMBOL))
        {
            return diamondSpr;
        }
        // Default hearts
        return heartSpr;
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
        if (_topSlot)
        {
            // calculate new index of the next top slot
            int oldIndex = _topSlot.GetSlotIndex();
            int newIndex = (oldIndex >= _reelStrip.Count - 1) ? 0 : (oldIndex + 1);
            Sprite newSprSymbol = GetSpriteBySpring(_reelStrip[newIndex]);
            // reset the position to be on top of the current top slot
            slot.ResetToPosition(_topSlot.transform.position + new Vector3(0.0f, 2.5f, 0.0f));
            slot.SetSlotData(newIndex, newSprSymbol);
            // assign new top slot to this slot
            _topSlot = slot;
        }
    }
}
