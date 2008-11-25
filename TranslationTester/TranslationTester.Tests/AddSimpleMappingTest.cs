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

using System;
using NUnit.Framework;

//  Narrative:
//As a Developer
//I want to be able to specify that a property on the 'from' type is directly
//assigned to a corresponding property on the 'to' type
//So that I can test that these mappings are fulfilled
//
//Acceptance Criteria:
//
//Scenario 1: Properties exists on 'from' and 'to'
//Given that the corresponding property exists on the 'From' class
//  And the corresponding property exists on the 'To' class
//When the Developer tries to add a simple mapping
//Then the test for 'AllPropertiesMapped' will not fail on this property
//
//Scenario 2: Property does not exist on one or both classes
//Given that the specified property does not exist on one of the classes
//When the Developer tries to add the mapping
//Then the addition should fail
// And the property name(s) that did not exist should be shown
//
//Scenario 3: Property already mapped to another property
//Given that the specified property exists
// And the property already has a mapping
//When the Developer tries to add a simple mapping
//Then the addition should succeed
//  And the mapping should be available for testing (One property could be
//mapped to multiple outcomes)
//
//Scenario 4: Property mapping already exists
//Given that the specified property exists
// And the property already has a mapping with the same 'from' and 'to'
//properties
//When the Developer tries to add a simple mapping
//Then the addition should fail
//  And the reason for the failure should be shown

namespace TranslationTester.Tests
{
  [TestFixture]
  public class AddSimpleMappingTest
  {
    TypeTranslationTester<SimpleFrom,SimpleTo> target;
    SimpleFrom from;
    SimpleTo to;
    
    [SetUp]
    public void Setup(){
      target=new TypeTranslationTester<SimpleFrom,SimpleTo>();
      from=new SimpleFrom();
      var translator=new SinglePropertyMappedTranslator();
      to=translator.translate(from);
    }
    
    [Test]
    [Description(@"Scenario 1: If a mapping is added for all properties no
      exception should be thrown")]
    public void MappedPropertyDoesNotThrow()
    {
      target.AddMapping(f=>f.Property1,t=>t.Property1);
      target.VerifyAllPropertiesMapped();
    }   
    
    [Test]
    [Description(@"Scenario 3: If another mapping is added from the same property the add should
      suceed")]
    public void AddSecondFromMappingSucceeds()
    {
      target.AddMapping(f=>f.Property1,t=>t.Property1);
      target.AddMapping(f=>f.Property1,t=>t.IntProperty2);
    }
    
    [Test]
    [Description(@"Scenario 4: Adding two identical simple mappings should fail.")]
    public void AddDuplicateSimpleMappingThrows()
    {
      target.AddMapping(f=>f.Property1,t=>t.Property1);
      var actual = Assert.Throws<PropertyAlreadyMappedException>(()=>target.AddMapping(f=>f.Property1,t=>t.Property1));
    }
    
    [Test]
    [Description(@"If a mapping is added from a type that can be implicitly
      converted to the to type (e.g. int to long)
      then the mapping should fail")]
    public void AddMappingWithDifferentTypesImplicitConversionThrows()
    {
      var actual = Assert.Throws<ArgumentException>(
        ()=>  target.AddMapping(f=>f.Property1,t=>t.Property2)//int to decimal
       );
    }
    
    [Test]
    [Description(@"If a mapping is added from a type that can be explicitly
      converted to the to type (e.g. int to short)
      then the mapping should fail")]
    public void AddMappingWithDifferentTypesExplicitConversionThrows()
    {
      var actual = Assert.Throws<ArgumentException>(
        ()=>target.AddMapping(f=>f.Property1,t=>t.ShortProp)//int to short
       );
    }      
  }
}
