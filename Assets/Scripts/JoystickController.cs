using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public GameObject knob;
    public GameObject center;
    public Vector2 joystickVec;
    private Vector2 joystickTouchPosition;
    private Vector2 joystickOriginalPosition;
    private float joystickRadius;

    [SerializeField] private float borderKnob;

    private void Start()
    {
        joystickOriginalPosition = center.transform.position;
        joystickRadius = center.GetComponent<RectTransform>().sizeDelta.y / borderKnob;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        knob.transform.position = Input.mousePosition;
        center.transform.position = Input.mousePosition;
        joystickTouchPosition = Input.mousePosition;
    }
    public void OnDrag(PointerEventData eventData)
    {
        PointerEventData pointerEventData = eventData as PointerEventData;
        Vector2 dragPosition = eventData.position;
        joystickVec = (dragPosition - joystickTouchPosition).normalized;

        float distance = Vector2.Distance(dragPosition, joystickTouchPosition);
        
        if(distance < joystickRadius)
        {
            knob.transform.position = joystickTouchPosition + joystickVec * distance;
        }
        else if(distance > joystickRadius)
        {
            knob.transform.position = joystickTouchPosition + joystickVec * joystickRadius;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        joystickVec = Vector2.zero;
        knob.transform.position = joystickOriginalPosition;
        center.transform.position = joystickOriginalPosition;
    }
}
