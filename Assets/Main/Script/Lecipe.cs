using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class Lecipe
{
    public List<Material> Materials;

    public List<Resualt> Resualts;

    [Serializable]
    public class Material
    {
        public m_eItemName Name;
        public int Count;
    }

    [Serializable]
    public class Resualt
    {
        public m_eItemName Name;
        public int Count;
    }

}


