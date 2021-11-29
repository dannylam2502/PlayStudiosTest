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
    private Vector3 _currentSlotVelocity;

    public delegate void OnSlotCollisionWithBottomCallback(Slot slot);

    private ReelState _state;
    public ReelState GetReelState() { return _state; }

    [Header("The current slot on the top of the reel, will change at runtime")]
    [SerializeField]
    private Slot _topSlot;
    private List<string> _reelStrip;
    // The index that the reel need to stop from data
    private int _stopIndex;
    // The slot ref that has the index equals _stopIndex;
    private Slot _resultSlot;
    // Cache the layout position
    private List<Vector3> _slotStartPositions = new List<Vector3>();
    // Start is called before the first frame update
    void Start()
    {
        string tag = bottomCollider.tag;
        // set up callback and some data
        foreach (Slot slot in _slots)
        {
            slot.SetOnCollisionBottomCallback(OnSlotCollisionBottom);
            slot.SetCollisionTag(tag);
            _slotStartPositions.Add(slot.transform.position);
        }
        // check if they forget to assign the first top slot, we will use the first slot by default
        if (_topSlot == null)
        {
            _topSlot = _slots[0];
        }
    }

    public void SetStopIndex(int index)
    {
        _stopIndex = index;
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
        if (_state == ReelState.kSpinning)
        {
            _currentSlotVelocity = _slotVelocity;
            MoveSlots(_currentSlotVelocity);
        }
        else if (_state == ReelState.kSpinToResult)
        {
            // Spin until the current top slot's index reach stop index
            if (_topSlot && _topSlot.GetSlotIndex() == _stopIndex)
            {
                // Set to phase 2
                SetState(ReelState.kSpinToResult2);
                _resultSlot = _topSlot;
            }
            else
            {
                MoveSlots(_currentSlotVelocity);
            }
        }
        else if (_state == ReelState.kSpinToResult2)
        {
            if (_resultSlot && _resultSlot.transform.position.y > 0) // Above center
            {
                // Change velocity here a little bit for better feeling
                _currentSlotVelocity *= 0.99f;
                MoveSlots(_currentSlotVelocity);
            }
            // Relayout the slots
            else
            {
                DoSlotLayout();
                // Set State Stppped
                SetState(ReelState.kStopped);
            }

        }
    }

    public void DoSlotLayout()
    {
        // use a temp list for safe
        var tempList = new List<Slot>();
        foreach (var item in _slots)
        {
            tempList.Add(item);
        }
        tempList.Sort((a, b) => (a.transform.position.y < b.transform.position.y ? 1 : -1));
        for (int i = 0; i < tempList.Count && i < _slotStartPositions.Count; i++)
        {
            tempList[i].ResetToPosition(_slotStartPositions[i]);
        }
    }

    public void SetState(ReelState state)
    {
        // On changed State
        if (_state != state)
        {
            _state = state;
            if (_state == ReelState.kSpinning)
            {
                foreach (var slot in _slots)
                {
                    slot.OnSpin();
                }
            }
        }
    }

    public void MoveSlots(Vector3 velocity)
    {
        var distance = velocity * Time.deltaTime;
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
            int newIndex = GetNewIndex(oldIndex);
            Sprite newSprSymbol = GetSpriteBySpring(_reelStrip[newIndex]);
            // reset the position to be on top of the current top slot
            slot.ResetToPosition(_topSlot.transform.position + new Vector3(0.0f, 2.5f, 0.0f));
            slot.SetSlotData(newIndex, newSprSymbol);
            // assign new top slot to this slot
            _topSlot = slot;
        }
    }

    public int GetNewIndex(int oldIndex)
    {
        //if (oldIndex >= _reelStrip.Count - 1) return 0;
        if (oldIndex == 0) return _reelStrip.Count - 1;
        return oldIndex - 1;
    }

    public void AnimateWinSymbol()
    {
        if (_resultSlot)
        {
            _resultSlot.PlayFadeAnimation();
        }
    }
}
