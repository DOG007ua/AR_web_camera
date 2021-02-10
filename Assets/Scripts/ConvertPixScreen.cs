using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvertPixScreen
{
    Vector2 sizeScreen;
    Vector2 sizeCamera;
    Vector2 coefScreenToCamera;

    public ConvertPixScreen(Vector2 sizeScreen, Vector2 sizeCamera)
    {
        this.sizeScreen = sizeScreen;
        this.sizeCamera = sizeCamera;
        coefScreenToCamera = new Vector2(sizeScreen.x / sizeCamera.x, sizeScreen.y / sizeCamera.y);
    }

    public Vector2 CameraToScreen(Vector2 value)
    {
        return value * coefScreenToCamera;
    }

    public Vector2 ScreenToCamera(Vector2 value)
    {
        return value / coefScreenToCamera;
    }
}
