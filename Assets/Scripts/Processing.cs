using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Processing
{
    private Color needColor;
    private Color[,] pixels;
    private Color[,] pixelsOriginal;
    private int[,] valueColor;
    private int width, heigth;

    public bool addLine;
    public bool findColor;
    public bool convertPixel;
    public bool needFindRentagle;

    private FindRentagle findRentagle;
    private float deltaMax = 0.05f;

    public Processing(int width, int heigth)
    {

        this.width = width;
        this.heigth = heigth;
        pixels = new Color[width, heigth];
        pixelsOriginal = new Color[width, heigth];
        valueColor = new int[width, heigth];

        addLine = false;
        findColor = false;
        convertPixel = false;
        needFindRentagle = true;

        findRentagle = new FindRentagle(width, heigth);
    }

    public void SetNeedColor(Color needColor, int x, int y)
    {
        float r = 0;
        float g = 0;
        float b = 0;

        for(int yPos = y - 1; yPos <= y + 1; yPos++)
        {
            for (int xPos = x - 1; xPos <= x + 1; xPos++)
            {
                r += pixelsOriginal[xPos, yPos].r;
                g += pixelsOriginal[xPos, yPos].g;
                b += pixelsOriginal[xPos, yPos].b;
            }
        }

        this.needColor = new Color(r / 9, g / 9, b / 9);


        findColor = true;
        //convertPixel = true;
    }

    public Color GetNeedColor()
    {
        return needColor;
    }

    public Color[,] GetPixels(Color[] pixel)
    {
        ConvertLineMass(pixel);
        if (addLine)
        {
            AddLine();
        }        
        
        if (findColor)
        {
            FindColor();

            if (convertPixel)
            {
                ConvertPixel();
            }

            if (needFindRentagle)
            {
                findRentagle.FindSquare(valueColor, pixels);
            }
        }


        return pixels;
    }

    private void ConvertLineMass(Color[] pixelLine)
    {
        int i = 0;
        for(int y = 0; y < heigth; y++)
        {
            for(int x = 0; x < width; x++)
            {
                pixels[x, y] = pixelLine[i];
                pixelsOriginal[x, y] = pixelLine[i];
                i++;
            }
        }
    }

    private void FindColor()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < heigth; y++)
            {
                valueColor[x, y] = InversPixel(pixels[x, y]);
            }
        }        
    }

    void ConvertPixel()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < heigth; y++)
            {
                pixels[x, y] = new Color(valueColor[x, y], valueColor[x, y], valueColor[x, y]);
            }
        }
    }

    private int InversPixel(Color originalColor)
    {
        float delta =
            Math.Abs(originalColor.r - needColor.r) +
            Math.Abs(originalColor.g - needColor.g) +
            Math.Abs(originalColor.b - needColor.b);

        delta = delta / 3.0f;

        if (delta < deltaMax) return 1;
        else return 0;
    }

    public void AddLine()
    {
        int size = 20;
        int amountX = width / size;
        int amountY = heigth / size;


        for(int x = 0; x < amountX; x++)
        {
            for(int y = 0; y < heigth; y++)
            {
                pixels[x * size, y] = new Color(255, 0, 0);
            }
        }

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < amountY; y++)
            {
                pixels[x, y * size] = new Color(255, 0, 0);
            }
        }
    }

    public BorderSquare GetMainSquare()
    {
        return findRentagle.mainSquare;
    }
}
