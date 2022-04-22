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
                SpawnPoint(pos);
                _vertices[i] = pos;
                pos = rot * pos;
            }
        }

        private void JumpStartInnerPoints()
        {
            _innerPoints = new List<Vector3>();
            var ab = _vertices[1] - _vertices[0];
            var ac = _vertices[2] - _vertices[0];
            var d = _vertices[0] + ab * Random.Range(0f, 0.5f) + ac * Random.Range(0f, 0.5f);
            SpawnPoint(d);
            _innerPoints.Add(d);
        }

        private IEnumerator GenerateInnerPoints()
        {
            while (true)
            {
                var vert = _vertices[Random.Range(0, _vertices.Length)];
                var point = _innerPoints[Random.Range(0, _innerPoints.Count)];
                var nextPos = vert + 0.5f * (point - vert);
                SpawnPoint(nextPos);
                _innerPoints.Add(nextPos);
                yield return null;
            }
        }

        private void SpawnPoint(Vector3 pos)
        {
            var point = Instantiate(pointPrefab, transform);
            point.localPosition = pos;
        }
    }
}
