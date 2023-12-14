using System;
using UnityEngine;

namespace BraveBloodMonsterHunt
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class MaxAttribute : PropertyAttribute
    {
        private string m_ValueName;
        
        public string ValueName
        {
            get => m_ValueName;
            set => m_ValueName = value;
        }

        public MaxAttribute(string valueName)
        {
            ValueName = valueName;
        }
    }
}