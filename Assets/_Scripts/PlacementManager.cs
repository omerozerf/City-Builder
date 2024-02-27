using UnityEngine;

namespace _Scripts
{
    public class PlacementManager : MonoBehaviour
    { 
        [SerializeField] private GameObject _buildPrefab;
        [SerializeField] private Transform _groundTransform;


        public void Build(Vector3 position)
        {
            Vector3 fixedPosition = position + _groundTransform.position;
            Instantiate(_buildPrefab, fixedPosition, Quaternion.identity);
        }
    }
}
