
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class LongClick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool pointerDown = false;
    private float pointerDownTimer;

    public float requiredHoldTime;
    public UnityEvent onLongClick;
    public UnityEvent onNormalClick;

    public void OnPointerDown(PointerEventData eventData)
    {
        pointerDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        reset();
    }

    void reset()
    {
        if (pointerDownTimer < requiredHoldTime && onNormalClick != null)
        {
            onNormalClick.Invoke();
        }

        pointerDown = false;
        pointerDownTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (pointerDown)
        {
            pointerDownTimer += Time.deltaTime;
            if (pointerDownTimer >= requiredHoldTime)
            {
                if (onLongClick != null)
                {
                    onLongClick.Invoke();

                    reset();
                }
            }
        }
    }
}
