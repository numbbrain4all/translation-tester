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
using System.Linq;

namespace TranslationTester.Tests
{
  //  Narrative:
  //As a Developer
  //I want to reduce the amount of ‘boilerplate’ code for testing translators
  //So that I can work more efficiently
//
  //Acceptance Criteria:
//
  //Scenario 1: Identical properties
  //Given both the 'from' and 'to' classes have identically named properties
  //  And Both properties have the same type
  //When  The Developer asks the TranslationTester to automatically map properties
  //Then  A simple mapping should be added for the property
//
  //Scenario 2: Identical names but different types
  //Given both the 'from' and 'to' classes have identically named properties
  //When  The Developer asks the TranslationTester to automatically map properties
  //Then  A simple mapping should be added for the property
//
  //Scenario 3: Potential mapping already added
  //Given that a potential automated mapping exists
  //  And the associated from property has already been mapped
  //When  The Developer asks the TranslationTester to automatically map properties
  //Then  No mapping should be added
  //  And the method should return successfully
//
  //Scenario 4: Other mapping already added
  //Given that a potential automated mapping exists
  //  And a seperate property has already been mapped
  //When  The Developer asks the TranslationTester to automatically map properties
  //Then  the mapping for the potentially automated mapping should be added
//
  //Scenario 5: Potential mapping already excluded
  //Given that a potential automated mapping exists
  //  And the associated from property has already been excluded
  //When  The Developer asks the TranslationTester to automatically map properties
  //Then  No mapping should be added
  //  And the method should return successfully
//
  //Scenario 6: Other property already excluded
  //Given that a potential automated mapping exists
  //  And a seperate property has already been excluded
  //When  The Developer asks the TranslationTester to automatically map properties
  //Then  the mapping for the potentially automated mapping should be added
//
  //Scenario 7: Mappings returned
  //Given that there are multiple potential automated mappings
  //When  The Developer asks the TranslationTester to automatically map properties
  //Then  the mappings will be returned from the method call




  [TestFixture]
  public class AutomateMappingsTest
  {
    TypeTranslationTester<MultipleFrom,ToAutomatedMappings> target;
    MultipleFrom from;
    ToAutomatedMappings to;
    
    [SetUp]
    public void Setup(){
      target=new TypeTranslationTester<MultipleFrom, ToAutomatedMappings>();
      from=new MultipleFrom();
      to=new ToAutomatedMappings();
    }
    
    [Test]
    [Description(@"Scenario 1: If the from and to types have identical properties
      a mapping will be automatically added")]
    public void IdenticalNameAndTypeMapped()
    {
      var mappings= target.AutomaticallyMapProperties();
      Assert.IsTrue(mappings.Any(p=>p.FromProperty=="Property1"));
    }
    
    [Test]
    [Description(@"Scenario 2: If the from and to types have identically named properties
      with different types a mapping will not be automatically added")]
    public void IdenticalNameDifferentTypeNotMapped()
    {
      var mappings= target.AutomaticallyMapProperties();
      Assert.IsFalse(mappings.Any(p=>p.FromProperty=="Property2"));
    }
    
    [Test]
    [Description(@"Scenario 3: Potential mapping already added, not added again
      by automatic method")]
    public void MappingAlreadyExistsNoChange()
    {
      var prop="Property1";
      target.AddMapping(prop,prop);
      var mappings= target.AutomaticallyMapProperties();
      Assert.IsFalse(mappings.Any(p=>p.FromProperty==prop));
    }
    
    [Test]
    [Description(@"Scenario 4: Potential mapping exists, other mapping already added 
      automated one added")]
    public void OtherMappingAlreadyExistsMappingAdded()
    {
      var prop="Property1";
      var prop2="StringProp";
      target.AddMapping(prop2,prop2);
      var mappings= target.AutomaticallyMapProperties();
      Assert.IsTrue(mappings.Any(p=>p.FromProperty==prop));
    }
    
    [Test]
    [Description(@"Scenario 5: Potential mapping already excluded, not added 
      by automatic method")]
    public void PropertyAlreadyExcludedNoChange()
    {
      var prop="Property1";
      target.ExcludeProperty(prop);
      var mappings= target.AutomaticallyMapProperties();
      Assert.IsFalse(mappings.Any(p=>p.FromProperty==prop));
    }
    
    [Test]
    [Description(@"Scenario 6: Another property excluded does not stop
      the automatic mapping being added")]
    public void OtherPropertyAlreadyExcludedMappingAdded()
    {
      var prop="Property1";
      var prop2="StringProp";
      target.ExcludeProperty(prop2);
      var mappings= target.AutomaticallyMapProperties();
      Assert.IsTrue(mappings.Any(p=>p.FromProperty==prop));
    }
    
    [Test]
    [Description(@"Scenario 7: The generated mappings should be returned")]
    public void MappingsReturned()
    {
      var mappings= target.AutomaticallyMapProperties();
      Assert.IsTrue(mappings.Count>1);
    }
  }
}
