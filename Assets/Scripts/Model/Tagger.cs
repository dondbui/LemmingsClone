﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    public enum TagTypes
    {
        Attraction,
        Character,
        Obstacle,
        Other,
        None
    }

    [Serializable]
    public class Tagger
    {
        public List<TagTypes> tags = new List<TagTypes>();
    }

}