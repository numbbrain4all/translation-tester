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
  using System.Collections.Generic;
  using System.Globalization;

  /// <summary>
  /// A class used to specify the translation between two types and then
  /// test that a translator meets the specification.
  /// </summary>
  /// <typeparam name="TFrom">The type being translated from.</typeparam>
  /// <typeparam name="TTo">The type being translated to.</typeparam>
  public class TypeTranslationTester<TFrom, TTo>
  {
    private string fromName;
    private string toName;
    private List<string> unmappedProperties;
    private List<string> allFromProperties;
    private List<string> allToProperties;
    private List<SimpleMapping> simpleMappings;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="TypeTranslationTester{TFrom, TTo}"/> class.
    /// </summary>
    public TypeTranslationTester()
    {
      this.simpleMappings = new List<SimpleMapping>();
      this.InitializeFromProperties();
      this.InitializeToProperties();
      this.InitializeUnmappedProperties();
    }
    
    /// <summary>
    /// Gets a value indicating whether all the properties on the 'From'
    /// type have been mapped or excluded.
    /// </summary>
    /// <value><c>True</c> if all the properties have been mapped or excluded,
    /// <c>falase</c> otherwise.</value>
    public bool AllPropertiesMapped
    {
      get
      {
        return this.unmappedProperties.Count == 0;
      }
    }
    
    /// <summary>
    /// Adds a simple one-to-one mapping to the translation specification.
    /// </summary>
    /// <param name="fromProperty">The name of the property on the 'From' type.</param>
    /// <param name="toProperty">The name of the property on the 'To' type.</param>
    public void AddMapping(string fromProperty, string toProperty)
    {
      var mapping = new SimpleMapping(this.fromName, fromProperty, this.toName, toProperty);
      
      if (false == this.allFromProperties.Contains(fromProperty))
      {
        throw new PropertyNotFoundException(
          string.Format(
            CultureInfo.CurrentCulture,
            Properties.Resources.ErrorSimpleMappingPropertyNotFound,
            mapping,
            this.fromName,
            fromProperty));
      }
      
      if (false == this.allToProperties.Contains(toProperty))
      {
        throw new PropertyNotFoundException(
          string.Format(
            CultureInfo.CurrentCulture,
            Properties.Resources.ErrorSimpleMappingPropertyNotFound,
            mapping,
            this.toName,
            toProperty));
      }
      
      if (this.simpleMappings.Contains(mapping))
      {
        throw new PropertyAlreadyMappedException(
          string.Format(
            CultureInfo.CurrentCulture,
            Properties.Resources.ErrorSimpleMappingMappingAlreadyExists,
            mapping));
      }
      
      this.unmappedProperties.Remove(fromProperty);
      this.simpleMappings.Add(mapping);
    }
    
    /// <summary>
    /// Excludes a property on the 'From' type from being mapped.
    /// </summary>
    /// <param name="fromProperty">The name of the property on the 'From' type.</param>
    public void ExcludeProperty(string fromProperty)
    {
      if (false == this.allFromProperties.Contains(fromProperty))
      {
        throw new PropertyNotFoundException(
          string.Format(
            CultureInfo.CurrentCulture,
            Properties.Resources.ErrorExclusionPropertyNotFound,
            this.fromName,
            fromProperty));
      }
      
      if (false == this.unmappedProperties.Contains(fromProperty))
      {
        throw new PropertyAlreadyMappedException(
          string.Format(
            CultureInfo.CurrentCulture,
            Properties.Resources.ErrorExclusionPropertyAlreadyMapped,
            this.fromName,
            fromProperty));
      }
      
      this.unmappedProperties.Remove(fromProperty);
    }
    
    /// <summary>
    /// Verifies that all the properties on the 'From' type were mapped or excluded.
    /// </summary>
    public void VerifyAllPropertiesMapped()
    {
      if (this.unmappedProperties.Count > 0)
      {
        throw new UnmappedPropertyException();
      }
    }
    
    private void InitializeFromProperties()
    {
      this.allFromProperties = new List<string>();
      var fromType = typeof(TFrom);
      this.fromName = fromType.Name;
      var allFromProps = fromType.GetProperties();
      foreach (var prop in allFromProps)
      {
        this.allFromProperties.Add(prop.Name);
      }
    }
    
    private void InitializeUnmappedProperties()
    {
      this.unmappedProperties = new List<string>(this.allFromProperties);
    }
    
    private void InitializeToProperties()
    {
      this.allToProperties = new List<string>();
      var toType = typeof(TTo);
      this.toName = toType.Name;
      var allToProps = toType.GetProperties();
      foreach (var prop in allToProps)
      {
        this.allToProperties.Add(prop.Name);
      }
    }
  }
}