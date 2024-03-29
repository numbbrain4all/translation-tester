﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3053
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TranslationTester.Properties
{
  using System;
  
  
  /// <summary>
  ///   A strongly-typed resource class, for looking up localized strings, etc.
  /// </summary>
  // This class was auto-generated by the StronglyTypedResourceBuilder
  // class via a tool like ResGen or Visual Studio.
  // To add or remove a member, edit your .ResX file then rerun ResGen
  // with the /str option, or rebuild your VS project.
  [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0")]
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
  [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
  internal class Resources
  {
    
    private static global::System.Resources.ResourceManager resourceMan;
    
    private static global::System.Globalization.CultureInfo resourceCulture;
    
    [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
    internal Resources()
    {
    }
    
    /// <summary>
    ///   Returns the cached ResourceManager instance used by this class.
    /// </summary>
    [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
    internal static global::System.Resources.ResourceManager ResourceManager
    {
      get
      {
        if (object.ReferenceEquals(resourceMan, null))
        {
          global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("TranslationTester.Properties.Resources", typeof(Resources).Assembly);
          resourceMan = temp;
        }
        return resourceMan;
      }
    }
    
    /// <summary>
    ///   Overrides the current thread's CurrentUICulture property for all
    ///   resource lookups using this strongly typed resource class.
    /// </summary>
    [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
    internal static global::System.Globalization.CultureInfo Culture
    {
      get
      {
        return resourceCulture;
      }
      set
      {
        resourceCulture = value;
      }
    }
    
    /// <summary>
    ///   Looks up a localized string similar to mapping from {0}.{1}.
    /// </summary>
    internal static string AbstractMapping
    {
      get
      {
        return ResourceManager.GetString("AbstractMapping", resourceCulture);
      }
    }
    
    /// <summary>
    ///   Looks up a localized string similar to &apos;{0}.{1}&apos; had the default value of &apos;{2}&apos;.
    /// </summary>
    internal static string ErrorDefaultValueForProperty
    {
      get
      {
        return ResourceManager.GetString("ErrorDefaultValueForProperty", resourceCulture);
      }
    }
    
    /// <summary>
    ///   Looks up a localized string similar to Unable to exclude property &apos;{0}.{1}&apos; as it is already mapped or excluded.
    /// </summary>
    internal static string ErrorExclusionPropertyAlreadyMapped
    {
      get
      {
        return ResourceManager.GetString("ErrorExclusionPropertyAlreadyMapped", resourceCulture);
      }
    }
    
    /// <summary>
    ///   Looks up a localized string similar to Unable to exclude property &apos;{0}.{1}&apos; as it was not found on the class.
    /// </summary>
    internal static string ErrorExclusionPropertyNotFound
    {
      get
      {
        return ResourceManager.GetString("ErrorExclusionPropertyNotFound", resourceCulture);
      }
    }
    
    /// <summary>
    ///   Looks up a localized string similar to Unsupported Expression {0}.
    /// </summary>
    internal static string ErrorExpressionNotSupported
    {
      get
      {
        return ResourceManager.GetString("ErrorExpressionNotSupported", resourceCulture);
      }
    }
    
    /// <summary>
    ///   Looks up a localized string similar to Unable to verify mappings as the following mapped properties had default values:.
    /// </summary>
    internal static string ErrorFromPropertyHasDefaultValue
    {
      get
      {
        return ResourceManager.GetString("ErrorFromPropertyHasDefaultValue", resourceCulture);
      }
    }
    
    /// <summary>
    ///   Looks up a localized string similar to Expression {0} is not a property expression.
    /// </summary>
    internal static string ErrorNotPropertyExpression
    {
      get
      {
        return ResourceManager.GetString("ErrorNotPropertyExpression", resourceCulture);
      }
    }
    
    /// <summary>
    ///   Looks up a localized string similar to The following mappings were unfulfilled by the translation method:.
    /// </summary>
    internal static string ErrorSimpleMappingFailed
    {
      get
      {
        return ResourceManager.GetString("ErrorSimpleMappingFailed", resourceCulture);
      }
    }
    
    /// <summary>
    ///   Looks up a localized string similar to {0}: fromValue={1}, toValue={2}.
    /// </summary>
    internal static string ErrorSimpleMappingFailedSub
    {
      get
      {
        return ResourceManager.GetString("ErrorSimpleMappingFailedSub", resourceCulture);
      }
    }
    
    /// <summary>
    ///   Looks up a localized string similar to Unable to add mapping &apos;{0}&apos;, an identical mapping already exists.
    /// </summary>
    internal static string ErrorSimpleMappingMappingAlreadyExists
    {
      get
      {
        return ResourceManager.GetString("ErrorSimpleMappingMappingAlreadyExists", resourceCulture);
      }
    }
    
    /// <summary>
    ///   Looks up a localized string similar to Unable to add mapping as property &apos;{0}.{1}&apos; was not found .
    /// </summary>
    internal static string ErrorSimpleMappingPropertyNotFound
    {
      get
      {
        return ResourceManager.GetString("ErrorSimpleMappingPropertyNotFound", resourceCulture);
      }
    }
    
    /// <summary>
    ///   Looks up a localized string similar to No translation method specified, either specify the translationMethod to be called or do the translation elsewhere and use the overload of VerifyAllMappings accepting an instance of the to type..
    /// </summary>
    internal static string ErrorTranslationMethodNotSpecified
    {
      get
      {
        return ResourceManager.GetString("ErrorTranslationMethodNotSpecified", resourceCulture);
      }
    }
    
    /// <summary>
    ///   Looks up a localized string similar to {0}.{1} to {2}.{3}.
    /// </summary>
    internal static string SimpleMapping
    {
      get
      {
        return ResourceManager.GetString("SimpleMapping", resourceCulture);
      }
    }
    
    /// <summary>
    ///   Looks up a localized string similar to {0}.{1} to {2}.{3}: fromValue={4}, toValue={5}.
    /// </summary>
    internal static string SimpleMappingWithValues
    {
      get
      {
        return ResourceManager.GetString("SimpleMappingWithValues", resourceCulture);
      }
    }
  }
}
