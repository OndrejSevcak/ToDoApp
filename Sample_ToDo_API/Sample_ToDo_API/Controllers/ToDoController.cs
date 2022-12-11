using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sample_ToDo_API.DataAccess;
using Sample_ToDo_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample_ToDo_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly IDapperService _dapper;

        public ToDoController(IDapperService dapper)
        {
            _dapper = dapper;
        }

        [HttpGet("")]   //path: api/todo
        public async Task<ActionResult<IEnumerable<ToDo>>> Get()
        {
            try
            {
                var todos = await _dapper.LoadData<ToDo>("dbo.proc_Temp_TodoGetAll");
                return Ok(todos);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("")]    //path: /api/todo?description=demo&title=Postman&fkUser=1
        //public async Task<ActionResult<int>> Create(string title, string description, int fkUser, int fkCategory, DateTime targetDate)
        public async Task<ActionResult<int>> Create(NewTodoDTO todo)
        {
            try
            {
                var par = new DynamicParameters();
                par.Add("@Description", todo.Description);
                par.Add("@Title", todo.Title);
                par.Add("@FK_User", todo.FK_User);
                par.Add("@FK_Category", todo.FK_Category);
                par.Add("@TargetDate", todo.TargetDate);

                var id = await _dapper.LoadData<int>("dbo.proc_Temp_TodoInsert", par);
                return Ok(id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("")]   //path: /api/todo?id=7&description=updatedemo&title=Postman&fkUser=1
        public async Task<ActionResult<int>> Update(int id, string title, string description, int fkUser, int fkCategory, DateTime targetDate)
        {
            try
            {
                var par = new DynamicParameters();
                par.Add("@Id", id);
                par.Add("@Title", title);
                par.Add("@Description", description);
                par.Add("@FK_User", fkUser);
                par.Add("@FK_Category", fkCategory);
                par.Add("@TargetDate", targetDate);


                var affectedRows = await _dapper.LoadData<int>("dbo.proc_Temp_TodoUpdate", par);
                return Ok(affectedRows);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("")]    //path: /api/todo?id=8&fkUser=1
        public async Task<ActionResult<string>> Delete(int id, int fkUser)
        {
            try
            {
                var par = new DynamicParameters();
                par.Add("@Id", id);
                par.Add("@FK_User", fkUser);

                var result = await _dapper.LoadData<string>("dbo.proc_Temp_TodoDelete", par);
                if(result.First().ToLower() == "ok")
                {
                    return Ok("ok");
                }
                else
                {
                    return BadRequest(result.First());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("SetDone")]
        public async Task<ActionResult<string>> SetDone(int id)
        {
            try
            {
                var par = new DynamicParameters();
                par.Add("@Id", id);

                var result = await _dapper.LoadData<string>("dbo.proc_Temp_TodoSetDone", par);
                if (result.First().ToLower() == "ok")
                {
                    return Ok("ok");
                }
                else
                {
                    return BadRequest(result.First());
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
