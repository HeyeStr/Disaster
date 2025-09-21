using System;
using Common;
using EventSo;
using UnityEngine;

namespace Phone
{
    public class test : MonoBehaviour
    {
        public TransmitMessageEventSo so;
        
        public bool isStart;

        public AssistanceMessage obj;
        
        private void Update()
        {
            if (isStart)
            {
                so.EventRise(obj);
                isStart = false;
            }
        }
    }
}