using Microsoft.AspNetCore.SignalR;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using SignalR_Db_Listener.Database.Entities;
using SignalR_Db_Listener.Hubs;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.Abstracts;

namespace SignalR_Db_Listener.Database
{
    public interface IDatabaseSubscription
    {
        void Configure(string tableName);
    }
    public class SqlTableDependencyFilter : ITableDependencyFilter
    {
        public string Translate()
        {
            return "HomeTeamName = 'fb'";
        }
    }
    public class DatabaseSubscription<T> : IDatabaseSubscription where T : class, new()
    {
        
        string connectionString = "Data Source=DESKTOP-EUDN6E9;Initial Catalog=signalR-db-listener;Trusted_Connection=True";
        SqlTableDependency<T> _tableDependency;
        IHubContext<HubExample> hubContext;
        
        public DatabaseSubscription(IHubContext<HubExample> hubContext)
        {
            this.hubContext = hubContext;
        }
        public void Configure(string tableName)
        {
            var sql = new SqlCommand("sELECT * FROM Match");
            var x = new SqlDependency();
            
            _tableDependency = new SqlTableDependency<T>(connectionString, tableName, includeOldValues: true/*, filter: new SqlTableDependencyFilter()*/);
            _tableDependency.OnChanged += async (o, e) =>
            {

                var oldValue = e.EntityOldValues as Match;
                var x = e.Entity as Match;

                var message = JsonConvert.SerializeObject(x);
                await hubContext.Clients.All.SendAsync("receiveMessage", message);
                await hubContext.Clients.Client("").SendAsync("receiveMessage");
                await hubContext.Clients.Client("").SendAsync("receiveMessage");
            };

            _tableDependency.OnError += (o, e) => { };

            _tableDependency.Start();
        }

        ~DatabaseSubscription() => _tableDependency.Stop();

    }
    public static class DatabaseSubscriptionMiddleware
    {
        public static void UseDatabasSubscription<T>(this IApplicationBuilder builder, string tableName) where T : class, IDatabaseSubscription
        {
            var subscription = (T)builder.ApplicationServices.GetService(typeof(T));
            subscription.Configure(tableName);
        }
    }

}
