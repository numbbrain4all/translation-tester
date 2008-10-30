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
  using System.Text;

  /// <summary>
  /// A class used to specify the translation between two types and then
  /// test that a translator meets the specification.
  /// </summary>
  /// <typeparam name="TFrom">The type being translated from.</typeparam>
  /// <typeparam name="TTo">The type being translated to.</typeparam>
  public class TypeTranslationTester<TFrom, TTo>
  {
    private Type fromType;
    private Type toType;
    private List<string> unmappedProperties;
    private List<string> mappedProperties;
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
      this.mappedProperties = new List<string>(this.allFromProperties.Count);
    }
    
    /// <summary>
    /// Gets or sets the method that should be called to perform the actual translation.
    /// </summary>
    /// <value>The method to be called to perform the translation.</value>
    public Func<TFrom, TTo> TranslationMethod
    {
      get;
      set;
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
    /// <returns>The mapping that was added.</returns>
    public AbstractMapping AddMapping(string fromProperty, string toProperty)
    {
      var mapping = new SimpleMapping(this.fromType.Name, fromProperty, this.toType.Name, toProperty);
      
      if (false == this.allFromProperties.Contains(fromProperty))
      {
        throw new PropertyNotFoundException(
          string.Format(
            CultureInfo.CurrentCulture,
            Properties.Resources.ErrorSimpleMappingPropertyNotFound,
            mapping,
            this.fromType.Name,
            fromProperty));
      }
      
      if (false == this.allToProperties.Contains(toProperty))
      {
        throw new PropertyNotFoundException(
          string.Format(
            CultureInfo.CurrentCulture,
            Properties.Resources.ErrorSimpleMappingPropertyNotFound,
            mapping,
            this.toType.Name,
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
      if (false == this.mappedProperties.Contains(fromProperty))
      {
        this.mappedProperties.Add(fromProperty);
      }
      
      return mapping;
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
            this.fromType.Name,
            fromProperty));
      }
      
      if (false == this.unmappedProperties.Contains(fromProperty))
      {
        throw new PropertyAlreadyMappedException(
          string.Format(
            CultureInfo.CurrentCulture,
            Properties.Resources.ErrorExclusionPropertyAlreadyMapped,
            this.fromType.Name,
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
    
    /// <summary>
    /// Verifies that all mapings that have been added are fulfilled.
    /// </summary>
    /// <remarks>Use this overload if the translation method has been specified.</remarks>
    /// <param name="from">The instance of the from type to use to exercise the translator.</param>
    public void VerifyAllMappings(TFrom from)
    {
      if (this.TranslationMethod == null)
      {
        throw new InvalidOperationException(Properties.Resources.ErrorTranslationMethodNotSpecified);
      }
      
      TTo to = this.TranslationMethod(from);
      this.VerifyAllMappings(from, to);
    }
    
    /// <summary>
    /// Verifies that all mapings that have been added are fulfilled.
    /// </summary>
    /// <remarks>Use this overload if the translation has been done elsewhere.</remarks>
    /// <param name="from">The instance of the from type to use to exercise the translator.</param>
    /// <param name="to">The instance of the to type that was returned by the translation method.</param>
    public void VerifyAllMappings(TFrom from, TTo to)
    {
      this.VerifyFromInstance(from);
      var failures = new List<AbstractMapping>(this.simpleMappings.Count);
      
      foreach (var mapping in this.simpleMappings)
      {
        var fromProperty = this.fromType.GetProperty(mapping.FromProperty);
        object fromValue = fromProperty.GetValue(from, null);
        object toValue = this.toType.GetProperty(mapping.ToProperty).GetValue(to, null);
        if (false == fromValue.Equals(toValue))
        {
          failures.Add(mapping);
        }
      }
      
      if (failures.Count > 0)
      {
        var failureMessage = new StringBuilder();
        failureMessage.Append(Properties.Resources.ErrorSimpleMappingFailed);
        foreach (SimpleMapping mapping in failures)
        {
          object fromValue = this.fromType.GetProperty(mapping.FromProperty).GetValue(from, null);
          object toValue = this.toType.GetProperty(mapping.ToProperty).GetValue(to, null);
          failureMessage.AppendLine();
          failureMessage.AppendFormat(
            CultureInfo.CurrentCulture,
            Properties.Resources.ErrorSimpleMappingFailedSub,
            mapping,
            fromValue,
            toValue == null ? "null" : toValue);
        }
        
        throw new MappingFailedException(failures, failureMessage.ToString());
      }
    }      
    
    /// <summary>
    /// Verifies an instance of the 'from' type is sufficiently specified
    /// to exercise the translation method and verify all the mappings.
    /// </summary>
    /// <param name="from">The from instance to verify.</param>
    public void VerifyFromInstance(TFrom from)
    {
      var failureMessage = new StringBuilder();
      foreach (var mappedProperty in this.mappedProperties)
      {
        var fromProperty = this.fromType.GetProperty(mappedProperty);
        object fromValue = fromProperty.GetValue(from, null);

        object defaultVal = GetDefaultValueForType(fromProperty.PropertyType);
        if (object.Equals(fromValue, defaultVal))
        {
          failureMessage.AppendLine();
          failureMessage.AppendFormat(
            CultureInfo.CurrentCulture,
            Properties.Resources.ErrorDefaultValueForProperty,
            this.fromType.Name,
            mappedProperty,
            defaultVal == null ? "null" : defaultVal);
        }
      }
      
      if (failureMessage.Length > 0)
      {
        throw new ArgumentException(
          Properties.Resources.ErrorFromPropertyHasDefaultValue + failureMessage.ToString(),
          "from");
      }
    }
    
    private static object GetDefaultValueForType(Type type)
    {
      if (type.IsValueType)
      {
        return Activator.CreateInstance(type);
      }
      else
      {
        return null;
      }
    }
    
    private void InitializeFromProperties()
    {
      this.allFromProperties = new List<string>();
      this.fromType = typeof(TFrom);
      var allFromProps = this.fromType.GetProperties();
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
      this.toType = typeof(TTo);
      var allToProps = this.toType.GetProperties();
      foreach (var prop in allToProps)
      {
        this.allToProperties.Add(prop.Name);
      }
    }
  }
}
