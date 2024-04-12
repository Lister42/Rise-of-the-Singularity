using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickFixer : StandaloneInputModule
{
    private float x = 0.0F;
    private float y = 0.0F;
    private float delay = 0.05F;

    void Start()
    {

        ClickForFix();
    }

    public void ClickForFix()
    {
        StartCoroutine(Click_Delayed());
    }

    private IEnumerator Click_Delayed()
    {
        yield return new WaitForSeconds(delay);
        Click();
    }

    private void Click()
    {
        Input.simulateMouseWithTouches = true;
        var pointerData = GetTouchPointerEventData(new Touch()
        {
            position = new Vector2(x, y),
        }, out bool b, out bool bb);

        ProcessTouchPress(pointerData, true, true);
    }
}