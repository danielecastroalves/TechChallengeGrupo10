﻿using System.Diagnostics.CodeAnalysis;

namespace FintechGrupo10.Infrastructure.Mongo.Utils
{
    [ExcludeFromCodeCoverage]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class CollectionNameAttribute : Attribute
    {
        public string Name { get; private set; }

        public CollectionNameAttribute(string name)
        {
            Name = name;
        }
    }
}
