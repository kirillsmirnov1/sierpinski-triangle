using UnityEngine;

namespace SierpinskiTriangle.Code
{
    public class PointScaling : MonoBehaviour
    {
        [SerializeField] private float decreaseRatio = 0.99f;
        [SerializeField] private float minScale = 0.01f;

        private Rigidbody _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if(_rb.IsSleeping()) _rb.WakeUp();
        }

        private void OnCollisionStay(Collision other)
        {
            if(transform.localScale.magnitude < other.transform.localScale.magnitude) return;
            if(transform.localScale.magnitude < minScale) return;
            transform.localScale *= decreaseRatio;
        }
    }
}