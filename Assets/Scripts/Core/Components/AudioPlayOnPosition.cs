using UnityEngine;
using System.Collections.Generic;

public class AudioPlayOnPosition : MonoBehaviour
{
  [SerializeField]
  private AudioClip clip;

  public void Play()
  {
    AudioSource.PlayClipAtPoint(this.clip, this.transform.position);
  }
}