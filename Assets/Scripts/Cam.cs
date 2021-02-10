using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Cam : MonoBehaviour
{
    public GameObject spriteGameObject;
    private Sprite sprite;
    private Texture2D texture;

    private RawImage rawImage;

    private WebCamDevice mainCamera;
    public WebCamTexture camTexture;
    public int heigthCam;
    public int widthCam;

    public Processing processing;
    public ConvertPixScreen convertPixScreen;
    private bool isIntializeCamera = false;

    private void Start()
    {
        StartCoroutine(Initialize());
    }

    private void Update()
    {
        if(isIntializeCamera)
        {
            SetImage();
        }               
    }

    private IEnumerator Initialize()
    {
        InitializeCam();
        yield return new WaitForSeconds(2.0f);
        SetSizeCam();
        InitializeSprite();
        processing = new Processing(widthCam, heigthCam);
        InitializeMoveObjectAR();
        InitializeMouseInformation();
        isIntializeCamera = true;

        rawImage.texture = camTexture;
    }

    private void InitializeMouseInformation()
    {
        var mouseInformation = GetComponent<MouseInformation>();
        mouseInformation.Initialize(widthCam, heigthCam, camTexture, convertPixScreen);
        mouseInformation.eventNewNeedColor += processing.SetNeedColor;
    }

    private void InitializeMoveObjectAR()
    {
        GetComponent<MoveObjectAR>().Initialize(processing, convertPixScreen);
    }

    private void InitializeCam()
    {
        WebCamDevice[] cameras = WebCamTexture.devices;
        mainCamera = cameras[0];
        camTexture = new WebCamTexture(mainCamera.name, 640, 480, 30);
        
        camTexture.filterMode = FilterMode.Trilinear;

        
        camTexture.Play();

    }

    private void SetSizeCam()
    {
        heigthCam = camTexture.height;
        widthCam = camTexture.width;

        convertPixScreen = new ConvertPixScreen(new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight), new Vector2(widthCam, heigthCam));
    }

    private void InitializeSprite()
    {
        texture = new Texture2D(widthCam, heigthCam);
        sprite = Sprite.Create(texture, new Rect(0, 0, widthCam, heigthCam), Vector2.zero);
        rawImage = spriteGameObject.GetComponent<RawImage>();
        spriteGameObject.GetComponent<SpriteRenderer>().sprite = sprite;
    }

    private void SetImage()
    {
        var massPixel = processing.GetPixels(camTexture.GetPixels());

        for (int x = 0; x < widthCam; x++)
        {
            for (int y = 0; y < heigthCam; y++)
            {
                texture.SetPixel(x, y, massPixel[x, y]);
            }
        }
        texture.Apply();
    }

    public Processing GetProcessing()
    {
        return processing;
    }

    public void ResetFilter()
    {
        processing.findColor = false;
        processing.convertPixel = false;
    }
}
