using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using System;
using System.Data;

namespace PdfSharpMigraDoc
{
    public class Ticket
    {
        public Document CreateDocument()
        {
            // Create a new MigraDoc document
            Document document = new Document();
            document.Info.Title = "Hello, MigraDoc";
            document.Info.Subject = "Demonstrates an excerpt of the capabilities of MigraDoc.";
            document.Info.Author = "Stefan Lange";

            //DefineStyles(document);
            Style style = document.Styles["Normal"];
            style.Font.Name = "Times New Roman";
            style.Font.Bold = true;

            Section section = document.AddSection();
            section.PageSetup = document.DefaultPageSetup.Clone();
            section.PageSetup.PageHeight = Unit.FromMillimeter(80);
            section.PageSetup.PageWidth = Unit.FromMillimeter(80);

            section.PageSetup.TopMargin = Unit.FromMillimeter(3);
            section.PageSetup.RightMargin = Unit.FromMillimeter(3);
            section.PageSetup.LeftMargin = Unit.FromMillimeter(4);
            section.PageSetup.BottomMargin = Unit.FromMillimeter(3);

            DemonstrateSimpleTable(document);

            return document;
        }

        public static void DefineStyles(Document document)
        {
            // Get the predefined style Normal.
            Style style = document.Styles["Normal"];

            style.Font.Name = "Times New Roman";

            style = document.Styles["Heading1"];
            style.Font.Name = "Tahoma";
            style.Font.Size = 14;
            style.Font.Bold = true;
            style.Font.Color = Colors.BlueViolet;
            style.ParagraphFormat.PageBreakBefore = true;
            style.ParagraphFormat.SpaceAfter = 6;
        }

        public static void DemonstrateSimpleTable(Document document)
        {
            //document.LastSection.AddParagraph("This is my label", "Heading2");
            #region DataTableTest
            DataTable datatable = new DataTable();
            datatable.Columns.Add("Dosage", typeof(int));
            datatable.Columns.Add("Drug", typeof(string));
            datatable.Columns.Add("Patient", typeof(string));
            datatable.Columns.Add("Date", typeof(DateTime));

            // Here we add five DataRows.
            datatable.Rows.Add(25, "Indocin", "David", DateTime.Now);
            datatable.Rows.Add(50, "Enebrel", "Sam", DateTime.Now);
            datatable.Rows.Add(10, "Hydralazine", "Christoff", DateTime.Now);
            datatable.Rows.Add(21, "Combivent", "Janet", DateTime.Now);
            datatable.Rows.Add(100, "Dilantin", "Melanie", DateTime.Now);
            #endregion


            Table table = new Table();
            table.Borders.Width = 0.75;

            Column column = table.AddColumn(Unit.FromMillimeter(17));
            //column.Format.Alignment = ParagraphAlignment.Center;

            table.AddColumn(Unit.FromMillimeter(53));

            Row row = table.AddRow();
            row.Height = (Unit.FromMillimeter(10));
            //row.Shading.Color = Colors.PaleGoldenrod;
            Cell cell = row.Cells[0];
            cell.AddParagraph("Item No");
            cell = row.Cells[1];
            cell.AddParagraph("ABC123456");
            row.Format.Alignment = ParagraphAlignment.Center;
            row.Cells[0].VerticalAlignment = VerticalAlignment.Center;
            row.Cells[1].VerticalAlignment = VerticalAlignment.Center;

            row = table.AddRow();
            row.Height = (Unit.FromMillimeter(40));
            cell = row.Cells[0];
            cell.AddParagraph("PVO");
            cell = row.Cells[1];
            cell.AddParagraph("TEst PVO");
            row.Format.Alignment = ParagraphAlignment.Center;
            row.Cells[0].VerticalAlignment = VerticalAlignment.Center;
            row.Cells[1].VerticalAlignment = VerticalAlignment.Center;

            row = table.AddRow();
            row.Height = (Unit.FromMillimeter(10));
            cell = row.Cells[0];
            cell.AddParagraph("SLA");
            cell = row.Cells[1];
            var time = DateTime.Now;
            cell.AddParagraph(time.ToString());
            row.Format.Alignment = ParagraphAlignment.Center;
            row.Cells[0].VerticalAlignment = VerticalAlignment.Center;
            row.Cells[1].VerticalAlignment = VerticalAlignment.Center;

            row = table.AddRow();
            row.Height = (Unit.FromMillimeter(10));
            cell = row.Cells[0];
            cell.AddParagraph("Pick Qty");
            cell = row.Cells[1];
            cell.AddParagraph("999");
            row.Format.Alignment = ParagraphAlignment.Center;
            row.Cells[0].VerticalAlignment = VerticalAlignment.Center;
            row.Cells[1].VerticalAlignment = VerticalAlignment.Center;

            table.SetEdge(0, 0, 2, 4, Edge.Box, BorderStyle.Single, 1.5, Colors.Black);

            document.LastSection.Add(table);
        }
    }
}
