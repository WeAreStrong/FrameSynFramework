using UnityEngine;
using UnityEngine.EventSystems;

public class ClickPoint : EventTrigger
{
    public override void OnPointerClick(PointerEventData eventData)
    {
        //DemoStart.instance.luaMgr.CallFunction("TestMove.SendMove", eventData.position);
        Move.to = eventData.position;
    }
}