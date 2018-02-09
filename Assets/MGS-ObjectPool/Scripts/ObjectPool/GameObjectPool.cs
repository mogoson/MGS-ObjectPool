/*************************************************************************
 *  Copyright 2018 Mogoson All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  GameObjectPool.cs
 *  Description  :  Define GameObjectPool.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  2/9/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace Developer.ObjectPool
{
    [AddComponentMenu("Developer/ObjectPool/GameObjectPool")]
    public class GameObjectPool : MonoBehaviour
    {
        #region Property and Field
        /// <summary>
        /// Type of pool.
        /// </summary>
        public GameObjectPoolType type;

        /// <summary>
        /// Prefab to create clone.
        /// </summary>
        public GameObject prefab;

        /// <summary>
        /// Max count limit of gameobjects.
        /// </summary>
        [SerializeField]
        protected int maxCount = 100;

        /// <summary>
        /// Max count limit of gameobjects.
        /// </summary>
        public int MaxCount
        {
            set { pool.MaxCount = value; }
            get { return pool.MaxCount; }
        }

        /// <summary>
        /// Current count of gameobjects.
        /// </summary>
        public int CurrentCount { get { return pool.CurrentCount; } }

        /// <summary>
        /// Pool of gameobjects.
        /// </summary>
        protected ObjectPool<GameObject> pool;
        #endregion

        #region Private Method
        protected virtual void Awake()
        {
            pool = new ObjectPool<GameObject>(Create, Reset, Dispose, maxCount);
        }

        /// <summary>
        /// Create new clone gameobject.
        /// </summary>
        /// <returns>Clone gameobject.</returns>
        protected virtual GameObject Create()
        {
            var clone = Instantiate(prefab);
            clone.transform.parent = transform;
            return clone;
        }

        /// <summary>
        /// Reset gameobject to recycle state.
        /// </summary>
        /// <param name="obj">GameObject to reset.</param>
        protected virtual void Reset(GameObject obj)
        {
            obj.tag = prefab.tag;
            obj.layer = prefab.layer;

            obj.transform.position = prefab.transform.position;
            obj.transform.rotation = prefab.transform.rotation;

            obj.transform.parent = null;
            obj.transform.localScale = prefab.transform.localScale;
            obj.transform.parent = transform;
        }

        /// <summary>
        /// Destroy gameobject.
        /// </summary>
        /// <param name="obj">Gameobject to destroy.</param>
        protected virtual void Dispose(GameObject obj)
        {
            Destroy(obj);
        }
        #endregion

        #region Public Method
        /// <summary>
        /// Take a new gameobject from pool.
        /// </summary>
        /// <returns>New gameobject.</returns>
        public virtual GameObject TakeNew()
        {
            var obj = pool.TakeNew();
            obj.SetActive(true);
            return obj;
        }

        /// <summary>
        /// Take a new gameobject from pool.
        /// </summary>
        /// <param name="position">Position of new gameobject.</param>
        /// <param name="rotation">Rotation of new gameobject.</param>
        /// <returns>New gameobject.</returns>
        public virtual GameObject TakeNew(Vector3 position, Quaternion rotation)
        {
            var obj = pool.TakeNew();
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            obj.SetActive(true);
            return obj;
        }

        /// <summary>
        /// Take a new gameobject from pool.
        /// </summary>
        /// <param name="parent">Parent of new gameobject.</param>
        /// <param name="localPosition">Local position of new gameobject.</param>
        /// <param name="localRotation">Local rotation of new gameobject.</param>
        /// <returns>New gameobject.</returns>
        public virtual GameObject TakeNew(Transform parent, Vector3 localPosition, Quaternion localRotation)
        {
            var obj = pool.TakeNew();
            obj.transform.parent = parent;
            obj.transform.localPosition = localPosition;
            obj.transform.localRotation = localRotation;
            obj.SetActive(true);
            return obj;
        }

        /// <summary>
        /// Recycle gameobject to pool.
        /// </summary>
        /// <param name="obj">GameObject to recycle.</param>
        public virtual void Recycle(GameObject obj)
        {
            obj.SetActive(false);
            pool.Recycle(obj);
        }

        /// <summary>
        /// Clear the gameobjects.
        /// </summary>
        public virtual void Clear()
        {
            pool.Clear();
        }
        #endregion
    }
}