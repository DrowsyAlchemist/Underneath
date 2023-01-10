using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class ActivatedAnimation : MonoBehaviour
{
    [SerializeField] private string _id;
    [SerializeField] private bool _saveActivateAnimation;
    [SerializeField] private string _savesFolderName;

    private const string ActivatingAnimation = "Activate";

    private Animator _animator;
    private bool _isActivated;

    private void Start()
    {
        _animator = GetComponent<Animator>();

        if (_saveActivateAnimation)
        {
            ActivationSaver.Load(this);

            if (_isActivated)
                _animator.Play(ActivatingAnimation);
        }
    }

    public void Activate()
    {
        _animator.Play(ActivatingAnimation);
        Action();
        _isActivated = true;

        if (_saveActivateAnimation)
            ActivationSaver.Save(this);
    }

    protected virtual void Action()
    {
    }

    private class ActivationSaver
    {
        private const string SavesFolderName = "/platforms";
        private const string SaveFileExtension = ".save";

        public static void Save(ActivatedAnimation platform)
        {
            string fileName = platform._id + SaveFileExtension;
           // var platformSaveData = new PlatformSaveData(platform.IsActivated);
            //SaveLoadManager.Save(SavesFolderName, fileName, platformSaveData);
        }

        public static void Load(ActivatedAnimation platform)
        {
            string fileName = platform._id + SaveFileExtension;
            var platformSaveData = (PlatformSaveData)SaveLoadManager.GetLoadOrDefault(SavesFolderName, fileName);

           // if (platformSaveData != null)
               // platform.IsActivated = platformSaveData.IsFallen;
        }

        [System.Serializable]
        private class PlatformSaveData
        {
            public bool IsFallen;

            public PlatformSaveData(bool isFallen)
            {
                IsFallen = isFallen;
            }
        }
    }
}
