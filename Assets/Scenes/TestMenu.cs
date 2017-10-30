using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using NUnit.Framework;
using System.Collections;
using UnityEngine.EventSystems;

public class TestMenu 
{

	[Test]
	public void TestMenuSimplePasses()
	{
		// Use the Assert class to test conditions.
	}

	// A UnityTest behaves like a coroutine in PlayMode
	// and allows you to yield null to skip a frame in EditMode
	[UnityTest]
	public IEnumerator TestMenuWithEnumeratorPasses()
	{
		// Use the Assert class to test conditions.
		UnityEngine.SceneManagement.SceneManager.LoadScene(Constants.MENU_SCENE_NAME,UnityEngine.SceneManagement.LoadSceneMode.Additive);
		yield return null;
		GameObject[] button = GameObject.FindGameObjectsWithTag ("MenuItem");
		ExecuteEvents.Execute(button[2],null, ExecuteEvents.submitHandler);

	}
		
}
