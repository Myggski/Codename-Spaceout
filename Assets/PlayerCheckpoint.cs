using UnityEngine;

public class PlayerCheckpoint : MonoBehaviour
{
  private GameObject player;

  private void Awake()
  {
    this.SetComponents();
  }

  private void SetComponents()
  {
    this.player = GameObject.FindGameObjectWithTag("Player");
  }

  public void Respawn()
  {
    if (this.player != null)
    {
      this.player.transform.position = this.transform.position;
      this.player.SetActive(true);
    }
  }
}
