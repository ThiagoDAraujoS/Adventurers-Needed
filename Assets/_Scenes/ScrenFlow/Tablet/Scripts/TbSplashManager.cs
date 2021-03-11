using UnityEngine;
using UnityEngine.SceneManagement;
namespace ScreenFlow.Tablet
{
    public class TbSplashManager : MonoBehaviour {

        public string NextScene;

        public void ChangeScene() {
            SceneManager.LoadScene(NextScene);
        }
    }
}