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
  using System.Collections.ObjectModel;
  using System.ComponentModel;
  using System.Globalization;
  using System.Linq;
  using System.Reflection;
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
      simpleMappings = new List<SimpleMapping>();
      InitializeFromProperties();
      InitializeToProperties();
      InitializeUnmappedProperties();
      mappedProperties = new List<string>(allFromProperties.Count);
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
        return unmappedProperties.Count == 0;
      }
    }
    
    /// <summary>
    /// Attempts to automatically map properties on the from class.
    /// </summary>
    /// <remarks>Any property that has an idetically named property on the to class will be mapped.</remarks>
    /// <returns>The mappings that were added.</returns>
    public Collection<SimpleMapping> AutomaticallyMapProperties()
    {
      var identicalProperties = new List<string>(unmappedProperties.Intersect(allToProperties));
      var addedMappings = new Collection<SimpleMapping>();
      foreach (var identical in identicalProperties)
      {
        try 
        {
          addedMappings.Add(AddMapping(identical, identical));
        } 
        catch (PropertyAlreadyMappedException) 
        { 
        }
        catch (ArgumentException) 
        { 
        }
      }
      
      return addedMappings;
    }
    
    /// <summary>
    /// Adds a simple one-to-one mapping to the translation specification.
    /// </summary>
    /// <param name="fromProperty">The name of the property on the 'From' type.</param>
    /// <param name="toProperty">The name of the property on the 'To' type.</param>
    /// <returns>The mapping that was added.</returns>
    public SimpleMapping AddMapping(string fromProperty, string toProperty)
    {
      var mapping = new SimpleMapping(fromType.Name, fromProperty, toType.Name, toProperty);
      
      if (false == allFromProperties.Contains(fromProperty))
      {
        throw new PropertyNotFoundException(
          string.Format(
            CultureInfo.CurrentCulture,
            Properties.Resources.ErrorSimpleMappingPropertyNotFound,
            mapping,
            fromType.Name,
            fromProperty));
      }
      
      if (false == allToProperties.Contains(toProperty))
      {
        throw new PropertyNotFoundException(
          string.Format(
            CultureInfo.CurrentCulture,
            Properties.Resources.ErrorSimpleMappingPropertyNotFound,
            mapping,
            toType.Name,
            toProperty));
      }
      
      if (simpleMappings.Contains(mapping))
      {
        throw new PropertyAlreadyMappedException(
          string.Format(
            CultureInfo.CurrentCulture,
            Properties.Resources.ErrorSimpleMappingMappingAlreadyExists,
            mapping));
      }      
      
      var fromProp = fromType.GetProperty(fromProperty);
      var fromPropType = fromProp.PropertyType;     
      var toProp = toType.GetProperty(toProperty);
      var toPropType = toProp.PropertyType;
      if (fromPropType != toPropType) 
      {
        throw new ArgumentException("Unable to add simple mappings where property types differ");
      }

      unmappedProperties.Remove(fromProperty);
      simpleMappings.Add(mapping);
      if (false == mappedProperties.Contains(fromProperty))
      {
        mappedProperties.Add(fromProperty);
      }
      
      return mapping;
    }
    
    /// <summary>
    /// Excludes a property on the 'From' type from being mapped.
    /// </summary>
    /// <param name="fromProperty">The name of the property on the 'From' type.</param>
    public void ExcludeProperty(string fromProperty)
    {
      if (false == allFromProperties.Contains(fromProperty))
      {
        throw new PropertyNotFoundException(
          string.Format(
            CultureInfo.CurrentCulture,
            Properties.Resources.ErrorExclusionPropertyNotFound,
            fromType.Name,
            fromProperty));
      }
      
      if (false == unmappedProperties.Contains(fromProperty))
      {
        throw new PropertyAlreadyMappedException(
          string.Format(
            CultureInfo.CurrentCulture,
            Properties.Resources.ErrorExclusionPropertyAlreadyMapped,
            fromType.Name,
            fromProperty));
      }
      
      unmappedProperties.Remove(fromProperty);
    }
    
    /// <summary>
    /// Verifies that all the properties on the 'From' type were mapped or excluded.
    /// </summary>
    public void VerifyAllPropertiesMapped()
    {
      if (unmappedProperties.Count > 0)
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
      if (TranslationMethod == null)
      {
        throw new InvalidOperationException(Properties.Resources.ErrorTranslationMethodNotSpecified);
      }
      
      TTo to = TranslationMethod(from);
      VerifyAllMappings(from, to);
    }
    
    /// <summary>
    /// Verifies that all mapings that have been added are fulfilled.
    /// </summary>
    /// <remarks>Use this overload if the translation has been done elsewhere.</remarks>
    /// <param name="from">The instance of the from type to use to exercise the translator.</param>
    /// <param name="to">The instance of the to type that was returned by the translation method.</param>
    public void VerifyAllMappings(TFrom from, TTo to)
    {
      VerifyFromInstance(from);
      var failures = new List<AbstractMapping>(simpleMappings.Count);
      
      foreach (var mapping in simpleMappings)
      {
        var fromProperty = fromType.GetProperty(mapping.FromProperty);
        var toProperty = toType.GetProperty(mapping.ToProperty);
        var fromValue = fromProperty.GetValue(from, null);
        var toValue = toProperty.GetValue(to, null);
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
          object fromValue = fromType.GetProperty(mapping.FromProperty).GetValue(from, null);
          object toValue = toType.GetProperty(mapping.ToProperty).GetValue(to, null);
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
      foreach (var mappedProperty in mappedProperties)
      {
        var fromProperty = fromType.GetProperty(mappedProperty);
        object fromValue = fromProperty.GetValue(from, null);

        object defaultVal = GetDefaultValueForType(fromProperty.PropertyType);
        if (object.Equals(fromValue, defaultVal))
        {
          failureMessage.AppendLine();
          failureMessage.AppendFormat(
            CultureInfo.CurrentCulture,
            Properties.Resources.ErrorDefaultValueForProperty,
            fromType.Name,
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
      allFromProperties = new List<string>();
      fromType = typeof(TFrom);
      var allFromProps = fromType.GetProperties();
      foreach (var prop in allFromProps)
      {
        allFromProperties.Add(prop.Name);
      }
    }
    
    private void InitializeUnmappedProperties()
    {
      unmappedProperties = new List<string>(allFromProperties);
    }
    
    private void InitializeToProperties()
    {
      allToProperties = new List<string>();
      toType = typeof(TTo);
      var allToProps = toType.GetProperties();
      foreach (var prop in allToProps)
      {
        allToProperties.Add(prop.Name);
      }
    }
  }
}
