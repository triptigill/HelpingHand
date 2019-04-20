namespace HelpingHand.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bills",
                c => new
                    {
                        BillId = c.Guid(nullable: false),
                        ClientId = c.Guid(nullable: false),
                        ProductId = c.Guid(nullable: false),
                        InvoiceNo = c.Int(nullable: false),
                        Companyname = c.String(nullable: false, maxLength: 20),
                        Invoicedate = c.DateTime(nullable: false),
                        Location = c.String(nullable: false, maxLength: 20),
                        Chalanno = c.Int(nullable: false),
                        Customername = c.String(nullable: false, maxLength: 20),
                        Customeraddress1 = c.String(nullable: false, maxLength: 20),
                        Customeraddress2 = c.String(nullable: false, maxLength: 20),
                        Mobile = c.String(nullable: false),
                        Totalamt = c.Int(nullable: false),
                        Charges = c.Int(nullable: false),
                        Discount = c.Int(nullable: false),
                        GTAmt = c.Int(nullable: false),
                        Serial = c.String(nullable: false),
                        Paidtype = c.String(nullable: false),
                        Amtword = c.String(nullable: false),
                        Narration = c.String(),
                        Chequeno = c.String(nullable: false),
                        Language = c.String(nullable: false),
                        Entrydate = c.DateTime(nullable: false),
                        Discountper = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BillId)
                .ForeignKey("dbo.ClientDetails", t => t.ClientId, cascadeDelete: true)
                .ForeignKey("dbo.ProductDetails", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ClientId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.ClientDetails",
                c => new
                    {
                        ClientId = c.Guid(nullable: false),
                        ClientName = c.String(nullable: false),
                        Address1 = c.String(nullable: false, maxLength: 20),
                        Address2 = c.String(nullable: false, maxLength: 20),
                        Telephone = c.String(nullable: false),
                        Mobile = c.String(nullable: false),
                        Email = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ClientId);
            
            CreateTable(
                "dbo.ProductDetails",
                c => new
                    {
                        ProductId = c.Guid(nullable: false),
                        Title = c.String(nullable: false),
                        Booktype = c.String(nullable: false),
                        Group = c.String(nullable: false),
                        Writer = c.String(nullable: false),
                        ProductCode = c.String(nullable: false, maxLength: 50),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Description = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ProductId);
            
            CreateTable(
                "dbo.BillDetails",
                c => new
                    {
                        BillDetailId = c.Guid(nullable: false),
                        BillId = c.Guid(nullable: false),
                        Qty = c.Int(nullable: false),
                        Rate = c.Double(nullable: false),
                        Ammount = c.Double(nullable: false),
                        Barcode = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.BillDetailId)
                .ForeignKey("dbo.Bills", t => t.BillId, cascadeDelete: true)
                .Index(t => t.BillId);
            
            CreateTable(
                "dbo.CompanyDetails",
                c => new
                    {
                        CompanyId = c.Guid(nullable: false),
                        CompanyName = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        City = c.String(nullable: false),
                        PIN = c.Int(nullable: false),
                        Telephone = c.String(nullable: false),
                        VAT = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CompanyId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.BillDetails", "BillId", "dbo.Bills");
            DropForeignKey("dbo.Bills", "ProductId", "dbo.ProductDetails");
            DropForeignKey("dbo.Bills", "ClientId", "dbo.ClientDetails");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.BillDetails", new[] { "BillId" });
            DropIndex("dbo.Bills", new[] { "ProductId" });
            DropIndex("dbo.Bills", new[] { "ClientId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.CompanyDetails");
            DropTable("dbo.BillDetails");
            DropTable("dbo.ProductDetails");
            DropTable("dbo.ClientDetails");
            DropTable("dbo.Bills");
        }
    }
}
