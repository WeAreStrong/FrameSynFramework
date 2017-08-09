using UnityEngine;
using UnityEngine.EventSystems;
using FrameSyn;

public class ClickPoint : EventTrigger
{
    public override void OnPointerClick(PointerEventData eventData)
    {
        MainGame.mLuaMgr.CallFunction("Demo1.SendMove", eventData.position, RealTime.frameCount);
        //Move.to = eventData.position;
    }
}