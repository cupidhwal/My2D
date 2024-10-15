using System.Collections.Generic;
using UnityEngine;

namespace My2D
{
    public class DetectionZone : MonoBehaviour
    {
        #region Variables
        // 감지된 콜라이더 리스트
        public List<Collider2D> detectedList = new();
        #endregion

        private void OnTriggerEnter2D(Collider2D collision)
        {
            detectedList.Add(collision);
            Debug.Log(collision);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            detectedList.Remove(collision);
        }
    }
}