using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class CubeComponent : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private new Transform transform;
    [SerializeField] private Animator animator;

    [Header("Commons")]
    [Tooltip("오브젝트에 사용되는 모든 렌더러 컴포넌트")]
    [SerializeField] MeshRenderer[] renderers;
    [Tooltip("모자 오브젝트")]
    [SerializeField] GameObject hat;
    [Tooltip("발사 지점")]
    [SerializeField] Transform firePoint;

    [System.Serializable]
    public struct Stats
    {
        public int health;
        public float healthRegen;

        [Space(10f)]
        public int attackPoint;
        public float attackSpeed;

        [HideInInspector]
        public float attackDelay;

        [Space(10f)]
        public float moveSpeed;
        public float rotSpeed;

        [Space(10f)]
        public float jumpAmount;
        [HideInInspector]
        public bool isJumping;
    }
    
    [Header("Player Stats")]
    public Stats stats;

    [Header("Infomation")]
    public new string name;

    [Multiline(3)]
    public string desc;

    [Header("Resuze Datas")]
    [Range(0.5f, 1.5f)]
    public float resizeAmoint;
    public Vector3 minSize;
    public Vector3 maxSize;
}
