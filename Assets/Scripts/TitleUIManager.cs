using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class TitleUIManager : MonoBehaviour
{
    public GameObject MapUI;
    private void Start()
    {
        MapUI.SetActive(false);
    }
    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MapUI.SetActive(true);
        }
    }
    public void SceneChange(string nam)
    {
        SceneManager.LoadScene(nam);
    }

}
