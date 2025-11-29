using UnityEngine;

public enum CellState
{
    Unclicked,
    Clicked,
    Crossed
};

public class ClickableSquare : MonoBehaviour
{
    private SpriteRenderer sr;
    private Color originalColor;
    public Color clickedColor = Color.red;
    private GameObject xMark;

    private CellState state; //Enum for state. 0 = unclicked; 1 = clicked; 2 = crossed out.
    private CellState solveState;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        state = CellState.Unclicked;
        originalColor = sr.color;
        xMark = new GameObject("XMark");
        xMark.transform.SetParent(transform);
        xMark.transform.localPosition = Vector3.zero;
        xMark.transform.localScale = Vector3.one * 0.07f;

        var xText = xMark.AddComponent<TextMesh>();
        xText.text = "X";
        xText.fontSize = 100;
        xText.anchor = TextAnchor.MiddleCenter;
        xText.alignment = TextAlignment.Center;
        xText.color = Color.black;

        xMark.SetActive(false);
        // Debug.Log("initialized");
    }
    void OnMouseEnter() { Debug.Log(gameObject.name + " Enter with solve state " + solveState); }
    // void OnMouseOver()   { /* fires each frame while pointer is over — careful! */ }
    void OnMouseExit() { Debug.Log(gameObject.name + " Exit"); }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Pressed left-click.");
            HandleClick(0);
        }

        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Pressed right-click.");
            HandleClick(1);
        }


        // if (Input.GetMouseButtonDown(2))
        //     Debug.Log("Pressed middle-click.");
    }
    public void SetColor(Color color)
    {
        clickedColor = color;
    }
    void HandleClick(int mousebutton)
    {
        Vector3 world = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 point = new Vector2(world.x, world.y);

        Collider2D hit = Physics2D.OverlapPoint(point);

        if (hit != null && hit.gameObject == gameObject)
        {
            Debug.Log($"{gameObject.name} OnMouseDown fired at time {Time.time} with state {state}");
            if (mousebutton == 0)
            {
                Debug.Log("Left click");
                if (state != CellState.Clicked)
                {
                    sr.color = clickedColor;
                    state = CellState.Clicked;
                }
                else
                {
                    sr.color = originalColor;
                    state = CellState.Unclicked;

                }
            }
            if (mousebutton == 1)
            {
                Debug.Log("Right click");
                if (state == CellState.Clicked || state == CellState.Crossed)
                {
                    sr.color = originalColor;
                    state = 0;
                }
                else if (state == 0)
                {
                    state = CellState.Crossed;
                }
            }
            if (state == CellState.Crossed)
            {
                xMark.SetActive(true);
            }
            else
            {
                xMark.SetActive(false);
            }
            Debug.Log($"State now {state}");
        }
    }
    public void setSolveState(int stateNum)
    {
        if (stateNum == 0)
        {
            solveState = CellState.Unclicked;
        }
        else
        {
            solveState = CellState.Clicked;
        }
        Debug.Log($"spawned object with solve state {stateNum}");
    }
}