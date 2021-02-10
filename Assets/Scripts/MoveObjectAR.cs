using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjectAR : MonoBehaviour
{
    public GameObject gameObjectAR;
    private BorderSquare borderSquare;
    private Processing porcessing;
    private ConvertPixScreen convertPixScreen;

    public void Initialize(Processing porcessing, ConvertPixScreen convertPixScreen)
    {
        this.porcessing = porcessing;
        this.convertPixScreen = convertPixScreen;
    }

    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        //MouseMove();
        Move();
    }

    private void Move()
    {
        BorderSquare mainSquare = porcessing?.GetMainSquare();
        if (mainSquare != null)
        {
            var positionInPixels = CenterSquare(mainSquare);
            var positionInScreen = convertPixScreen.CameraToScreen(positionInPixels);
            var positionInScreenAndZ = new Vector3(positionInScreen.x, positionInScreen.y, 4.14f);
            var positionGO = Camera.main.ScreenToWorldPoint(positionInScreenAndZ);
            positionGO = new Vector3(positionGO.x, positionGO.y, 0);
            gameObjectAR.transform.position = positionGO;
        }
    }

    private Vector2 CenterSquare(BorderSquare square)
    {
        var x = square.xMin + (square.xMax - square.xMin) / 2.0f;
        var y = square.yMin + (square.yMax - square.yMin) / 2.0f;
        return new Vector2(x, y);
    }

    private void MouseMove()
    {
        var posScreen = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 4.14f);
        var posGO = Camera.main.ScreenToWorldPoint(posScreen);
        posGO = new Vector3(posGO.x, posGO.y, 0);
        //Debug.Log(posGO + "___" posGO.y)
        //gameObjectAR.transform.position = posGO;
    }
}
