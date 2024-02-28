using System;
using System.Collections;
using Core.SceneObjects;
using UnityEngine;

namespace Core.UI
{
    public class PopupText : MonoBehaviour
    {
        [SerializeField] private GameObject popupText;
        private void OnEnable()
        {
            InteractEvents.OnMoneyInteract += DisplayText;
        }

        private void OnDestroy()
        {
            InteractEvents.OnMoneyInteract -= DisplayText;
        }

        private void DisplayText(CubeMoney cubeMoney)
        {
            StartCoroutine(Display());
        }

        IEnumerator Display()
        {
            var newPopupText = Instantiate(popupText, transform);
            yield return new WaitForSeconds(1);
            Destroy(newPopupText);
        }
    }
}