using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CannonController : MonoBehaviour
{
    private Vector3 targetPosition = Vector3.zero;

    [SerializeField, Range(0, 100)]
    public float _power = 5f;
    [SerializeField, Range(0, 90)]
    private float _angle = 10f;

    public TextMeshProUGUI angleText;
    public Slider powerSlider;

    [SerializeField]
    private GameObject _bulletPref;
    [SerializeField]
    private Transform _firePos;

    private void Start()
    {
        powerSlider.minValue = 0;
        powerSlider.maxValue = 100;
        powerSlider.value = _power;

        angleText.text = $"Angle : {_angle}";
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                targetPosition = hit.point;
            }
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            if(_angle < 90)
            {
                ++_angle;
                angleText.text = $"Angle : {_angle}";
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (_angle > 0)
            {
                --_angle;
                angleText.text = $"Angle : {_angle}";
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // fire
            FireBomb();
        }

        // Rotation
        if (targetPosition != Vector3.zero)
        {
            Vector3 targetDir = targetPosition - transform.position;
            targetDir.Normalize();

            transform.rotation = Quaternion.Lerp(transform.rotation, 
                Quaternion.LookRotation(targetDir) * Quaternion.Euler(new Vector3(-_angle, 0, 0)), 10f * Time.deltaTime);
        }
    }

    public void FireBomb()
    {
        GameObject bullet = Instantiate(_bulletPref, _firePos.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * _power, ForceMode.Impulse);
    }

    public void ChangePowerValue()
    {
        _power = powerSlider.value;
    }
}
