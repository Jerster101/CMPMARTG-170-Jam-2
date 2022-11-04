using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerNetwork : NetworkBehaviour
{
    private readonly NetworkVariable<PlayerNetworkData> _netState = new NetworkVariable<PlayerNetworkData>(writePerm: NetworkVariableWritePermission.Owner);
    private Vector3 _vel;
    private float _rotVel;
    //private bool _attacking;
    //private static GameObject hitbox;
    [SerializeField] private float _cheapInterpolationTime = 0.1f;

    //private void Start()
    //{
        //hitbox = gameObject.transform.GetChild(1).gameObject;
    //}

    void Update()
    {
        if (IsOwner)
        {
            _netState.Value = new PlayerNetworkData()
            {
                Position = transform.position,
                Rotation = transform.rotation.eulerAngles,
                //Attacking = hitbox.activeInHierarchy
            };
        }
        else
        {
            transform.position = Vector3.SmoothDamp(transform.position, _netState.Value.Position, ref _vel, _cheapInterpolationTime);
            transform.rotation = Quaternion.Euler(
                0,
                Mathf.SmoothDampAngle(transform.rotation.eulerAngles.y, _netState.Value.Rotation.y, ref _rotVel, _cheapInterpolationTime),
                0);
            //hitbox.SetActive(_attacking);
            //Debug.Log(_attacking);
        }
    }

    struct PlayerNetworkData : INetworkSerializable
    {
        private float _x, _z;
        private short _yRot;
        //private bool _attacking;

        internal Vector3 Position
        {
            get => new Vector3(_x, 0, _z);
            set
            {
                _x = value.x;
                _z = value.z;
            }
        }

        internal Vector3 Rotation
        {
            get => new Vector3(0, _yRot, 0);
            set => _yRot = (short)value.y;
        }

        //internal bool Attacking
       // {
            //get => _attacking;
            //set => _attacking = hitbox.activeInHierarchy;
        //}

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref _x);
            serializer.SerializeValue(ref _z);
            serializer.SerializeValue(ref _yRot);
            //serializer.SerializeValue(ref _attacking);
        }
    }

}
