namespace CareviewTest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v20 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Invoices", "Client_Id", "dbo.Clients");
            DropForeignKey("dbo.InvoiceLineItems", "Invoice_Id", "dbo.Invoices");
            DropIndex("dbo.Invoices", new[] { "Client_Id" });
            DropIndex("dbo.InvoiceLineItems", new[] { "Invoice_Id" });
            RenameColumn(table: "dbo.Invoices", name: "Client_Id", newName: "ClientId");
            RenameColumn(table: "dbo.InvoiceLineItems", name: "Invoice_Id", newName: "InvoiceId");
            AlterColumn("dbo.Invoices", "ClientId", c => c.Int(nullable: false));
            AlterColumn("dbo.InvoiceLineItems", "InvoiceId", c => c.Int(nullable: false));
            CreateIndex("dbo.Invoices", "ClientId");
            CreateIndex("dbo.InvoiceLineItems", "InvoiceId");
            AddForeignKey("dbo.Invoices", "ClientId", "dbo.Clients", "Id", cascadeDelete: true);
            AddForeignKey("dbo.InvoiceLineItems", "InvoiceId", "dbo.Invoices", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InvoiceLineItems", "InvoiceId", "dbo.Invoices");
            DropForeignKey("dbo.Invoices", "ClientId", "dbo.Clients");
            DropIndex("dbo.InvoiceLineItems", new[] { "InvoiceId" });
            DropIndex("dbo.Invoices", new[] { "ClientId" });
            AlterColumn("dbo.InvoiceLineItems", "InvoiceId", c => c.Int());
            AlterColumn("dbo.Invoices", "ClientId", c => c.Int());
            RenameColumn(table: "dbo.InvoiceLineItems", name: "InvoiceId", newName: "Invoice_Id");
            RenameColumn(table: "dbo.Invoices", name: "ClientId", newName: "Client_Id");
            CreateIndex("dbo.InvoiceLineItems", "Invoice_Id");
            CreateIndex("dbo.Invoices", "Client_Id");
            AddForeignKey("dbo.InvoiceLineItems", "Invoice_Id", "dbo.Invoices", "Id");
            AddForeignKey("dbo.Invoices", "Client_Id", "dbo.Clients", "Id");
        }
    }
}
