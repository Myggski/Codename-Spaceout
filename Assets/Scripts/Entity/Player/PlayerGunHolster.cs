using UnityEngine;
using System;

public class PlayerGunHolster : MonoBehaviour
{
  [SerializeField]
  private GameObject gunPrefab;
  [SerializeField]
  private PlayerGunDataModel playerGunDataModel;

  private SpriteRenderer gunSpriteRenderer;
  private GunShooter gunShooter;

  private void Start()
  {
    this.SetComponents();
    this.RemoveGun();
    this.Setup();
  }

  private void SetComponents()
  {
    this.gunShooter = this.gunPrefab.GetComponent<GunShooter>();
    this.gunSpriteRenderer = this.gunPrefab.GetComponent<SpriteRenderer>();
  }

  private void Setup()
  {
    if (this.playerGunDataModel.HasGunEquipped())
    {
      this.EquipGun(this.playerGunDataModel.GunItem);
    }
    else
    {
      this.gunPrefab.SetActive(false);
    }
  }

  private void RemoveGun()
  {
    this.playerGunDataModel.ResetStats();
  }

  private void EquipGun(GunItem gunModel)
  {
    if (!this.gunPrefab.activeSelf)
    {
      this.gunPrefab.SetActive(true);
    }

    if (this.gunSpriteRenderer != null)
    {
      this.gunSpriteRenderer.sprite = gunModel.sprite;
    }

    if (this.gunShooter != null)
    {
      this.gunShooter.Setup(gunModel.projectilePrefab, gunModel.muzzleFlashPrefab);
    }

    this.playerGunDataModel.SwapStats(gunModel);
  }

  public void Equip(GunItem gunModel, Action<GunItem> callback)
  {
    if (callback != null && this.playerGunDataModel.HasGunEquipped())
    {
      callback(this.playerGunDataModel.GunItem);
    }

    this.EquipGun(gunModel);
  }
}
