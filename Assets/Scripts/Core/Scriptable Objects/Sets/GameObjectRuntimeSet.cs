using UnityEngine;

[CreateAssetMenu(menuName = "Sets/gameObjects")]
public class GameObjectRuntimeSet : RuntimeSet<GameObject>
{
  public void RemoveById(int id)
  {
    int index = this.items.FindIndex(item => item.GetInstanceID() == id);

    if (index < 0)
    {
      index = this.items.FindIndex(items => items == null);
    }

    if (index >= 0)
    {
      this.items.RemoveAt(index);
    }
  }
}