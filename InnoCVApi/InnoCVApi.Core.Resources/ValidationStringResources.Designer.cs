﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InnoCVApi.Core.Resources {
    using System;
    
    
    /// <summary>
    ///   Clase de recurso fuertemente tipado, para buscar cadenas traducidas, etc.
    /// </summary>
    // StronglyTypedResourceBuilder generó automáticamente esta clase
    // a través de una herramienta como ResGen o Visual Studio.
    // Para agregar o quitar un miembro, edite el archivo .ResX y, a continuación, vuelva a ejecutar ResGen
    // con la opción /str o recompile su proyecto de VS.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class ValidationStringResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ValidationStringResources() {
        }
        
        /// <summary>
        ///   Devuelve la instancia de ResourceManager almacenada en caché utilizada por esta clase.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("InnoCVApi.Core.Resources.ValidationStringResources", typeof(ValidationStringResources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Reemplaza la propiedad CurrentUICulture del subproceso actual para todas las
        ///   búsquedas de recursos mediante esta clase de recurso fuertemente tipado.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Request body is invalid.
        /// </summary>
        public static string Error_InvalidBody {
            get {
                return ResourceManager.GetString("Error_InvalidBody", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Invalid birthdate.
        /// </summary>
        public static string Message_InvalidBirthDate {
            get {
                return ResourceManager.GetString("Message_InvalidBirthDate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Birth date is required.
        /// </summary>
        public static string Message_RequiredBirthDate {
            get {
                return ResourceManager.GetString("Message_RequiredBirthDate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a User Id is required.
        /// </summary>
        public static string Message_RequiredId {
            get {
                return ResourceManager.GetString("Message_RequiredId", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a User name is required.
        /// </summary>
        public static string Message_RequiredName {
            get {
                return ResourceManager.GetString("Message_RequiredName", resourceCulture);
            }
        }
    }
}
