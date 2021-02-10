using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseInformation : MonoBehaviour
{
    public Text[] texts;
    public event Action<Color, int, int> eventNewNeedColor;

    private WebCamTexture camTexture;
    private int heigthCam;
    private int widthCam;
    private Color colorMouse;
    private int xMouse;
    private int yMouse;
    private Cam cam;
    private ConvertPixScreen convertPixScreen;

    private void Start()
    {
        cam = GetComponent<Cam>();
    }

    private void Update()
    {
        MouseMove();
        MouseClick();
        AcceleratorValue();
    }

    public void Initialize(int width, int heigth, WebCamTexture camTexture, ConvertPixScreen convertPixScreen)
    {
        heigthCam = heigth;
        widthCam = width;
        this.camTexture = camTexture;
        this.convertPixScreen = convertPixScreen;
    }

    private void ColorPixelCamera()
    {
        /*if (cam.processing == null) return;
        Vector2 pos;
        //RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, canvas.worldCamera, out pos);
       // RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, canvas.worldCamera, out pos);
        xMouse = (int)((widthCam / 2.0f) + pos.x);
        yMouse = (int)((heigthCam / 2.0f) + pos.y);
        colorMouse = camTexture.GetPixel(xMouse, yMouse);
        texts[0].text = $"R:{colorMouse.r * 255} ({(colorMouse.r - cam.processing.GetNeedColor().r).ToString("0.0000")})";
        texts[1].text = $"G:{colorMouse.g * 255} ({(colorMouse.g - cam.processing.GetNeedColor().g).ToString("0.0000")})";
        texts[2].text = $"B:{colorMouse.b * 255} ({(colorMouse.b - cam.processing.GetNeedColor().b).ToString("0.0000")})";*/
    }

    private void MouseMove()
    {
        if (cam.processing == null) return;

        Vector2 posPixel = convertPixScreen.ScreenToCamera(BorderCheck(Input.mousePosition));
        xMouse = (int)posPixel.x;
        yMouse = (int)posPixel.y;
        colorMouse = camTexture.GetPixel(xMouse, yMouse);
        //texts[0].text = $"R:{colorMouse.r * 255} ({(colorMouse.r - cam.processing.GetNeedColor().r).ToString("0.0000")})";
        //texts[1].text = $"G:{colorMouse.g * 255} ({(colorMouse.g - cam.processing.GetNeedColor().g).ToString("0.0000")})";
        //texts[2].text = $"B:{colorMouse.b * 255} ({(colorMouse.b - cam.processing.GetNeedColor().b).ToString("0.0000")})";
    }

    private void AcceleratorValue()
    {
        /*texts[0].text = Input.acceleration.x.ToString();
        texts[1].text = Input.acceleration.y.ToString();
        texts[2].text = Input.acceleration.z.ToString();*/

        texts[1].text = cam.camTexture.width.ToString();
        texts[2].text = cam.camTexture.height.ToString();
        texts[3].text = cam.widthCam.ToString();
        texts[4].text = cam.heigthCam.ToString();
    }

    private Vector2 BorderCheck(Vector2 mousePosition)
    {
        float x = 0;
        float y = 0;
        if (mousePosition.x < 0) x = 0;
        else if (mousePosition.x > Camera.main.pixelWidth) x = Camera.main.pixelWidth;
        else x = mousePosition.x;

        if (mousePosition.y < 0) y = 0;
        else if (mousePosition.y > Camera.main.pixelHeight) y = Camera.main.pixelHeight;
        else y = mousePosition.y;
        return new Vector2(x, y);

    }

    private void MouseClick()
    {
        if(Input.GetMouseButtonDown(0))
        {
            eventNewNeedColor?.Invoke(colorMouse, xMouse, yMouse);            
        }
    }
}
