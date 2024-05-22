using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnionCQRS.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class add_store_procedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            AddGetAllEmployeeDepartmentProcedure(migrationBuilder);
        }

        private void AddGetAllEmployeeDepartmentProcedure(MigrationBuilder migrationBuilder)
        {
            string query = $@"
								IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = 'GetAllEmployDepartment')
								BEGIN
									EXEC('CREATE PROCEDURE GetAllEmployeeDepartment
                                    AS
                                    BEGIN
                                        SELECT 
                                            E.Id AS EmployeeId,
                                            E.FullName AS EmployeeFullName,
                                            E.Birthday AS EmployeeBirthday,
                                            E.Email AS EmployeeEmail,
                                            E.Phone AS EmployeePhone,
                                            D.Id AS DepartmentId,
                                            D.DepartmentName AS DepartmentName,
                                            D.Phone AS DepartmentPhone,
                                            D.[Describe] AS DepartmentDescription
                                        FROM 
                                            [dbo].[Employee] AS E
                                        INNER JOIN 
                                            [dbo].[Department] AS D ON E.DepartmentId = D.Id;
                                    END')
								END
							";

            migrationBuilder.Sql(query, suppressTransaction: true);
        }

    }
}
