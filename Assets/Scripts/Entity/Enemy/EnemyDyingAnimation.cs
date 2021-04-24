using UnityEngine;

public class EnemyDyingAnimation : MonoBehaviour
{
  private Animator animator;

  private void Start()
  {
    this.SetComponents();
  }

  private void SetComponents()
  {
    this.animator = GetComponent<Animator>();
  }

  public void Dying()
  {
    this.animator.SetBool("Dying", true);
  }
}