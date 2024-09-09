using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class Block : MonoBehaviour
{
    public Action<int> OnBlockDestroyed;

    [SerializeField]
    private GameObject _pointShowerGO;
    [SerializeField]
    private int _pointsGained=100;
    [SerializeField]
    private int _hitsToDestroy = 1;
    [SerializeField]
    private int _maxPossibleHits = 3;

    [SerializeField]
    private AudioClip _hitSound;
    [SerializeField]
    private AudioClip _destroySound;

    private SpriteRenderer _blockSR;


    private int _currentRemainHits;

    private void OnValidate()
    {
        if (_blockSR == null) 
        {
            _blockSR = GetComponent<SpriteRenderer>();
        }
        if (_blockSR) 
        {
            _currentRemainHits = _hitsToDestroy;
            UpdateBlockColor();
        }
    }
    private void Awake()
    {
        _currentRemainHits = _hitsToDestroy;
        _blockSR = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<BallController>()) 
        {
            _currentRemainHits -= 1;
            if (_currentRemainHits > 0) 
            {
                SoundManager.Instance.PlaySound(_hitSound);
                UpdateBlockColor();
            }
            else 
            {
                OnBlockDestroyed?.Invoke(_pointsGained);
                SoundManager.Instance.PlaySound(_destroySound);
                var pointsGO = Instantiate(_pointShowerGO, transform);
                pointsGO.transform.SetParent(null);
                var text = pointsGO.GetComponentInChildren<TMP_Text>();
                text.text = "+" + _pointsGained.ToString();
                text.color= Color.Lerp(Color.green, Color.red, (float)_hitsToDestroy/_maxPossibleHits);
                Destroy(pointsGO, 0.4f);
                Destroy(this.gameObject);
            }
        }
    }
    private void UpdateBlockColor() 
    {
        float t = (float)_currentRemainHits / _maxPossibleHits;
        _blockSR.color = Color.Lerp(Color.green, Color.red, t);
    }
}
