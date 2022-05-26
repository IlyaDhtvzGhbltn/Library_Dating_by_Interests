using Microsoft.EntityFrameworkCore.Migrations;

namespace Library.Entities.Migrations
{
    public partial class getElibibleUserStoredProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sqlProc = @"CREATE PROCEDURE GetUsersWithSameSubscription
                @APIUSERID UNIQUEIDENTIFIER
                AS
                BEGIN
                SELECT RESULT.TARGET_USER_SUBSCRIPT as YoutubeChannelId, RESULT.OTHER_SUBSCRIBED_USER AS ApiUserId FROM
                (SELECT* FROM

                    (SELECT[YoutubeChannelId] AS TARGET_USER_SUBSCRIPT FROM[ApiUserYoutubeChannel]
                    WHERE [ApiUserId] = @APIUSERID) AS TARGUSER
                    JOIN
                    (SELECT[ApiUserId] AS OTHER_SUBSCRIBED_USER, [YoutubeChannelId] AS CHANNEL FROM[ApiUserYoutubeChannel]
                    WHERE[ApiUserId] != @APIUSERID)
                    AS OTHERS
                    ON TARGUSER.TARGET_USER_SUBSCRIPT = OTHERS.CHANNEL
                ) AS RESULT
                END;";
            migrationBuilder.Sql(sqlProc);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var dropSqlProc = "DROP PROC GetUsersWithSameSubscription";
            migrationBuilder.Sql(dropSqlProc);
        }
    }
}
