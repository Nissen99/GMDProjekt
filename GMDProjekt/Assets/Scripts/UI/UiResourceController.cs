using Combat.AttackManager;
using UnityEngine;
using UnityEngine.UI;

// TODO: This is a copy of the UIHealthController class, should be made less DRY, but i did not think it was worth the time
// as this is the only 2 UI sliders i just duplicated the code
namespace UI
{
    public class UiResourceController : MonoBehaviour
    {
        public GameObject PlayerThatHasResourceManager;

        private Slider _resourceSlider;
        // Start is called before the first frame update
        void Start()
        {
            PlayerThatHasResourceManager.GetComponent<ResourceManager>().OnResourceChanged.AddListener(UpdateResource);
            _resourceSlider = GetComponent<Slider>();
            _resourceSlider.value = 100;
        }
    
        void UpdateResource(int currentResource, int maxResource)
        {
            var currentResourcePercentage = (float)currentResource / maxResource;
            if (currentResourcePercentage < 0)
            {
                currentResourcePercentage = 0;
            }

            _resourceSlider.value = currentResourcePercentage;
        }
    }
}
