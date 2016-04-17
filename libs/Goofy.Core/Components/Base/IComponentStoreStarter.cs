using Goofy.Core.Infrastructure;

namespace Goofy.Core.Components.Base
{
    public interface IComponentStoreStarter<T> : IDesignTimeService where T : IComponentStore
    {
        /*
            Esto está feo pero, si pongo T store entonces cuando trato de llamar al método de extensión
            IComponentStoreExtensions.StartStore da error en tiempo de ejecución en la línea:
                - storeStarter.StartStore(componentStore); 
            porque el tipo que se le pasa al método es el estático y no el de ejecución.
            *** El problema que quiero resolver usando ese código es el siguiente:
                - Tengo un IComponentStore(necesario para abstraerme de la forma en que se almacenan las componentes)
                que quisiera tener una forma de iniciarlo(En el caso particular de ComponentStore, crear las tablas
                en la base de datos para el ComponentContext) sin que el método para hacerlo formase parte de su 
                contrato para que no se llame múltiples veces, además de que el IComponentStore es un servicio para 
                usar desde las Componentes de 3ros y no quisiera que contuviese servicios de tiempo de diseño, que sólo
                se usan al cargar Goofy(como IMigrationsModelDiffer, MigrationsSqlGenerator)
            ***
        */
        void StartStore(object store);
    }
}
