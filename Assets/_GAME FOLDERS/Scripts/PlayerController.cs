using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private LayerMask layer;
    private Vector3 _clickLocation;
    [SerializeField] private float _stopDistance;
    [SerializeField] private float _moveSpeed;

    private Vector3 _targetScale;
    [SerializeField] private Vector3 _scaleFactor;
    [SerializeField] private float _scaleThreshold;
    [SerializeField] private float _lerpSpeed;

    private void Start()
    {
        _targetScale = transform.localScale;
    }
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, 500f, layer))
            {
                _clickLocation = hit.point;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            _clickLocation = transform.position;
        }
    }
    private void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, _clickLocation) >= _stopDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, _clickLocation, _moveSpeed * Time.fixedDeltaTime);
        }
        if (Vector3.Distance(transform.localScale, _targetScale) >= _scaleThreshold)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, _targetScale, _lerpSpeed * Time.fixedDeltaTime);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GrowOrb"))
        {
            Destroy(other.gameObject);
            _targetScale = transform.localScale + _scaleFactor;
            _scaleFactor *= 1.2f;
        }
        if (other.CompareTag("ShrinkOrb"))
        {
            Destroy(other.gameObject);
            _targetScale = transform.localScale - _scaleFactor;
            _scaleFactor /= 1.2f;
        }
    }
}
