using System;
using Player;
using TMPro;
using UnityEngine;

namespace UI
{
    public class PauseElement : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI amountText;
        [SerializeField] private GameObject removeIconImage;
        [SerializeField] private int time;
        [SerializeField] private bool isMultiplePause;

        private PlayerController _playerController;
        
        private void Awake()
        {
            amountText.text = time.ToString();
            if (time == 0)
            {
                removeIconImage.SetActive(true);
                amountText.gameObject.SetActive(false);
            }
            _playerController = FindObjectOfType<PlayerController>();
        }

        public void OnClick()
        {
            _playerController.SelectPauseTime(time, isMultiplePause);
        }
    }
}