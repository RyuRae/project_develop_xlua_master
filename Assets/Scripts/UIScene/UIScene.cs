using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

namespace UISceneModule
{
    public enum ESceneType
    {
        Main = 0,
        Nomal = 1,
        Popup = 2,
        Announce = 3,
    }
    public class UIScene : MonoBehaviour
    {

        protected string mUIName = "";

        private Dictionary<string, UISceneWidget> mUIWidgets = new Dictionary<string, UISceneWidget>();

        private Dictionary<string, UIComponent> mUIComponents = new Dictionary<string, UIComponent>();

        //public UIAnchor.Side side = UIAnchor.Side.Center;

        public ESceneType type = ESceneType.Nomal;

        protected virtual void Start()
        {
            this.FindChildWidgets(gameObject.transform);
            FindChildUIButtons(transform);
        }


        public virtual bool IsVisible()
        {
            return gameObject.activeSelf;
        }
        public virtual void SetVisible(bool visible)
        {
            gameObject.SetActive(visible);
        }
        protected UISceneWidget GetWidget(string name)
        {
            // If allready find out, return 
            if (mUIWidgets.ContainsKey(name))
                return mUIWidgets[name];

            // Find out widget with name and add to dictionary
            Transform t = gameObject.transform.Find(name);
            if (t == null) return null;

            UISceneWidget widget = t.gameObject.GetComponent<UISceneWidget>();
            if (widget != null)
            {
                mUIWidgets.Add(widget.gameObject.name, widget);
            }

            return t.gameObject.GetComponent<UISceneWidget>();
        }
        protected T GetWidget<T>(string name) where T : Component
        {
            // Find out widget with name and add to dictionary
            GameObject go = GameObject.Find(name);
            if (go == null) return null;

            T widget = go.GetComponent<T>();

            return widget;
        }

        public UIComponent GetUIComponent(string name)
        {
            if (mUIComponents.ContainsKey(name))
            {
                return mUIComponents[name];
            }
            return null;
        }

        private void FindChildWidgets(Transform t)
        {
            UISceneWidget widget = t.gameObject.GetComponent<UISceneWidget>();
            if (widget != null)
            {
                //			Debug.LogWarning("FindChildWidgets Parent[" + t.name + "] " + t.gameObject.name);
                string name = t.gameObject.name;
                if (!mUIWidgets.ContainsKey(name))
                {
                    mUIWidgets.Add(name, widget);
                }
                else
                {
                    //				Debug.LogWarning("Scene[" + this.transform.name + "]UISceneWidget[" + name + "]is exist!");
                }
            }
            for (int i = 0; i < t.childCount; ++i)
            {
                Transform child = t.GetChild(i);
                FindChildWidgets(child);
            }
        }

        private void FindChildUIButtons(Transform trans)
        {
            UIComponent button = trans.GetComponent<UIComponent>();
            if (button != null)
            {
                string name = trans.name;
                if (!mUIComponents.ContainsKey(name))
                {
                    mUIComponents.Add(name, button);
                }
            }
            if (trans.childCount > 0)
            {
                for (int i = 0; i < trans.childCount; i++)
                {
                    Transform tempTrans = trans.GetChild(i);
                    FindChildUIButtons(tempTrans);
                }
            }
            
        }

    }
}
