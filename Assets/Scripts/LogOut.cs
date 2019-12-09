using Assets.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogOut : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		GetComponent<Button>().onClick.AddListener(TaskOnClick);
		
    }

    // Update is called once per frame
    void TaskOnClick()
    {
        Connect.History.Clear();
		Connect.q.Clear();
	    SceneManager.LoadScene("WelcomeScene");
    }
}
