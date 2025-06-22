using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using Unity.VisualScripting;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private bool _isPlaying = true;

    [SerializeField]
    private bool _isLeftSide = false;

    [SerializeField]
    private float _speed;

    [SerializeField] private GamePlayController _gameplayController;

    [SerializeField] private TMP_Text _score;

    [SerializeField]
    private Vector3 _leftPos;
    
    [SerializeField]
    private Vector3 _rightPost;

    [SerializeField] private GameObject _winEffect;
    [SerializeField] private GameObject _loseEffect;

    private Vector3 _targetPosition;

    private Vector3 _refPos;

    private int _currentScore = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        _targetPosition = Random.Range(0, 2) == 1 ? _leftPos : _rightPost;
        _currentScore = 0;
        UpdateScoreToUI();
    }

    public void StartGame()
    {
        _isPlaying = true;
    }

    public void EndGame()
    {
        _isPlaying = false;
    }

    // Update is called once per frame
    void Update(){
        if (_isPlaying)
        {
            HandleInput();
            HandlePlayerPos ();
        }        
    }

    private void HandleInput (){

        if(Input.GetKeyDown(KeyCode.Space)){
            _isLeftSide = !_isLeftSide;
            _targetPosition = _isLeftSide? _leftPos: _rightPost;
        }
    }
    private void HandlePlayerPos () {
        float distance = Vector3.Distance(transform.position, _targetPosition);

        if (distance > 0.01f)
        {
            // Smooth movement
            transform.position = Vector3.Lerp(transform.position, _targetPosition, _speed * Time.deltaTime);

            // Add rotation effect while moving
            float rotateAmount = 20f * Mathf.Sin(Time.time * 10f); // wobble effect or dynamic turn
            transform.rotation = Quaternion.Euler(0f, 0f, _isLeftSide ? rotateAmount : -rotateAmount);
        }
        else
        {
            // Snap to final position and reset rotation
            transform.position = _targetPosition;
            transform.rotation = Quaternion.identity;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!_isPlaying) return;
        
        if (other.TryGetComponent(out Item item))
        {
            var isWin = item.GetItemType() == Item.ItemType.Collectable;
            if (isWin)
            {
                UpdateScore();
                _gameplayController.OnCollectedItem(transform);
               
            }
            else
            {
                _gameplayController.OnCollectedObstacle(transform);
            }
            var effect = Instantiate(isWin? _winEffect : _loseEffect);
            effect.transform.position = this.transform.position;
            Destroy(other.gameObject); 
        }
    }

    private void UpdateScore()
    {
        _currentScore++;
        UpdateScoreToUI();
    }

    private void UpdateScoreToUI()
    {
        _score.text = _currentScore.ToString();
    }
}
