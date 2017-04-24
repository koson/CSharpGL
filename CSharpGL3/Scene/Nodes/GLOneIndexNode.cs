﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharpGL
{
    public sealed class GLOneIndexNode : GLIndexNode
    {
        private static readonly Type type = typeof(GLOneIndexNode);
        internal override Type ThisTypeCache
        {
            get { return type; }
        }
    }
}