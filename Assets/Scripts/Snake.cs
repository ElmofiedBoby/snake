using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Snake : MonoBehaviour
{
    // Direction of Snake
    private Vector2 _direction = Vector2.right;
    public float speed = 1f;

    private List<Transform> _segments;
    public Transform segmentPrefab;
    public Transform theSnake;

    private void Start() {
        _segments = new List<Transform>();
        _segments.Add(this.transform);
        InvokeRepeating("Move", speed, speed);  
    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
            _direction = Vector2.up;
        }
        else if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
            _direction = Vector2.left;
        }
        else if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
            _direction = Vector2.down;
        }
        else if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
            _direction = Vector2.right;
        }
    }

    private void Move() {
        for(int i = _segments.Count - 1; i > 0; i--) {
            _segments[i].position = _segments[i-1].position;
        }

        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + _direction.x,
            Mathf.Round(this.transform.position.y) + _direction.y,
            0.0f
        );
    }

    private void Grow() {
        Transform segment = Instantiate(this.segmentPrefab);
        if(_segments.Count != 1) {
            segment.position = _segments[_segments.Count-1].position;
        }
        else {
            if(_segments[_segments.Count-1].position.y > theSnake.position.y) {
                segment.position = _segments[_segments.Count-1].position + new Vector3(1, 0, 0);
            }
            else if(_segments[_segments.Count-1].position.y < theSnake.position.y) {
                segment.position = _segments[_segments.Count-1].position + new Vector3(-1, 0, 0);
            }
            else if(_segments[_segments.Count-1].position.x > theSnake.position.x) {
                segment.position = _segments[_segments.Count-1].position + new Vector3(0, 1, 0);
            }
            else if(_segments[_segments.Count-1].position.x < theSnake.position.x) {
                segment.position = _segments[_segments.Count-1].position + new Vector3(0, -1, 0);
            }
            
        }
        

        _segments.Add(segment);
    }

    private void ResetState() {
        for(int i = 1; i < _segments.Count; i++) {
            Destroy(_segments[i].gameObject);
        }

        _segments.Clear();
        _segments.Add(this.transform);

        this.transform.position = Vector3.zero;

        SceneManager.LoadScene("DeathMenu");
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Food") {
            Grow();
        }

        if(other.tag == "Obstacle") {
            ResetState();
        }

        if(other.tag == "Player") {
            ResetState();
        }
    }

}
