using System;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class GridSpawner : MonoBehaviour
{
    public GameObject squarePrefab;
    public GameObject linePrefab;
    public GameObject numberLabelPrefab;
    public int gridWidth;
    public int gridHeight;
    public float spacing = 1.1f; // space between squares
    public float padding = 0.1f;
    public Color gridColor;
    private string[] rowHintNumbers;
    private string[] colHintNumbers;
    // private int[,] solvePattern = {
    //     {0,0,0,0,0,0,0,0,0,0},
    //     {0,0,0,0,1,1,1,1,1,0},
    //     {0,0,0,1,1,1,1,1,1,0},
    //     {0,0,0,1,0,0,0,0,1,0},
    //     {0,0,0,1,0,0,0,0,1,0},
    //     {0,0,0,1,0,0,0,0,1,0},
    //     {0,0,0,1,0,0,1,1,1,0},
    //     {0,1,1,1,0,0,1,1,1,0},
    //     {0,1,1,1,0,0,0,0,0,0},
    //     {0,0,0,0,0,0,0,0,0,0},

    // };
    private int[,] solvePattern = {
        {0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,1,1,1,1,1,0},
        {0,0,0,1,1,1,1,1,1,0},
        {0,0,0,1,0,0,0,0,1,0},
        {0,0,0,1,0,0,0,0,1,0},
        {0,0,0,1,0,0,0,0,1,0},
        {0,0,0,1,0,0,1,1,1,0},
        {0,1,1,1,0,0,1,1,1,0},
        {0,1,1,1,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0},

    };
    void GenHintNumbers()
    {
        rowHintNumbers = new string[gridHeight];
        colHintNumbers = new string[gridWidth];
        string defaultText;
        // bool counting = false;
        int cellcount = 0;  //Running total of cells seen, reset on a blank cell
        int howManySegments;
        for (int i = 0; i < gridHeight; i++)    //Loop over each row
        {
            howManySegments = 0;
            defaultText = "";
            for (int j = 0; j < gridWidth; j++)
            {
                if (solvePattern[i, j] == 1)
                {
                    cellcount++;
                }
                else //Non filled in square state
                {
                    if (cellcount != 0) //Previous cells were red
                    {
                        defaultText += " " + cellcount.ToString();
                        howManySegments++;
                    }
                    cellcount = 0;
                }
            }
            if (cellcount != 0)
            {
                defaultText += " " + cellcount.ToString();
                howManySegments++;
            }
            cellcount = 0;

            if (howManySegments == 0)
            {
                defaultText = "0";
            }
            rowHintNumbers[i] = defaultText;
            // Debug.Log("Row " + i.ToString() + "with " + howManySegments.ToString()+ "segments : " + defaultText);
        }
        for (int i = 0; i < gridWidth; i++) //Loop over each column
        {
            howManySegments = 0;
            defaultText = "";
            for (int j = 0; j < gridHeight; j++)
            {
                if (solvePattern[j, i] == 1)
                {
                    cellcount++;
                }
                else
                {
                    if (cellcount != 0)
                    {
                        defaultText += "\n" + cellcount.ToString();
                        howManySegments++;
                    }
                    cellcount = 0;
                }
            }
            if (cellcount != 0)
            {
                defaultText += "\n" + cellcount.ToString();
                howManySegments++;
            }
            cellcount = 0;

            if (howManySegments == 0)
            {
                defaultText = "0";
            }
            colHintNumbers[i] = defaultText;
        }
        // Debugging
        for (int k = 0; k < gridHeight; k++)
        {
            Debug.Log("Row " + k.ToString() + ": " + rowHintNumbers[k]);
        }
        for (int k = 0; k < gridWidth; k++)
        {
            Debug.Log("Col " + k.ToString() + ": "+colHintNumbers[k]);           
        }
        //
    }
    void Start()
    {
        if (squarePrefab == null || linePrefab == null)
        {
            Debug.LogError("No square prefab assigned!");
            return;
        }
        Vector2 squareSize = squarePrefab.GetComponent<SpriteRenderer>().bounds.size;
        float xStep = squareSize.x + padding;
        float yStep = squareSize.y + padding;

        //print hint numbers
        GenHintNumbers();

        Vector2 start = new Vector2(
            -((gridWidth - 1) * xStep) / 2f,
            -((gridHeight - 1) * yStep) / 2f
        );

        // Spawn squares
        for (int y = 0; y < gridHeight; y++)
        {
            //Generate horizontal labels
            Vector3 labelPos = new Vector3(start.x, start.y + y * yStep, 0f);
            GameObject label = Instantiate(numberLabelPrefab, labelPos, Quaternion.identity, transform);
            var tmp = label.GetComponent<TextMeshPro>();
            tmp.text = rowHintNumbers[gridHeight - y - 1];
            tmp.ForceMeshUpdate();
            Bounds b = tmp.textBounds;
            label.transform.position = new Vector3(start.x -squareSize.x*(10.7f), label.transform.position.y, 0);
            tmp.alignment = TextAlignmentOptions.Right;
            tmp.color = Color.black;
            tmp.fontSize = 40f;
            label.transform.localScale = Vector3.one * 0.2f;
            for (int x = 0; x < gridWidth; x++)
            {
                Vector2 pos = new Vector2(start.x + x * xStep, start.y + y * yStep);
                GameObject spawnedSquare = Instantiate(squarePrefab, pos, Quaternion.identity, transform);
                ClickableSquare cs = spawnedSquare.GetComponent<ClickableSquare>();
                if (cs != null)
                {
                    cs.setSolveState(solvePattern[gridHeight-y-1,x]); //set all to be clicked
                    cs.SetColor(gridColor);
                }
            }
        }
        //Vertical grid numbers
        for(int x = 0; x < gridWidth; x++)
        {
            Vector3 labelPos = new Vector3(start.x + x * xStep, start.y + gridHeight * yStep * 2-0.5f, 0f);
            GameObject label = Instantiate(numberLabelPrefab, labelPos, Quaternion.identity, transform);
            var tmp = label.GetComponent<TextMeshPro>();
            tmp.text = colHintNumbers[x];
            tmp.ForceMeshUpdate();
            Bounds b = tmp.textBounds;
            label.transform.position = new Vector3(label.transform.position.x, label.transform.position.y, 0);
            tmp.alignment = TextAlignmentOptions.Bottom;
            tmp.color = Color.black;
            tmp.fontSize = 40f;
            label.transform.localScale = Vector3.one * 0.2f;
        }

        // Spawn vertical lines
        for (int x = 0; x <= gridWidth; x++)
        {
            Vector2 pos = new Vector2(start.x - xStep / 2f + x * xStep, start.y + (gridHeight - 1) * yStep / 2f);
            GameObject line = Instantiate(linePrefab, pos, Quaternion.identity, transform);
            if (x % 5 == 0)
            {
                line.transform.localScale = new Vector3(0.1f, gridHeight * yStep, 1f);
            }
            else
            {
                line.transform.localScale = new Vector3(0.05f, gridHeight * yStep, 1f);
            }

        }

        // Spawn horizontal lines
        for (int y = 0; y <= gridHeight; y++)
        {
            Vector2 pos = new Vector2(start.x + (gridWidth - 1) * xStep / 2f, start.y - yStep / 2f + y * yStep);
            GameObject line = Instantiate(linePrefab, pos, Quaternion.identity, transform);
            if (y % 5 == 0)
            {
                line.transform.localScale = new Vector3(gridWidth * xStep, 0.1f, 1f);
            }
            else
            {
                line.transform.localScale = new Vector3(gridWidth * xStep, 0.05f, 1f);

            }

        }
        // for (int y = 0; y <= gridHeight; y++)
        // {
        //     Vector2 pos = new Vector2(start.x + (gridWidth - 1) * xStep / 2f, start.y - yStep / 2f + y * yStep);
   
        // }

    }
}