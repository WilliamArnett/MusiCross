using UnityEngine;

public class GridSpawner : MonoBehaviour
{
    public GameObject squarePrefab;
    public GameObject linePrefab;
    public int gridWidth = 10;
    public int gridHeight = 10;
    public float spacing = 1.1f; // space between squares
    public float padding = 0.1f;

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
        // center the grid roughly around origin

        Vector2 start = new Vector2(
            -((gridWidth - 1) * xStep) / 2f,
            -((gridHeight - 1) * yStep) / 2f
        );

        // Spawn squares
        for (int y = 0; y < gridHeight; y++)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                Vector2 pos = new Vector2(start.x + x * xStep, start.y + y * yStep);
                Instantiate(squarePrefab, pos, Quaternion.identity, transform);
            }
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
    }
}