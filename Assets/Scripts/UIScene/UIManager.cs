using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Uitls;

namespace UISceneModule
{
    public class UIName
    {
        //public const string UISceneEquipManager = "UIScene_EquipManager";
     
    }
    public class UIManager : MonoSingleton<UIManager>
    {

        private Dictionary<string, UIScene> mUIScene = new Dictionary<string, UIScene>();

        public void InitializeUIs()
        {

            mUIScene.Clear();
            Object[] uis = FindObjectsOfType(typeof(UIScene));
            if (uis != null)
            {
                foreach (Object obj in uis)
                {
                    UIScene ui = obj as UIScene;
                    ui.SetVisible(false);
                    mUIScene.Add(ui.gameObject.name, ui);
                }
            }
            SetMainVisible();
        }

        public void SetVisible(string name, bool visible)
        {
            if (visible && !IsVisible(name))
            {
                OpenScene(name);
            }
            else if (!visible && IsVisible(name))
            {
                CloseScene(name);
            }
        }

        public bool IsVisible(string name)
        {
            UIScene ui = GetUI(name);
            if (ui != null)
                return ui.IsVisible();
            return false;
        }
        private UIScene GetUI(string name)
        {
            UIScene ui;
            return mUIScene.TryGetValue(name, out ui) ? ui : null;
        }

        public T GetUI<T>(string name) where T : UIScene
        {
            return GetUI(name) as T;
        }

        private bool isLoaded(string name)
        {
            if (mUIScene.ContainsKey(name))
            {
                return true;
            }
            return false;
        }

        private void OpenScene(string name)
        {
            if (isLoaded(name))
            {
                mUIScene[name].SetVisible(true);
            }
        }
        private void CloseScene(string name)
        {
            if (isLoaded(name))
            {
                mUIScene[name].SetVisible(false);
            }
        }

        public void SetMainVisible()
        {
            //SetVisible(UIName.UISceneEquipManager, true);
            //SetVisible(UIName.UISceneClassMode, true);
        }

    }
}
