using UnityEngine;
using UnityEditor;

public class AppMenu
{
    private static string packageFile = "2022.3.23f1.unitypackage";

    [MenuItem("Main Menu/Export Backup", false, 0)]
    static void action01()
    {
        string[] exportpaths = new string[]
        {
            "Assets/Game",
            "Assets/Editor", 
            "ProjectSettings/TagManager.asset",
            "ProjectSettings/EditorBuildSettings.asset"
        };

        AssetDatabase.ExportPackage(exportpaths, packageFile,
            ExportPackageOptions.Interactive |
            ExportPackageOptions.Recurse |
            ExportPackageOptions.IncludeDependencies);

    }

    [MenuItem("Main Menu/Import Backup", false, 1)]
    static void action02()
    {
        AssetDatabase.ImportPackage(packageFile, true);
    }
}
