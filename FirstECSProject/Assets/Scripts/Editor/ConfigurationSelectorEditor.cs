using UnityEditor;
using System.IO;

namespace Kulikova
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(ConfigurationSelector))]
    public class ConfigurationSelectorEditor : Editor
    {
        private int _choiceIndex = 0;

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var playerDataIds = AssetDatabase.FindAssets("t:PlayerData");

            string[] playerDataNames = new string[playerDataIds.Length];
            for (int i = 0; i < playerDataIds.Length; i++)
            {
                var playerDataName = AssetDatabase.GUIDToAssetPath(playerDataIds[i]);
                playerDataNames[i] = Path.GetFileName(playerDataName);
            }

            _choiceIndex = EditorGUILayout.Popup(_choiceIndex, playerDataNames);

            var playerDataPath = AssetDatabase.GUIDToAssetPath(playerDataIds[_choiceIndex]);
            var playerData = AssetDatabase.LoadAssetAtPath<PlayerData>(playerDataPath);

            var selector = target as ConfigurationSelector;
            selector.SelectedPlayerData = playerData;
        }
    }
}
