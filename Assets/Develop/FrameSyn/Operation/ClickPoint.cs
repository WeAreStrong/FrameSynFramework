using UnityEngine;
using UnityEngine.EventSystems;
using FrameSyn;

public class ClickPoint : EventTrigger
{
    public override void OnPointerClick(PointerEventData eventData)
    {
        DemoStart.instance.luaMgr.CallFunction("Room.SendMove", eventData.position, RealTime.frameCount);
        //Move.to = eventData.position;
    }
}