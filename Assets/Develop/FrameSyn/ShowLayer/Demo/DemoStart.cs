﻿using UnityEngine;
using LuaInterface;
using FrameSyn;

public class DemoStart : MonoBehaviour
{
    public static DemoStart instance;

    public LuaManager luaMgr;

    public Transform moveTarget;

	// Use this for initialization
	void Start()
    {
        instance = this;
        luaMgr = gameObject.AddComponent<LuaManager>();

        gameObject.AddComponent<FrameSynMgr>();
	}

    void Destroy()
    {
        instance = null;
    }
}