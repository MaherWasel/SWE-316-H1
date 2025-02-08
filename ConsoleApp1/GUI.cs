using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using OfficeOpenXml;

namespace ConsoleApp1
{
    public class MainForm : Form
    {
        private LocationCollection locationCollection;
        private SectionsCollection sectionsCollection;
        private Button selectedLocationButton;

        // UI Components
        private Panel locationPanel;
        private ListBox sectionsListBox;
        private Label locationDetailsLabel;

        public MainForm()
        {
            // Initialize form components
            InitializeComponent();

            // Load data
            locationCollection = new LocationCollection();
            sectionsCollection = new SectionsCollection();
            LoadData();
            InitializeLocationButtons();
        }

        private void InitializeComponent()
        {
            this.Text = "Location Viewer";
            this.Size = new Size(800, 500);

            // Scrollable Panel for Locations (Left Side)
            locationPanel = new Panel
            {
                Location = new Point(10, 10),
                Size = new Size(250, 450),
                AutoScroll = true,
                BorderStyle = BorderStyle.FixedSingle
            };
            this.Controls.Add(locationPanel);

            // Sections List Box (Right Side)
            sectionsListBox = new ListBox
            {
                Location = new Point(400, 10),
                Size = new Size(350, 450)
            };
            this.Controls.Add(sectionsListBox);

            // Location Details Label (Above the list box)
            locationDetailsLabel = new Label
            {
                Location = new Point(270, 10),
                Width = 120,
                Height = 60,
                Visible = false
            };
            this.Controls.Add(locationDetailsLabel);
        }

        private void LoadData()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Term 242 Schedule.xlsx");
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Error: File not found at {filePath}");
                return;
            }

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                if (package.Workbook.Worksheets.Count == 0)
                {
                    Console.WriteLine("Error: No worksheets found in the Excel file.");
                    return;
                }

                var worksheet = package.Workbook.Worksheets[0];

                if (worksheet.Dimension == null)
                {
                    Console.WriteLine("Error: Worksheet is empty.");
                    return;
                }

                int rowCount = worksheet.Dimension.Rows;
                for (int row = 1; row <= rowCount; row++)
                {
                    int? term = int.TryParse(worksheet.Cells[row, 1].Text, out int termVal) ? termVal : (int?)null;
                    int? crn = int.TryParse(worksheet.Cells[row, 2].Text, out int crnVal) ? crnVal : (int?)null;
                    int? sectionNumber = int.TryParse(worksheet.Cells[row, 5].Text, out int sectionVal) ? sectionVal : (int?)null;

                    string courseCode = string.IsNullOrWhiteSpace(worksheet.Cells[row, 3].Text) ? null : worksheet.Cells[row, 3].Text;
                    string dept = string.IsNullOrWhiteSpace(worksheet.Cells[row, 4].Text) ? null : worksheet.Cells[row, 4].Text;
                    string title = string.IsNullOrWhiteSpace(worksheet.Cells[row, 6].Text) ? null : worksheet.Cells[row, 6].Text;
                    string act = string.IsNullOrWhiteSpace(worksheet.Cells[row, 7].Text) ? null : worksheet.Cells[row, 7].Text;
                    string days = string.IsNullOrWhiteSpace(worksheet.Cells[row, 8].Text) ? null : worksheet.Cells[row, 8].Text;
                    string startTime = string.IsNullOrWhiteSpace(worksheet.Cells[row, 9].Text) ? null : worksheet.Cells[row, 9].Text;
                    string endTime = string.IsNullOrWhiteSpace(worksheet.Cells[row, 10].Text) ? null : worksheet.Cells[row, 10].Text;
                    string building = string.IsNullOrWhiteSpace(worksheet.Cells[row, 11].Text) ? null : worksheet.Cells[row, 11].Text;
                    string room = string.IsNullOrWhiteSpace(worksheet.Cells[row, 12].Text) ? null : worksheet.Cells[row, 12].Text;

                    Course course = new Course(title, dept, courseCode, act);
                    Location location = new Location(building, room);

                    if (!locationCollection.LocationExists(location))
                    {
                        locationCollection.AddLocation(location);
                    }

                    MeetingInfo meetingInfo = new MeetingInfo(days, startTime, endTime, location);
                    Section section = new Section(sectionNumber ?? 0, crn ?? 0, course, term ?? 0, meetingInfo);

                    if (!sectionsCollection.SectionExists(section))
                    {
                        sectionsCollection.AddSection(section);
                        course.AddSection(section);
                    }
                }
            }

            Console.WriteLine("✅ Data successfully loaded from Excel!");
            Console.WriteLine(sectionsCollection.GetSections().Count);
        }

        private void InitializeLocationButtons()
        {
            var locations = locationCollection.GetLocations();
            int buttonTop = 5;

            foreach (var location in locations)
            {
                Button locationButton = new Button
                {
                    Text = $"{location.GetBuilding()} - {location.GetRoom()}",
                    Location = new Point(5, buttonTop),
                    Width = 220
                };

                locationButton.Click += (sender, e) => LocationButton_Click(sender, location);
                locationPanel.Controls.Add(locationButton);

                buttonTop += 35;
            }
        }

        private void LocationButton_Click(object sender, Location location)
        {
            if (selectedLocationButton != null)
            {
                selectedLocationButton.BackColor = SystemColors.Control;
            }

            selectedLocationButton = (Button)sender;
            selectedLocationButton.BackColor = Color.Cyan;

            ShowLocationDetails(location);
            LoadSectionsForLocation(location);
        }

        private void ShowLocationDetails(Location location)
        {
            locationDetailsLabel.Text = $"Building: {location.GetBuilding()}\nRoom: {location.GetRoom()}";
            locationDetailsLabel.Visible = true;
        }

        private void LoadSectionsForLocation(Location location)
        {
            sectionsListBox.Items.Clear();

            foreach (var section in sectionsCollection.SearchSection(location))
            {
                sectionsListBox.Items.Add($"CRN: {section.GetCRN()} - {section.GetCourse().GetCourseCode()}");
            }
        }
    }
}
