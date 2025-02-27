using System.Collections.Generic;
using UnityEngine;

namespace Brawlley
{
    public class MenuManager : MonoBehaviour
    {
        #region Data
        [Header("Menu Data")]
        Stack<MenuPanel> menuStack = new();
        #endregion

        #region Menu Methods
        public void SubscribeMenuPanel(MenuPanel menuPanel) => menuStack.Push(menuPanel);
        public void UnsubscribeMenuPanel() => menuStack.Pop();
        public void CloseAllMenuPanels()
        {
            while (menuStack.Count > 0)
            {
                MenuPanel menuPanel = menuStack.Pop();
                menuPanel.Close();
            }
        }
        public void QuitGame() => Application.Quit();
        #endregion
    }
}