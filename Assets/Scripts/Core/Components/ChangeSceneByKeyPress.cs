using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Utilities;

public class ChangeSceneByKeyPress : MonoBehaviour
{

  [SerializeField]
  private SceneField scene;
  [SerializeField]
  private Animator animator;
  [SerializeField]
  private bool canChangeScene;
  private bool changingScene;

  private void Awake()
  {
    this.SetComponents();
  }

  private void Update()
  {
    this.CheckKeyPress();
  }

  private void SetComponents()
  {
    this.animator = GetComponent<Animator>();
  }

  private void CheckKeyPress()
  {
    if (!canChangeScene)
    {
      return;
    }

    if (Input.anyKeyDown && this.scene != null && this.scene.SceneName != string.Empty && !this.changingScene)
    {
      StartCoroutine(this.ChangeScene());
    }
  }

  private IEnumerator ChangeScene()
  {
    this.changingScene = true;

    if (this.animator != null)
    {
      this.animator.SetTrigger("Start");
    }

    yield return new WaitForSeconds(1f);

    SceneManager.LoadScene(this.scene.SceneName);
  }
}
