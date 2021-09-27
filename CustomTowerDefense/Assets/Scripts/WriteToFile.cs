using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WriteToFile : MonoBehaviour
{
    private void OnGUI()
    {
        GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
        buttonStyle.fontSize = 25;
        if(GUI.Button(new Rect(10,10,450,80),"Send Information To File", buttonStyle))
        {
            StartCoroutine(sendTextToFile());
        }
        if (GUI.Button(new Rect(10, 100, 450, 80), "Get Information To File", buttonStyle))
        {
            StartCoroutine(getTextFromFile());
        }
    }

    IEnumerator sendTextToFile()
    {
        bool successful = true;

        List<IMultipartFormSection> form = new List<IMultipartFormSection>();
        form.Add(new MultipartFormDataSection("name", "Joe Bloggs"));
        form.Add(new MultipartFormDataSection("kills", "92"));
        UnityWebRequest www = UnityWebRequest.Post("http://localhost:9000/WebSaving.php", form);
        www.chunkedTransfer = false;
        www.useHttpContinue = false;
        yield return www.SendWebRequest();
        if (www.error != null)
        {
            Debug.Log(www.error);
            Debug.Log(www.url);
            successful = false;
        }
        else
        {
            //Debug.Log(www.text);
            Debug.Log(www.downloadHandler.text);
            Debug.Log(www.url);
            successful = true;
        }
    }

    IEnumerator getTextFromFile()
    {
        bool successful = true;
      
        UnityWebRequest www = UnityWebRequest.Get("http://localhost:9000/WebLoading.php");
        www.chunkedTransfer = false;
        www.useHttpContinue = false;
        yield return www.SendWebRequest();
        if (www.error != null)
        {
            Debug.Log(www.error);
            Debug.Log(www.url);
            successful = false;
        }
        else
        {
            //Debug.Log(www.text);
            Debug.Log(www.downloadHandler.text);
            Debug.Log(www.url);
            successful = true;
        }
    }
}
