using Assets.Scripts.Manager;
using Assets.Scripts.Tools;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EraseInfo : MonoBehaviour
{
	void Start()
	{
		GetComponent<Button>().onClick.AddListener(TaskOnClick);

	}

	// Update is called once per frame
	void TaskOnClick()
	{
		SerializationManager.ClearInfo(FileFolderHelper.StorageFilePath);
		SceneManager.LoadScene("LoginScene");
	}
}
