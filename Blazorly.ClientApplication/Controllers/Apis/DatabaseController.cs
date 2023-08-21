using Blazorly.ClientApplication.Core;
using Blazorly.ClientApplication.Core.DB;
using Blazorly.ClientApplication.Core.Dto;
using Blazorly.ClientApplication.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI.Common;

namespace Blazorly.ClientApplication.Controllers.Apis
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatabaseController : ControllerBase
    {
        private DBFactory db;

        public DatabaseController()
        {
            db = new DBFactory(AppConfigs.DatabaseType, AppConfigs.DatabaseConnectionString, AppConfigs.DBTimeout, AppConfigs.Schema);
        }

        [HttpGet]
        [Route("schema")]
        public async Task<ActionResult> GetSchema(bool reload = false)
        {
            Schema result = new Schema();
            try
            {
                if(reload)
                {
                    AppConfigs.Schema = db.GetSchema();
                }

                if(AppConfigs.Schema == null)
                {
                    AppConfigs.Schema = db.GetSchema();
                }

                result = db.GetSchema();
                
                if (result == null)
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

            return Ok(result);
        }

        [HttpPost]
        [Route("install")]
        public async Task<ActionResult> Install(bool refresh = false)
        {
            
        }
    }
}
