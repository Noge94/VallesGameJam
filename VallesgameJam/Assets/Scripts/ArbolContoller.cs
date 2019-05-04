using UnityEngine;

namespace DefaultNamespace
{
    public class ArbolContoller : MonoBehaviour
    {
        public void Play()
        {
            GetComponent<Animator>().enabled = true;
        }
    }
}