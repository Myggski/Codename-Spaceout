using UnityEngine;

/// <summary>
/// Replaces mouse pointer with a crosshair
/// </summary>
public class MousePointerIcon : MonoBehaviour
{
  [SerializeField]
  private GameObject crosshairPrefab;
  private GameObject crosshair;
  private SpriteRenderer spriteRenderer;

  private void OnDisable()
  {
    this.HideCrosshair();
  }

  private void OnEnable()
  {
    this.Setup();
  }

  void Update()
  {
    this.UpdatePosition();
  }

  private void Setup()
  {
    Cursor.visible = false;
    this.crosshair = Instantiate(this.crosshairPrefab, this.transform.position, this.transform.rotation);
  }

  private void HideCrosshair()
  {
    Cursor.visible = true;
    this.crosshair.SetActive(false);
  }

  private void UpdatePosition()
  {
    if (this.crosshairPrefab != null)
    {
      this.crosshair.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
  }
}