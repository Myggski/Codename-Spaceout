using UnityEngine;

[
    RequireComponent(typeof(SpriteRenderer))
]
public class PlayerGhostEffect : MonoBehaviour
{
  [SerializeField]
  private GameObject ghostEffectPrefab;
  [SerializeField]
  private float ghostDelay;
  private float ghostDelayTimer;
  private SpriteRenderer spriteRenderer;
  private bool createGhost;

  private void Awake()
  {
    this.SetComponents();
  }

  private void Start()
  {
    this.ghostDelayTimer = this.ghostDelay;
  }

  // Update is called once per frame
  private void Update()
  {
    this.TrySpawnGhost();
  }

  private void SetComponents()
  {
    this.spriteRenderer = GetComponent<SpriteRenderer>();
  }

  private void TrySpawnGhost()
  {
    if (!createGhost)
    {
      return;
    }

    if (this.ghostDelayTimer > 0)
    {
      this.ghostDelayTimer -= Time.deltaTime;
    }
    else
    {
      this.SpawnGhost();
    }
  }

  private void SpawnGhost()
  {
    GameObject ghostEffect = Instantiate(this.ghostEffectPrefab, this.transform);
    ghostEffect.transform.SetParent(null);

    SpriteRenderer ghostEffectSpriteRenderer = ghostEffect.GetComponent<SpriteRenderer>();

    if (ghostEffectSpriteRenderer != null && this.spriteRenderer != null)
    {
      ghostEffectSpriteRenderer.sprite = this.spriteRenderer.sprite;
      ghostEffectSpriteRenderer.sortingOrder = this.spriteRenderer.sortingOrder;
    }

    this.ghostDelayTimer = this.ghostDelay;
    Destroy(ghostEffect, 1f);
  }

  /// <summary>
  /// Sets createGhost to true/false, to turn on or off the ghost spawning script in update.
  /// Also spawns a ghost directly, to not have to wait for the delay, for the first and last spawn.
  /// </summary>
  /// <param name="createGhost"></param>
  public void ToggleCreateGhosts(bool createGhost)
  {
    this.createGhost = createGhost;

    this.SpawnGhost();
  }
}
