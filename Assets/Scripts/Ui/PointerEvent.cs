using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PointerEvent : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public enum eState
    {
        Up,
        Down
    }

    [System.Serializable]
    public struct EventInfo
    {
        public PointerEventData.InputButton button;
        public eState state;
        public UnityEvent unityevent;
    }

    public EventInfo[] eventInfos;
    public void OnPointerDown(PointerEventData eventData)
    {
       foreach(EventInfo eventInfo in eventInfos)
        {
            if(eventData.button == eventInfo.button && eventInfo.state == eState.Down)
            {
                eventInfo.unityevent.Invoke();
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        foreach (EventInfo eventInfo in eventInfos)
        {
            if (eventData.button == eventInfo.button && eventInfo.state == eState.Up)
            {
                eventInfo.unityevent.Invoke();
            }
        }
    }
}
