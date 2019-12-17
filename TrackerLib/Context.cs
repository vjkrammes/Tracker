using System.Collections.Generic;
using System.Data;
using System.Linq;

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

using TrackerCommon;

using TrackerLib.Entities;

namespace TrackerLib
{
    public class Context : DbContext
    {

        private readonly string _connectionString;

        #region DbSets

        public DbSet<ClientEntity> Clients { get; set; }
        public DbSet<ClientTypeEntity> ClientTypes { get; set; }
        public DbSet<HoursEntity> Hours { get; set; }
        public DbSet<MileageEntity> Mileage { get; set; }
        public DbSet<NoteEntity> Notes { get; set; }
        public DbSet<PhoneEntity> Phones { get; set; }
        public DbSet<PhoneTypeEntity> PhoneTypes { get; set; }
        public DbSet<SettingsEntity> SystemSettings { get; set; }

        #endregion

        #region Constructors

        public Context(DbContextOptions<Context> options) : base(options)
        {
            _connectionString = CSBuilder.Build();
        }

        public Context() : base()
        {
            _connectionString = CSBuilder.Build();
        }

        #endregion

        #region Public Methods / Properties

        public SettingsEntity GetSettings { get => SystemSettings.FirstOrDefault(); }

        public void Seed()
        {
            if (GetSettings is null)
            {
                SystemSettings.Add(SettingsEntity.Default);
                SaveChanges();
            }
        }

        public IEnumerable<int> Years()
        {
            HashSet<int> hyears = new HashSet<int>(Hours.Select(x => x.Date.Year).Distinct());
            HashSet<int> myears = new HashSet<int>(Mileage.Select(x => x.Date.Year).Distinct());
            hyears.UnionWith(myears);
            return hyears.ToList();
        }

        public DatabaseInfo DatabaseInfo()
        {
            var conn = Database.GetDbConnection();
            if (!(conn is SqlConnection connection))
            {
                return new DatabaseInfo();
            }
            var command = new SqlCommand("sp_spaceused")
            {
                CommandType = CommandType.StoredProcedure,
                Connection = connection
            };
            using var adapter = new SqlDataAdapter(command);
            var dataset = new DataSet();
            connection.Open();
            adapter.Fill(dataset);
            connection.Close();
            var ret = new DatabaseInfo(dataset);
            return ret;
        }

        public void Backup(string filename)
        {
            var config = ConfigurationFactory.Create();
            var conn = Database.GetDbConnection();
            if (!(conn is SqlConnection connection))
            {
                return;
            }
            using var command = new SqlCommand("backup database @n to disk = @l with init;")
            {
                CommandType = CommandType.Text,
                Connection = connection
            };
            command.Parameters.Add(new SqlParameter
            {
                ParameterName = "n",
                SqlDbType = SqlDbType.NVarChar,
                Value = config[Constants.DatabaseConfig]
            });
            command.Parameters.Add(new SqlParameter
            {
                ParameterName = "l",
                SqlDbType = SqlDbType.NVarChar,
                Value = filename
            });
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        #endregion

        #region Overrides

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);
            builder.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ClientEntity>().HasIndex(x => x.Name).IsUnique().IsClustered(false);
            builder.Entity<ClientEntity>().HasOne(x => x.ClientType);

            builder.Entity<ClientTypeEntity>().HasIndex(x => x.Name).IsUnique().IsClustered(false);

            builder.Entity<HoursEntity>().HasIndex(x => x.ClientId).IsClustered(false);
            builder.Entity<HoursEntity>().HasIndex(x => x.Date).IsClustered(false);
            builder.Entity<HoursEntity>().Property(x => x.Date).HasColumnType(Constants.Date);
            builder.Entity<HoursEntity>().Property(x => x.Time).HasColumnType(Constants.Hours);

            builder.Entity<MileageEntity>().HasIndex(x => x.ClientId).IsClustered(false);
            builder.Entity<MileageEntity>().HasIndex(x => x.Date).IsClustered(false);
            builder.Entity<MileageEntity>().Property(x => x.Date).HasColumnType(Constants.Date);
            builder.Entity<MileageEntity>().Property(x => x.Miles).HasColumnType(Constants.Miles);

            builder.Entity<NoteEntity>().HasIndex(x => x.ClientId).IsClustered(false);
            builder.Entity<NoteEntity>().HasIndex(x => x.Date).IsClustered(false);
            builder.Entity<NoteEntity>().Property(x => x.Date).HasColumnType(Constants.Datetime2);
            builder.Entity<NoteEntity>().HasOne(x => x.Client);

            builder.Entity<PhoneEntity>().HasIndex(x => x.ClientId).IsClustered(false);
            builder.Entity<PhoneEntity>().HasOne(x => x.PhoneType);
            builder.Entity<PhoneEntity>().HasOne(x => x.Client);

            builder.Entity<PhoneTypeEntity>().HasIndex(x => x.Name).IsUnique().IsClustered(false);

            builder.Entity<SettingsEntity>().HasKey(x => x.SystemId);
            builder.Entity<SettingsEntity>().HasIndex(x => x.ProductName).IsUnique().IsClustered(false);
        }

        #endregion
    }
}
