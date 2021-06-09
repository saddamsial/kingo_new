#if UNITY_EDITOR
using UnityEditor;
using UnityEngine.SceneManagement;
using System.IO;
#endif
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;

namespace MTAssets.EasyMeshCombiner
{
    /*
      This class is responsible for the functioning of the "Combined Meshes Manager" component, and all its functions.
    */
    /*
     * The Easy Mesh Combiner was developed by Marcos Tomaz in 2019.
     * Need help? Contact me (mtassets@windsoft.xyz)
     */

    [AddComponentMenu("")] //Hide this script in component menu.
    public class CombinedMeshesManager : MonoBehaviour
    {
        //Private variables
        [HideInInspector]
        public int ExportToObjStartIndexOffSet = 0;

#if UNITY_EDITOR
        //Public variables of Interface
        private bool gizmosOfThisComponentIsDisabled = false;

        //Classes of script
        [System.Serializable]
        public class OriginalGameObjectWithMesh
        {
            //Class that stores a original GameObject With Mesh data, to restore on undo merge.

            public GameObject gameObject;
            public bool originalGoState;
            public MeshRenderer meshRenderer;
            public bool originalMrState;

            public OriginalGameObjectWithMesh(GameObject gameObject, bool originalGoState, MeshRenderer meshRenderer, bool originalMrState)
            {
                this.gameObject = gameObject;
                this.originalGoState = originalGoState;
                this.meshRenderer = meshRenderer;
                this.originalMrState = originalMrState;
            }
        }

        //Enums of script
        public enum UndoMethod
        {
            EnableOriginalMeshes,
            ReactiveOriginalGameObjects
        }

        //Variables of script
        [HideInInspector]
        public UndoMethod undoMethod;
        [HideInInspector]
        public List<OriginalGameObjectWithMesh> originalGosToRestore = new List<OriginalGameObjectWithMesh>();
        [HideInInspector]
        public string pathsOfAssetToDelete;
        [HideInInspector]
        public bool thisIsPrefab = false;

        //The UI of this component
        #region INTERFACE_CODE
        [UnityEditor.CustomEditor(typeof(CombinedMeshesManager))]
        public class CustomInspector : UnityEditor.Editor
        {
            //Private temp variables
            Vector2 scrollviewMaterials = Vector2.zero;

            public override void OnInspectorGUI()
            {
                //Start the undo event support, draw default inspector and monitor of changes
                CombinedMeshesManager script = (CombinedMeshesManager)target;
                EditorGUI.BeginChangeCheck();
                Undo.RecordObject(target, "Undo Event");
                script.gizmosOfThisComponentIsDisabled = MTAssetsEditorUi.DisableGizmosInSceneView("CombinedMeshesManager", script.gizmosOfThisComponentIsDisabled);

                //Start of UI
                EditorGUILayout.HelpBox("This GameObject contains the meshes you previously combined. Through this component you can manage the mesh resulting from the merge.", MessageType.Info);

                GUILayout.Space(20);

                //Verify if has missing files of merge, if data save in assets option is enabled
                if (script.pathsOfAssetToDelete != "")
                {
                    MeshFilter mergedMesh = script.GetComponent<MeshFilter>();
                    if (mergedMesh.sharedMesh == null)
                    {
                        EditorGUILayout.HelpBox("Oops! It looks like there are missing mesh files in this merge. To solve this problem, you can undo this merge and re-do it again!", MessageType.Error);
                        GUILayout.Space(20);
                    }
                }

                //If this merge is a prefab, a copy of original, not renderizes the management buttons
                if (script.thisIsPrefab == false || script.originalGosToRestore.Count > 0)
                {
                    //Select all original gameObjects
                    EditorGUILayout.LabelField("Selection Of Original GameObjects", EditorStyles.boldLabel);
                    GUILayout.Space(10);

                    if (GUILayout.Button("Select All Original GameObjects!", GUILayout.Height(30)))
                    {
                        List<GameObject> gameObjects = new List<GameObject>();
                        foreach (OriginalGameObjectWithMesh ogo in script.originalGosToRestore)
                            gameObjects.Add(ogo.gameObject);
                        Selection.objects = gameObjects.ToArray();
                    }

                    //Select all original gameObjects with X material
                    Dictionary<Material, List<GameObject>> objects = new Dictionary<Material, List<GameObject>>();
                    foreach (OriginalGameObjectWithMesh oGo in script.originalGosToRestore)
                    {
                        if (oGo == null)
                            continue;

                        foreach (Material mat in oGo.meshRenderer.sharedMaterials)
                            if (mat != null)
                            {
                                if (objects.ContainsKey(mat) == false)
                                    objects.Add(mat, new List<GameObject>() { oGo.gameObject });
                                if (objects.ContainsKey(mat) == true)
                                    objects[mat].Add(oGo.gameObject);
                            }
                    }

                    //Create a scroll view to select all gameobjects where material is equal to...
                    GUILayout.Space(10);
                    EditorGUILayout.LabelField("Selection By Material", EditorStyles.boldLabel);
                    GUILayout.Space(10);

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Select All Original Meshes That Uses...", GUILayout.Width(320));
                    GUILayout.Space(MTAssetsEditorUi.GetInspectorWindowSize().x - 320);
                    EditorGUILayout.LabelField("Size", GUILayout.Width(30));
                    EditorGUILayout.IntField(objects.Keys.Count, GUILayout.Width(50));
                    EditorGUILayout.EndHorizontal();
                    GUILayout.BeginVertical("box");
                    scrollviewMaterials = EditorGUILayout.BeginScrollView(scrollviewMaterials, GUIStyle.none, GUI.skin.verticalScrollbar, GUILayout.Width(MTAssetsEditorUi.GetInspectorWindowSize().x), GUILayout.Height(150));
                    if (objects.Keys.Count == 0)
                        EditorGUILayout.HelpBox("Oops! The original materials of this blend were not found!", MessageType.Info);
                    if (objects.Keys.Count > 0)
                        foreach (var key in objects.Keys)
                            if (GUILayout.Button("\"" + key.name + "\" Material", GUILayout.Height(24)))
                                Selection.objects = objects[key].ToArray();
                    EditorGUILayout.EndScrollView();
                    GUILayout.EndVertical();

                    //Undo and delete this merge
                    GUILayout.Space(10);
                    EditorGUILayout.LabelField("Management Of This Merge", EditorStyles.boldLabel);
                    GUILayout.Space(10);

                    if (script.GetComponent<MeshCollider>() == null)
                        if (GUILayout.Button("Add Mesh Collider To This Mesh", GUILayout.Height(30)))
                        {
                            script.gameObject.AddComponent<MeshCollider>();
                            Debug.Log("A Mesh Collider was added to this mesh!.");
                        }
                    if (GUILayout.Button("Recalculate Normals Of This Mesh", GUILayout.Height(30)))
                    {
                        script.GetComponent<MeshFilter>().sharedMesh.RecalculateNormals();
                        Debug.Log("The normals of this mesh resulting from the merging were recalculated.");
                    }
                    if (GUILayout.Button("Recalculate Tangents Of This Mesh", GUILayout.Height(30)))
                    {
                        script.GetComponent<MeshFilter>().sharedMesh.RecalculateTangents();
                        Debug.Log("The tangents of this mesh resulting from the merging were recalculated.");
                    }
                    if (GUILayout.Button("Export This Mesh As OBJ", GUILayout.Height(30)))
                        script.ExportMeshAsObj(script);
                    if (GUILayout.Button("Optimize This Mesh", GUILayout.Height(30)))
                    {
                        script.GetComponent<MeshFilter>().sharedMesh.Optimize();
                        Debug.Log("The mesh resulting from the merge has been optimized!");
                    }
                    if (GUILayout.Button("Undo And Delete This Merge", GUILayout.Height(30)))
                    {
                        bool confirmation = EditorUtility.DisplayDialog("Undo",
                            "This combined mesh and your GameObject will be deleted and removed from your scene. The original GameObjects/Meshes will be restored to their original state before the merge.\n\nAre you sure you want to undo this merge?",
                            "Yes",
                            "No");
                        if (confirmation == true)
                            script.UndoAndDeleteThisMerge();
                    }
                }
                if (script.thisIsPrefab == true && script.originalGosToRestore.Count == 0)
                    EditorGUILayout.HelpBox("This merge is a Prefab of the original merge. If you want to manage the merge, please go to the original resulting merge, the merge that was generated when you first combined these meshes. If you no longer have it, and you want to undo this merge, you can just delete this GameObject, but this will not automatically re-activate the original meshes, since the Prefabs of the mesh resulting from the merging lose references to the original meshes.", MessageType.Warning);

                //Final space
                GUILayout.Space(10);

                //Stop paint of GUI, if this gameobject no more exists
                if (script == null)
                    return;

                //Apply changes on script, case is not playing in editor
                if (GUI.changed == true && Application.isPlaying == false)
                {
                    EditorUtility.SetDirty(script);
                    UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(script.gameObject.scene);
                }
                if (EditorGUI.EndChangeCheck() == true)
                {

                }
            }
        }
        #endregion

        //Tools code

        static string ExportToObjProcessTransform(Transform transform, CombinedMeshesManager script)
        {
            StringBuilder meshString = new StringBuilder();
            meshString.Append("#" + transform.name
                            + "\n#-------"
                            + "\n");

            meshString.Append("g ").Append(transform.name).Append("\n");

            MeshFilter mf = transform.GetComponent<MeshFilter>();

            if (mf)
                meshString.Append(ExportToObjMeshToString(mf, transform, script));

            for (int i = 0; i < transform.childCount; i++)
                meshString.Append(ExportToObjProcessTransform(transform.GetChild(i), script));

            return meshString.ToString();
        }

        public static string ExportToObjMeshToString(MeshFilter mf, Transform t, CombinedMeshesManager script)
        {
            Vector3 s = t.localScale;
            Vector3 p = t.localPosition;
            Quaternion r = t.localRotation;
            int numVertices = 0;

            Mesh m = mf.sharedMesh;
            if (!m)
                return "####Error####";

            Material[] mats = mf.GetComponent<MeshRenderer>().sharedMaterials;
            StringBuilder sb = new StringBuilder();

            foreach (Vector3 vv in m.vertices)
            {
                Vector3 v = t.TransformPoint(vv);
                numVertices++;
                sb.Append(string.Format("v {0} {1} {2}\n", v.x, v.y, -v.z));
            }

            sb.Append("\n");

            foreach (Vector3 nn in m.normals)
            {
                Vector3 v = r * nn;
                sb.Append(string.Format("vn {0} {1} {2}\n", -v.x, -v.y, v.z));
            }

            sb.Append("\n");

            foreach (Vector3 v in m.uv)
            {
                sb.Append(string.Format("vt {0} {1}\n", v.x, v.y));
            }

            for (int material = 0; material < m.subMeshCount; material++)
            {
                sb.Append("\n");
                sb.Append("usemtl ").Append(mats[material].name).Append("\n");
                sb.Append("usemap ").Append(mats[material].name).Append("\n");

                int[] triangles = m.GetTriangles(material);
                for (int i = 0; i < triangles.Length; i += 3)
                {
                    sb.Append(string.Format("f {0}/{0}/{0} {1}/{1}/{1} {2}/{2}/{2}\n",
                        triangles[i] + 1 + script.ExportToObjStartIndexOffSet, triangles[i + 1] + 1 + script.ExportToObjStartIndexOffSet, triangles[i + 2] + 1 + script.ExportToObjStartIndexOffSet));
                }
            }

            script.ExportToObjStartIndexOffSet += numVertices;
            return sb.ToString();
        }

        //Component code

        void UndoAndDeleteThisMerge()
        {
            //Undo the merge according the type of merge
            if (undoMethod == UndoMethod.EnableOriginalMeshes)
            {
                foreach (OriginalGameObjectWithMesh original in originalGosToRestore)
                {
                    //Skip, if is null
                    if (original.meshRenderer == null)
                    {
                        continue;
                    }
                    original.meshRenderer.enabled = original.originalMrState;
                }
            }
            if (undoMethod == UndoMethod.ReactiveOriginalGameObjects)
            {
                foreach (OriginalGameObjectWithMesh original in originalGosToRestore)
                {
                    //Skip, if is null
                    if (original.gameObject == null)
                    {
                        continue;
                    }
                    original.gameObject.SetActive(original.originalGoState);
                }
            }

            //Delete unused asset, if this is not a prefab
            if (thisIsPrefab == false)
            {
                if (AssetDatabase.LoadAssetAtPath(pathsOfAssetToDelete, typeof(Mesh)) != null)
                {
                    AssetDatabase.DeleteAsset(pathsOfAssetToDelete);
                }
            }

            //Set scene as dirty
            UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene());

            //Show dialog
            Debug.Log("The merge was successfully undone. All of the original GameObject/Meshes that this Manager could still access have been restored!\n\nIf you had chosen to save the merged meshes to your project files, all useless mesh files were deleted automatically!");

            //Destroy this merge
            DestroyImmediate(this.gameObject, true);
        }

        void ExportMeshAsObj(CombinedMeshesManager script)
        {
            //Open the export window
            string folder = EditorUtility.OpenFolderPanel("Select Folder To Export", "", "");
            if (String.IsNullOrEmpty(folder) == true)
                return;

            //Show progress bar
            EditorUtility.DisplayProgressBar("A moment", "Exporting Mesh as OBJ", 1.0f);

            //Get this mesh
            MeshRenderer meshRenderer = this.gameObject.GetComponent<MeshRenderer>();
            MeshFilter meshFilter = this.gameObject.GetComponent<MeshFilter>();

            //Start export of mesh
            ExportToObjStartIndexOffSet = 0;
            StringBuilder meshString = new StringBuilder();
            meshString.Append("#" + meshFilter.sharedMesh.name + ".obj"
                                + "\n#" + System.DateTime.Now.ToLongDateString()
                                + "\n#" + System.DateTime.Now.ToLongTimeString()
                                + "\n#-------"
                                + "\n\n");
            Transform transform = this.gameObject.transform;
            Vector3 originalPosition = transform.position;
            transform.position = Vector3.zero;
            meshString.Append(ExportToObjProcessTransform(transform, script));
            string meshStringResult = meshString.ToString();
            using (StreamWriter stringWriter = new StreamWriter(folder + "/" + meshFilter.sharedMesh.name + ".obj"))
            {
                stringWriter.Write(meshStringResult);
            }
            transform.position = originalPosition;
            ExportToObjStartIndexOffSet = 0;

            //Clear progress bar
            EditorUtility.ClearProgressBar();

            //Show warning
            Debug.Log("The mesh was successfully exported to the directory \"" + folder + "\".");
        }
#endif
    }
}