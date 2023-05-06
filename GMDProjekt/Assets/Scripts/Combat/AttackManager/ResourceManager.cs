using System.Collections;
using Events;
using UnityEngine;
using UnityEngine.Events;

namespace Combat.AttackManager
{
    public class ResourceManager : MonoBehaviour, IResourceManager
    {
        public int currentResource;
        public int maxResource;
        public int resourceRegenRate;
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
            return currentResource >= amount;
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