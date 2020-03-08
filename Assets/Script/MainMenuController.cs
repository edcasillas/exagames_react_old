using CommonUtils.Inspector.SceneRefs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
	[SerializeField] private SceneReference sceneToLoad;
	
    void Update()
    {
		if(Input.GetKeyDown(KeyCode.Return))
		{
			ScenesManager.Instance?.ChangeScene(sceneToLoad);
		}
    }
}
