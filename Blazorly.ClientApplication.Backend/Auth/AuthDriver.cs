using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Dapper;
using System.Data.SqlClient;

namespace YourNamespace
{
    public abstract class AuthDriver
    {
        protected SqlConnection knex;
        protected SchemaOverview schema;

        public AuthDriver()
        {
            this.knex = new SqlConnection("Your Connection String Here");
            this.schema = new SchemaOverview { Collections = new Dictionary<string, Collection>(), Relations = new List<Relation>() };
        }

        // Get user id for a given provider payload
        public abstract Task<string> GetUserIDAsync(Dictionary<string, object> payload);

        // Verify user password
        public abstract Task VerifyAsync(User user, string password = null);

        // Check with the (external) provider if the user is allowed entry
        public virtual async Task LoginAsync(User user, Dictionary<string, object> payload)
        {
            // Implementation here if needed
        }

        // Handle user session refresh
        public virtual async Task RefreshAsync(User user)
        {
            // Implementation here if needed
        }

        // Handle user session termination
        public virtual async Task LogoutAsync(User user)
        {
            // Implementation here if needed
        }
    }

    public class SchemaOverview
    {
        public Dictionary<string, Collection> Collections { get; set; }
        public List<Relation> Relations { get; set; }
    }

    public class Collection
    {
        // Define collection properties here
    }

    public class Relation
    {
        // Define relation properties here
    }

    public class User
    {
        // Define user properties here
    }
}


