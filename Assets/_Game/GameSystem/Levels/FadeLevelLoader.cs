using UnityEngine;

namespace LOK1game
{
    public class FadeLevelLoader : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        public void LoadLevel(LevelData levelData)
        {
            _animator.SetTrigger("Play");
            
            SilentlyLoadLevel.LoadLevel(levelData);
        }
    }
}