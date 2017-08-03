﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace Pomelo.DotNetClient
{
    public class DelayItem
    {
        private string route ;
        private string msg ;
        private Action<string> action ;

        public DelayItem(string r, string m, Action<string> a)
        {
            route = r ;
            msg = m ;
            action = a ;
        }

        public string Route{get{ return route ;}}
        public string Msg{get{ return msg ;}}
        public Action<string> Callback{get{return action;}}
    }


    public class MsgQueue
    {
        private int capacity = 1000;
        private Queue<DelayItem> content = new Queue<DelayItem>();

        public MsgQueue()
        {

        }

        public void Enqueue(string route ,string msg ,Action<string> action = null)
        {
            if( content.Count < capacity )
            {
                content.Enqueue(new DelayItem(route , msg , action));
            }
        }

        public DelayItem Dequeue()
        {
            return content.Count == 0 ? null : content.Dequeue();
        }

        public bool IsEmpty()
        {
            return content.Count == 0 ;
        }
    }
}
