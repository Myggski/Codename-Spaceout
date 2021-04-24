using UnityEngine;

/// <summary>
/// Replaces mouse pointer with a crosshair
/// </summary>
public class MousePointerIcon : MonoBehaviour
{
  [SerializeField]
  private GameObject crosshair;
  private SpriteRenderer spriteRenderer;

  void Start()
  {
    Cursor.visible = false;
  }

  void Update()
  {
    this.crosshair.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
  }
}