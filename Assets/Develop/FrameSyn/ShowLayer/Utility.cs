using UnityEngine;
using UnityEngine.UI;
using LuaInterface;

public class Utility
{
    public static void AddClickEvent(GameObject button, LuaFunction luaFunc)
    {
        UnityEngine.Events.UnityAction callback = delegate()
        {
            luaFunc.Call();
        };

        Button btn = button.GetComponent<Button>();
        btn.onClick.AddListener(callback);
    }
}