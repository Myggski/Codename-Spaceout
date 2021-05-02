using UnityEngine;

public class RemoveFromRuntimeSet : MonoBehaviour
{
  [SerializeField]
  private GameObjectRuntimeSet enemyList;

  private void OnDestroy()
  {
    this.Remove();
  }

  private void Remove()
  {
    this.enemyList.RemoveById(this.gameObject.GetInstanceID());
  }
}
