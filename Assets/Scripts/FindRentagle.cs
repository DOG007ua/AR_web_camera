using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindRentagle
{
    public BorderSquare mainSquare;

    private int size = 10;
    private int width;
    private int heigth;
    private int amountSquareY;
    private int amountSquareX;
    private int[,] inputMass;
    private Color[,] pixels;
    private List<Square> listSquare;
    private GroupSquare groupSquare;
    

    public FindRentagle(int width, int heigth)
    {       
        groupSquare = new GroupSquare(width);
        this.width = width;
        this.heigth = heigth;
        amountSquareX = width / size;
        amountSquareY = heigth / size;
    }

    public void FindSquare(int[,] inputMass, Color[,] pixels)
    {
        listSquare = new List<Square>();
        this.pixels = pixels;
        this.inputMass = inputMass;
        for(int y = 1; y < amountSquareY - 2; y++)
        {
            for (int x = 1; x < amountSquareX - 2; x++)
            {
                float needPix = CalculationNeedPixInSquare(x, y);
                ProcessinqSquare(needPix, x, y);                
            }
        }
        var list = groupSquare.GetList(listSquare);
        PaintGroup(list);
    }

    private int CalculationNeedPixInSquare(int xPosSquare, int yPosSquare)
    {
        int amountNeedPixel = 0;
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                int xPos = xPosSquare * size + x;
                int yPos = yPosSquare * size + y;
                if (inputMass[xPos, yPos] == 1) amountNeedPixel++;
            }
        }
        return amountNeedPixel;
    }

    private void ProcessinqSquare(float needPix, int x, int y)
    {        
        if (needPix / (size * size) > 0.35f)
        {
            listSquare.Add(new Square(x, y, width));
        }
    }

    private void PaintGroup(List<Group> list)
    {
        PaintManySquare(list);
        PaintBigSquare(list);
    }

    private void PaintBigSquare(List<Group> list)
    {
        Color[] colors = new Color[]{
            new Color(255, 0, 0),
            new Color(0, 255, 0),
            new Color(0, 0, 255),
            new Color(255, 255, 0),
            new Color(255, 0, 255),
            new Color(0, 255, 255),
            new Color(255,255,255) };


        int maxCount = 0;
        Group group = null;
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].GetCount() > maxCount)
            {
                maxCount = list[i].GetCount();
                group = list[i];
            }
        }
        if (group == null) return;
        mainSquare = group.GetGranica(size);
        PaintSquare(mainSquare.xMin, mainSquare.xMax, mainSquare.yMin, mainSquare.yMax, colors[1]);
    }

    private void PaintManySquare(List<Group> list)
    {
        Color[] colors = new Color[]{
            new Color(255, 0, 0),
            //new Color(0, 255, 0),
            new Color(0, 0, 255),
            new Color(255, 255, 0),
            new Color(255, 0, 255),
            new Color(0, 255, 255),
            new Color(255,255,255) };


        /*int maxCount = 0;
        Group group = null;
        for(int i = 0; i < list.Count; i++)
        {
            if (list[i].GetCount() > maxCount)
            {
                maxCount = list[i].GetCount();
                group = list[i];
            }            
        }

        if (group == null) return;
        for (int j = 0; j < group.list.Count; j++)
        {
            var v = group.list[j];

            PaintLine(v.x, v.y, colors[0]);
        }*/

        for (int i = 0; i < list.Count; i++)
        {
            Color color = i < colors.Length ? color = colors[i] : color = colors[0];
            for (int j = 0; j < list[i].list.Count; j++)
            {
                var v = list[i].list[j];

                PaintLine(v.x, v.y, color);
            }
        }
    }

    private void PaintLine(int xS, int yS, Color color)
    {
        for (int xLine = 0; xLine < 2; xLine++)
        {
            for (int yLine = 0; yLine < size; yLine++)
            {
                pixels[(xS + xLine) * size, yS * size + yLine] = color;
            }
        }

        for (int xLine = 0; xLine < size; xLine++)
        {
            for (int yLine = 0; yLine < 2; yLine++)
            {
                pixels[xS * size + xLine, (yS + yLine) * size] = color;
            }
        }
    }

    private void PaintSquare(int x1, int x2, int y1, int y2, Color color)
    {
        var sizeX = x2 - x1;
        var sizeY = y2 - y1;

        for (int yLine = 0; yLine < sizeY; yLine++)
        {
            pixels[x1, y1 + yLine] = color;
            pixels[x2, y1 + yLine] = color;
        }

        for (int xLine = 0; xLine < sizeX; xLine++)
        {
            pixels[x1 + xLine, y1] = color;
            pixels[x1 + xLine, y2] = color;
        }
    }
}
