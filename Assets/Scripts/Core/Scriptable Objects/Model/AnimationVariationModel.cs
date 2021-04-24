using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// This is for performance purposes, with the Object pooler
/// </summary>
[CreateAssetMenu(menuName = "Models/AnimationVariationModel")]
public class AnimationVariationModel : ScriptableObject
{
  [Tooltip("Minimum number of seconds until switching to a variation")]
  public float Min;
  [Tooltip("Maximum number of seconds until switching to a variation")]
  public float Max;
  public AnimationClip defaultAnimation;
  public List<AnimationVariant> animationVariants;
}

[System.Serializable]
public struct AnimationVariant
{
  [Tooltip("Minimum number of times the animation should play before switching back to default animation")]
  public int Min;
  [Tooltip("Maximum of times the animation should play before switching back to default animation")]
  public int Max;
  [Tooltip("The animation clip that will be played number of times before switching back to default")]
  public AnimationClip animationClip;
}