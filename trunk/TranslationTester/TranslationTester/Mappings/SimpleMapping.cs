#region License
// Copyright (c) 2008, Alex McMahon
// All rights reserved.
//
// Redistribution and use in source and binary forms, with or without modification,
// are permitted provided that the following conditions are met:
//
//  * Redistributions of source code must retain the above copyright notice,
// this list of conditions and the following disclaimer.
//  * Redistributions in binary form must reproduce the above copyright notice,
// this list of conditions and the following disclaimer in the documentation
// and/or other materials provided with the distribution.
//  * Neither the name of Alex McMahon nor the names of its
// contributors may be used to endorse or promote products derived from this
// software without specific prior written permission.
//
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
// ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE
// FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
// DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
// SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
// CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
// OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF
// THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
#endregion

namespace TranslationTester
{
using System;
using System.Globalization;
using System.Reflection;

  /// <summary>
  /// Represents a one-to-one mapping between properties on two types.
  /// </summary>
  public class SimpleMapping : AbstractMapping
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="SimpleMapping" /> class.
    /// </summary>
    /// <param name="fromType">The <see cref="Type" /> of the 'From' type.</param>
    /// <param name="fromProperty">The property on the 'From' type.</param>
    /// <param name="toType">The <see cref="Type" /> of the 'To' type.</param>
    /// <param name="toProperty">The property on the 'To' type.</param>
    public SimpleMapping(
      Type fromType,
      string fromProperty,
      Type toType,
      string toProperty)
      : base(fromType, fromProperty)
    {
      ToType = toType;
      ToProperty = toType.GetProperty(toProperty);
      if (ToProperty == null)
      {
        throw new PropertyNotFoundException(
          string.Format(
            CultureInfo.CurrentCulture,
            Properties.Resources.ErrorSimpleMappingPropertyNotFound,
            ToName,
            toProperty));
      }
      
      if (FromPropertyType != ToPropertyType) 
      {
        throw new ArgumentException("Unable to add simple mappings where property types differ");
      }
    }
    
    /// <summary>
    /// Gets the <see cref="Type" /> of the 'To' type.
    /// </summary>
    /// <value>The <see cref="Type" /> of the 'To' type.</value>
    public Type ToType { get; private set; }
    
    /// <summary>
    /// Gets the <see cref="PropertyInfo"/> of the 'To' property.
    /// </summary>
    /// <value>The <see cref="PropertyInfo"/> of the 'To' property.</value>
    public PropertyInfo ToProperty { get; private set; }
    
    /// <summary>
    /// Gets the <see cref="Type"/> of the 'To' property.
    /// </summary>
    /// <value>Gets the <see cref="Type"/> of the 'To' property.</value>
    public Type ToPropertyType
    {
      get
      {
        return ToProperty.PropertyType;
      }
    }
    
    /// <summary>
    /// Gets the name of the 'To' type.
    /// </summary>
    /// <value>The name of the 'To' type.</value>
    public string ToName 
    {
      get
      {
        return ToType.Name;
      }
    }
    
    /// <summary>
    /// Gets the name of the property on the 'To' class.
    /// </summary>
    /// <value>The property on the 'To' class.</value>
    public string ToPropertyName 
    {
      get
      {
        return ToProperty.Name;      
      }    
    }
    
    /// <summary>
    /// Returns a string representation of the Simple Mapping.
    /// </summary>
    /// <returns>A string representation of the Simple Mapping.</returns>
    public override string ToString()
    {
      return string.Format(
        CultureInfo.CurrentCulture,
        Properties.Resources.SimpleMapping,
        FromName,
        FromProperty,
        ToName,
        ToProperty);
    }
    
    /// <summary>
    /// Gets a hash code for this simple mapping.
    /// </summary>
    /// <returns>An int to use as a hash code for this mapping.</returns>
    public override int GetHashCode()
    {
      return FromName.GetHashCode()
        ^ FromProperty.GetHashCode()
        ^ ToName.GetHashCode()
        ^ ToProperty.GetHashCode();
    }
    
    /// <summary>
    /// Determines whether this mapping equals another object.
    /// </summary>
    /// <param name="obj">The object to compare to.</param>
    /// <returns>True if the object is a SimpleMapping and all the properties are the same,
    /// false otherwise.</returns>
    public override bool Equals(object obj)
    {
      SimpleMapping otherMapping = obj as SimpleMapping;
      if (otherMapping == null)
      {
        return false;
      }
      
      return Equals(otherMapping);
    }
    
    /// <summary>
    /// Determines if this mapping is equal to another mapping.
    /// </summary>
    /// <param name="other">The mapping to compare to.</param>
    /// <returns>True if all the properties match, false otherwise.</returns>
    public bool Equals(SimpleMapping other)
    {
      return FromName == other.FromName
        && ToName == other.ToName
        && FromProperty == other.FromProperty
        && ToProperty == other.ToProperty;
    }
  }
}
