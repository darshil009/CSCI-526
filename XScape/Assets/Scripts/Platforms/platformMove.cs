using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class platformMove : MonoBehaviour
{
    [Header(
        "Control which direction the platform moves: \n-1 to move on negative Axis\n 1 on positive axis\n 0 to not move on that axis")]
    [SerializeField, Range(-1, 1)] private int XAxis;
    [SerializeField, Range(-1, 1)] private int YAxis;
    [SerializeField, Range(-1,1)] private int ZAxis;

    
    [Tooltip("How far the platform is going to move in each step")]
    [SerializeField] private float stepDistance = 8f;
    
    [Tooltip("No of steps between [first position, last position]")]
    [SerializeField, Range(2,10)] private int steps = 2;
    
    [Tooltip("Speed of the moving platform")]
    [SerializeField, Range(2,10)] private int speed = 5;


    private Vector3[] _posArr;
    private int _currentDirection = 1;
    private int _currentIndex = 0;
    private int _targetIndex = 0;
    private float _timeElapsed, _time;
    
    // Start is called before the first frame update
    void Start()
    {
        _posArr = new Vector3[steps];
        GeneratePositions();
    }

    private void GeneratePositions()
    {
        _posArr[0] = transform.position;
        for (int i = 1; i < steps; i++)
        {
            Vector3 curPos = new Vector3();
            curPos.x = _posArr[i - 1].x + XAxis * stepDistance;
            curPos.y = _posArr[i - 1].y + YAxis * stepDistance;
            curPos.z = _posArr[i - 1].z + ZAxis * stepDistance;
            _posArr[i] = curPos;
        }
    }

    // Update is called once per frame

    private void Update()
    {
        Move();
        
    }

    private void Move()
    {
        _timeElapsed += Time.deltaTime;
        float pct = _timeElapsed / _time;
        pct = Mathf.SmoothStep(0, 1, pct);
        if (pct >= 1)
        {
            transform.DetachChildren();
            return;
        }
        transform.position = Vector3.Lerp(_posArr[_currentIndex], _posArr[_targetIndex], pct);
    }
    
    private void OnMouseDown()
    {
        float pct = _timeElapsed / _time;
        pct = Mathf.SmoothStep(0, 1, pct);
        if (pct < 1)
        {
            return;
        }
        _timeElapsed = 0;
        _time = stepDistance / speed;
        GetNextIndex();
    }

    private void GetNextIndex()
    {
        var temp = _targetIndex;
        _targetIndex += _currentDirection;
        if (_targetIndex < 0)
        {
            _currentDirection *= -1;
            _targetIndex = 1;
        }
        else if (_targetIndex == _posArr.Length)
        {
            _currentDirection *= -1;
            _targetIndex = _posArr.Length - 2;
        }
        _currentIndex = temp;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MagnetBlock") || other.CompareTag("MagnetBlock1") || other.CompareTag("Player"))
            other.transform.SetParent(transform);
    }

    private void OnTriggerExit(Collider other)
    {
        other.transform.parent = null;
    }
}



   



