    using UnityEditor;
     
    public class MaxAtlasSize : EditorWindow
    {
        int[] kSizeValues = { 32, 64, 128, 256, 512, 1024, 2048, 4096 };
        string[] kSizeStrings = { "32","64","128","256","512", "1024", "2048", "4096" };
     
        void OnGUI()
        {
            LightmapEditorSettings.maxAtlasHeight = EditorGUILayout.IntPopup("Max Atlas Size", LightmapEditorSettings.maxAtlasHeight, kSizeStrings, kSizeValues);
            LightmapEditorSettings.maxAtlasWidth = LightmapEditorSettings.maxAtlasHeight;
        }
     
        [MenuItem("Utilities/Max Atlas Size")]
        static void Init()
        {
            EditorWindow window = EditorWindow.GetWindow(typeof(MaxAtlasSize));
            window.Show();
        }
    }
