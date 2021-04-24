using UnityEngine;

[
    RequireComponent(typeof(SpriteRenderer))
]
public class ItemDisplayer : MonoBehaviour
{
  [SerializeField]
  private Item item;

  [SerializeField]
  private Material outlineMaterial;
  private SpriteRenderer spriteRenderer;

  public Item Item
  {
    get => this.item;
  }

  private void Awake()
  {
    this.SetComponents();
    this.Setup();
  }

  private void SetComponents()
  {
    this.spriteRenderer = GetComponent<SpriteRenderer>();
  }

  public void Swap(Item item)
  {
    this.item = item;
    this.Setup();
  }

  private void Setup()
  {
    this.spriteRenderer.sprite = this.item.sprite;

    if (this.outlineMaterial != null)
    {
      this.spriteRenderer.material = this.outlineMaterial;
    }
  }

  public void EnableHighlight()
  {
    this.spriteRenderer.material.SetFloat("_Thickness", 1);
  }

  public void DisableHighlight()
  {
    this.spriteRenderer.material.SetFloat("_Thickness", 0);
  }
}
