using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseOverTime : MonoBehaviour
{
  [SerializeField]
  private FloatVariable valueToIncrease;
  [SerializeField]
  private float increaseBy;
  [SerializeField]
  private float increaseAfterNumberOfSeconds;
  private float passedTime;
  private bool increase;

  private float defaultValue;

  private void OnEnable()
  {
    this.StartIncrease();
  }

  private void OnDisable()
  {
    this.Reset();
  }

  private void Update()
  {
    this.TryToIncrease();
  }

  private void TryToIncrease()
  {
    if (!this.increase)
    {
      return;
    }

    if (this.passedTime > increaseAfterNumberOfSeconds)
    {
      this.valueToIncrease.ApplyChange(this.increaseBy);
      this.passedTime = 0;
    }
    else
    {
      this.passedTime += Time.deltaTime;
    }
  }

  private void Reset()
  {
    this.valueToIncrease.Value = this.defaultValue;
    this.increase = false;
  }

  private void StartIncrease()
  {
    this.defaultValue = this.valueToIncrease.Value;
    this.increase = true;
  }
}
