using Model;
using System;
using UnityEngine;

namespace WorldObjects
{
    public class Obstacle : BaseWorldObject
    {
        public override void Start()
        {
            base.Start();

            // All obstacles need to have their objects tagged 
            tagger.tags.Add(TagTypes.Obstacle);
        }

    }
}