using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SierpinskiTriangle.Code
{
    public class Triangle : MonoBehaviour
    {
        [SerializeField] private Transform pointPrefab;
        [SerializeField] private float radius = 5;
        [SerializeField] private float scale = .05f;

        [Header("Debug")]
        [SerializeField] private int count;

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
            _vertices = new[]
            {
                radius * new Vector3(Mathf.Sqrt(8f/9f), 0, -1f/3f),
                radius * new Vector3(-Mathf.Sqrt(2f/9f), Mathf.Sqrt(2f/3f), -1f/3f),
                radius * new Vector3(-Mathf.Sqrt(2f/9f), -Mathf.Sqrt(2f/3f), -1f/3f),
                radius * new Vector3(0, 0, 1)
            };

            foreach (var vertex in _vertices)
            {
                SpawnPoint(vertex);
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
            while (_innerPoints.Count < int.MaxValue)
            {
                var vert = _vertices[Random.Range(0, _vertices.Length)];
                var point = _innerPoints[Random.Range(0, _innerPoints.Count)];
                var nextPos = vert + 0.5f * (point - vert);
                SpawnPoint(nextPos);
                _innerPoints.Add(nextPos);
                count = _innerPoints.Count;
                yield return null;
            }
        }

        private void SpawnPoint(Vector3 pos)
        {
            var point = Instantiate(pointPrefab, transform);
            point.localPosition = pos;
            point.localScale = scale * Vector3.one;
        }
    }
}
