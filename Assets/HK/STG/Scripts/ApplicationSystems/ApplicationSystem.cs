using UnityEngine;

namespace HK.STG.ApplicationSystems
{
    public sealed class ApplicationSystem : MonoBehaviour
    {
        [SerializeField]
        private int targetFrameRate;

        void Awake()
        {
            Application.targetFrameRate = this.targetFrameRate;
        }
    }
}
