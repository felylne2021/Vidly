namespace Vidly.Migrations {
    using System;
    using System.Data.Entity.Migrations;
    using System.Web.Mvc.Ajax;

    public partial class PopulateGenres : DbMigration {
        public override void Up() {
            Sql("INSERT INTO Genres VALUES ('Comedy')");
            Sql("INSERT INTO Genres VALUES ('Action')");
            Sql("INSERT INTO Genres VALUES ('Thriller')");
            Sql("INSERT INTO Genres VALUES ('Romance')");
            Sql("INSERT INTO Genres VALUES ('Sci-fi')");
        }

        public override void Down() {
        }
    }
}
