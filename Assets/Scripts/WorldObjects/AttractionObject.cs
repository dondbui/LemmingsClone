using Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WorldObjects
{
    public class AttractionObject : Obstacle
    {
        public override void Start()
        {
            base.Start();

            // Tagged as an attraction to attract walkers
            tagger.tags.Add(TagTypes.Attraction);
        }
    }
}