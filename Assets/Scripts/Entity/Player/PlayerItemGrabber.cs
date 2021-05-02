using UnityEngine;

/// <summary>
/// Grabs selectable items in the world, and calls action depending on item-type
/// </summary>
public class PlayerItemGrabber : MonoBehaviour
{
  private ItemDisplayer itemToSelect;

  private void OnTriggerEnter2D(Collider2D other)
  {
    this.TryHighlightItem(other);
  }

  private void OnTriggerExit2D(Collider2D other)
  {
    this.TryRemoveHighlightItem(other);
  }

  /// <summary>
  /// When the player is getting to a lootable position, the item is getting highlighted and can be picked up
  /// </summary>
  /// <param name="other"></param>
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

  /// <summary>
  /// When the player leaves the selectable area of the highlighted item, it become deselected
  /// </summary>
  /// <param name="other"></param>
  private void TryRemoveHighlightItem(Collider2D other)
  {
    ItemDisplayer item = other.GetComponent<ItemDisplayer>();

    if (item != null && this.itemToSelect != null && item.Equals(this.itemToSelect))
    {
      this.itemToSelect.DisableHighlight();
      this.itemToSelect = null;
    }
  }

  /// <summary>
  /// Trying to equip the gun
  /// </summary>
  /// <param name="gunModel"></param>
  private void EquipGun(GunItem gunModel)
  {
    PlayerGunHolster gunHolster = GetComponent<PlayerGunHolster>();

    if (gunHolster != null)
    {
      gunHolster.Equip(gunModel, this.SwapItem);
    }
  }

  /// <summary>
  /// Swaps the selected item
  /// Example: When the inventory is full, it swaps the new item with the old
  /// </summary>
  /// <param name="item"></param>
  private void SwapItem(Item item)
  {
    this.itemToSelect.Swap(item);
    Instantiate(this.itemToSelect.gameObject, this.transform.position, this.itemToSelect.gameObject.transform.rotation);
  }

  /// <summary>
  /// Depending on type, call action for the specific type
  /// Example: GunItem calls equip gun, to change or add the new gun
  /// </summary>
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