using PlanetGearScheme.Core.Dictionares;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PlanetGearScheme.Components {
    public class SceneNavigationManager : MonoBehaviour {
        public void OnMainMenu() {
            SceneManager.LoadScene(SceneConstants.Names.MainMenu.ToString());
        }

        public void OnPlanetGear() {
            SceneManager.LoadScene(SceneConstants.Names.PlanetReductor.ToString());
        }

        public void OnExitGame() {
            Application.Quit();

#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#endif
        }
    }
}
