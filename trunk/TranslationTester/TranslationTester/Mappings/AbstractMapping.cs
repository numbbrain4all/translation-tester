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
  /// Abstract base class for all mappings.
  /// </summary>
  public abstract class AbstractMapping
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="AbstractMapping" /> class.
    /// </summary>
    /// <param name="fromType">The <see cref="Type" /> of the 'From' type.</param>
    /// <param name="fromProperty">The property on the 'From' type.</param>
    protected AbstractMapping(
      Type fromType,
      string fromProperty)
    {
      FromType = fromType;
      FromProperty = fromType.GetProperty(fromProperty);
      if (FromProperty == null)
      {
        throw new PropertyNotFoundException(
          string.Format(
            CultureInfo.CurrentCulture,
            Properties.Resources.ErrorSimpleMappingPropertyNotFound,
            FromName,
            fromProperty));
      }
    }
    
    /// <summary>
    /// Gets the <see cref="Type" /> of the 'From' type.
    /// </summary>
    /// <value>The <see cref="Type" /> of the 'From' type.</value>
    public Type FromType { get; private set; }
    
    /// <summary>
    /// Gets the <see cref="PropertyInfo"/> of the 'From' property.
    /// </summary>
    /// <value>The <see cref="PropertyInfo"/> of the 'From' property.</value>
    public PropertyInfo FromProperty { get; private set; }
    
    /// <summary>
    /// Gets the <see cref="Type"/> of the 'From' property.
    /// </summary>
    /// <value>Gets the <see cref="Type"/> of the 'From' property.</value>
    public Type FromPropertyType 
    {
      get
      {
        return FromProperty.PropertyType;
      }
    }
    
    /// <summary>
    /// Gets the name of the 'From' type.
    /// </summary>
    /// <value>The name of the 'From' type.</value>
    public string FromName 
    {
      get
      {
        return FromType.Name;
      }
    }
    
    /// <summary>
    /// Gets the name of the property on the 'From' type.
    /// </summary>
    /// <value>The property on the 'From' type.</value>
    public string FromPropertyName 
    {
      get
      {
        return FromProperty.Name;
      }
    }
  }
}
