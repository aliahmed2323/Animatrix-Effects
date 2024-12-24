using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using Animatrix;

namespace Animatrix.CustomEditors
{
    public class AnimatrixButtonContextMenu
    {
        [MenuItem("GameObject/UI/Animatrix Effects/Animatrix Button", false, 0)]
        private static void CreateCustomUIButton(MenuCommand menuCommand)
        {
            // Create a new GameObject for the custom UI button
            GameObject customButton = new GameObject("Animatrix Button");

            // Ensure it has a RectTransform component for UI
            RectTransform rectTransform = customButton.AddComponent<RectTransform>();
            customButton.AddComponent<CanvasRenderer>();
            customButton.AddComponent<Image>(); // Add an Image for the background
                                                // Add a Button component
            customButton.AddComponent<AnimatrixButton>();

            // Optionally set default properties
            Image image = customButton.GetComponent<Image>();
            image.color = Color.white; 

            // Set it as a child of the selected UI Canvas or create one if none exists
            GameObject parent = menuCommand.context as GameObject;
            if (parent == null || parent.GetComponentInParent<Canvas>() == null)
            {
                // Create a Canvas if one doesn't exist
                GameObject canvas = new GameObject("Canvas");
                canvas.AddComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
                canvas.AddComponent<CanvasScaler>();
                canvas.AddComponent<GraphicRaycaster>();

                // Set it as the parent
                parent = canvas;
            }
            customButton.transform.SetParent(parent.transform, false);

            GameObject text = new GameObject("Text");
            text.transform.SetParent(customButton.transform);
            RectTransform textRectTransform = text.AddComponent<RectTransform>();
            text.AddComponent<CanvasRenderer>();
            text.AddComponent<TMPro.TextMeshProUGUI>();
            text.GetComponent<TMPro.TMP_Text>().text = "Animatrix Button";
            text.GetComponent<TMPro.TMP_Text>().color = Color.black;
            text.GetComponent<TMPro.TMP_Text>().alignment = TMPro.TextAlignmentOptions.Center;
            

            // Set position and size
            rectTransform.anchoredPosition = Vector2.zero;
            rectTransform.sizeDelta = new Vector2(160, 30);
            textRectTransform.anchoredPosition = Vector2.zero;
            textRectTransform.sizeDelta = new Vector2(320, 60);

            // Register the creation for undo operations
            Undo.RegisterCreatedObjectUndo(customButton, "CreateAnimatrixButton");

            // Select the newly created object
            Selection.activeGameObject = customButton;
        }
    }
}