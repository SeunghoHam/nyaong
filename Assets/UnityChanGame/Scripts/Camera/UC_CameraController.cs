using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using Cinemachine;

namespace UC
{
    public class UC_CameraController : MonoBehaviour
    {
        //[SerializeField] private CinemachineVirtualCamera _virtualCam;

        [SerializeField] private GameObject cam_Default;
        [SerializeField] private GameObject cam_Game;
        /// <summary>
        /// 카메라 모드 변경
        /// </summary>
        /// <param name="_Mode">"Game", "Defualt"</param>
        public void ChangeCameraMode(string _Mode)
        {
            switch (_Mode)
            {
                case "Game":
                    cam_Default.SetActive(false);
                    cam_Game.SetActive(true);
                    break;
                case "Default":
                    cam_Default.SetActive(true);
                    cam_Game.SetActive(false);
                    break;
            }
        }
    
    }

}
