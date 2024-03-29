﻿using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Markup;

// All other assembly info is defined in SolutionAssemblyInfo.cs

[assembly: AssemblyTitle("Orc.Theming")]
[assembly: AssemblyProduct("Orc.Theming")]
[assembly: AssemblyDescription("Orc.Theming library")]
[assembly: NeutralResourcesLanguage("en-US")]

[assembly: XmlnsPrefix("http://schemas.wildgums.com/orc/theming", "orctheming")]
[assembly: XmlnsDefinition("http://schemas.wildgums.com/orc/theming", "Orc.Theming")]
[assembly: XmlnsDefinition("http://schemas.wildgums.com/orc/theming", "Orc.Theming.Behaviors")]
[assembly: XmlnsDefinition("http://schemas.wildgums.com/orc/theming", "Orc.Theming.Controls")]
[assembly: XmlnsDefinition("http://schemas.wildgums.com/orc/theming", "Orc.Theming.Converters")]
//[assembly: XmlnsDefinition("http://schemas.wildgums.com/orc/theming", "Orc.Theming.Fonts")]
//[assembly: XmlnsDefinition("http://schemas.wildgums.com/orc/theming", "Orc.Theming.Markup")]
//[assembly: XmlnsDefinition("http://schemas.wildgums.com/orc/theming", "Orc.Theming.Theming")]
[assembly: XmlnsDefinition("http://schemas.wildgums.com/orc/theming", "Orc.Theming.Views")]
//[assembly: XmlnsDefinition("http://schemas.wildgums.com/orc/theming", "Orc.Theming.Windows")]

[assembly: InternalsVisibleTo("Orc.Theming.Tests")]

[assembly: ThemeInfo(
    ResourceDictionaryLocation.None, //where theme specific resource dictionaries are located
                                     //(used if a resource is not found in the page, 
                                     // or application resource dictionaries)
    ResourceDictionaryLocation.SourceAssembly //where the generic resource dictionary is located
                                              //(used if a resource is not found in the page, 
                                              // app, or any theme specific resource dictionaries)
    )]
