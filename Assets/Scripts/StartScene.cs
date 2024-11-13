using UnityEngine;

public class StartScene : MonoBehaviour
{
    [SerializeField] private GameObject _ui;

    private void Update()
    {
       if(Input.GetKeyDown(KeyCode.F))
            _ui.gameObject.SetActive(true);
    }
}