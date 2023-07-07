using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoaderScript : MonoBehaviour
{
    public Animator transition;

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0)) {
            //LoadNextLevel(1);
        //}
    }

    public void LoadNextLevel (string id) {
        StartCoroutine(LoadLevel(id));
    }

    IEnumerator LoadLevel(string levelIndex) {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene(levelIndex);
    }
}