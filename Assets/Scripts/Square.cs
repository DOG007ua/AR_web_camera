using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class GroupSquare
{
    private int width;
    private List<Square> listInput;
    private List<Group> listReturn;

    public GroupSquare(int width)
    {
        this.width = width;
    }
    
    public List<Group> GetList(List<Square> listInput)
    {
        int idNow = -1;
        this.listInput = listInput;
        listReturn = new List<Group>();
        for(int i = 0; i < listInput.Count; i++)
        {
            if(listInput[i].InList == false)
            {
                idNow++;
                listReturn.Add(CreateGroup(listInput[i], idNow));
            }
        }
        return listReturn;
    }

    private Group CreateGroup(Square squareFirst, int idList)
    {
        Group group = new Group();
        group.list = new List<Square>();
        group.list.Add(squareFirst);
        for(int i = 0; i < group.list.Count; i++)
        {
            FindSquare(group.list[i], group.list, idList);
        }
        return group;
    }

    /// <summary>
    /// Нахожу соседей квадрата
    /// </summary>
    /// <param name="square"></param>
    /// <param name="list"></param>
    /// <param name="idList"></param>
    private void FindSquare(Square square, List<Square> list, int idList)
    {
        square.InList = true;
        square.idList = idList;

        for (int y = 0; y < 3; y++)
        {
            for(int x = 0; x < 3; x++)
            {
                var id = CalculationID(square.x - 1 + x, square.y - 1 + y);
                var value = listInput.FirstOrDefault(v => v.ID == id);
                if(value != null)
                {
                    if(value.InList == false)
                    {
                        list.Add(value);
                        value.InList = true;
                        value.idList = idList;
                    }
                }
            }
        }
    }

    private int CalculationID(int x, int y)
    {
        return x * width + y;
    }
}


public class Group
{
    public List<Square> list;

    public Group()
    {

    }

    public int GetCount() => list.Count;


    public BorderSquare GetGranica(int size)
    {
        int xMin = 1000000000;
        int xMax = 0;
        int yMin = 1000000000;
        int yMax = 0;

        foreach(var value in list)
        {
            if (value.x > xMax) xMax = value.x;
            if (value.x < xMin) xMin = value.x;

            if (value.y > yMax) yMax = value.y;
            if (value.y < yMin) yMin = value.y;
        }
        return new BorderSquare(xMin * size, (xMax + 1) * size, yMin * size, (yMax + 1) * size );
        //return (xMin * size, (xMax + 1) * size, yMin * size, (yMax + 1) * size);
    }
}



public class Square
{
    public int ID;
    public int x;
    public int y;
    public int idList;
    public bool InList;

    public Square(int x, int y, int width)
    {
        this.x = x;
        this.y = y;
        ID = x * width + y;
        idList = -1;
        InList = false;
    }
}

public class BorderSquare
{
    public int xMin;
    public int xMax;
    public int yMin;
    public int yMax;

    public BorderSquare(int xMin, int xMax, int yMin, int yMax)
    {
        this.xMin = xMin;
        this.xMax = xMax;
        this.yMin = yMin;
        this.yMax = yMax;
    }
}
