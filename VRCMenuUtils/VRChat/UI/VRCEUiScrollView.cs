﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using UnityEngine.UI;

using VRCMenuUtils;

namespace VRChat.UI
{
    public class VRCEUiScrollView
    {
        #region VRCUI Properties
        public bool Success { get; private set; }
        #endregion

        #region UI Properties
        public Transform Control { get; private set; }
        public Transform ContentControl { get; private set; }

        public RectTransform Position { get; private set; }
        public RectTransform ContentPosition { get; private set; }
        #endregion

        #region Control Properties
        public ScrollRect ScrollRect { get; private set; }
        public ContentSizeFitter SizeFitter { get; private set; }
        public VerticalLayoutGroup LayoutGroup { get; private set; }
        public Mask Mask { get; private set; }
        public Image Image { get; private set; }
        #endregion

        public VRCEUiScrollView(string name, Vector2 position, Vector2 size, float spacing, Transform parent = null)
        {
            // Create game objects
            GameObject goControl = new GameObject(name);
            GameObject goContentControl = new GameObject("Content");

            // Get positions
            Position = goControl.GetOrAddComponent<RectTransform>();
            ContentPosition = goContentControl.GetOrAddComponent<RectTransform>();

            // Create control properties
            ScrollRect = goControl.AddComponent<ScrollRect>();
            SizeFitter = goContentControl.AddComponent<ContentSizeFitter>();
            LayoutGroup = goContentControl.AddComponent<VerticalLayoutGroup>();
            Mask = goControl.AddComponent<Mask>();
            Image = goControl.AddComponent<Image>();

            // Set UI properties
            Control = goControl.transform;
            ContentControl = goContentControl.transform;

            // Set required parts
            if (parent != null)
                Control.SetParent(parent);

            // Setup ScrollView
            Control.localScale = Vector3.one;
            Control.localRotation = Quaternion.identity;
            Control.localPosition = Vector3.zero;
            Position.localPosition = new Vector3(position.x, position.y, 0f);
            Position.sizeDelta = size;
            ScrollRect.vertical = true;
            ScrollRect.horizontal = false;

            // Setup mask
            Mask.showMaskGraphic = false;
            Texture2D texture = new Texture2D(2, 2);
            Color color = new Color(0f, 0f, 0f, 1f);
            texture.SetPixels(new Color[] { color, color, color, color });
            texture.Apply();
            Image.sprite = Sprite.Create(texture, new Rect(0f, 0f, 2f, 2f), new Vector2(0f, 0f));

            // Setup Content
            ContentControl.SetParent(Control);
            ContentControl.localScale = Vector3.one;
            ContentControl.localRotation = Quaternion.identity;
            ContentControl.localPosition = Vector3.zero;
            ContentPosition.localPosition = new Vector3(0f, 10f, 0f);
            ContentPosition.sizeDelta = new Vector2(size.x, spacing);
            SizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
            LayoutGroup.spacing = spacing;
            LayoutGroup.childAlignment = TextAnchor.UpperCenter;
            LayoutGroup.childForceExpandHeight = false;
            LayoutGroup.childForceExpandWidth = false;
            LayoutGroup.childControlHeight = true;
            LayoutGroup.childControlWidth = true;

            ScrollRect.content = ContentPosition;
            ScrollRect.viewport = Position;

            // Finish
            Success = true;
        }
    }
}
