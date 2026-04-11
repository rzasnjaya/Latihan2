using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;

    public GameObject panel;
    public Transform progressBar;

    private Vector3 barScale = Vector3.one;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        DisablePanel();
    }

    void DisablePanel()
    {
        panel.SetActive(false);
    }

    void UpdateBar(float value)
    {
        barScale.x = value;
        progressBar.localScale = barScale;
    }

    IEnumerator LoadScene(string sceneName)
    {
        panel.SetActive(true);
        UpdateBar(0f);
        yield return null; // tunggu 1 frame dulu biar panel keliatan

        AsyncOperation asyncLoading = SceneManager.LoadSceneAsync(sceneName);
        asyncLoading.allowSceneActivation = false;

        while (asyncLoading.progress < 0.9f)
        {
            UpdateBar(Mathf.Clamp01(asyncLoading.progress / 0.9f));
            yield return null;
        }

        // Animasi fake loading dari 0.9 ke 1.0 biar keliatan
        float fakeProgress = 0.9f;
        while (fakeProgress < 1f)
        {
            fakeProgress += Time.deltaTime * 0.5f; // kecepatan animasi
            UpdateBar(Mathf.Clamp01(fakeProgress));
            yield return null;
        }

        UpdateBar(1f);
        yield return new WaitForSeconds(0.3f);

        asyncLoading.allowSceneActivation = true;
        DisablePanel();
    }

    public void ChangeScene(string sceneName)
    {
        StartCoroutine(LoadScene(sceneName));
    }
}
