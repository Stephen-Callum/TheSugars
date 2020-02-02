using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Base_PickUp : MonoBehaviour
{
    // ============ PROPERTIES AND FIELDS ================
    // Rotation speed access method
    public float PickupRotationSpeed { get; } = 1.0f;
    // AudioSource accessor method
    public AudioSource PickupSound { get => _pickupSound; set => _pickupSound = value; }
    //protected int PickUpValue { get => _pickUpValue; set => _pickUpValue = value; }

    [SerializeField]
    private AudioSource _pickupSound;
    [SerializeField]
    private float _amplitude = .1f;
    private float _frequency = 1f;
    private Vector3 _posOffset = new Vector3();
    private Vector3 _tempPos = new Vector3();
    private Color _pickupColor;
    private bool _isCollected;
    private float collectedTimer = 0.0f;
    private SugarRush_GameMode _sugarRushGameMode;
    //private int _pickUpValue;

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

    public virtual void AddScore(int score)
    {
        _sugarRushGameMode.ScoreBox.GetComponent<Text>().text = "999";
    }

    private void Start()
    {
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
            _pickupColor.a = Mathf.Lerp(1.0f, 0.0f, collectedTimer * 2);
            _pickupColor = transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material.color = _pickupColor;
            print("alpha colour = " + _pickupColor.a);

        }
    }
}
