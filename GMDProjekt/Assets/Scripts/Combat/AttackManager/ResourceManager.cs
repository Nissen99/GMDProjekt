using System.Collections;
using DefaultNamespace;
using Events;
using UnityEngine;
using UnityEngine.Events;

namespace Combat.AttackManager
{
    public class ResourceManager : MonoBehaviour, IResourceManager
    {
        public int currentResource;
        public int maxResource = 100;
        public int resourceRegenRate = 15;
        public ResourceChangedEvent OnResourceChanged;

        
        // I am using Awake because it is an event that other scripts will likely subscribe to in their start, so we need
        // to make sure this has happened before start.
        private void Awake()
        {
            OnResourceChanged = new ResourceChangedEvent(); 
        }

        // Start is called before the first frame update
        void Start()
        {
            currentResource = maxResource;
            StartCoroutine(RegenerateResource());
        }

        public bool HasEnough(int amount)
        {
            if (currentResource >= amount)
            {
                return true;
            }
            
            FindObjectOfType<AudioManager>().Play(AUDIOCLIPS.MORE_ENERGY_NEEDED);
            return false;
        }

        public void Spend(int amount)
        {
            currentResource -= amount;
            if (currentResource < 0)
            {
                currentResource = 0;
            }
            OnResourceChanged.Invoke(currentResource, maxResource);
        }

        public void Generate(int amount)
        {
            currentResource += amount;
            if (currentResource > maxResource)
            {
                currentResource = maxResource;
            }
            OnResourceChanged.Invoke(currentResource, maxResource);
        }

        IEnumerator RegenerateResource()
        {
            while (true)
            {
                Generate(resourceRegenRate);
                yield return new WaitForSeconds(1);
            }
        }
    }
}