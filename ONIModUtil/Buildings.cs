using System.Reflection;

namespace ONIModUtil
{
    public class Buildings
    {
        public static void AddBuildings(string buildingsNamespace)
        {
            string mynamespace = "GravitasLabReplica.BuildingConfigs";
            var types = Assembly.GetExecutingAssembly().GetTypes().Where(x => x.Namespace == buildingsNamespace);

            foreach(var t in types)
            {
                var obj = Activator.CreateInstance(t);
            }
            
        }
    }
}