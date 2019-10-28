using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class menuController : MonoBehaviour
{
    [SerializeField]
    private int id;
    [SerializeField]
    private Canvas menu;
    [SerializeField]
    private Canvas loading;
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private bool check;
    public void Play()
    {
        StartCoroutine(LoadAsynchronously(id));
    }

    public void Quit()
    {
        Application.Quit();
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        menu.gameObject.SetActive(false);
        loading.gameObject.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync(id);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;
            yield return null;
        }
    }

    public void ChangeMenu()
    {
        menu.gameObject.SetActive(!menu.gameObject.activeInHierarchy);
    }

    public void Update()
    {
        if (check)
            if (Input.GetKeyDown(StaticMaster.settings.input.menu))
                ChangeMenu();
                
        
    }
}
