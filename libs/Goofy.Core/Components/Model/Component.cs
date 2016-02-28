
namespace Goofy.Core.Components
{
    public class Component
    {
        public int ComponentId { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public bool Installed { get; set; }
        public bool IsSystemComponent { get; set; }

        /*
            No es necesario guardar dependencias entre componentes en la base de datos
            La forma de hacerlo sería algo como lo de abajo porque EF 7 no soporta relaciones muchos-a-muchos todavía
        */
        //public List<ComponentToComponent> Dependencies { get; set; }
    }
}
