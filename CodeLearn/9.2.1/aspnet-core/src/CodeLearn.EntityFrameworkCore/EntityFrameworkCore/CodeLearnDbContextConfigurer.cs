using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace CodeLearn.EntityFrameworkCore
{
    public static class CodeLearnDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<CodeLearnDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<CodeLearnDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
