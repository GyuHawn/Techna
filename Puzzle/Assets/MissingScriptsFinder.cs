using UnityEngine;
using UnityEditor;

public class MissingScriptsFinder : MonoBehaviour
{
    [MenuItem("Tools/Find Missing Scripts")]
    static void FindMissingScripts()
    {
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
        int missingScriptCount = 0;

        foreach (GameObject obj in allObjects)
        {
            Component[] components = obj.GetComponents<Component>();
            for (int i = 0; i < components.Length; i++)
            {
                if (components[i] == null)
                {
                    missingScriptCount++;
                    Debug.LogWarning("Missing script found in object: " + obj.name, obj);
                }
            }
        }

        if (missingScriptCount > 0)
        {
            Debug.LogWarning("Total missing scripts: " + missingScriptCount);
        }
        else
        {
            Debug.Log("No missing scripts found.");
        }
    }
}
