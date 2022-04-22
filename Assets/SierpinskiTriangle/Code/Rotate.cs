using UnityEngine;

namespace SierpinskiTriangle.Code
{
    public class Rotate : MonoBehaviour
    {
        [SerializeField] private Vector3 speed = Vector3.up;

        private void Update()
        {
            transform.Rotate(speed * Time.deltaTime);
        }
    }
}