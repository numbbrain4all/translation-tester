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
using System.Collections.Generic;
using TranslationTester.Exceptions;

namespace TranslationTester
{
	public class TypeTranslationTester<TFrom,TTo>
	{
		public bool AllPropertiesMapped{
			get{
				return unmappedProperties.Count==0;
			}
		}
		
		List<string> unmappedProperties;
		List<string> allToProperties;
		//List<SimpleMapping> simpleMappings;
		
		
		public TypeTranslationTester()
		{
			//simpleMappings=new List<SimpleMapping>();
			
			InitializeFromProperties();
			InitializeToProperties();			
		}

		void InitializeFromProperties()
		{
			unmappedProperties=new List<string>();
			var allFromProps = typeof(TFrom).GetProperties();
			foreach (var Prop in allFromProps) {
				unmappedProperties.Add(Prop.Name);
			}
		}
		
		void InitializeToProperties()
		{
			allToProperties=new List<string>();
			var allToProps = typeof(TTo).GetProperties();
			foreach (var Prop in allToProps) {
				allToProperties.Add(Prop.Name);
			}
		}

		
		public void AddMapping(string fromProperty, string toProperty){
			if(false==unmappedProperties.Contains(fromProperty)){
				throw new PropertyNotFoundException();
			}
			if(false==allToProperties.Contains(toProperty)){
				throw new PropertyNotFoundException();			
			}
			unmappedProperties.Remove(fromProperty);
			//simpleMappings.Add(new SimpleMapping(fromProperty,toProperty));
		}
		
		public void VerifyAllPropertiesMapped(TFrom from, TTo to){
			if(unmappedProperties.Count>0){
				throw new UnmappedPropertyException();
			}
		}
		
	}
}
