using DefaultNamespace;
using Events;
using UnityEngine;
using UnityEngine.UI;

namespace Combat.HitPoints
{
    public class HealthBarController : MonoBehaviour
    {
        //Why can I not just go "_healthPercentage = healthBarUI.GetComponent<Slider>();", it does not work, but i have no clue why :shrug:
        private IHpController _hpController;

        [SerializeField] private GameObject healthBarUI;

        [SerializeField] private Slider  healthPercentage;

        // Start is called before the first frame update
        void Start()
        {
            GetComponent<HpController>().onHealthChange.AddListener(hideHealthBarOnDied);
            _hpController = GetComponent<IHpController>();
            healthBarUI.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            if (_hpController.GetMaxHealth() == _hpController.GetCurrentHealth() || healthBarUI == null)
            {
                return;
            }
        
            healthBarUI.SetActive(true);
        
            setHealthPercentage(_hpController.GetMaxHealth(), _hpController.GetCurrentHealth());
        }
        

        private void setHealthPercentage(int maxHp, int currentHp)
        {
            healthPercentage.value = (float) currentHp/maxHp;;
        }

        private void hideHealthBarOnDied(int currentHp, int maxHp)
        {
            if (currentHp <= 0)
            {
                Destroy(healthBarUI.gameObject);
            }
        }
    }
}

    

