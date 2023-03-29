using Dapper;
using DevSchool.Entities;
using DevSchool.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace DevSchool.Controllers
{
    [Route("api/dev-students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly string _connectonString;
        public StudentsController(IConfiguration configuration)
        {
            _connectonString = configuration.GetConnectionString("DevSchoolCS");
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            using (var sqlConn = new SqlConnection(_connectonString))
            {
                const string query = "SELECT [Id]" +
                                        ",[Fullname]" +
                                        ",[BirthDate]" +
                                        ",[SchoolClass]" +
                                        ",[IsActive]" +
                                    "FROM " +
                                        "[Students]" +
                                    "Where IsActive = 1";
                var students = await sqlConn.QueryAsync<Student>(query);

                return Ok(students);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            using (var sqlConn = new SqlConnection(_connectonString))
            {
                var parameters = new
                {
                    id
                };

                const string query = "SELECT [Id]" +
                                        ",[Fullname]" +
                                        ",[BirthDate]" +
                                        ",[SchoolClass]" +
                                        ",[IsActive]" +
                                    "FROM " +
                                        "[Students]" +
                                    "Where Id = @id";
                var student = await sqlConn.QuerySingleOrDefaultAsync<Student>(query, parameters);

                if (student is null)
                    return NotFound();

                return Ok(student);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Post(StudentInputModel model)
        {
            var student = new Student(model.FullName, model.BirthDate, model.SchoolClass);
            var parameters = new
            {
                student.FullName,
                student.BirthDate,
                student.SchoolClass,
                student.IsActive
            };

            using (var sqlConn = new SqlConnection(_connectonString))
            {
                const string query = "INSERT INTO Students OUTPUT INSERTED.Id Values( " +
                                        "@Fullname" +
                                        ",@BirthDate" +
                                        ",@SchoolClass" +
                                        ",@IsActive)";

                var id = await sqlConn.ExecuteScalarAsync<int>(query, parameters);

                return Ok(id);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Put(int id, StudentInputModel model)
        {
            var student = new Student(model.FullName, model.BirthDate, model.SchoolClass);
            var parameters = new
            {
                id,
                model.FullName,
                model.BirthDate,
                model.SchoolClass
            };

            using (var sqlConn = new SqlConnection(_connectonString))
            {
                const string query = "UPDATE Students SET " +
                                        "Fullname = @Fullname" +
                                        ",BirthDate = @BirthDate" +
                                        ",SchoolClass = @SchoolClass" +
                                        " WHERE Id = @id";

                await sqlConn.ExecuteScalarAsync<int>(query, parameters);

                return NoContent();
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            const string query = "UPDATE Students SET IsActive = 0 WHERE Id = @id";

            var parameters = new { id };

            using (var sqlConn = new SqlConnection(_connectonString))
            {
                await sqlConn.ExecuteScalarAsync<int>(query, parameters);

                return NoContent();
            }
        }

    }
}
