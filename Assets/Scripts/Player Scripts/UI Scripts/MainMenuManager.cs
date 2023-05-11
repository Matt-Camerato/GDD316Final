using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    private Animator anim;

    private void Start() => anim = GetComponent<Animator>();

    public void StartGame() => SceneManager.LoadScene(1);

    public void Settings() => anim.SetTrigger("Settings");
    public void Return() => anim.SetTrigger("Return");

    public void InteractSFX() => AudioManager.Instance.InteractSFX();
}
