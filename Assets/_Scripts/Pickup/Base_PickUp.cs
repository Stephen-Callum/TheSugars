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
    public CollectedEvent m_CollectedEvent = new CollectedEvent();

    public float PickupRotationSpeed { get; } = 1.0f;
    public AudioSource PickupSound { get => _pickupSound; set => _pickupSound = value; }
    public int PickUpValue { get => pickUpValue; set => pickUpValue = value; }
    protected SugarRush_GameMode _sugarRushGameMode;

    [SerializeField]
    protected int pickUpValue;

    [SerializeField]
    private AudioSource _pickupSound;
    private float _amplitude = .1f;
    private float _frequency = 1f;
    private Vector3 _posOffset = new Vector3();
    private Vector3 _tempPos = new Vector3();
    private Color _pickupColor;
    private bool _isCollected;
    private float collectedTimer = 0.0f;


    
    // Pickup rotation motion around Y axis
    public virtual void RotatePickup() => transform.Rotate(0, PickupRotationSpeed, 0, Space.World);

    // returns the pickupValue
    virtual public int GetValue()
    {
        return 0;
    }

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
        PickupSound.Play();
        m_CollectedEvent.Invoke(this);
        Destroy(gameObject, 1);
    }

    // Handle what happens when the player overlaps 
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OnCollection();
        }
    }

    // Called during the first frame
    private void Start()
    {
        _posOffset = transform.position;
        //_pickupSound = GetComponent<AudioSource>();
        _pickupColor = transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material.color;
        _isCollected = false;
    }

    // Instantiate game objects
    protected virtual void Awake()
    {
        _sugarRushGameMode = GameObject.FindGameObjectWithTag("GameMode").GetComponent<SugarRush_GameMode>();
        m_CollectedEvent.AddListener(_sugarRushGameMode.IncrementScore);
        PickupSound = GetComponent<AudioSource>();
    }

    // Unity Update function every frame.
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
