using UnityEngine;

namespace _Scripts
{
    public class PlacementManager : MonoBehaviour
    { 
        [SerializeField] private GameObject _buildPrefab;


        public void Build(Vector3 position)
        {
            Instantiate(_buildPrefab, position, Quaternion.identity);
        }
    }
}
