using Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WorldObjects
{
    public abstract class BaseWorldObject : MonoBehaviour
    {
        public Tagger tagger;

        // Use this for initialization
        public virtual void Start()
        {
            Init();
        }

        public virtual void Init()
        {
            tagger = new Tagger();
        }

        // Update is called once per frame
        public void Update()
        {

        }
    }
}