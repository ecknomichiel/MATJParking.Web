using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Configuration;

namespace MATJParking.Web.DataAccess
{
    public class GarageContextFactory
    {
        Dictionary<string, IGarageContextCreator> contexts = new Dictionary<string, IGarageContextCreator>();
        private static GarageContextFactory instance = null;
        public static  GarageContextFactory Instance 
        { 
            get 
            {  
                if (instance == null)
                {
                    instance = new GarageContextFactory();
                    instance.RegisterAllClassesThatImplementIGarageContextCreator();
                }
                return instance;
            } 
        }

        private void RegisterAllClassesThatImplementIGarageContextCreator()
        {
            Assembly currAssembly = Assembly.GetExecutingAssembly();

            Type baseType = typeof(IGarageContextCreator);

            foreach (Type type in currAssembly.GetTypes())
            {
                if (!type.IsClass || type.IsAbstract)
                {
                    continue;
                }

                IGarageContextCreator creatorObject =
                    System.Activator.CreateInstance(type) as IGarageContextCreator;
                if (creatorObject != null)
                {
                    contexts.Add(creatorObject.FactoryKey, creatorObject);
                }
            }
        }

        public IGarageContext GetGarageContext()
        {
            string key = GetKeyFromConfiguration();
            
            IGarageContextCreator creator;
            if (!contexts.TryGetValue(key, out creator))
                creator = contexts.Values.First();
            return creator.CreateContext();
        }

        private string GetKeyFromConfiguration()
        {
            return ConfigurationManager.AppSettings["GarageContextKey"];
        }
    }

    public interface IGarageContextCreator
    {
        string FactoryKey { get; }
        IGarageContext CreateContext();
    }
}