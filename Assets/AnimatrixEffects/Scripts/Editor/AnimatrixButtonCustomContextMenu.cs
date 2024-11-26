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
            AnimatrixButton animatrixButton = customButton.AddComponent<AnimatrixButton>();
            // Optionally set default properties
            Image image = customButton.GetComponent<Image>();
            image.color = Color.cyan; // Set the button background to cyan

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

            // Set position and size
            rectTransform.anchoredPosition = Vector2.zero;
            rectTransform.sizeDelta = new Vector2(160, 30);

            // Register the creation for undo operations
            Undo.RegisterCreatedObjectUndo(customButton, "CreateAnimatrixButton");

            // Select the newly created object
            Selection.activeGameObject = customButton;
        }
    }
}