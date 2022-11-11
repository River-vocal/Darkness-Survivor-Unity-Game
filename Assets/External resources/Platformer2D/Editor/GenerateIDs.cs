using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using UnityEditor.SceneManagement;

namespace IndieMarc.Platformer
{
    public class GenerateIDs : ScriptableWizard
    {

        [MenuItem("Tools/Generate UIDs")]
        static void SelectAllOfTagWizard()
        {
            ScriptableWizard.DisplayWizard<GenerateIDs>("Generate Unique IDs", "Generate UIDs");
        }

        void OffsetObjects(UniqueID[] objs)
        {
            HashSet<string> existing_ids = new HashSet<string>();

            foreach (UniqueID uid_obj in objs)
            {
                if (uid_obj.unique_id != "")
                {
                    if (existing_ids.Contains(uid_obj.unique_id))
                        uid_obj.unique_id = "";
                    else
                        existing_ids.Add(uid_obj.unique_id);
                }
            }

            foreach (UniqueID uid_obj in objs)
            {
                if (uid_obj.unique_id == "")
                {
                    //Generate new ID
                    string new_id = "";
                    while (new_id == "" || existing_ids.Contains(new_id))
                    {
                        new_id = UniqueID.GenerateUniqueID();
                    }

                    //Add new id
                    uid_obj.unique_id = uid_obj.uid_prefix + new_id;
                    existing_ids.Add(new_id);
                    EditorUtility.SetDirty(uid_obj);
                }
            }
            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        }

        void OnWizardCreate()
        {
            OffsetObjects(GameObject.FindObjectsOfType<UniqueID>());
        }
    }

}