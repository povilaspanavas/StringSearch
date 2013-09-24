﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace PUnit.Framework
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TestFixtureAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class TestAttribute : Attribute
    {
    }

    public class AttributeParser
    {
        public static T GetAttribute<T>(Type type) where T : class
        {
            object[] attributes = type.GetCustomAttributes(typeof(T), true);
            if (attributes.Length == 0)
                return null;
            return attributes[0] as T;
        }

        public static T GetAttribute<T>(PropertyInfo propertyInfo) where T : class
        {
            object[] attributes = propertyInfo.GetCustomAttributes(typeof(T), true);
            if (attributes.Length == 0)
                return null;
            return attributes[0] as T;
        }

        public static object GetAttribute(MethodInfo mi, Type typeofAttribute)
        {
            object[] attributes = mi.GetCustomAttributes(typeofAttribute, true);
            if (attributes.Length == 0)
                return null;
            return attributes[0];
        }

        public static T GetAttribute<T>(MethodBase mi) where T : class
        {
            object[] attributes = mi.GetCustomAttributes(typeof(T), true);
            if (attributes.Length == 0)
                return null;
            return attributes[0] as T;
        }
    }
}