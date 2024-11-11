using System;
using System.IO;
using System.Xml;
using System.Text;
using System.Windows.Forms;
using System.Xml.XPath;

namespace XMLChecker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            klarText.Visible = false;

        }

        // Button click event
        private void check_button_Click(object sender, EventArgs e)
        {
            string folderPath1 = path1_textbox.Text;
            string folderPath2 = path2_textbox.Text;

            CompareXmlFiles(folderPath1, folderPath2);
            LogDone($"Klar med filerna i {folderPath1} och {folderPath2}", true);
        }

        private void CompareXmlFiles(string folderPath1, string folderPath2)
        {
            // Get all XML files in both folders
            string[] xmlFiles1 = Directory.GetFiles(folderPath1, "*.xml");
            string[] xmlFiles2 = Directory.GetFiles(folderPath2, "*.xml");

            foreach (var file1 in xmlFiles1)
            {
                string fileName1 = Path.GetFileName(file1);
                string fileKey1 = GetFileKey(fileName1);


                // Kolla efter fil i folder2
                foreach (var file2 in xmlFiles2)
                {
                    string fileName2 = Path.GetFileName(file2);
                    string fileKey2 = GetFileKey(fileName2);

                    // Kolla om delarna matchar
                    if (fileKey1 == fileKey2)
                    {
                        CompareXml(file1, file2);
                        break;
                    }
                }
            }
        }

        private string GetFileKey(string fileName)
        {
            // Splitta namnet med "-2
            string[] parts = fileName.Split('-');
            if (parts.Length >= 4)
            {
                return parts[2] + "-" + parts[3];
            }
            else
            {
                return string.Empty;
            }
        }

        private void CompareXml(string filePath1, string filePath2)
        {
            try
            {
                XmlDocument xmlDoc1 = new XmlDocument();
                XmlDocument xmlDoc2 = new XmlDocument();

                xmlDoc1.Load(filePath1);
                xmlDoc2.Load(filePath2);

                CompareXmlNodes(xmlDoc1.DocumentElement, xmlDoc2.DocumentElement, filePath1, filePath2);
            }
            catch (Exception ex)
            {
                LogDifference($"Error: {ex.Message}", true);
            }
        }


        private void CompareXmlNodes(XmlNode node1, XmlNode node2, string filename1, string filename2)
        {
            // Exkluderar seabxml-head för att timestamps är horig
            if (node1.Name == "seabxml-head" || node2.Name == "seabxml-head")
            {
                return;
            }
            // Kontrollera om båda noderna är bladnoder (inga barn)
            if (node1.ChildNodes.Count == 0 && (node2 == null || node2.ChildNodes.Count == 0))
            {

                if (node1.Name != node2?.Name || node1.InnerText != node2?.InnerText)
                {
                    LogDifference("--------------------------------------------------------");
                    LogDifference($"Differans hittad i bladnod '{node1.ParentNode.ParentNode.ParentNode.Name}\\{node1.ParentNode.ParentNode.Name}\\{node1.ParentNode.Name}\\{node1.Name}'", true);
                    LogDifference($"File 1({filename1}): {node1.InnerText}", true);
                    LogDifference($"File 2({filename2}): {(node2?.InnerText ?? "Nod saknas")}", true);
                    LogDifference("--------------------------------------------------------");
                }
                return;
            }

            // Om det inte är en bladnod, loopa över barnen i node1
            for (int i = 0; i < node1.ChildNodes.Count; i++)
            {
                XmlNode childNode1 = node1.ChildNodes[i];
                XmlNode childNode2 = (node2 != null && i < node2.ChildNodes.Count) ? node2.ChildNodes[i] : null;

                if (childNode2 != null)
                {
                    // Rekursivt jämför barnnoder
                    CompareXmlNodes(childNode1, childNode2, filename1, filename2);
                }
                else
                {
                    // Om barnnoden saknas i node2 och det är en bladnod, logga som skillnad
                    if (childNode1.ChildNodes.Count == 0) // Kontrollera att det är en bladnod
                    {
                        LogDifference($"Bladnod '{childNode1.Name}' saknas i fil 2.", true);
                    }
                }
            }
        }




        private void LogDifference(string message, bool isError = false)
        {
            // Logga differansmeddelande
            logTextBox.SelectionColor = isError ? Color.Red : Color.Black;
            logTextBox.AppendText(message + Environment.NewLine);


        }
        private void LogDone(string message, bool isDone = false)
        {
            // Logga klarmeddelande
            logTextBox.SelectionColor = isDone ? Color.Green : Color.Blue;
            logTextBox.AppendText(message);
            klarText.Visible = isDone;
        }

        private void logClear_Click(object sender, EventArgs e)
        {
            logTextBox.Text = string.Empty;
            klarText.Visible = false;
        }
    }
}
