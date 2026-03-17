using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootOutPoint : MonoBehaviour
{
    public bool AreaCleared {  get; private set; }

    private bool _activePoint;
    private PlayerMove _playerMove;

    public void Initialize(PlayerMove playerMove)
    {
        _playerMove = playerMove;
    }
  
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _playerMove.SetPlayerMovement(false);
        }

        if (Input.GetKeyDown(KeyCode.Return) && _activePoint)
        {
            _playerMove.SetPlayerMovement(true);
            AreaCleared = true;
            _activePoint = false;
        }
    }

    public void StartShootOut()
    {
        _activePoint = true;
        _playerMove.SetPlayerMovement(false);
    }
}
