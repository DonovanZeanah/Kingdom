namespace KingdomWebApp.Models
{
    [Serializable]
    public class MockData
    {
        public List<int> Data { get; set; }
        public string? Label { get; set; }
        public string? BackgroundColor { get; set; }

        public MockData()
        {
            Data = new List<int>();
        }
    }
}
