namespace CareviewTest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v10 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        NDISNumber = c.String(maxLength: 20),
                        EmailAddress = c.String(),
                        DateOfBirth = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Invoices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InvoiceNumber = c.String(maxLength: 50),
                        InvoiceDate = c.DateTime(nullable: false),
                        Client_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.Client_Id)
                .Index(t => t.Client_Id);
            
            CreateTable(
                "dbo.InvoiceLineItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Quantity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Invoice_Id = c.Int(),
                        Service_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Invoices", t => t.Invoice_Id)
                .ForeignKey("dbo.Services", t => t.Service_Id)
                .Index(t => t.Invoice_Id)
                .Index(t => t.Service_Id);
            
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Rate = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InvoiceLineItems", "Service_Id", "dbo.Services");
            DropForeignKey("dbo.InvoiceLineItems", "Invoice_Id", "dbo.Invoices");
            DropForeignKey("dbo.Invoices", "Client_Id", "dbo.Clients");
            DropIndex("dbo.InvoiceLineItems", new[] { "Service_Id" });
            DropIndex("dbo.InvoiceLineItems", new[] { "Invoice_Id" });
            DropIndex("dbo.Invoices", new[] { "Client_Id" });
            DropTable("dbo.Services");
            DropTable("dbo.InvoiceLineItems");
            DropTable("dbo.Invoices");
            DropTable("dbo.Clients");
        }
    }
}
