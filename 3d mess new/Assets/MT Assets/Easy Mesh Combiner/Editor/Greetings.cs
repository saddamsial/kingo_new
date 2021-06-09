using UnityEditor;
using System.IO;

namespace MTAssets.EasyMeshCombiner.Editor
{
    /*
     * This class is responsible for displaying the welcome message when installing this asset. 
     */

    [InitializeOnLoad]
    class Greetings
    {
        //This asset parameters 

        public static string assetName = "Easy Mesh Combiner";
        public static string pathForThisAsset = "Assets/MT Assets/Easy Mesh Combiner";
        public static string optionalObservation = "";
        public static string pathToGreetingsFile = "Assets/MT Assets/_AssetsData/Greetings/GreetingsData.Emc.ini";
        public static string linkForAssetStorePage = "https://assetstore.unity.com/publishers/40306";

        //Greetings script methods

        static Greetings()
        {
            //Run the script after Unity compiles
            EditorApplication.delayCall += Run;
        }

        static void Run()
        {
            //Create base directory "_AssetsData" and "Greetings" if not exists yet
            CreateBaseDirectoriesIfNotExists();

            //Verify if the greetings message already showed, if not yet, show the message
            VerifyAndShowAssetGreentingsMessageIfNeverShowedYet();
        }

        public static void CreateBaseDirectoriesIfNotExists()
        {
            //Create the directory to feedbacks folder, of this asset
            if (!AssetDatabase.IsValidFolder("Assets/MT Assets"))
                AssetDatabase.CreateFolder("Assets", "MT Assets");
            if (!AssetDatabase.IsValidFolder("Assets/MT Assets/_AssetsData"))
                AssetDatabase.CreateFolder("Assets/MT Assets", "_AssetsData");
            if (!AssetDatabase.IsValidFolder("Assets/MT Assets/_AssetsData/Greetings"))
                AssetDatabase.CreateFolder("Assets/MT Assets/_AssetsData", "Greetings");
        }

        public static void VerifyAndShowAssetGreentingsMessageIfNeverShowedYet()
        {
            //If the greetings file not exists
            if (AssetDatabase.LoadAssetAtPath(pathToGreetingsFile, typeof(object)) == null)
            {
                //Create a new greetings file
                File.WriteAllText(pathToGreetingsFile, "Done");

                //Show greetings and save
                EditorUtility.DisplayDialog(assetName + " was imported!",
                    "The " + assetName + " was imported for your project. Please do not change the directory of the files for this asset. You should be able to locate it in the directory" +
                    "\n\n" +
                    "(" + pathForThisAsset + ")" +
                    "\n\n" +
                    optionalObservation +
                    "Remember to read the documentation to understand how to use this asset and get the most out of it!" +
                    "\n" +
                    "The documentation can be found in the directory of this asset. Just open the 'Documentation.zip' file and open the 'Documentation.html'." +
                    "\n\n" +
                    "You can get support via email (mtassets@windsoft.xyz)" +
                    "\n\n" +
                    "- Thank you for purchasing the asset! :)",
                    "Ok, Cool!");

                //Select the folder of project
                UnityEngine.Object assetFolder = (UnityEngine.Object)AssetDatabase.LoadAssetAtPath(pathForThisAsset, typeof(UnityEngine.Object));
                Selection.activeObject = assetFolder;
                EditorGUIUtility.PingObject(assetFolder);

                //Update files
                AssetDatabase.Refresh();
            }
        }
    }
}