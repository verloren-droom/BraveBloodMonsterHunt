using System;
using UnityEngine;

namespace BraveBloodMonsterHunt
{
    /// <summary>
    /// add new file button
    /// support : ScriptableObject 
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class AddButtonAttribute : PropertyAttribute { }
}