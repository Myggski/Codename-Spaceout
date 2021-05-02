using UnityEngine;
using System.Linq;

/// <summary>
/// This script makes the enemy more alive
/// It randomly changes Idle-states after a certain amount of time
/// </summary>
public class EnemyAnimationBehaviour : StateMachineBehaviour
{
  [SerializeField]
  private AnimationVariationModel animationVariationModel;
  private AnimationVariant nextAnimation;
  private float animationPlaytime;
  private int lastPlayed;

  /// <summary>
  /// Sets the next random animation, and when it should play
  /// animationPlaytime = animation playtime length * random number of times its going to repeat
  /// </summary>
  /// <param name="animator"></param>
  /// <param name="stateInfo"></param>
  /// <param name="layerIndex"></param>
  public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {
    if (this.IsCurrentAnimationDefault(animator, layerIndex))
    {
      this.animationPlaytime = this.animationVariationModel.defaultAnimation.length * Random.Range(this.animationVariationModel.Min, this.animationVariationModel.Max);
      this.nextAnimation = this.GetNextAnimation();
    }
    else if (animator.GetCurrentAnimatorClipInfo(layerIndex).Length > 0)
    {
      AnimationVariant currentAnimation = this.GetAnimationVariantByHashCode(animator, layerIndex);

      if (!currentAnimation.Equals(null) && currentAnimation.animationClip != null)
      {
        this.animationPlaytime = currentAnimation.animationClip.length * Random.Range(currentAnimation.Min, currentAnimation.Max);
      }
      else
      {
        this.PlayDefaultAnimation(animator);
      }
    }
    else
    {
      this.PlayDefaultAnimation(animator);
    }
  }

  /// <summary>
  /// When the playtime of the current animation is zero or less it changes animation
  /// </summary>
  /// <param name="animator"></param>
  /// <param name="stateInfo"></param>
  /// <param name="layerIndex"></param>
  public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {
    if (animator.GetCurrentAnimatorClipInfo(layerIndex).Length == 0)
    {
      return;
    }

    if (this.animationPlaytime <= 0)
    {
      if (this.IsCurrentAnimationDefault(animator, layerIndex) && !this.nextAnimation.animationClip.Equals(null))
      {
        animator.Play(this.GetHashCodeByName(this.nextAnimation.animationClip.name));
      }
      else
      {
        this.PlayDefaultAnimation(animator);
      }
    }

    this.animationPlaytime -= Time.deltaTime;
  }

  /// <summary>
  /// Randomly selects the next animation to play
  /// </summary>
  /// <returns></returns>
  private AnimationVariant GetNextAnimation()
  {
    AnimationVariant animationVariant = animationVariationModel.animationVariants[Random.Range(0, animationVariationModel.animationVariants.Count)];
    return animationVariant;
  }

  /// <summary>
  /// Checks to see if the enemy is playing the default idle-animation
  /// </summary>
  /// <param name="animator"></param>
  /// <param name="layerIndex"></param>
  /// <returns></returns>
  private bool IsCurrentAnimationDefault(Animator animator, int layerIndex)
  {
    return animator.GetCurrentAnimatorClipInfo(layerIndex).Length == 0 || animator.GetCurrentAnimatorClipInfo(layerIndex)[0].clip == null || (animator.GetCurrentAnimatorClipInfo(layerIndex).Length > 0 && animator.GetCurrentAnimatorClipInfo(layerIndex)[0].clip.Equals(this.animationVariationModel.defaultAnimation));
  }

  /// <summary>
  /// Gets the animation by name
  /// </summary>
  /// <param name="name"></param>
    /// <returns></returns>
  private int GetHashCodeByName(string name)
  {
    return Animator.StringToHash($"Base Layer.{name}");
  }

  /// <summary>
  /// Finds the animation from the model by name
  /// </summary>
  /// <param name="animator"></param>
  /// <param name="layerIndex"></param>
  /// <returns></returns>
  private AnimationVariant GetAnimationVariantByHashCode(Animator animator, int layerIndex)
  {
    return this.animationVariationModel.animationVariants.FirstOrDefault(x => x.animationClip.name == animator.GetCurrentAnimatorClipInfo(layerIndex)[0].clip.name);
  }

  /// <summary>
  /// Plays the default animation
  /// </summary>
  /// <param name="animator"></param>
  private void PlayDefaultAnimation(Animator animator)
  {
    animator.Play(this.GetHashCodeByName(this.animationVariationModel.defaultAnimation.name));
  }
}
