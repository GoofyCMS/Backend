using Microsoft.Data.Entity.Migrations;

using Goofy.Core.Components.Base;
using Goofy.Data.DataProvider;

namespace Goofy.Data.WebFramework.Components
{
    public class ComponentStoreStarter : IComponentStoreStarter<GoofyDbComponentStore>
    {
        private readonly IEntityFrameworkDataProvider _dataProvider;
        private readonly IMigrationsModelDiffer _modelDiffer;
        private readonly MigrationsSqlGenerator _sqlGenerator;

        public ComponentStoreStarter(IMigrationsModelDiffer modelDiffer,
                                     MigrationsSqlGenerator sqlGenerator,
                                     IEntityFrameworkDataProvider dataProvider)
        {
            _modelDiffer = modelDiffer;
            _sqlGenerator = sqlGenerator;
            _dataProvider = dataProvider;
        }

        public void StartStore(object store)
        {
            ((GoofyDbComponentStore)store).ComponentContext.CreateTablesIfNotExists(_modelDiffer, _sqlGenerator, _dataProvider);
        }
    }
}
