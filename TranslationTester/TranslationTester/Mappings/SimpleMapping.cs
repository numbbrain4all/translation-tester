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

  /// <summary>
  /// Represents a one-to-one mapping between properties on two types.
  /// </summary>
  public class SimpleMapping : AbstractMapping
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="SimpleMapping" /> class.
    /// </summary>
    /// <param name="fromName">The name of the 'From' type.</param>
    /// <param name="fromProperty">The property on the 'From' type.</param>
    /// <param name="toName">The name of the 'To' type.</param>
    /// <param name="toProperty">The property on the 'To' type.</param>
    public SimpleMapping(
      string fromName,
      string fromProperty,
      string toName,
      string toProperty)
      : base(fromName, fromProperty)
    {
      this.ToName = toName;
      this.ToProperty = toProperty;
    }
    
    /// <summary>
    /// Gets the name of the 'To' type.
    /// </summary>
    /// <value>The name of the 'To' type.</value>
    public string ToName { get; private set; }
    
    /// <summary>
    /// Gets the name of the property on the 'To' class.
    /// </summary>
    /// <value>The property on the 'To' class.</value>
    public string ToProperty { get; private set; }
    
    /// <summary>
    /// Returns a string representation of the Simple Mapping.
    /// </summary>
    /// <returns>A string representation of the Simple Mapping.</returns>
    public override string ToString()
    {
      return string.Format(
        CultureInfo.CurrentCulture,
        Properties.Resources.SimpleMapping,
        this.FromName,
        this.FromProperty,
        this.ToName,
        this.ToProperty);
    }
    
    /// <summary>
    /// Gets a hash code for this simple mapping.
    /// </summary>
    /// <returns>An int to use as a hash code for this mapping.</returns>
    public override int GetHashCode()
    {
      return this.FromName.GetHashCode()
        ^ this.FromProperty.GetHashCode()
        ^ this.ToName.GetHashCode()
        ^ this.ToProperty.GetHashCode();
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
      
      return this.Equals(otherMapping);
    }
    
    /// <summary>
    /// Determines if this mapping is equal to another mapping.
    /// </summary>
    /// <param name="other">The mapping to compare to.</param>
    /// <returns>True if all the properties match, false otherwise.</returns>
    public bool Equals(SimpleMapping other)
    {
      return this.FromName == other.FromName
        && this.ToName == other.ToName
        && this.FromProperty == other.FromProperty
        && this.ToProperty == other.ToProperty;
    }
  }
}
