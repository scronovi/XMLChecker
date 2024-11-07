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
            if (radioButton_sch.Checked) { 
                radioButton_tim.Checked = false;
            }else { radioButton_tim.Checked = true; }
        
        }

        // Button click event
        private void check_button_Click(object sender, EventArgs e)
        {
            string folderPath1 = path1_textbox.Text;
            string folderPath2 = path2_textbox.Text;

            
            string netArea_fromGui = anlid_textbox.Text;    // Används inte men spara ifall man vill göra programmet mer modulärt
            string metering_fromGui = "";                   // Används inte men spara ifall man vill göra programmet mer modulärt
            if (radioButton_tim.Checked)
            {
                metering_fromGui = "TIM";
            }else if (radioButton_sch.Checked)
            {
                metering_fromGui = "SCH";
            }

            CompareXmlFiles(folderPath1, folderPath2);
        }

        private void CompareXmlFiles(string folderPath1, string folderPath2)
        {
            // Get all XML files in both folders
            string[] xmlFiles1 = Directory.GetFiles(folderPath1, "*.xml");
            string[] xmlFiles2 = Directory.GetFiles(folderPath2, "*.xml");

            // Compare the files one by one
            foreach (var file1 in xmlFiles1)
            {
                string fileName1 = Path.GetFileName(file1);
                string fileKey1 = GetFileKey(fileName1);

                /* Används inte men spara ifall man vill göra programmet mer modulärt
                    filekey har ju nu alltså INTE nätområde eller tim/sch, det är part 0 + 1
                    så då blir det SCH-FLN "-" fileKey1.xml
                    string fullKey1 = metering + "-" + netArea + "-" + fileKey1;
                **/
                // Look for a corresponding file in folder 2
                foreach (var file2 in xmlFiles2)
                {
                    string fileName2 = Path.GetFileName(file2);
                    string fileKey2 = GetFileKey(fileName2);
                    //string fullKey2 = metering + "-" + netArea + "-" + fileKey1;

                    // Check if the keys match
                    if (fileKey1 == fileKey2)
                    {
                        CompareXml(file1, file2);
                        break; // No need to compare further once a match is found
                    }
                }
            }
        }

        private string GetFileKey(string fileName)
        {
            // Split the filename by "-" and extract the 3rd and 4th parts
            string[] parts = fileName.Split('-');
            if (parts.Length >= 4)
            {
                return parts[2] + "-" + parts[3]; // Return the 3rd and 4th parts
            }
            else
            {
                return string.Empty; // If the filename does not follow the expected format
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
                LogDifference($"Error comparing files: {ex.Message}", true);
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
                // Jämför bladnoder direkt
                if (node1.Name != node2?.Name || node1.InnerText != node2?.InnerText)
                {
                    LogDifference("--------------------------------------------------------");
                    LogDifference($"Difference found in leaf node '{node1.ParentNode.ParentNode.ParentNode.Name}\\{node1.ParentNode.ParentNode.Name}\\{node1.ParentNode.Name}\\{node1.Name}'", true);
                    LogDifference($"File 1({filename1}): {node1.InnerText}", true);
                    LogDifference($"File 2({filename2}): {(node2?.InnerText ?? "Node missing")}", true);
                    LogDifference("--------------------------------------------------------");
                }
                return; // Avsluta om vi är på bladnivå
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
                        LogDifference($"Leaf node '{childNode1.Name}' is missing in second file.", true);
                    }
                }
            }
        }




        private void LogDifference(string message, bool isError = false)
        {
            // Append the message to the log TextBox
            logTextBox.SelectionColor = isError ? Color.Red : Color.Black;
            logTextBox.AppendText(message + Environment.NewLine);
        }

        
    }
}
