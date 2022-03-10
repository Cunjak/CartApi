using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class triggers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"create trigger [dbo].[trgAfterUpdateDateCart]
                ON [dbo].[Cart]
                AFTER UPDATE 
                AS BEGIN
                   DECLARE @vTimeCreatedNew datetimeoffset(7)
                   DECLARE @vTimeCreatedOld datetimeoffset(7)
                   select @vTimeCreatedNew = TimeCreated from inserted
                   select @vTimeCreatedOld = TimeCreated from deleted
                    
                   if(@vTimeCreatedNew <> @vTimeCreatedOld)
                    begin
                    PRINT 'Cant change TimeCreated'
                    ROLLBACK TRANSACTION
                    end
                    
                   UPDATE dbo.Cart
                   SET TimeUpdated = sysdatetimeoffset()
                   FROM INSERTED i
                   WHERE i.Id = Cart.Id
                END");

            migrationBuilder.Sql(
                @"create trigger [dbo].[trgAfterUpdateDateCartItem]
                ON [dbo].[CartItem]
                AFTER UPDATE 
                AS BEGIN
                   DECLARE @vTimeCreatedNew datetimeoffset(7)
                   DECLARE @vTimeCreatedOld datetimeoffset(7)
                   select @vTimeCreatedNew = TimeCreated from inserted
                   select @vTimeCreatedOld = TimeCreated from deleted
                    
                   if(@vTimeCreatedNew <> @vTimeCreatedOld)
                    begin
                    PRINT 'Cant change TimeCreated'
                    ROLLBACK TRANSACTION
                    end

                   UPDATE dbo.CartItem
                   SET TimeUpdated = sysdatetimeoffset()
                   FROM INSERTED i
                   WHERE i.Id = CartItem.Id
                END");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"drop trigger [dbo].[trgAfterUpdateDateCart]");

            migrationBuilder.Sql(@"drop trigger [dbo].[trgAfterUpdateDateCartItem]");
        }
    }
}
