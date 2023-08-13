using Blazorly.ClientApplication.Core;
using Blazorly.ClientApplication.Core.Dto;
using Blazorly.ClientApplication.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Dynamic;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Blazorly.ClientApplication.Controllers.Apis
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private DBFactory db;

        public ItemsController()
        {
            db = new DBFactory(AppConfigs.DatabaseType, AppConfigs.DatabaseConnectionString, AppConfigs.DBTimeout, AppConfigs.Schema);
        }

        [HttpPost]
        [Route("{collection}")]
        public async Task<ActionResult> Insert(string collection, JsonDocument data)
        {
            ItemsResponse result = new ItemsResponse();
            try
            {
                var json = data.RootElement.GetRawText();
                var obj = JsonConvert.DeserializeObject<ExpandoObject>(json);
                await db.Insert(collection, obj);
                result.Success = true;
            }
            catch(AccessDeniedException adEx)
            {
                result.Message = adEx.Message;
#if DEBUG
                result.Error = adEx.ToString();
#endif
                return Unauthorized(result);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;

#if DEBUG
                result.Error = ex.ToString();
#endif
                return StatusCode(500, result);
            }

            return Ok(result);
        }

        [HttpPatch]
        [Route("{collection}/{key}/{value}")]
        public async Task<ActionResult> Update(string collection, string key, string value, JsonDocument data)
        {
            ItemsResponse result = new ItemsResponse();
            try
            {
                var json = data.RootElement.GetRawText();
                var obj = JsonConvert.DeserializeObject<ExpandoObject>(json);
                await db.Update(collection, key, value, obj);
                result.Success = true;
            }
            catch (AccessDeniedException adEx)
            {
                result.Message = adEx.Message;
#if DEBUG
                result.Error = adEx.ToString();
#endif
                return Unauthorized(result);
            }
            catch (RecordNotFoundException rnfEx)
            {
                result.Message = rnfEx.Message;
#if DEBUG
                result.Error = rnfEx.ToString();
#endif
                return NotFound(result);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;

#if DEBUG
                result.Error = ex.ToString();
#endif
                return StatusCode(500, result);
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("{collection}/{key}/{value}")]
        public async Task<ActionResult> Read(string collection, string key, string value)
        {
            ItemsResponse result = new ItemsResponse();
            try
            {
                result.Data = await db.Read(collection, key, value);
                if(result.Data == null)
                {
                    return NoContent();
                }
            }
            catch (AccessDeniedException adEx)
            {
                result.Message = adEx.Message;
#if DEBUG
                result.Error = adEx.ToString();
#endif
                return Unauthorized(result);
            }
            catch (RecordNotFoundException rnfEx)
            {
                result.Message = rnfEx.Message;
#if DEBUG
                result.Error = rnfEx.ToString();
#endif
                return NotFound(result);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;

#if DEBUG
                result.Error = ex.ToString();
#endif
                return StatusCode(500, result);
            }

            return Ok(result);
        }

        [HttpOptions("SEARCH")]
        [Route("{collection}")]
        public async Task<ActionResult> Query(string collection, int? page, int? count, ItemsQueryRequest data)
        {
            ItemsResponse result = new ItemsResponse();
            try
            {
                var resultSet = await db.Query(collection, data, page, count);
                
                result.Data = DBUtils.ParseQueryResult(resultSet.List.ToList());
                result.HasNext = resultSet.HasNext;
                result.HasPrevious = resultSet.HasPrevious;
                result.Count = resultSet.Count;
                result.TotalPages = resultSet.TotalPages;
                result.PerPage = resultSet.PerPage;
                if (result.Data == null)
                {
                    return NoContent();
                }
            }
            catch (AccessDeniedException adEx)
            {
                result.Message = adEx.Message;
#if DEBUG
                result.Error = adEx.ToString();
#endif
                return Unauthorized(result);
            }
            catch (RecordNotFoundException rnfEx)
            {
                result.Message = rnfEx.Message;
#if DEBUG
                result.Error = rnfEx.ToString();
#endif
                return NotFound(result);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;

#if DEBUG
                result.Error = ex.ToString();
#endif
                return StatusCode(500, result);
            }

            return Ok(result);
        }
    }
}
