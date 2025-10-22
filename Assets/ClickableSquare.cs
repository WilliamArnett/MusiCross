using UnityEngine;

public class ClickableSquare : MonoBehaviour
{
    private SpriteRenderer sr;
    private Color originalColor;
    public Color clickedColor = Color.red;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
        Debug.Log("initialized");
    }
    void OnMouseEnter()  { Debug.Log(gameObject.name + " Enter"); }
    void OnMouseOver()   { /* fires each frame while pointer is over — careful! */ }
    void OnMouseExit()   { Debug.Log(gameObject.name + " Exit"); }
    void OnMouseDown()
    {
        Debug.Log($"{gameObject.name} OnMouseDown fired at time {Time.time}");
        // Toggle between original and clicked color
        if (sr.color == originalColor)
            sr.color = clickedColor;
        else
            sr.color = originalColor;
    }
}