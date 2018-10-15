using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;

namespace InnoCVApi.Core.Common.ModelMapper
{
    public class ModelMapper : IModelMapper
    {
        static ModelMapper()
        {
            var appAssemblies = GetApplicationAssemblies();

            var profiles = SelectAutomapperProfiles(appAssemblies);

            ConfigureAutoMapper(profiles);
        }

        private static void ConfigureAutoMapper(IEnumerable<Type> profiles)
        {
            Mapper.Initialize(cfg =>
            {
                foreach (var profile in profiles)
                {
                    if (profile.Name != "NammedProfile")
                    {
                        cfg.AddProfile(Activator.CreateInstance(profile) as Profile);
                    }
                }
            });
        }

        private static IEnumerable<Assembly> GetApplicationAssemblies()
        {
            return AppDomain.CurrentDomain.GetAssemblies().ToList()
                .Where(assembly => assembly.FullName.StartsWith("InnoCVApi."));
        }

        private static IEnumerable<Type> SelectAutomapperProfiles(IEnumerable<Assembly> appAssemblies)
        {
            return appAssemblies.SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.BaseType == typeof(Profile));
        }

        /// <summary>
        ///     Adapts a source type to a target type.
        /// </summary>
        /// <typeparam name="TSource">Source type</typeparam>
        /// <typeparam name="TTarget">Target type</typeparam>
        /// <param name="source">Source object instance</param>
        /// <returns>Returns a <typeparam name="TTarget"> object instance</typeparam></returns>
        public TTarget Map<TSource, TTarget>(TSource source)
            where TSource : class
            where TTarget : class, new()
        {
            return Mapper.Map<TSource, TTarget>(source);
        }

        /// <summary>
        ///     Adapts an object type to a target type.
        /// </summary>
        /// <typeparam name="TTarget">Target type</typeparam>
        /// <param name="source">Source object instance</param>
        /// <returns>Returns a <typeparam name="TTarget"> object instance</typeparam></returns>
        public TTarget Map<TTarget>(object source) where TTarget : class, new()
        {
            return Mapper.Map<TTarget>(source);
        }
    }
}