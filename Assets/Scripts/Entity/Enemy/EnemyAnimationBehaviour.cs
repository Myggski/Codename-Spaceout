using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyAnimationBehaviour : StateMachineBehaviour
{
  [SerializeField]
  private AnimationVariationModel animationVariationModel;
  private AnimationVariant nextAnimation;
  private float animationPlaytime;
  private int lastPlayed;

  // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
  public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {
    if (this.IsCurrentAnimationDefault(animator, layerIndex))
    {
      this.animationPlaytime = this.animationVariationModel.defaultAnimation.length * Random.Range(this.animationVariationModel.Min, this.animationVariationModel.Max);
      this.nextAnimation = this.GetNextAnimation();
    }
    else
    {
      AnimationVariant currentAnimation = this.GetAnimationVariantByHashCode(animator, layerIndex);

      if (!currentAnimation.Equals(null))
      {
        this.animationPlaytime = currentAnimation.animationClip.length * Random.Range(currentAnimation.Min, currentAnimation.Max);
      }
      else
      {
        this.PlayDefaultAnimation(animator);
      }
    }
  }

  // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
  public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {
    if (this.animationPlaytime <= 0)
    {
      if (this.IsCurrentAnimationDefault(animator, layerIndex))
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

  private AnimationVariant GetNextAnimation()
  {
    return animationVariationModel.animationVariants[Random.Range(0, animationVariationModel.animationVariants.Count)];
  }

  private bool IsCurrentAnimationDefault(Animator animator, int layerIndex)
  {
    return animator.GetCurrentAnimatorClipInfo(layerIndex)[0].clip.Equals(this.animationVariationModel.defaultAnimation);
  }

  private int GetHashCodeByName(string name)
  {
    return Animator.StringToHash($"Base Layer.{name}");
  }

  private AnimationVariant GetAnimationVariantByHashCode(Animator animator, int layerIndex)
  {
    return this.animationVariationModel.animationVariants.Find(x => x.animationClip.name == animator.GetCurrentAnimatorClipInfo(layerIndex)[0].clip.name);
  }

  private void PlayDefaultAnimation(Animator animator)
  {
    animator.Play(this.GetHashCodeByName(this.animationVariationModel.defaultAnimation.name));
  }
}
