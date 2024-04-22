using FluentMigrator;

namespace EmployeesAPI.Migrations
{
    [Migration(20240422110930)]
    public class Migration_20240422110930 : Migration
    {
        public override void Down()
        {
            Delete.Table("Employees");
            Delete.Table("Passports");
            Delete.Table("Departments");
        }

        public override void Up()
        {
            Create.Table("Passports")
                .WithColumn("id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("type").AsString().NotNullable()
                .WithColumn("number").AsString().NotNullable().Unique();
            
            Create.Table("Departments")
                .WithColumn("id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("departmentname").AsString().NotNullable()
                .WithColumn("departmentphone").AsString();

            Create.Table("Employees")
                .WithColumn("id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("name").AsString().NotNullable()
                .WithColumn("surname").AsString()
                .WithColumn("phone").AsString().NotNullable()
                .WithColumn("companyid").AsInt32().NotNullable()
                .WithColumn("passportid").AsInt32()
                .WithColumn("departmentid").AsInt32();
        }
    }
}
