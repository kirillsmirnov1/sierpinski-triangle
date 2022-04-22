using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SierpinskiTriangle.Code
{
    public class Triangle : MonoBehaviour
    {
        [SerializeField] private Transform pointPrefab;
        [SerializeField] private float radius = 5;
        
        private Vector3[] _vertices;
        private List<Vector3> _innerPoints;

        private void Start()
        {
            CreateVertices();
            JumpStartInnerPoints();
            StartCoroutine(GenerateInnerPoints());
        }

        private void CreateVertices()
        {
            _vertices = new Vector3[3];
            var pos = Vector3.up * radius;
            var rot = Quaternion.Euler(0, 0, 120);

            for (int i = 0; i < 3; i++)
            {
                _vertices[i] = Instantiate(pointPrefab, pos, Quaternion.identity, transform).position;
                pos = rot * pos;
            }
        }

        private void JumpStartInnerPoints()
        {
            _innerPoints = new List<Vector3>();
            var ab = _vertices[1] - _vertices[0];
            var ac = _vertices[2] - _vertices[0];
            var d = _vertices[0] + ab * Random.Range(0f, 0.5f) + ac * Random.Range(0f, 0.5f);
            _innerPoints.Add(Instantiate(pointPrefab, d, Quaternion.identity, transform).position);
        }

        private IEnumerator GenerateInnerPoints()
        {
            while (true)
            {
                var vert = _vertices[Random.Range(0, _vertices.Length)];
                var point = _innerPoints[Random.Range(0, _innerPoints.Count)];
                var nextPos = vert + 0.5f * (point - vert);
                _innerPoints.Add(Instantiate(pointPrefab, nextPos, Quaternion.identity, transform).position);
                yield return null;
            }
        }
    }
}
