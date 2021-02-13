using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityArrowHandler : MonoBehaviour
{
    [SerializeField] Color defaultColor;
    [SerializeField] Color highlightColor;
    [SerializeField] Color selectColor;
    [SerializeField] SpriteRenderer spriteRenderer;
    StarController star;
    Vector2 lastMousePos;
    Camera mainCam;
    private void Start() {
        mainCam = Camera.main;
        star = GetComponent<StarController>();
    }
    public void OnMouseEnter() {
        Debug.Log("Enter!");
        spriteRenderer.color = highlightColor;
        
    }
    public void OnMouseExit() {
        Debug.Log("Exit!");
        spriteRenderer.color = defaultColor;
    }
    public void OnMouseDown() {
        Debug.Log("Down!");
        lastMousePos = Input.mousePosition;
        spriteRenderer.color = selectColor;
    }
    public void OnMouseDrag() {
        Debug.Log("Drag!");
        star.rb.velocity += (Vector2)(mainCam.ScreenToWorldPoint(Input.mousePosition) - mainCam.ScreenToWorldPoint(lastMousePos));
        lastMousePos = Input.mousePosition;
    }
    public void OnMouseUp() {
        Debug.Log("Up!");
        spriteRenderer.color = defaultColor;
    }
}
