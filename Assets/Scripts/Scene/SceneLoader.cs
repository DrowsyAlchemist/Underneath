using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private TMP_Text _progressText;
    [SerializeField] private SceneLoadAnimator _loadAnimator;
    [SerializeField] private SceneMusic _sceneMusic;
    [SerializeField] private string _mainMenuSceneName = "MainMenu";

    private const int MaxFakeProgressStep = 3;
    private const int MinFakeProgressStep = 0;
    private static SceneLoader _instance;

    private AsyncOperation _loadingSceneOperation;
    private Vector3 _playerSpawnPosition;
    private int fakeProgress;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        enabled = false;
    }

    private void OnEnable()
    {
        fakeProgress = 0;
        _progressText.text = "0 %";
    }

    private void Update()
    {
        fakeProgress += Random.Range(MinFakeProgressStep, MaxFakeProgressStep);
        fakeProgress = Mathf.Clamp(fakeProgress, 0, 99);
        _progressText.text = fakeProgress + " %";

        if (_loadingSceneOperation.isDone)
        {
            _progressText.text = "100 %";
            OnSceneLoaded();
        }
    }

    public static void LoadScene(string sceneName, Vector3 playerSpawnPosition)
    {
        _instance._playerSpawnPosition = playerSpawnPosition;
        _instance.StartCoroutine(_instance.LoadSceneWithAnimation(sceneName));
    }

    private IEnumerator LoadSceneWithAnimation(string sceneName)
    {
        _loadAnimator.PlayUnload();
        yield return new WaitForEndOfFrame();
        float delay = _loadAnimator.CurrentAnimationLength;
        yield return new WaitForSeconds(delay);
        LoadSceneAsync(sceneName);
    }

    private void LoadSceneAsync(string sceneName)
    {
        AccessPoint.SetEnable(false);
        enabled = true;
        _loadingSceneOperation = SceneManager.LoadSceneAsync(sceneName);
    }

    private void OnSceneLoaded()
    {
        enabled = false;
        _loadAnimator.PlayLoad();
        AccessPoint.Player.transform.position = _playerSpawnPosition;

        if (SceneManager.GetActiveScene().name.Equals(_mainMenuSceneName) == false)
        {
            AccessPoint.SetEnable(true);
            _sceneMusic.PlayGameMusic();
        }
        else
        {
            _sceneMusic.PlayMainMenuMusic();
        }
    }
}
