using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace ConsoleAppWAPTests
{
    class Program
    {
        static void Main(string[] args)
        {
            ValidateXSD();
           
            ApplyXSLTransformation();
        }

        private static void ApplyXSLTransformation()
        {
            FileStream myXmlFile, styleSheetFile;
            myXmlFile = File.Open("GreatesChessPlayers.xml", FileMode.Open);
            XPathDocument myXPathDoc = new XPathDocument(myXmlFile);
            XslTransform myXslTrans = new XslTransform();
            styleSheetFile = File.Open("Rule.xsl", FileMode.Open);
            XmlTextReader stylesheetReader = new XmlTextReader(styleSheetFile);
            myXslTrans.Load(stylesheetReader);
            XmlTextWriter myWriter = new XmlTextWriter("result.html", null);
            myXslTrans.Transform(myXPathDoc, null, myWriter);
        }

        private static void ValidateXSD()
        {

            //Approach1();

            Approach2();

            //Approach3();
        }

        private static void Approach3()
        {
            XmlReaderSettings booksSettings = new XmlReaderSettings();
            booksSettings.Schemas.Add("http://www.contoso.com/books", "books.xsd");
            booksSettings.ValidationType = ValidationType.Schema;
            booksSettings.ValidationEventHandler += new ValidationEventHandler(booksSettingsValidationEventHandler);

            XmlReader books = XmlReader.Create("books.xml", booksSettings);

            while (books.Read()) { }
        }

        static void booksSettingsValidationEventHandler(object sender, ValidationEventArgs e)
        {
            if (e.Severity == XmlSeverityType.Warning)
            {
                Console.Write("WARNING: ");
                Console.WriteLine(e.Message);
            }
            else if (e.Severity == XmlSeverityType.Error)
            {
                Console.Write("ERROR: ");
                Console.WriteLine(e.Message);
            }
        }

        private static void Approach2()
        {
            var path = new Uri(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase)).LocalPath;
            XmlSchemaSet schema = new XmlSchemaSet();
            schema.Add("", path + "\\students.xsd");
            XmlReader rd = XmlReader.Create(path + "\\students.xml");
            XDocument doc = XDocument.Load(rd);
            doc.Validate(schema, ValidationEventHandler);
        }

        private static void Approach1()
        {
            // Set the validation settings.
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.ValidationType = ValidationType.Schema;
            settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessInlineSchema;
            settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessSchemaLocation;
            settings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;
            settings.ValidationEventHandler += new ValidationEventHandler(ValidationCallBack);

            // Create the XmlReader object.
            XmlReader reader = XmlReader.Create("products.xml", settings);

            // Parse the file. 
            while (reader.Read()) ;
        }

        static void ValidationEventHandler(object sender, ValidationEventArgs e)
        {
            XmlSeverityType type = XmlSeverityType.Warning;
            if (Enum.TryParse<XmlSeverityType>("Error", out type))
            {
                if (type == XmlSeverityType.Error)
                {
                    //throw new Exception(e.Message);
                    Console.WriteLine($"Error: {e.Severity}, {e.Message}");
                }
                if (type == XmlSeverityType.Warning)
                {
                    //throw new Exception(e.Message);
                    Console.WriteLine($"Warning: {e.Severity}, {e.Message}");
                }
            }
        }

        // Display any warnings or errors.
        private static void ValidationCallBack(object sender, ValidationEventArgs args)
        {
            if (args.Severity == XmlSeverityType.Warning)
                Console.WriteLine("\tWarning: Matching schema not found.  No validation occurred." + args.Message);
            else
                Console.WriteLine("\tValidation error: " + args.Message);

        }
    }
}
