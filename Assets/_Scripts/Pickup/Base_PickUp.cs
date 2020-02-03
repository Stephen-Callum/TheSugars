using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[System.Serializable]
public class CollectedEvent : UnityEvent<Base_PickUp>
{
}

public class Base_PickUp : MonoBehaviour
{
    // ============ PROPERTIES AND FIELDS ================
    public CollectedEvent m_CollectedEvent = new CollectedEvent();

    // Rotation speed access method
    public float PickupRotationSpeed { get; } = 1.0f;
    // AudioSource accessor method
    public AudioSource PickupSound { get => _pickupSound; set => _pickupSound = value; }
    public SugarRush_GameMode _sugarRushGameMode;
    protected int PickUpValue;

    [SerializeField]
    private AudioSource _pickupSound;
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
        _posOffset = transform.position;
        _pickupSound = GetComponent<AudioSource>();
        _pickupColor = transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material.color;
        print(_pickupColor.ToString());
        _isCollected = false;
    }

    protected virtual void Awake()
    {
        m_CollectedEvent.AddListener(_sugarRushGameMode.IncrementScore);
    }

    protected virtual void Update()
    {
        if (_isCollected)
        {
            collectedTimer += Time.deltaTime;
            _pickupColor.a = Mathf.Lerp(1.0f, 0.0f, collectedTimer * 2);
            _pickupColor = transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material.color = _pickupColor;
        }
    }
}
