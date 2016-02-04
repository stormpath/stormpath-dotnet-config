﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Microsoft.Extensions.Configuration.Contrib.Stormpath.Yaml {
    using System;
    using System.Reflection;
    
    
    /// <summary>
    ///    A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        internal Resources() {
        }
        
        /// <summary>
        ///    Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Microsoft.Extensions.Configuration.Contrib.Stormpath.Yaml.Resources", typeof(Resources).GetTypeInfo().Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///    Overrides the current thread's CurrentUICulture property for all
        ///    resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to The configuration file &apos;{0}&apos; was not found and is not optional..
        /// </summary>
        internal static string Error_FileNotFound {
            get {
                return ResourceManager.GetString("Error_FileNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to File path must be a non-empty string..
        /// </summary>
        internal static string Error_InvalidFilePath {
            get {
                return ResourceManager.GetString("Error_InvalidFilePath", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to A duplicate key &apos;{0}&apos; was found..
        /// </summary>
        internal static string Error_KeyIsDuplicated {
            get {
                return ResourceManager.GetString("Error_KeyIsDuplicated", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to Unsupported YAML token &apos;{0}&apos; was found. Path &apos;{1}&apos;, line {2} position {3}..
        /// </summary>
        internal static string Error_UnsupportedYAMLToken {
            get {
                return ResourceManager.GetString("Error_UnsupportedYAMLToken", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to Could not parse the YAML file. Error on line number &apos;{0}&apos;: &apos;{1}&apos;..
        /// </summary>
        internal static string Error_YAMLParseError {
            get {
                return ResourceManager.GetString("Error_YAMLParseError", resourceCulture);
            }
        }
    }
}
