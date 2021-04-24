using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Add things to list under runtime
/// For example adding texts for dialogue, or add collision tiles to a list
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class RuntimeSet<T> : ScriptableObject
{
  public List<T> items = new List<T>();

  public void Add(T value)
  {
    if (!this.items.Contains(value))
      this.items.Add(value);
  }

  public void Remove(T value)
  {
    if (this.items.Contains(value))
      this.items.Remove(value);
  }

  public void Clear()
  {
    this.items = new List<T>();
  }
}