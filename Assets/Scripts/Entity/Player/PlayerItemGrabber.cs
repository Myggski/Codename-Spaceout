using UnityEngine;

public class PlayerItemGrabber : MonoBehaviour
{
  [SerializeField]
  private ItemDisplayer itemToSelect;

  private void OnTriggerEnter2D(Collider2D other)
  {
    this.TryHighlightItem(other);
  }

  private void OnTriggerExit2D(Collider2D other)
  {
    this.TryDisableHighlightItem(other);
  }

  private void TryHighlightItem(Collider2D other)
  {
    ItemDisplayer item = other.GetComponent<ItemDisplayer>();

    if (item != null)
    {
      if (this.itemToSelect != null)
      {
        this.itemToSelect.DisableHighlight();
      }

      this.itemToSelect = item;
      this.itemToSelect.EnableHighlight();
    }
  }

  private void TryDisableHighlightItem(Collider2D other)
  {
    ItemDisplayer item = other.GetComponent<ItemDisplayer>();

    if (item != null && this.itemToSelect != null && item.Equals(this.itemToSelect))
    {
      this.itemToSelect.DisableHighlight();
      this.itemToSelect = null;
    }
  }

  private void EquipGun(GunItem gunModel)
  {
    PlayerGunHolster gunHolster = GetComponent<PlayerGunHolster>();

    if (gunHolster != null)
    {
      gunHolster.Equip(gunModel, this.SwapItem);
    }
  }

  private void SwapItem(Item item)
  {
    this.itemToSelect.Swap(item);
    Instantiate(this.itemToSelect.gameObject, this.transform.position, this.itemToSelect.gameObject.transform.rotation);
  }

  public void PickUp()
  {
    if (this.itemToSelect != null)
    {
      if (this.itemToSelect.Item is GunItem)
      {
        this.EquipGun((GunItem)this.itemToSelect.Item);
      }

      Destroy(this.itemToSelect.gameObject);
    }
  }
}