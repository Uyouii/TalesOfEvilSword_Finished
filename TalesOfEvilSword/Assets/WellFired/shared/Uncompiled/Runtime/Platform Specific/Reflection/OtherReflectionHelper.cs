using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

#if !NETFX_CORE
namespace WellFired.Shared
{
	public class ReflectionHelper : IReflectionHelper
	{
		public bool IsAssignableFrom(Type first, Type second)
		{
			return first.IsAssignableFrom(second);
		}
		
		public bool IsEnum(Type type)
		{
			return type.IsEnum;
		}
		
		private IEnumerable GetBaseTypes(Type type)
		{
			yield return type;
			Type baseType;

			baseType = type.BaseType;
			
			if (baseType != null)
			{
				foreach (var t in GetBaseTypes(baseType))
				{
					yield return t;
				}
			}
		}
		
		public PropertyInfo GetProperty(Type type, string name)
		{
			return type.GetProperty(name);
		}
		
		public MethodInfo GetMethod(Type type, string name)
		{
			return type.GetMethod(name);
		}
		
		public FieldInfo GetField(Type type, string name)
		{
			return type.GetField(name);
		}
		
		public bool IsValueType(Type type)
		{
			return type.IsValueType;
		}

		public MethodInfo GetNonPublicStaticMethod(Type type, string name)
		{
			return type.GetMethod(name, BindingFlags.NonPublic | BindingFlags.Static);
		}

		public MethodInfo GetNonPublicMethod(Type type, string name)
		{
			return type.GetMethod(name, BindingFlags.NonPublic | BindingFlags.Instance);
		}

		public PropertyInfo GetNonPublicInstanceProperty(Type type, string name)
		{
			return type.GetProperty(name, BindingFlags.NonPublic | BindingFlags.Instance);
		}

		public FieldInfo GetNonPublicInstanceField(Type type, string name)
		{
			return type.GetField(name, BindingFlags.NonPublic | BindingFlags.Instance);
		}
	}
}
#endif