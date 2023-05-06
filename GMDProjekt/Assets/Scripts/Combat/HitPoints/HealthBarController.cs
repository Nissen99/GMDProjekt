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
            _hpController = GetComponent<IHpController>();
            healthBarUI.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            if (_hpController.MaxHp == _hpController.CurrentHp)
            {
                return;
            }
        
            healthBarUI.SetActive(true);
        
            setHealthPercentage(_hpController.MaxHp, _hpController.CurrentHp);
        }
        

        private void setHealthPercentage(int maxHp, int currentHp)
        {
            healthPercentage.value = (float) currentHp/maxHp;;
        }
    }
}

    

