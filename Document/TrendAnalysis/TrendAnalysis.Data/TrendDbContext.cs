using TrendAnalysis.Models.DataBase;


namespace TrendAnalysis.Data
{
    using Config;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration;
    using System.Linq;
    using System.Reflection;
    using Models;
    using Migrations;

    public class TrendDbContext : DbContext
    {
        //您的上下文已配置为从您的应用程序的配置文件(App.config 或 Web.config)
        //使用“TrendDbContext”连接字符串。默认情况下，此连接字符串针对您的 LocalDb 实例上的
        //“TrendAnalysis.Data.TrendDbContext”数据库。
        // 
        //如果您想要针对其他数据库和/或数据库提供程序，请在应用程序配置文件中修改“TrendDbContext”
        //连接字符串。
        public TrendDbContext(): this(Settings.ConnectionString)
        {
        }

        public TrendDbContext(string connStr) : base(connStr)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<TrendDbContext, Configuration>());
        }
        //为您要在模型中包含的每种实体类型都添加 DbSet。有关配置和使用 Code First  模型
        //的详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=390109。

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Types().Configure(entity => entity.ToTable("T_" + entity.ClrType.Name));
            modelBuilder.Types<BaseEntity>().Configure(entity => entity.HasKey(b => b.Id));
            modelBuilder.Types<BaseEntity>().Configure(entity => entity.Property(p => p.RowVersion).IsRowVersion());

            //添加映射类
            var mapTypes = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.Namespace != null)
                .Where(t => t.BaseType != null && t.BaseType.IsGenericType && t.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));
            foreach(var type in mapTypes)
            {
                dynamic instance = Activator.CreateInstance(type);
                //添加映射类
                modelBuilder.Configurations.Add(instance);
            }

            //添加实体类型
            var entityTypes =Assembly.Load("TrendAnalysis.Models").GetTypes().Where(t => t.BaseType != null && t.BaseType == typeof(BaseEntity));
            foreach (var type in entityTypes)
            {
                modelBuilder.RegisterEntityType(type);
            }

            base.OnModelCreating(modelBuilder);

            //var modelAssembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.FullName.StartsWith(nameof(TrendAnalysis.Models)));
            //if (modelAssembly == null)
            //    throw new Exception(string.Format("未能加载{0}程序集！",nameof(TrendAnalysis.Models)));

            ////添加实体类型
            //var entityTypes = modelAssembly.GetTypes().Where(t=>t.BaseType!=null && t.BaseType==typeof(BaseEntity));
            //foreach(var type in entityTypes)
            //{
            //    modelBuilder.RegisterEntityType(type);
            //}
        }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}