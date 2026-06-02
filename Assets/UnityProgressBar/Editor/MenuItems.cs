using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UnityProgressBar.Editor
{
    static class MenuItems
    {
        const int MenuPriority = 10;

        [MenuItem("GameObject/UI/Progress Bar/Progress Bar - Fill", false, MenuPriority)]
        public static void CreateFillProgressBar(MenuCommand menuCommand)
        {
            var path = "Packages/com.annulusgames.ugui-progress-bar/Editor/DefaultAssets/Progress Bar - Fill.prefab";
            CreateUIItem(path, "Progress Bar", menuCommand);
        }

        [MenuItem("GameObject/UI/Progress Bar/Circular Progress Bar", false, MenuPriority + 1)]
        public static void CreateCircularFillProgressBar(MenuCommand menuCommand)
        {
            var path = "Packages/com.annulusgames.ugui-progress-bar/Editor/DefaultAssets/Circular Progress Bar.prefab";
            CreateUIItem(path, "Circular Progress Bar", menuCommand);
        }

        [MenuItem("GameObject/UI/Progress Bar/Progress Bar - Stretch", false, MenuPriority + 2)]
        public static void CreateStretchProgressBar(MenuCommand menuCommand)
        {
            var path = "Packages/com.annulusgames.ugui-progress-bar/Editor/DefaultAssets/Progress Bar - Stretch.prefab";
            CreateUIItem(path, "Progress Bar", menuCommand);
        }

        static void CreateUIItem(string assetPath, string objectName, MenuCommand menuCommand)
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(assetPath);
            var obj = Object.Instantiate(prefab);

            var canvas = FindObjectOfType<Canvas>();
            if (canvas == null)
            {
                canvas = new GameObject("Canvas").AddComponent<Canvas>();
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                canvas.gameObject.AddComponent<CanvasScaler>();
                canvas.gameObject.AddComponent<GraphicRaycaster>();

                if (FindObjectOfType<EventSystem>() == null)
                {
                    _ = new GameObject("EventSystem", typeof(EventSystem), typeof(StandaloneInputModule));
                }
            }

            obj.name = objectName;
            GameObjectUtility.SetParentAndAlign(obj, menuCommand.context as GameObject ?? canvas.gameObject);
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localScale = Vector3.one;

            Undo.RegisterCreatedObjectUndo(obj, "Create " + objectName);
            Selection.activeGameObject = obj;
        }

        static T FindObjectOfType<T>() where T : Object
        {
#if UNITY_2023_1_OR_NEWER || UNITY_6000_0_OR_NEWER
            return Object.FindAnyObjectByType<T>();
#else
            return Object.FindObjectOfType<T>();
#endif
        }
    }
}
