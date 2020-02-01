using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_PickUp : MonoBehaviour
{
    // ============ PROPERTIES AND FIELDS ================
    // Rotation speed access method
    public float PickupRotationSpeed { get; } = 1.0f;
    // AudioSource accessor method
    public AudioSource PickupSound { get => _pickupSound; set => _pickupSound = value; }
    public float FadePerSecond { get => _fadePerSecond; set => _fadePerSecond = value; }

    [SerializeField]
    private AudioSource _pickupSound;
    [SerializeField]
    private float _fadePerSecond = 2.5f;
    // Reference to ScoreText UI element
    private GameObject ScoreText;
    private float _amplitude = .1f;
    private float _frequency = 1f;
    private Vector3 _posOffset = new Vector3();
    private Vector3 _tempPos = new Vector3();
    private Color _pickupColor;
    private bool _isCollected;
    private float collectedTimer = 0.0f;

    // ============ METHODS ==================
    // Pickup rotation motion around Y axis
    public virtual void RotatePickup() => transform.Rotate(0, PickupRotationSpeed, 0, Space.World);

    // Pickup hover motion on Y Axis
    public virtual void YAxisHoverMotion() 
    {
        _tempPos = _posOffset;
        _tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * _frequency) * _amplitude;
        transform.position = _tempPos;
    }

    // On collection of Pickup (NOTE: acts independently of CollectionAnimation() in Update()
    public virtual void OnCollection()
    {
        gameObject.GetComponent<BoxCollider>().enabled = false;
        _isCollected = true;
        print("isCollected = TRUE");
        Destroy(gameObject, 1);
    }


    private void Start()
    {
        ScoreText = GameObject.FindGameObjectWithTag("ScoreText");
        _posOffset = transform.position;
        _pickupSound = GetComponent<AudioSource>();
        _pickupColor = transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material.color;
        print(_pickupColor.ToString());
        _isCollected = false;
    }

    protected virtual void Update()
    {
        if (_isCollected)
        {
            collectedTimer += Time.deltaTime;
            print("Color changing");
            _pickupColor.a = Mathf.Lerp(1.0f, 0.0f, collectedTimer);
            _pickupColor = transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material.color = _pickupColor;
            print("alpha colour = " + _pickupColor.a);

        }
    }
}
